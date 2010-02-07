using System;

namespace GoldParser
{
	/// <summary>
	/// State of LR parser.
	/// </summary>
	public class LRState
	{
		/// <summary>
		/// Creates a new instance of the <c>LRState</c> class
		/// </summary>
		/// <param name="index">Index of the LR state in the LR state table.</param>
		/// <param name="actions">List of all available LR actions in this state.</param>
		/// <param name="transitionVector">Transition vector which has symbol index as an index.</param>
		public LRState(Int32 index, LRStateAction[] actions, LRStateAction[] transitionVector)
		{
			m_Index = index;
			m_Actions = actions;
			m_TransitionVector = transitionVector;
		}

		/// <summary>
		/// Gets index of the LR state in LR state table.
		/// </summary>
		public Int32 Index
		{
			get { return m_Index; }
		}

		/// <summary>
		/// Gets LR state action count.
		/// </summary>
		public Int32 ActionCount
		{
			get { return m_Actions.Length; }
		}

		/// <summary>
		/// Returns state action by its index.
		/// </summary>
		/// <param name="index">State action index.</param>
		/// <returns>LR state action for the given index.</returns>
		public LRStateAction GetAction(Int32 index)
		{
			return m_Actions[index];
		}

		/// <summary>
		/// Returns LR state action by symbol index.
		/// </summary>
		/// <param name="symbolIndex">Symbol Index to search for.</param>
		/// <returns>LR state action object.</returns>
		public LRStateAction GetActionBySymbolIndex(Int32 symbolIndex)
		{
			return m_TransitionVector[symbolIndex];
		}

		private readonly Int32 m_Index;
		private readonly LRStateAction[] m_Actions;
		private readonly LRStateAction[] m_TransitionVector;
	}
}
