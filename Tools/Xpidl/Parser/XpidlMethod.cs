using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XPIDL.Parser
{
	[Flags]
	internal enum XpidlMethodModifier : byte
	{
		None = 0x00,
		NoScript = 0x01,
		NotXpcom = 0x02
	}

	internal sealed class XpidlMethod : XpidlNode
	{
		public XpidlMethod(String name, XpidlType type, XpidlMethodModifier modifier, IEnumerable<XpidlMethodParameter> parameters)
		{
			m_Name = name;
			m_Type = type;
			m_Modifier = modifier;
			m_ReadOnlyParameters = new List<XpidlMethodParameter>(parameters).AsReadOnly();
		}

		public String Name
		{
			get { return m_Name; }
		}

		public XpidlType Type
		{
			get { return m_Type; }
		}

		public XpidlMethodModifier Modifier
		{
			get { return m_Modifier; }
		}

		public IList<XpidlMethodParameter> Parameters
		{
			get { return m_ReadOnlyParameters; }
		}

		public Int32 GetParameterIndex(String parameterName)
		{
			for (Int32 i = 0; i < Parameters.Count; ++i)
			{
				if (Parameters[i].Name == parameterName)
				{
					return i;
				}
			}

			return -1;
		}

		private readonly String m_Name;
		private readonly XpidlType m_Type;
		private readonly XpidlMethodModifier m_Modifier;
		private readonly ReadOnlyCollection<XpidlMethodParameter> m_ReadOnlyParameters;
	}
}
