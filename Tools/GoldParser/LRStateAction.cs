using System;

namespace GoldParser
{
	/// <summary>
	/// Action in a LR State. 
	/// </summary>
	public class LRStateAction
	{
		/// <summary>
		/// Creats a new instance of the <c>LRStateAction</c> class.
		/// </summary>
		/// <param name="index">Index of the LR state action.</param>
		/// <param name="symbol">Symbol associated with the action.</param>
		/// <param name="action">Action type.</param>
		/// <param name="value">Action value.</param>
		public LRStateAction(Int32 index, Symbol symbol, LRAction action, Int32 value)
		{
			m_Index = index;
			m_Symbol = symbol;
			m_Action = action;
			m_Value = value;
		}

		/// <summary>
		/// Gets index of the LR state action.
		/// </summary>
		public Int32 Index
		{
			get { return m_Index; }
		}

		/// <summary>
		/// Gets symbol associated with the LR state action.
		/// </summary>
		public Symbol Symbol
		{
			get { return m_Symbol; }
		}

		/// <summary>
		/// Gets action type.
		/// </summary>
		public LRAction Action
		{
			get { return m_Action; }
		}

		/// <summary>
		/// Gets the action value.
		/// </summary>
		public Int32 Value
		{
			get { return m_Value; }
		}

		private readonly Int32 m_Index;
		private readonly Symbol m_Symbol;
		private readonly LRAction m_Action;
		private readonly Int32 m_Value;
	}
}
