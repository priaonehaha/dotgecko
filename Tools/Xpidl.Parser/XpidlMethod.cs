using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Xpidl.Parser
{
	public enum XpidlMethodModifier : byte
	{
		NoScript,
		NotXpcom,
		BinaryName
	}

	public sealed class XpidlMethod : XpidlNode
	{
		internal XpidlMethod(String name, String type, XpidlModifiers<XpidlMethodModifier> modifiers, IEnumerable<XpidlMethodParameter> parameters)
		{
			m_Name = name;
			m_Type = type;
			m_Modifiers = (modifiers ?? new XpidlModifiers<XpidlMethodModifier>()).AsReadOnly();
			m_ReadOnlyParameters = (parameters != null ? new List<XpidlMethodParameter>(parameters) : new List<XpidlMethodParameter>(0)).AsReadOnly();
		}

		public String Name
		{
			get { return m_Name; }
		}

		public String Type
		{
			get { return m_Type; }
		}

		public XpidlModifiers<XpidlMethodModifier> Modifiers
		{
			get { return m_Modifiers; }
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
		private readonly String m_Type;
		private readonly XpidlModifiers<XpidlMethodModifier> m_Modifiers;
		private readonly ReadOnlyCollection<XpidlMethodParameter> m_ReadOnlyParameters;
	}
}
