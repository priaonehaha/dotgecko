using System;

namespace Xpidl.Parser
{
	[Flags]
	public enum XpidlParameterDirection : byte
	{
		In = 0x01,
		Out = 0x02
	}

	public enum XpidlParamModifier : byte
	{
		Array,
		SizeIs,
		IidIs,
		RetVal,
		Const,
		Shared,
		Optional
	}

	public sealed class XpidlMethodParameter
	{
		internal XpidlMethodParameter(String name, String type, XpidlParameterDirection direction, XpidlModifiers<XpidlParamModifier> modifiers)
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

		public String Type
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
		private readonly String m_Type;
		private readonly XpidlParameterDirection m_Direction;
		private readonly XpidlModifiers<XpidlParamModifier> m_Modifiers;
	}
}
