using System;

namespace Xpidl.Parser
{
	[Flags]
	internal enum XpidlParameterDirection : byte
	{
		In = 0x01,
		Out = 0x02
	}

	internal enum XpidlParamModifier : byte
	{
		Array,
		SizeIs,
		IidIs,
		RetVal,
		Const,
		Shared,
		Optional
	}

	internal sealed class XpidlMethodParameter
	{
		public XpidlMethodParameter(String name, XpidlType type, XpidlParameterDirection direction, XpidlModifiers<XpidlParamModifier> modifiers)
		{
			m_Name = name;
			m_Type = type;
			m_Direction = direction;
			m_Modifiers = (modifiers ?? new XpidlModifiers<XpidlParamModifier>()).AsReadOnly();
		}

		public String Name
		{
			get { return m_Name; }
		}

		public XpidlType Type
		{
			get { return m_Type; }
		}

		public XpidlParameterDirection Direction
		{
			get { return m_Direction; }
		}

		public XpidlModifiers<XpidlParamModifier> Modifiers
		{
			get { return m_Modifiers; }
		}

		private readonly String m_Name;
		private readonly XpidlType m_Type;
		private readonly XpidlParameterDirection m_Direction;
		private readonly XpidlModifiers<XpidlParamModifier> m_Modifiers;
	}
}
