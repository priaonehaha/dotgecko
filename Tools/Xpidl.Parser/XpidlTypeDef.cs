using System;

namespace Xpidl.Parser
{
	public sealed class XpidlTypeDef : XpidlNode
	{
		internal XpidlTypeDef(String name, XpidlType type)
		{
			m_Name = name;
			m_Type = type;
		}

		public String Name
		{
			get { return m_Name; }
		}

		public XpidlType Type
		{
			get { return m_Type; }
		}

		public override String ToString()
		{
			return String.Format("typedef {0} {1};", Type, Name);
		}

		private readonly String m_Name;
		private readonly XpidlType m_Type;
	}
}
