using System;

namespace XPIDL.Parser
{
	internal sealed class XpidlAttribute : XpidlNode
	{
		public XpidlAttribute(String name, XpidlType type, Boolean isReadOnly, XpidlMethodModifier modifier)
		{
			m_Name = name;
			m_Type = type;
			m_IsReadOnly = isReadOnly;
			m_Modifier = modifier;
		}

		public String Name
		{
			get { return m_Name; }
		}

		public XpidlType Type
		{
			get { return m_Type; }
		}

		public Boolean IsReadOnly
		{
			get { return m_IsReadOnly; }
		}

		public XpidlMethodModifier Modifier
		{
			get { return m_Modifier; }
		}

		private readonly String m_Name;
		private readonly XpidlType m_Type;
		private readonly Boolean m_IsReadOnly;
		private readonly XpidlMethodModifier m_Modifier;
	}
}
