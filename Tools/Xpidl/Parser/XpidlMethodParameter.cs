using System;

namespace XPIDL.Parser
{
	[Flags]
	internal enum XpidlParameterDirection : byte
	{
		In = 0x01,
		Out = 0x02
	}

	internal sealed class XpidlMethodParameter
	{
		public XpidlMethodParameter(String name, XpidlType type, XpidlParameterDirection direction)
		{
			m_Name = name;
			m_Type = type;
			m_Direction = direction;
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

		private readonly String m_Name;
		private readonly XpidlType m_Type;
		private readonly XpidlParameterDirection m_Direction;
	}
}
