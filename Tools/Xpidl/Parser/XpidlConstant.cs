using System;
using System.Linq.Expressions;

namespace XPIDL.Parser
{
	internal sealed class XpidlConstant : XpidlNode
	{
		public XpidlConstant(String name, XpidlType type, Expression value)
		{
			m_Name = name;
			m_Type = type;
			m_Value = value;
		}

		public String Name
		{
			get { return m_Name; }
		}

		public XpidlType Type
		{
			get { return m_Type; }
		}

		public Expression Value
		{
			get { return m_Value; }
		}

		private readonly String m_Name;
		private readonly XpidlType m_Type;
		private readonly Expression m_Value;
	}
}
