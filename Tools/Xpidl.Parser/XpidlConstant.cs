using System;
using System.Linq.Expressions;

namespace Xpidl.Parser
{
	public sealed class XpidlConstant : XpidlNode
	{
		internal XpidlConstant(String name, String type, Expression value)
		{
			m_Name = name;
			m_Type = type;
			m_Value = value;
		}

		public String Name
		{
			get { return m_Name; }
		}

		public String Type
		{
			get { return m_Type; }
		}

		public Expression Value
		{
			get { return m_Value; }
		}

		private readonly String m_Name;
		private readonly String m_Type;
		private readonly Expression m_Value;
	}
}
