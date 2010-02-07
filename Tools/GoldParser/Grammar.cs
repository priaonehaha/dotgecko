using System;
using System.IO;
using System.Text;
using GoldParser.Properties;

namespace GoldParser
{
	internal static class BinaryReaderExtensions
	{
		public static String ReadUnicodeString(this BinaryReader binaryReader)
		{
			var result = new StringBuilder();
			var unicodeChar = (Char)binaryReader.ReadUInt16();
			while (unicodeChar != (Char)0)
			{
				result.Append(unicodeChar);
				unicodeChar = (Char)binaryReader.ReadUInt16();
			}
			return result.ToString();
		}
	}

	/// <summary>
	/// Contains grammar tables required for parsing.
	/// </summary>
	public sealed class Grammar
	{
		/// <summary>
		/// Identifies Gold parser grammar file.
		/// </summary>
		public const String FileHeader = "GOLD Parser Tables/v1.0";

		/// <summary>
		/// Creates a new instance of <c>Grammar</c> class
		/// </summary>
		/// <param name="reader"></param>
		public Grammar(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			Load(reader);
		}

		/// <summary>
		/// Gets grammar name.
		/// </summary>
		public String Name
		{
			get { return m_Name; }
		}

		/// <summary>
		/// Gets grammar version.
		/// </summary>
		public String Version
		{
			get { return m_Version; }
		}

		/// <summary>
		/// Gets grammar author.
		/// </summary>
		public String Author
		{
			get { return m_Author; }
		}

		/// <summary>
		/// Gets grammar description.
		/// </summary>
		public String About
		{
			get { return m_About; }
		}

		/// <summary>
		/// Gets the start symbol for the grammar.
		/// </summary>
		public Symbol StartSymbol
		{
			get { return m_SymbolTable[m_StartSymbolIndex]; }
		}

		/// <summary>
		/// Gets the value indicating if the grammar is case sensitive.
		/// </summary>
		public Boolean CaseSensitive
		{
			get { return m_CaseSensitive; }
		}

		/// <summary>
		/// Gets initial DFA state.
		/// </summary>
		public DfaState DfaInitialState
		{
			get { return m_DfaInitialState; }
		}

		/// <summary>
		/// Gets initial LR state.
		/// </summary>
		public LRState InitialLRState
		{
			get { return m_LRStateTable[m_LRInitialState]; }
		}

		/// <summary>
		/// Gets a special symbol to designate last token in the input stream.
		/// </summary>
		public Symbol EndSymbol
		{
			get { return m_EndSymbol; }
		}

		internal Rule[] RuleTable
		{
			get { return m_RuleTable; }
		}

		internal DfaState[] DfaStateTable
		{
			get { return m_DfaStateTable; }
		}

		internal LRState[] LrStateTable
		{
			get { return m_LRStateTable; }
		}

		internal Int32 DfaInitialStateIndex
		{
			get { return m_DfaInitialStateIndex; }
		}

		internal Int32 LrInitialState
		{
			get { return m_LRInitialState; }
		}

		internal Symbol ErrorSymbol
		{
			get { return m_ErrorSymbol; }
		}

		/// <summary>
		/// Loads grammar from the binary reader.
		/// </summary>
		private void Load(BinaryReader reader)
		{
			if (reader.ReadUnicodeString() != FileHeader)
			{
				throw new FileLoadException(Resources.Grammar_WrongFileHeader);
			}

			while (reader.PeekChar() != -1)
			{
				RecordDataType recordDataType = ReadNextRecord(reader);
				switch (recordDataType)
				{
					case RecordDataType.Parameters:
						ReadParameters(reader);
						break;

					case RecordDataType.TableCounts:
						ReadTableCounts(reader);
						break;

					case RecordDataType.Initial:
						ReadInitialStates(reader);
						break;

					case RecordDataType.Symbols:
						ReadSymbols(reader);
						break;

					case RecordDataType.CharSets:
						ReadCharSets(reader);
						break;

					case RecordDataType.Rules:
						ReadRules(reader);
						break;

					case RecordDataType.DfaStates:
						ReadDfaStates(reader);
						break;

					case RecordDataType.LRStates:
						ReadLRStates(reader);
						break;

					default:
						throw new FileLoadException(Resources.Grammar_InvalidRecordType);
				}
			}
			m_DfaInitialState = m_DfaStateTable[m_DfaInitialStateIndex];
			OptimizeDfaTransitionVectors();
		}

		/// <summary>
		/// Reads the next record in the binary grammar file.
		/// </summary>
		/// <returns>Read record type.</returns>
		private RecordDataType ReadNextRecord(BinaryReader reader)
		{
			var recordType = (RecordType)reader.ReadByte();
			//Structure below is ready for future expansion
			switch (recordType)
			{
				case RecordType.Multitype:
					//Read the number of entry's
					m_EntryCount = reader.ReadUInt16();
					return (RecordDataType)ReadByteEntry(reader);

				default:
					throw new FileLoadException(Resources.Grammar_InvalidRecordHeader);
			}
		}

		/// <summary>
		/// Reads grammar header information.
		/// </summary>
		private void ReadParameters(BinaryReader reader)
		{
			m_Name = ReadStringEntry(reader);
			m_Version = ReadStringEntry(reader);
			m_Author = ReadStringEntry(reader);
			m_About = ReadStringEntry(reader);
			m_CaseSensitive = ReadBoolEntry(reader);
			m_StartSymbolIndex = ReadInt16Entry(reader);
		}

		/// <summary>
		/// Reads table record counts and initializes tables.
		/// </summary>
		private void ReadTableCounts(BinaryReader reader)
		{
			// Initialize tables
			m_SymbolTable = new Symbol[ReadInt16Entry(reader)];
			m_CharSetTable = new String[ReadInt16Entry(reader)];
			m_RuleTable = new Rule[ReadInt16Entry(reader)];
			m_DfaStateTable = new DfaState[ReadInt16Entry(reader)];
			m_LRStateTable = new LRState[ReadInt16Entry(reader)];
		}

		/// <summary>
		/// Read initial DFA and LR states.
		/// </summary>
		private void ReadInitialStates(BinaryReader reader)
		{
			m_DfaInitialStateIndex = ReadInt16Entry(reader);
			m_LRInitialState = ReadInt16Entry(reader);
		}

		/// <summary>
		/// Read symbol information.
		/// </summary>
		private void ReadSymbols(BinaryReader reader)
		{
			Int32 index = ReadInt16Entry(reader);
			String name = ReadStringEntry(reader);
			var symbolType = (SymbolType)ReadInt16Entry(reader);

			var symbol = new Symbol(index, name, symbolType);
			switch (symbolType)
			{
				case SymbolType.Error:
					m_ErrorSymbol = symbol;
					break;

				case SymbolType.End:
					m_EndSymbol = symbol;
					break;
			}
			m_SymbolTable[index] = symbol;
		}

		/// <summary>
		/// Read Char set information.
		/// </summary>
		private void ReadCharSets(BinaryReader reader)
		{
			m_CharSetTable[ReadInt16Entry(reader)] = ReadStringEntry(reader);
		}

		/// <summary>
		/// Read rule information.
		/// </summary>
		private void ReadRules(BinaryReader reader)
		{
			Int32 index = ReadInt16Entry(reader);
			Symbol nonTerminal = m_SymbolTable[ReadInt16Entry(reader)];
			ReadEmptyEntry(reader);
			var symbols = new Symbol[m_EntryCount];
			for (Int32 i = 0; i < symbols.Length; i++)
			{
				symbols[i] = m_SymbolTable[ReadInt16Entry(reader)];
			}
			var rule = new Rule(index, nonTerminal, symbols);
			m_RuleTable[index] = rule;
		}

		/// <summary>
		/// Read DFA state information.
		/// </summary>
		private void ReadDfaStates(BinaryReader reader)
		{
			Int32 index = ReadInt16Entry(reader);
			Boolean acceptState = ReadBoolEntry(reader);
			Int16 acceptIndex = ReadInt16Entry(reader);
			ReadEmptyEntry(reader);

			// Read DFA edges
			var edges = new DfaEdge[m_EntryCount / 3];
			for (Int32 i = 0; i < edges.Length; i++)
			{
				edges[i] = new DfaEdge(ReadInt16Entry(reader), ReadInt16Entry(reader));
				ReadEmptyEntry(reader);
			}

			// Create DFA state and store it in DFA state table
			Symbol acceptSymbol = acceptState ? m_SymbolTable[acceptIndex] : null;
			ObjectMap transitionVector = CreateDfaTransitionVector(edges);
			var dfaState = new DfaState(index, acceptSymbol, transitionVector);
			m_DfaStateTable[index] = dfaState;
		}

		/// <summary>
		/// Read LR state information.
		/// </summary>
		private void ReadLRStates(BinaryReader reader)
		{
			Int32 index = ReadInt16Entry(reader);
			ReadEmptyEntry(reader);
			var stateTable = new LRStateAction[m_EntryCount / 4];
			for (Int32 i = 0; i < stateTable.Length; i++)
			{
				Symbol symbol = m_SymbolTable[ReadInt16Entry(reader)];
				var action = (LRAction)ReadInt16Entry(reader);
				Int32 targetIndex = ReadInt16Entry(reader);
				ReadEmptyEntry(reader);
				stateTable[i] = new LRStateAction(i, symbol, action, targetIndex);
			}

			// Create the transition vector
			var transitionVector = new LRStateAction[m_SymbolTable.Length];
			Array.Clear(transitionVector, 0, transitionVector.Length);
			
			for (Int32 i = 0; i < stateTable.Length; i++)
			{
				transitionVector[stateTable[i].Symbol.Index] = stateTable[i];
			}

			var lrState = new LRState(index, stateTable, transitionVector);
			m_LRStateTable[index] = lrState;
		}

		/// <summary>
		/// Creates the DFA state transition vector.
		/// </summary>
		/// <param name="edges">Array of automata edges.</param>
		/// <returns>Hashtable with the transition information.</returns>
		private ObjectMap CreateDfaTransitionVector(DfaEdge[] edges)
		{
			var transitionVector = new ObjectMap();
			for (Int32 i = edges.Length - 1; i >= 0; i--)
			{
				String charSet = m_CharSetTable[edges[i].CharSetIndex];
				foreach (Char ch in charSet)
				{
					transitionVector[ch] = edges[i].TargetIndex;
				}
			}
			return transitionVector;
		}

		/// <summary>
		/// Reads empty entry from the grammar file.
		/// </summary>
		private void ReadEmptyEntry(BinaryReader reader)
		{
			if (ReadEntryDataType(reader) != EntryDataType.Empty)
			{
				throw new FileLoadException(Resources.Grammar_EmptyEntryExpected);
			}
			m_EntryCount--;
		}

		/// <summary>
		/// Reads String entry from the grammar file.
		/// </summary>
		/// <returns>String entry content.</returns>
		private String ReadStringEntry(BinaryReader reader)
		{
			if (ReadEntryDataType(reader) != EntryDataType.String)
			{
				throw new FileLoadException(Resources.Grammar_StringEntryExpected);
			}
			m_EntryCount--;
			return reader.ReadUnicodeString();
		}

		/// <summary>
		/// Reads Int16 entry from the grammar file.
		/// </summary>
		/// <returns>Int16 entry content.</returns>
		private Int16 ReadInt16Entry(BinaryReader reader)
		{
			if (ReadEntryDataType(reader) != EntryDataType.Integer)
			{
				throw new FileLoadException(Resources.Grammar_IntegerEntryExpected);
			}
			m_EntryCount--;
			return reader.ReadInt16();
		}

		/// <summary>
		/// Reads Byte entry from the grammar file.
		/// </summary>
		/// <returns>Byte entry content.</returns>
		private Byte ReadByteEntry(BinaryReader reader)
		{
			if (ReadEntryDataType(reader) != EntryDataType.Byte)
			{
				throw new FileLoadException(Resources.Grammar_ByteEntryExpected);
			}
			m_EntryCount--;
			return reader.ReadByte();
		}

		/// <summary>
		/// Reads boolean entry from the grammar file.
		/// </summary>
		/// <returns>Boolean entry content.</returns>
		private Boolean ReadBoolEntry(BinaryReader reader)
		{
			if (ReadEntryDataType(reader) != EntryDataType.Boolean)
			{
				throw new FileLoadException(Resources.Grammar_BooleanEntryExpected);
			}
			m_EntryCount--;
			return reader.ReadBoolean();
		}

		/// <summary>
		/// Reads entry type.
		/// </summary>
		/// <returns>Entry type.</returns>
		private EntryDataType ReadEntryDataType(BinaryReader reader)
		{
			if (m_EntryCount == 0)
			{
				throw new FileLoadException(Resources.Grammar_NoEntry);
			}
			return (EntryDataType)reader.ReadByte();
		}

		private void OptimizeDfaTransitionVectors()
		{
			foreach (DfaState state in m_DfaStateTable)
			{
				ObjectMap transitions = state.TransitionVector;
				for (Int32 i = transitions.Count - 1; i >= 0; i--)
				{
					Int32 key = transitions.GetKey(i);
					Object transition = transitions[key];
					if (transition != null)
					{
						var transitionIndex = (Int16)transition;
						transitions[key] = transitionIndex >= 0 ? m_DfaStateTable[transitionIndex] : null;
					}
				}
				transitions.ReadOnly = true;
			}
		}

		private enum RecordType : byte
		{
			Multitype = (Byte)'M' // 77
		}

		/// <summary>
		/// Record type Byte in the binary grammar file.
		/// </summary>
		private enum RecordDataType : byte
		{
			Parameters = (Byte)'P', // 80
			TableCounts = (Byte)'T', // 84
			Initial = (Byte)'I', // 73
			Symbols = (Byte)'S', // 83
			CharSets = (Byte)'C', // 67
			Rules = (Byte)'R', // 82
			DfaStates = (Byte)'D', // 68
			LRStates = (Byte)'L' // 76
			//Comment = (Byte)'!'  // 33
		}

		/// <summary>
		/// Entry type Byte in the binary grammar file.
		/// </summary>
		private enum EntryDataType : byte
		{
			Empty = (Byte)'E', // 69
			Integer = (Byte)'I', // 73
			String = (Byte)'S', // 83
			Boolean = (Byte)'B', // 66
			Byte = (Byte)'b'  // 98
		}

		/// <summary>
		/// Edge between DFA states.
		/// </summary>
		private struct DfaEdge
		{
			public DfaEdge(Int16 charSetIndex, Int16 targetIndex)
			{
				m_CharSetIndex = charSetIndex;
				m_TargetIndex = targetIndex;
			}

			public Int16 CharSetIndex
			{
				get { return m_CharSetIndex; }
			}

			public Int16 TargetIndex
			{
				get { return m_TargetIndex; }
			}

			private readonly Int16 m_CharSetIndex;
			private readonly Int16 m_TargetIndex;
		}

		// Grammar header information
		private String m_Name;               // Name of the grammar
		private String m_Version;            // Version of the grammar
		private String m_Author;             // Author of the grammar
		private String m_About;              // Grammar description
		private Boolean m_CaseSensitive;     // Grammar is case sensitive or not
		private Int32 m_StartSymbolIndex;    // Start symbol index

		// Tables read from the binary grammar file
		private Symbol[] m_SymbolTable;      // Symbol table
		private String[] m_CharSetTable;     // Charset table
		private Rule[] m_RuleTable;          // Rule table
		private DfaState[] m_DfaStateTable;  // DFA state table
		private LRState[] m_LRStateTable;    // LR state table

		// Initial states
		private Int32 m_DfaInitialStateIndex;// DFA initial state index
		private DfaState m_DfaInitialState;  // DFA initial state 
		private Int32 m_LRInitialState;      // LR initial state

		// Internal state of grammar parser
		private UInt16 m_EntryCount;         // Number of entries left

		private Symbol m_ErrorSymbol;
		private Symbol m_EndSymbol;
	}
}
