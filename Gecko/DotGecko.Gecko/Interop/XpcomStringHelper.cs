using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	internal static class XpcomStringHelper
	{
		internal static String Get(Action<StringBuilder> getter)
		{
			try
			{
				var retval = new StringBuilder();
				getter(retval);
				return retval.ToString();
			}
			catch (COMException ex)
			{
				if ((nsResult)ex.ErrorCode == nsResult.NS_ERROR_UNEXPECTED)
				{
					return null;
				}
				throw;
			}
		}

		internal static String Get<T1>(Action<T1, StringBuilder> getter, T1 arg1)
		{
			var retval = new StringBuilder();
			getter(arg1, retval);
			return retval.ToString();
		}

		internal static String Get<T1, T2>(Action<T1, T2, StringBuilder> getter, T1 arg1, T2 arg2)
		{
			var retval = new StringBuilder();
			getter(arg1, arg2, retval);
			return retval.ToString();
		}

		internal static String Get<T1, T2, T3>(Action<T1, T2, T3, StringBuilder> getter, T1 arg1, T2 arg2, T3 arg3)
		{
			var retval = new StringBuilder();
			getter(arg1, arg2, arg3, retval);
			return retval.ToString();
		}
	}
}
