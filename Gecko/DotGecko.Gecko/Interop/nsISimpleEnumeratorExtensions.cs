using System;
using System.Collections.Generic;

namespace DotGecko.Gecko.Interop
{
	internal static class nsISimpleEnumeratorExtensions
	{
		internal static IEnumerable<T> ToEnumerable<T>(this nsISimpleEnumerator simpleEnumerator, Converter<Object, T> converter)
		{
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			if (simpleEnumerator == null)
			{
				yield break;
			}
			Object item;
			while (simpleEnumerator.HasMoreElements() && (simpleEnumerator.GetNext(out item) == nsResult.NS_OK))
			{
				yield return converter(item);
			}
			yield break;
		}
	}
}
