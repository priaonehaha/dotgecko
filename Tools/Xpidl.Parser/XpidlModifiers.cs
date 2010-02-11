using System;
using System.Collections.Generic;

namespace Xpidl.Parser
{
	public sealed class XpidlModifiers<T> where T : struct //enum
	{
		internal XpidlModifiers()
		{
			if (!typeof(T).IsEnum)
			{
				throw new InvalidOperationException("Only enumeration can be used as a type parameter");
			}

			m_Parameters = new Dictionary<T, String>(0);
		}

		public String this[T modifier]
		{
			get
			{
				String parameter;
				return m_Parameters.TryGetValue(modifier, out parameter) ? parameter : null;
			}
		}

		public Boolean Contains(T modifier)
		{
			return m_Parameters.ContainsKey(modifier);
		}

		internal void Add(T modifier)
		{
			Add(modifier, null);
		}

		internal void Add(T modifier, String parameter)
		{
			if (m_IsReadOnly)
			{
				throw new InvalidOperationException("Object is read only");
			}

			m_Parameters[modifier] = parameter;
		}

		internal XpidlModifiers<T> AsReadOnly()
		{
			m_IsReadOnly = true;
			return this;
		}

		private Boolean m_IsReadOnly;
		private readonly Dictionary<T, String> m_Parameters;
	}
}
