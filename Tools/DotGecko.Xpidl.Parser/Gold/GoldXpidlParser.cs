using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using GoldParser;

namespace DotGecko.Xpidl.Parser.Gold
{
	public sealed partial class GoldXpidlParser : IXpidlParser
	{
		public GoldXpidlParser(BinaryReader grammarBinaryReader)
		{
			m_Grammar = new Grammar(grammarBinaryReader);
		}

		public XpidlFile Parse(TextReader xpidlTextReader)
		{
			try
			{
				ComplexSyntaxNode rootSyntaxNode = ParseImpl(xpidlTextReader);
				DisplaySyntaxTree(rootSyntaxNode);
				XpidlFile xpidlFile = CreateXpidlFile(rootSyntaxNode);
				return xpidlFile;
			}
			catch(XpidlParserException)
			{
				throw;
			}
			catch(Exception e)
			{
				throw new XpidlParserException("Internal parser error", e);
			}
		}

		private ComplexSyntaxNode ParseImpl(TextReader xpidlTextReader)
		{
			var goldParser = new GoldParser.Parser(xpidlTextReader, m_Grammar) { TrimReductions = true, IgnoreNestedComments = true };
			goldParser.AddCommentSymbols(
				new Regex(@"^\/\*$", RegexOptions.Singleline),  //  /*
				new Regex(@"^\*\/$", RegexOptions.Singleline)); //  */
			goldParser.AddCommentSymbols(
				new Regex(@"^\%\{\s*C\+\+$", RegexOptions.Singleline),     //  %{ C++
				new Regex(@"^\%\}(\s*C\+\+)?$", RegexOptions.Singleline)); //  %} C++

			var rootSyntaxNode = new ComplexSyntaxNode(null);
			while (true)
			{
				ParseMessage parseMessage = goldParser.Parse();
				switch (parseMessage)
				{
					case ParseMessage.Empty:
						break;

					// Comment or inline C header
					case ParseMessage.CommentLineRead:
					case ParseMessage.CommentBlockRead:
						var commentSyntaxNode = new CommentSyntaxNode(goldParser.CommentText);
						if (goldParser.TokenSyntaxNode == null)
						{
							rootSyntaxNode.AddChildNode(commentSyntaxNode);
						}
						else
						{
							((SyntaxNode)goldParser.TokenSyntaxNode).AttachCommentNode(commentSyntaxNode);
						}
						break;

					// Read valid token
					case ParseMessage.TokenRead:
						var simpleSyntaxNode = new SimpleSyntaxNode(goldParser.TokenSymbol, goldParser.TokenText);
						goldParser.TokenSyntaxNode = simpleSyntaxNode;
						break;

					// Can create new xpidl-node
					case ParseMessage.Reduction:
						var complexSyntaxNode = new ComplexSyntaxNode(goldParser.ReductionRule);
						for (Int32 i = 0; i < goldParser.ReductionCount; ++i)
						{
							var syntaxNode = (SyntaxNode)goldParser.GetReductionSyntaxNode(i);
							complexSyntaxNode.AddChildNode(syntaxNode);
							if (i == (goldParser.ReductionCount - 1))
							{
								complexSyntaxNode.ReattachCommentNodes(syntaxNode);
							}
							else
							{
								syntaxNode.DetachCommentNodes(complexSyntaxNode);
							}
						}
						goldParser.TokenSyntaxNode = complexSyntaxNode;
						break;

					// Parsing successfully completed
					case ParseMessage.Accept:
						var acceptedSyntaxNode = (SyntaxNode)goldParser.TokenSyntaxNode;
						Debug.Assert(acceptedSyntaxNode != null);
						rootSyntaxNode.AddChildNode(acceptedSyntaxNode);
						acceptedSyntaxNode.DetachCommentNodes(rootSyntaxNode);
						return rootSyntaxNode;

					// Grammar table is not loaded
					case ParseMessage.NotLoadedError:
						throw new XpidlParserException("Grammar not loaded");

					// Unexpected end of input
					case ParseMessage.CommentError:
						throw new XpidlParserException("Comment error");

					// Invalid token
					case ParseMessage.LexicalError:
						throw new XpidlParserSyntaxException("Can not recognize token", goldParser.TokenText, goldParser.LineNumber, goldParser.LinePosition);

					// Unexpected token
					case ParseMessage.SyntaxError:
						throw new XpidlParserSyntaxException("Unexpected token", goldParser.TokenText, goldParser.LineNumber, goldParser.LinePosition);

					// Fatal internal error
					case ParseMessage.InternalError:
						throw new XpidlParserException("Internal parser error");
				}
			}
		}

		[Conditional("DEBUG")]
		private static void DisplaySyntaxTree(ComplexSyntaxNode rootSyntaxNode)
		{
			for (Int32 i = 0; i < rootSyntaxNode.Count; ++i)
			{
				SyntaxNode currentSyntaxNode = rootSyntaxNode[i];

				if (currentSyntaxNode is CommentSyntaxNode)
				{
					var commentSyntaxNode = (CommentSyntaxNode)currentSyntaxNode;
					if (commentSyntaxNode.IsInlineCHeader)
					{
						Debug.WriteLine("%{ C++ inline c header %}");
					}
					else
					{
						Debug.WriteLine("/* comment */");
					}
				}
				else if (currentSyntaxNode is SimpleSyntaxNode)
				{
					Debug.WriteLine(((SimpleSyntaxNode)currentSyntaxNode).Text);
				}
				else
				{
					var complexSyntaxNode = (ComplexSyntaxNode)currentSyntaxNode;
					Debug.WriteLine(complexSyntaxNode.Rule.Name);
					Debug.Indent();
					DisplaySyntaxTree(complexSyntaxNode);
					Debug.Unindent();
				}
			}
		}

		private readonly Grammar m_Grammar;
	}
}
