using System;
using System.Collections.Generic;
using System.Text;

namespace GoldParser
{
	/// <summary>
	/// Represents a terminal or nonterminal symbol used by the Deterministic
	/// Finite Automata (DFA) and LR Parser. 
	/// </summary>
	/// <remarks>
	/// Symbols can be either terminals (which represent a class of 
	/// tokens - such as identifiers) or nonterminals (which represent 
	/// the rules and structures of the grammar).  Terminal symbols fall 
	/// into several categories for use by the GOLD Parser Engine 
	/// which are enumerated in <c>SymbolType</c> enumeration.
	/// </remarks>
	public class Symbol
	{
		/// <summary>
		/// Creates a new instance of <c>Symbol</c> class.
		/// </summary>
		/// <param name="index">Symbol index in symbol table.</param>
		/// <param name="name">Name of the symbol.</param>
		/// <param name="symbolType">Type of the symbol.</param>
		public Symbol(Int32 index, String name, SymbolType symbolType)
		{
			m_Index = index;
			m_Name = name;
			m_SymbolType = symbolType;
		}

		/// <summary>
		/// Returns the index of the symbol in the GOLDParser object's Symbol Table.
		/// </summary>
		public Int32 Index
		{
			get { return m_Index; }
		}

		/// <summary>
		/// Returns the name of the symbol.
		/// </summary>
		public String Name
		{
			get { return m_Name; }
		}

		/// <summary>
		/// Returns an enumerated data type that denotes
		/// the class of symbols that the object belongs to.
		/// </summary>
		public SymbolType SymbolType
		{
			get { return m_SymbolType; }
		}

		/// <summary>
		/// Returns the text representation of the symbol.
		/// In the case of nonterminals, the name is delimited by angle brackets,
		/// special terminals are delimited by parenthesis
		/// and terminals are delimited by single quotes 
		/// (if special characters are present).
		/// </summary>
		/// <returns>String representation of symbol.</returns>
		public override String ToString()
		{
			if (m_Text == null)
			{
				switch (SymbolType)
				{
					case SymbolType.NonTerminal:
						m_Text = '<' + Name + '>';
						break;

					case SymbolType.Terminal:
						m_Text = FormatTerminalSymbol(Name);
						break;

					default:
						m_Text = '(' + Name + ')';
						break;
				}
			}
			return m_Text;
		}

		private static String FormatTerminalSymbol(IEnumerable<Char> source)
		{
			var result = new StringBuilder();
			foreach (Char ch in source)
			{
				if (ch == '\'')
				{
					result.Append("''");
				}
				else if (IsQuotedChar(ch) || (ch == '"'))
				{
					result.Append(new[] { '\'', ch, '\'' });
				}
				else
				{
					result.Append(ch);
				}
			}
			return result.ToString();
		}

		private static Boolean IsQuotedChar(Char value)
		{
			const String quotedChars = "|-+*?()[]{}<>!";
			return (quotedChars.IndexOf(value) >= 0);
		}

		private readonly Int32 m_Index; // symbol index in symbol table
		private readonly String m_Name; // name of the symbol
		private readonly SymbolType m_SymbolType; // type of the symbol
		private String m_Text; // printable representation of symbol
	}
}
