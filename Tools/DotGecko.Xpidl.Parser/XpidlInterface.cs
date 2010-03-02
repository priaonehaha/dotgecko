using System;

namespace DotGecko.Xpidl.Parser
{
	public enum XpidlInterfaceModifier : byte
	{
		Scriptable,
		Function,
		Object,
		NotXpcom,
		NoScript
	}

	public sealed class XpidlInterface : XpidlComplexNode
	{
		internal XpidlInterface(String name, String baseName, Guid uuid, XpidlModifiers<XpidlInterfaceModifier> modifiers)
		{
			m_Name = name;
			m_BaseName = baseName;
			m_Uuid = uuid;
			m_Modifiers = (modifiers ?? new XpidlModifiers<XpidlInterfaceModifier>()).AsReadOnly();
		}

		public String Name
		{
			get { return m_Name; }
		}

		public String BaseName
		{
			get { return m_BaseName; }
		}

		public Guid Uuid
		{
			get { return m_Uuid; }
		}

		public XpidlModifiers<XpidlInterfaceModifier> Modifiers
		{
			get { return m_Modifiers; }
		}

		internal void AddNode(XpidlComment comment)
		{
			AddNodeImpl(comment);
		}

		internal void AddNode(XpidlInlineCHeader inlineCHeader)
		{
			AddNodeImpl(inlineCHeader);
		}

		internal void AddNode(XpidlConstant constant)
		{
			AddNodeImpl(constant);
		}

		internal void AddNode(XpidlAttribute attribute)
		{
			AddNodeImpl(attribute);
		}

		internal void AddNode(XpidlMethod method)
		{
			AddNodeImpl(method);
		}

		private readonly String m_Name;
		private readonly String m_BaseName;
		private readonly Guid m_Uuid;
		private readonly XpidlModifiers<XpidlInterfaceModifier> m_Modifiers;
	}
}
