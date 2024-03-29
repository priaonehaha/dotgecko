﻿using System;

namespace DotGecko.Xpidl.Parser
{
	public sealed class XpidlTypeDef : XpidlNode
	{
		internal XpidlTypeDef(String name, String type)
		{
			m_Name = name;
			m_Type = type;
		}

		public String Name
		{
			get { return m_Name; }
		}

		public String Type
		{
			get { return m_Type; }
		}

		public override String ToString()
		{
			return String.Format("typedef {0} {1};", Type, Name);
		}

		private readonly String m_Name;
		private readonly String m_Type;
	}
}
