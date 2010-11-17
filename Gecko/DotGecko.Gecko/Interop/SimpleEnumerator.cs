using System;
using System.Collections.Generic;

namespace DotGecko.Gecko.Interop
{
	internal sealed class SimpleEnumerator<T, U> : nsISimpleEnumerator where U : class
	{
		internal SimpleEnumerator(IList<T> list, Converter<T, U> converter = null)
		{
			m_List = list;
			m_Converter = converter;
			m_Index = 0;
		}

		Boolean nsISimpleEnumerator.HasMoreElements()
		{
			return m_Index < m_List.Count;
		}

		nsResult nsISimpleEnumerator.GetNext(out Object retval)
		{
			if (m_Index < m_List.Count)
			{
				retval = Convert(m_List[m_Index++]);
				return nsResult.NS_OK;
			}
			retval = null;
			return nsResult.NS_ERROR_FAILURE;
		}

		private U Convert(T value)
		{
			if (m_Converter != null)
			{
				return m_Converter(value);
			}
			if (typeof(U).IsAssignableFrom(typeof(T)))
			{
				return (U)(Object)value;
			}
			return (U)System.Convert.ChangeType(value, typeof(U));
		}

		private readonly IList<T> m_List;
		private readonly Converter<T, U> m_Converter;
		private Int32 m_Index;
	}
}
