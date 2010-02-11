using System;

namespace Xpidl.Parser
{
	[Flags]
	public enum XpidlInterfaceModifier : byte
	{
		None = 0x00,
		Scriptable = 0x01,
		Function = 0x02,
		Object = 0x04,
		NotXpcom = 0x08,
		NoScript = 0x10
	}

	public sealed class XpidlInterface : XpidlComplexNode
	{
		internal XpidlInterface(String name, Guid uuid, XpidlInterfaceModifier modifier, String baseName)
		{
			m_Name = name;
			m_Uuid = uuid;
			m_Modifier = modifier;
			m_BaseName = baseName;
		}

		public String Name
		{
			get { return m_Name; }
		}

		public Guid Uuid
		{
			get { return m_Uuid; }
		}

		public XpidlInterfaceModifier Modifier
		{
			get { return m_Modifier; }
		}

		public String BaseName
		{
			get { return m_BaseName; }
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
		private readonly Guid m_Uuid;
		private readonly XpidlInterfaceModifier m_Modifier;
		private readonly String m_BaseName;
	}
}
