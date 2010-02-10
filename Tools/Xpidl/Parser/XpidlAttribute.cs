using System;
using System.Diagnostics;

namespace Xpidl.Parser
{
	internal sealed class XpidlAttribute : XpidlNode
	{
		public XpidlAttribute(String name, XpidlType type, Boolean isReadOnly, XpidlModifiers<XpidlMethodModifier> modifiers)
		{
			m_Name = name;
			m_Type = type;
			m_IsReadOnly = isReadOnly;
			m_Modifiers = (modifiers ?? new XpidlModifiers<XpidlMethodModifier>()).AsReadOnly();

			Debug.WriteLine(ToString());
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

		public XpidlModifiers<XpidlMethodModifier> Modifiers
		{
			get { return m_Modifiers; }
		}

		public override String ToString()
		{
			return String.Format(new BooleanFormatProvider(), "{0:readonly |}attribute {1} {2};", IsReadOnly, Type, Name);
		}

		private readonly String m_Name;
		private readonly XpidlType m_Type;
		private readonly Boolean m_IsReadOnly;
		private readonly XpidlModifiers<XpidlMethodModifier> m_Modifiers;
	}
}
