using System;
using System.Text;

namespace GoldParser
{
	/// <summary>
	/// Rule is the logical structures of the grammar.
	/// </summary>
	/// <remarks>
	/// Rules consist of a head containing a nonterminal 
	/// followed by a series of both nonterminals and terminals.
	/// </remarks>	
	public class Rule
	{
		/// <summary>
		/// Creates a new instance of <c>Rule</c> class.
		/// </summary>
		/// <param name="index">Index of the rule in the grammar rule table.</param>
		/// <param name="nonTerminal">Nonterminal of the rule.</param>
		/// <param name="symbols">Terminal and nonterminal symbols of the rule.</param>
		public Rule(Int32 index, Symbol nonTerminal, Symbol[] symbols)
		{
			m_Index = index;
			m_NonTerminal = nonTerminal;
			m_Symbols = symbols;
			m_HasOneNonTerminal = (symbols.Length == 1) && (symbols[0].SymbolType == SymbolType.NonTerminal);
		}

		/// <summary>
		/// Gets index of the rule in the rule table.
		/// </summary>
		public Int32 Index
		{
			get { return m_Index; }
		}

		/// <summary>
		/// Gets the head symbol of the rule.
		/// </summary>
		public Symbol NonTerminal
		{
			get { return m_NonTerminal; }
		}

		/// <summary>
		/// Gets name of the rule.
		/// </summary>
		public String Name
		{
			get { return '<' + m_NonTerminal.Name + '>'; }
		}

		/// <summary>
		/// Gets number of symbols.
		/// </summary>
		public Int32 Count
		{
			get { return m_Symbols.Length; }
		}

		/// <summary>
		/// Gets symbol by its index.
		/// </summary>
		public Symbol this[Int32 index]
		{
			get { return m_Symbols[index]; }
		}

		/// <summary>
		/// Gets true if the rule contains exactly one symbol.
		/// </summary>
		/// <remarks>Used by the Parser object to TrimReductions</remarks>
		public Boolean ContainsOneNonTerminal
		{
			get { return m_HasOneNonTerminal; }
		}

		/// <summary>
		/// Gets the rule definition.
		/// </summary>
		public String Definition
		{
			get
			{
				var result = new StringBuilder();
				for (Int32 i = 0; i < m_Symbols.Length; i++)
				{
					result.Append(m_Symbols[i].ToString());
					if (i < m_Symbols.Length - 1)
					{
						result.Append(' ');
					}
				}
				return result.ToString();
			}
		}

		/// <summary>
		/// Returns the Backus-Noir representation of the rule.
		/// </summary>
		/// <returns></returns>
		public override String ToString()
		{
			return Name + " ::= " + Definition;
		}

		private readonly Int32 m_Index;
		private readonly Symbol m_NonTerminal;
		private readonly Symbol[] m_Symbols;
		private readonly Boolean m_HasOneNonTerminal;
	}
}
