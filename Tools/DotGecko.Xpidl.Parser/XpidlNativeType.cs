using System;

namespace DotGecko.Xpidl.Parser
{
	public enum XpidlNativeTypeModifier : byte
	{
		Ref,
		Ptr,
		NsId,
		DomString,
		Utf8String,
		CString,
		AString
	}

	public sealed class XpidlNativeType : XpidlNode
	{
		internal XpidlNativeType(String name, String definition, XpidlModifiers<XpidlNativeTypeModifier> modifiers)
		{
			m_Name = name;
			m_Definition = definition;
			m_Modifiers = (modifiers ?? new XpidlModifiers<XpidlNativeTypeModifier>()).AsReadOnly();
		}

		public String Name
		{
			get { return m_Name; }
		}

		public String Definition
		{
			get { return m_Definition; }
		}

		public XpidlModifiers<XpidlNativeTypeModifier> Modifiers
		{
			get { return m_Modifiers; }
		}

		public override String ToString()
		{
			return String.Format("native {0} ({1});", Name, Definition);
		}

		private readonly String m_Name;
		private readonly String m_Definition;
		private readonly XpidlModifiers<XpidlNativeTypeModifier> m_Modifiers;
	}
}
