using System;
using System.IO;
using System.Text;

namespace GoldParser
{
	/// <summary>
	/// Pull parser which uses Grammar table to parser input stream.
	/// </summary>
	public sealed class Parser
	{
		/// <summary>
		/// Initializes new instance of Parser class.
		/// </summary>
		/// <param name="textReader">TextReader instance to read data from.</param>
		/// <param name="grammar">Grammar with parsing tables to parser input stream.</param>
		public Parser(TextReader textReader, Grammar grammar)
		{
			if (textReader == null)
			{
				throw new ArgumentNullException("textReader");
			}
			if (grammar == null)
			{
				throw new ArgumentNullException("grammar");
			}

			m_TextReader = textReader;
			m_BufferSize = MinimumBufferSize;
			m_Buffer = new Char[m_BufferSize + 1];
			m_LineLength = Undefined;
			ReadBuffer();

			m_InputTokens = new Token[MinimumInputTokenCount];
			m_LRStack = new LRStackItem[MinimumLRStackSize];

			m_Grammar = grammar;

			// Put grammar start symbol into LR parsing stack.
			m_LRState = m_Grammar.InitialLRState;
			var start = new LRStackItem { Token = { Symbol = m_Grammar.StartSymbol }, State = m_LRState };
			m_LRStack[m_LRStackIndex] = start;

			m_ReductionCount = Undefined; // there are no reductions yet.
		}

		/// <summary>
		/// Gets the parser's grammar.
		/// </summary>
		public Grammar Grammar
		{
			get { return m_Grammar; }
		}

		/// <summary>
		/// Gets or sets flag to trim reductions.
		/// </summary>
		public Boolean TrimReductions
		{
			get { return m_TrimReductions; }
			set { m_TrimReductions = value; }
		}

		/// <summary>
		/// Gets source of parsed data.
		/// </summary>
		public TextReader TextReader
		{
			get { return m_TextReader; }
		}

		/// <summary>
		/// Gets current Char position.
		/// </summary>
		public Int32 CharPosition
		{
			get { return m_CharIndex + m_BufferStartIndex; }
		}

		/// <summary>
		/// Gets current line number. It is 1-based.
		/// </summary>
		public Int32 LineNumber
		{
			get { return m_LineNumber; }
		}

		/// <summary>
		/// Gets current Char position in the current source line. It is 1-based.
		/// </summary>
		public Int32 LinePosition
		{
			get { return CharPosition - m_LineStart + 1; }
		}

		/// <summary>
		/// Gets current source line text. It can be truncated if line is longer than 2048 characters.
		/// </summary>
		public String LineText
		{
			get
			{
				Int32 lineStart = Math.Max(m_LineStart, 0);
				Int32 lineLength;
				if (m_LineLength == Undefined)
				{
					// Line was requested outside of SourceLineReadCallback call
					lineLength = m_CharIndex - lineStart;
				}
				else
				{
					lineLength = m_LineLength - (lineStart - m_LineStart);
				}
				return lineLength > 0 ? new String(m_Buffer, lineStart, lineLength) : String.Empty;
			}
		}

		/// <summary>
		/// Gets or sets current token symbol.
		/// </summary>
		public Symbol TokenSymbol
		{
			get { return m_Token.Symbol; }
			set { m_Token.Symbol = value; }
		}

		/// <summary>
		/// Gets or sets current token text.
		/// </summary>
		public String TokenText
		{
			get
			{
				if (m_Token.Text == null)
				{
					m_Token.Text = m_Token.Length > 0
						? new String(m_Buffer, m_Token.Start - m_BufferStartIndex, m_Token.Length)
						: String.Empty;
				}
				return m_Token.Text;
			}
			set { m_Token.Text = value; }
		}

		/// <summary>
		/// Gets or sets current token position relative to input stream beginning.
		/// </summary>
		public Int32 TokenCharPosition
		{
			get { return m_Token.Start; }
			set { m_Token.Start = value; }
		}

		/// <summary>
		/// Gets or sets current token text length.
		/// </summary>
		public Int32 TokenLength
		{
			get { return m_Token.Length; }
			set { m_Token.Length = value; }
		}

		/// <summary>
		/// Gets or sets current token line number. It is 1-based.
		/// </summary>
		public Int32 TokenLineNumber
		{
			get { return m_Token.LineNumber; }
			set { m_Token.LineNumber = value; }
		}

		/// <summary>
		/// Gets or sets current token position in current source line. It is 1-based.
		/// </summary>
		public Int32 TokenLinePosition
		{
			get { return m_Token.LinePosition; }
			set { m_Token.LinePosition = value; }
		}

		/// <summary>
		/// Gets or sets token syntax Object associated with the current token or reduction.
		/// </summary>
		public Object TokenSyntaxNode
		{
			get
			{
				if (m_ReductionCount == Undefined)
				{
					return m_Token.SyntaxNode;
				}
				return m_LRStack[m_LRStackIndex].Token.SyntaxNode;
			}
			set
			{
				if (m_ReductionCount == Undefined)
				{
					m_Token.SyntaxNode = value;
				}
				else
				{
					m_LRStack[m_LRStackIndex].Token.SyntaxNode = value;
				}
			}
		}

		/// <summary>
		/// Returns String representation of the token.
		/// </summary>
		/// <returns>String representation of the token.</returns>
		public String TokenString
		{
			get
			{
				if (m_Token.Symbol.SymbolType != SymbolType.Terminal)
				{
					return m_Token.Symbol.ToString();
				}
				var sb = new StringBuilder(m_Token.Length);
				for (Int32 i = 0; i < m_Token.Length; i++)
				{
					Char ch = m_Buffer[m_Token.Start - m_BufferStartIndex + i];
					if (ch < ' ')
					{
						switch (ch)
						{
							case '\n':
								sb.Append("{LF}");
								break;
							case '\r':
								sb.Append("{CR}");
								break;
							case '\t':
								sb.Append("{HT}");
								break;
						}
					}
					else
					{
						sb.Append(ch);
					}
				}
				return sb.ToString();
			}
		}

		/// <summary>
		/// Gets current LR state.
		/// </summary>
		public LRState CurrentLRState
		{
			get { return m_LRState; }
		}

		/// <summary>
		/// Gets current reduction syntax rule.
		/// </summary>
		public Rule ReductionRule
		{
			get { return m_LRStack[m_LRStackIndex].Rule; }
		}

		/// <summary>
		/// Gets number of items in the current reduction
		/// </summary>
		public Int32 ReductionCount
		{
			get { return m_ReductionCount; }
		}

		/// <summary>
		/// Gets current comment text.
		/// </summary>
		public String CommentText
		{
			get
			{
				if (m_Token.Symbol != null)
				{
					switch (m_Token.Symbol.SymbolType)
					{
						case SymbolType.CommentLine:
							m_CommentText = new StringBuilder();
							m_CommentText.Append(TokenText);
							DiscardInputToken(); //Remove token 
							MoveToLineEnd();
							String lineComment = m_CommentText.ToString();
							m_CommentText = null;
							return lineComment;

						case SymbolType.CommentStart:
							m_CommentText = new StringBuilder();
							ProcessBlockComment();
							String blockComment = m_CommentText.ToString();
							m_CommentText = null;
							return blockComment;
					}
				}
				return String.Empty;
			}
		}

		/// <summary>
		/// Gets or sets callback function to track source line text.
		/// </summary>
		public SourceLineReadCallback SourceLineReadCallback
		{
			get { return m_SourceLineReadCallback; }
			set { m_SourceLineReadCallback = value; }
		}

		/// <summary>
		/// Pushes a token to the input token stack.
		/// </summary>
		/// <param name="symbol">Token symbol.</param>
		/// <param name="text">Token text.</param>
		/// <param name="syntaxNode">Syntax node associated with the token.</param>
		public void PushInputToken(Symbol symbol, String text, Object syntaxNode)
		{
			if (m_Token.Symbol != null)
			{
				if (m_InputTokenCount == m_InputTokens.Length)
				{
					var newTokenArray = new Token[m_InputTokenCount * 2];
					Array.Copy(m_InputTokens, newTokenArray, m_InputTokenCount);
					m_InputTokens = newTokenArray;
				}
				m_InputTokens[m_InputTokenCount++] = m_Token;
			}
			m_Token = new Token { Symbol = symbol, Text = text, Length = (text != null) ? text.Length : 0, SyntaxNode = syntaxNode };
		}

		/// <summary>
		/// Pops token from the input token stack.
		/// </summary>
		/// <returns>Token symbol from the top of input token stack.</returns>
		public Symbol PopInputToken()
		{
			Symbol result = m_Token.Symbol;
			if (m_InputTokenCount > 0)
			{
				m_Token = m_InputTokens[--m_InputTokenCount];
			}
			else
			{
				m_Token.Symbol = null;
				m_Token.Text = null;
			}
			return result;
		}

		/// <summary>
		/// Reads next token from the input stream.
		/// </summary>
		/// <returns>Token symbol which was read.</returns>
		public Symbol ReadToken()
		{
			m_Token.Text = null;
			m_Token.Start = m_CharIndex + m_BufferStartIndex;
			m_Token.LineNumber = m_LineNumber;
			m_Token.LinePosition = m_CharIndex + m_BufferStartIndex - m_LineStart + 1;
			Int32 lookahead = m_CharIndex;  // Next look ahead Char in the input
			Int32 tokenLength = 0;
			Symbol tokenSymbol = null;

			Char ch = m_Buffer[lookahead];
			if (ch == EndOfString)
			{
				if (ReadBuffer() == 0)
				{
					m_Token.Symbol = m_Grammar.EndSymbol;
					m_Token.Length = 0;
					return m_Token.Symbol;
				}
				lookahead = m_CharIndex;
				ch = m_Buffer[lookahead];
			}
			DfaState dfaState = m_Grammar.DfaInitialState;
			while (true)
			{
				dfaState = dfaState.TransitionVector[ch] as DfaState;

				// This block-if statement checks whether an edge was found from the current state.
				// If so, the state and current position advance. Otherwise it is time to exit the main loop
				// and report the token found (if there was it fact one). If the LastAcceptState is -1,
				// then we never found a match and the Error Token is created. Otherwise, a new token
				// is created using the Symbol in the Accept State and all the characters that
				// comprise it.
				if (dfaState != null)
				{
					// This code checks whether the target state accepts a token. If so, it sets the
					// appropiate variables so when the algorithm in done, it can return the proper
					// token and number of characters.
					lookahead++;
					if (dfaState.AcceptSymbol != null)
					{
						tokenSymbol = dfaState.AcceptSymbol;
						tokenLength = lookahead - m_CharIndex;
					}
					ch = m_Buffer[lookahead];
					if (ch == EndOfString)
					{
						m_PreserveChars = lookahead - m_CharIndex;
						if (ReadBuffer() == 0)
						{
							// Found end of of stream
							lookahead = m_CharIndex + m_PreserveChars;
						}
						else
						{
							lookahead = m_CharIndex + m_PreserveChars;
							ch = m_Buffer[lookahead];
						}
						m_PreserveChars = 0;
					}
				}
				else
				{
					if (tokenSymbol != null)
					{
						m_Token.Symbol = tokenSymbol;
						m_Token.Length = tokenLength;
						MoveBy(tokenLength);
					}
					else
					{
						//Tokenizer cannot recognize symbol
						m_Token.Symbol = m_Grammar.ErrorSymbol;
						m_Token.Length = 1;
						MoveBy(1);
					}
					break;
				}
			}
			return m_Token.Symbol;
		}

		/// <summary>
		/// Gets reduction item syntax Object by its index.
		/// </summary>
		/// <param name="index">Index of reduction item.</param>
		/// <returns>Syntax Object attached to reduction item.</returns>
		public Object GetReductionSyntaxNode(Int32 index)
		{
			if (index < 0 || index >= m_ReductionCount)
			{
				throw new IndexOutOfRangeException();
			}
			return m_LRStack[m_LRStackIndex - m_ReductionCount + index].Token.SyntaxNode;
		}

		/// <summary>
		/// Gets array of expected token symbols.
		/// </summary>
		public Symbol[] GetExpectedTokens()
		{
			return m_ExpectedTokens;
		}

		/// <summary>
		/// Executes next step of parser and returns parser state.
		/// </summary>
		/// <returns>Parser current state.</returns>
		public ParseMessage Parse()
		{
			if (m_Token.Symbol != null)
			{
				switch (m_Token.Symbol.SymbolType)
				{
					case SymbolType.CommentLine:
						DiscardInputToken(); //Remove it 
						MoveToLineEnd();
						break;

					case SymbolType.CommentStart:
						ProcessBlockComment();
						break;
				}
			}
			while (true)
			{
				if (m_Token.Symbol == null)
				{
					//We must read a token
					Symbol readTokenSymbol = ReadToken();
					SymbolType symbolType = readTokenSymbol.SymbolType;
					if (m_CommentLevel == 0
						&& symbolType != SymbolType.CommentLine
						&& symbolType != SymbolType.CommentStart
						&& symbolType != SymbolType.WhiteSpace)
					{
						return ParseMessage.TokenRead;
					}
				}
				else
				{
					//==== Normal parse mode - we have a token and we are not in comment mode
					switch (m_Token.Symbol.SymbolType)
					{
						case SymbolType.WhiteSpace:
							DiscardInputToken();  // Discard Whitespace
							break;

						case SymbolType.CommentStart:
							m_CommentLevel = 1; // Switch to block comment mode.
							return ParseMessage.CommentBlockRead;

						case SymbolType.CommentLine:
							return ParseMessage.CommentLineRead;

						case SymbolType.Error:
							return ParseMessage.LexicalError;

						default:
							//Finally, we can parse the token
							TokenParseResult parseResult = ParseToken();
							switch (parseResult)
							{
								case TokenParseResult.Accept:
									return ParseMessage.Accept;

								case TokenParseResult.InternalError:
									return ParseMessage.InternalError;

								case TokenParseResult.ReduceNormal:
									return ParseMessage.Reduction;

								case TokenParseResult.Shift:
									//A simple shift, we must continue
									DiscardInputToken(); // Okay, remove the top token, it is on the stack
									break;

								case TokenParseResult.SyntaxError:
									return ParseMessage.SyntaxError;

								default:
									//Do nothing
									break;
							}
							break;
					}
				}
			}
		}

		/// <summary>
		/// Reads next characters to the buffer.
		/// </summary>
		/// <returns>Number of characters read.</returns>
		private Int32 ReadBuffer()
		{
			// Find out how many bytes to preserve.
			// We truncate long lines.
			Int32 lineStart = (m_LineStart < 0) ? 0 : m_LineStart;
			Int32 lineCharCount = m_CharIndex - lineStart;
			if (lineCharCount > m_BufferSize / 2)
			{
				lineCharCount = m_BufferSize / 2;
			}
			Int32 moveIndex = m_CharIndex - lineCharCount;
			Int32 moveCount = lineCharCount + m_PreserveChars;
			if (moveCount > 0)
			{
				// We need to keep current token characters.
				if (m_BufferSize - moveCount < 20)
				{
					// Grow the buffer
					m_BufferSize = m_BufferSize * 2;
					var newBuffer = new Char[m_BufferSize + 1];
					Array.Copy(m_Buffer, moveIndex, newBuffer, 0, moveCount);
					m_Buffer = newBuffer;
				}
				else
				{
					Array.Copy(m_Buffer, moveIndex, m_Buffer, 0, moveCount);
				}
			}

			// Read as many characters as possible.
			Int32 count = m_BufferSize - moveCount;
			Int32 result = m_TextReader.Read(m_Buffer, moveCount, count);
			// Mark character after last read one as End-Of-String
			m_Buffer[moveCount + result] = EndOfString;
			// Adjust buffer variables.
			m_BufferStartIndex += moveIndex;
			m_CharIndex -= moveIndex;
			m_LineStart -= moveIndex;
			return result;
		}

		/// <summary>
		/// Increments current Char index by delta character positions.
		/// </summary>
		/// <param name="delta">Number to increment Char index.</param>
		private void MoveBy(Int32 delta)
		{
			for (Int32 i = delta; --i >= 0; )
			{
				if (m_Buffer[m_CharIndex++] == '\n')
				{
					if (m_SourceLineReadCallback != null)
					{
						m_LineLength = m_CharIndex - m_LineStart - 1; // Exclude '\n'
						Int32 lastIndex = m_LineStart + m_LineLength - 1;
						if (lastIndex >= 0 && m_Buffer[lastIndex] == '\r')
						{
							m_LineLength--;
						}
						if (m_LineLength < 0)
						{
							m_LineLength = 0;
						}
						m_SourceLineReadCallback(this, m_LineStart + m_BufferStartIndex, m_LineLength);
					}
					m_LineNumber++;
					m_LineStart = m_CharIndex;
					m_LineLength = Undefined;
				}
				if (m_Buffer[m_CharIndex] == '\0')
				{
					if (m_SourceLineReadCallback != null)
					{
						m_LineLength = m_CharIndex - m_LineStart;
						if (m_LineLength > 0)
						{
							m_SourceLineReadCallback(this, m_LineStart + m_BufferStartIndex, m_LineLength);
						}
						m_LineLength = Undefined;
					}
				}
			}
		}

		/// <summary>
		/// Moves current Char pointer to the end of source line.
		/// </summary>
		private void MoveToLineEnd()
		{
			while (true)
			{
				Char ch = m_Buffer[m_CharIndex];
				switch (ch)
				{
					case '\r':
					case '\n':
						return;

					case EndOfString:
						if (ReadBuffer() == 0)
						{
							return;
						}
						break;

					default:
						if (m_CommentText != null)
						{
							m_CommentText.Append(ch);
						}
						break;
				}
				m_CharIndex++;
			}
		}

		/// <summary>
		/// Removes current token and pops next token from the input stack.
		/// </summary>
		private void DiscardInputToken()
		{
			if (m_InputTokenCount > 0)
			{
				m_Token = m_InputTokens[--m_InputTokenCount];
			}
			else
			{
				m_Token.Symbol = null;
				m_Token.Text = null;
			}
		}

		private void ProcessBlockComment()
		{
			if (m_CommentLevel > 0)
			{
				if (m_CommentText != null)
				{
					m_CommentText.Append(TokenText);
				}
				DiscardInputToken();
				while (true)
				{
					SymbolType symbolType = ReadToken().SymbolType;
					if (m_CommentText != null)
					{
						m_CommentText.Append(TokenText);
					}
					DiscardInputToken();
					switch (symbolType)
					{
						case SymbolType.CommentStart:
							m_CommentLevel++;
							break;

						case SymbolType.CommentEnd:
							m_CommentLevel--;
							if (m_CommentLevel == 0)
							{
								// Done with comment.
								return;
							}
							break;

						case SymbolType.End:
							//TODO: replace with special exception.
							throw new Exception("CommentError");

						default:
							//Do nothing, ignore
							//The 'comment line' symbol is ignored as well
							break;
					}
				}
			}
		}

		private TokenParseResult ParseToken()
		{
			LRStateAction stateAction = m_LRState.GetActionBySymbolIndex(m_Token.Symbol.Index);
			if (stateAction != null)
			{
				//Work - shift or reduce
				if (m_ReductionCount > 0)
				{
					Int32 newIndex = m_LRStackIndex - m_ReductionCount;
					m_LRStack[newIndex] = m_LRStack[m_LRStackIndex];
					m_LRStackIndex = newIndex;
				}
				m_ReductionCount = Undefined;
				switch (stateAction.Action)
				{
					case LRAction.Accept:
						m_ReductionCount = 0;
						return TokenParseResult.Accept;

					case LRAction.Shift:
						m_LRState = m_Grammar.LrStateTable[stateAction.Value];
						var nextToken = new LRStackItem { Token = m_Token, State = m_LRState };
						if (m_LRStack.Length == ++m_LRStackIndex)
						{
							var largerLrStack = new LRStackItem[m_LRStack.Length + MinimumLRStackSize];
							Array.Copy(m_LRStack, largerLrStack, m_LRStack.Length);
							m_LRStack = largerLrStack;
						}
						m_LRStack[m_LRStackIndex] = nextToken;
						return TokenParseResult.Shift;

					case LRAction.Reduce:
						//Produce a reduction - remove as many tokens as members in the rule & push a nonterminal token
						Int32 ruleIndex = stateAction.Value;
						Rule currentRule = m_Grammar.RuleTable[ruleIndex];

						//======== Create Reduction
						LRStackItem head;
						TokenParseResult parseResult;
						LRState nextState;
						if (m_TrimReductions && currentRule.ContainsOneNonTerminal)
						{
							//The current rule only consists of a single nonterminal and can be trimmed from the
							//parse tree. Usually we create a new Reduction, assign it to the Data property
							//of Head and push it on the stack. However, in this case, the Data property of the
							//Head will be assigned the Data property of the reduced token (i.e. the only one
							//on the stack).
							//In this case, to save code, the value popped of the stack is changed into the head.
							head = m_LRStack[m_LRStackIndex];
							head.Token.Symbol = currentRule.NonTerminal;
							head.Token.Text = null;
							parseResult = TokenParseResult.ReduceEliminated;
							//========== Goto
							nextState = m_LRStack[m_LRStackIndex - 1].State;
						}
						else
						{
							//Build a Reduction
							head = new LRStackItem
							{
								Rule = currentRule,
								Token = { Symbol = currentRule.NonTerminal, Text = null }
							};
							m_ReductionCount = currentRule.Count;
							parseResult = TokenParseResult.ReduceNormal;
							//========== Goto
							nextState = m_LRStack[m_LRStackIndex - m_ReductionCount].State;
						}

						//========= If nextAction is null here, then we have an Internal Table Error!!!!
						LRStateAction nextAction = nextState.GetActionBySymbolIndex(currentRule.NonTerminal.Index);
						if (nextAction != null)
						{
							m_LRState = m_Grammar.LrStateTable[nextAction.Value];
							head.State = m_LRState;
							if (parseResult == TokenParseResult.ReduceNormal)
							{
								if (m_LRStack.Length == ++m_LRStackIndex)
								{
									var largerLrStack = new LRStackItem[m_LRStack.Length + MinimumLRStackSize];
									Array.Copy(m_LRStack, largerLrStack, m_LRStack.Length);
									m_LRStack = largerLrStack;
								}
								m_LRStack[m_LRStackIndex] = head;
							}
							else
							{
								m_LRStack[m_LRStackIndex] = head;
							}
							return parseResult;
						}
						return TokenParseResult.InternalError;
				}
			}

			//=== Syntax Error! Fill Expected Tokens
			m_ExpectedTokens = new Symbol[m_LRState.ActionCount];
			Int32 length = 0;
			for (Int32 i = 0; i < m_LRState.ActionCount; i++)
			{
				switch (m_LRState.GetAction(i).Symbol.SymbolType)
				{
					case SymbolType.Terminal:
					case SymbolType.End:
						m_ExpectedTokens[length++] = m_LRState.GetAction(i).Symbol;
						break;
				}
			}
			if (length < m_ExpectedTokens.Length)
			{
				var newArray = new Symbol[length];
				Array.Copy(m_ExpectedTokens, newArray, length);
				m_ExpectedTokens = newArray;
			}
			return TokenParseResult.SyntaxError;
		}

		/// <summary>
		/// Result of parsing token.
		/// </summary>
		private enum TokenParseResult
		{
			//Empty = 0,
			Accept = 1,
			Shift = 2,
			ReduceNormal = 3,
			ReduceEliminated = 4,
			SyntaxError = 5,
			InternalError = 6
		}

		/// <summary>
		/// Represents data about current token.
		/// </summary>
		private struct Token
		{
			internal Symbol Symbol;     // Token symbol.
			internal String Text;       // Token text.
			internal Int32 Start;         // Token start stream start.
			internal Int32 Length;        // Token length.
			internal Int32 LineNumber;    // Token source line number. (1-based).
			internal Int32 LinePosition;  // Token position in source line (1-based).
			internal Object SyntaxNode; // Syntax node which can be attached to the token.
		}

		/// <summary>
		/// Represents item in the LR parsing stack.
		/// </summary>
		private struct LRStackItem
		{
			internal Token Token;   // Token in the LR stack item.
			internal LRState State; // LR state associated with the item.
			internal Rule Rule;     // Reference to a grammar rule if the item contains non-terminal.
		}

		private readonly Grammar m_Grammar;               // Grammar of parsed language.
		private Boolean m_TrimReductions = true; // Allowes to minimize reduction tree.

		private readonly TextReader m_TextReader;       // Data to parse.
		private Char[] m_Buffer;           // Buffer to keep current characters.
		private Int32 m_BufferSize;       // Size of the buffer.
		private Int32 m_BufferStartIndex; // Absolute position of buffered first character. 
		private Int32 m_CharIndex;        // Index of character in the buffer.
		private Int32 m_PreserveChars;    // Number of characters to preserve when buffer is refilled.
		private Int32 m_LineStart;        // Relative position of line start to the buffer beginning.
		private Int32 m_LineLength;       // Length of current source line.
		private Int32 m_LineNumber = 1;   // Current line number.
		private Int32 m_CommentLevel;     // Keeps stack level for embedded comments
		private StringBuilder m_CommentText;   // Current comment text.

		private SourceLineReadCallback m_SourceLineReadCallback; // Called when line reading finished. 

		private Token m_Token;            // Current token
		private Token[] m_InputTokens;      // Stack of input tokens.
		private Int32 m_InputTokenCount;  // How many tokens in the input.

		private LRStackItem[] m_LRStack;        // Stack of LR states used for LR parsing.
		private Int32 m_LRStackIndex;   // Index of current LR state in the LR parsing stack. 
		private LRState m_LRState;        // Current LR state.
		private Int32 m_ReductionCount; // Number of items in reduction. It is Undefined if no reducton available. 
		private Symbol[] m_ExpectedTokens; // What tokens are expected in case of error?  

		private const Int32 MinimumBufferSize = 4096;   // Minimum size of Char buffer.
		private const Char EndOfString = (Char)0;     // Designates last String terminator.
		private const Int32 MinimumInputTokenCount = 2; // Minimum input token stack size.
		private const Int32 MinimumLRStackSize = 256;   // Minimum size of reduction stack.
		private const Int32 Undefined = -1;             // Used for undefined Int32 values. 
	}
}
