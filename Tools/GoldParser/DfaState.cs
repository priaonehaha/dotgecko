using System;

namespace GoldParser
{
	/// <summary>
	/// State in the Deterministic Finite Automata 
	/// which is used by the tokenizer.
	/// </summary>
	public sealed class DfaState
	{
		/// <summary>
		/// Creates a new instance of the <c>DfaState</c> class.
		/// </summary>
		/// <param name="index">Index in the DFA state table.</param>
		/// <param name="acceptSymbol">Symbol to accept.</param>
		/// <param name="transitionVector">Transition vector.</param>
		public DfaState(Int32 index, Symbol acceptSymbol, ObjectMap transitionVector)
		{
			m_Index = index;
			m_AcceptSymbol = acceptSymbol;
			m_TransitionVector = transitionVector;
		}

		/// <summary>
		/// Gets index of the state in DFA state table.
		/// </summary>
		public Int32 Index
		{
			get { return m_Index; }
		}

		/// <summary>
		/// Gets the symbol which can be accepted in this DFA state.
		/// </summary>
		public Symbol AcceptSymbol
		{
			get { return m_AcceptSymbol; }
		}

		internal ObjectMap TransitionVector
		{
			get { return m_TransitionVector; }
		}

		private readonly Int32 m_Index;
		private readonly Symbol m_AcceptSymbol;
		private readonly ObjectMap m_TransitionVector;
	}
}
