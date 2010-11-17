using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	internal static class XpcomString
	{
		internal static String Get(Action<StringBuilder> getter)
		{
			try
			{
				var retval = new StringBuilder();
				getter(retval);
				return retval.ToString();
			}
			catch(COMException ex)
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

		internal static String GetString(this IntPtr ptrAString)
		{
			if (Xpcom.NS_StringGetIsVoid(ptrAString))
			{
				return null;
			}
			IntPtr data;
			UInt32 length = Xpcom.NS_StringGetData(ptrAString, out data, IntPtr.Zero);
			return length > 0 ? Marshal.PtrToStringUni(data, (Int32)length) : String.Empty;
		}

		internal static void SetString(this IntPtr ptrAString, String value)
		{
			if (value == null)
			{
				Xpcom.NS_StringSetIsVoid(ptrAString, true);
			}
			else
			{
				Xpcom.NS_StringSetData(ptrAString, value, (UInt32)value.Length);
			}
		}

		internal static String GetCString(this IntPtr ptrACString)
		{
			if (Xpcom.NS_CStringGetIsVoid(ptrACString))
			{
				return null;
			}
			IntPtr data;
			UInt32 length = Xpcom.NS_CStringGetData(ptrACString, out data, IntPtr.Zero);
			return length > 0 ? Marshal.PtrToStringAnsi(data, (Int32)length) : String.Empty;
		}

		internal static void SetCString(this IntPtr ptrACString, String value)
		{
			if (value == null)
			{
				Xpcom.NS_CStringSetIsVoid(ptrACString, true);
			}
			else
			{
				Xpcom.NS_CStringSetData(ptrACString, value, (UInt32)value.Length);
			}
		}

		internal static String GetUTF8String(this IntPtr ptrAUTF8String)
		{
			if (Xpcom.NS_CStringGetIsVoid(ptrAUTF8String))
			{
				return null;
			}
			IntPtr data;
			UInt32 length = Xpcom.NS_CStringGetData(ptrAUTF8String, out data, IntPtr.Zero);

			if (length == 0)
			{
				return String.Empty;
			}

			var utf8Data = new Byte[length];
			Marshal.Copy(data, utf8Data, 0, (Int32)length);
			return Encoding.UTF8.GetString(utf8Data);
		}

		internal static void SetUTF8String(this IntPtr ptrAUTF8String, String value)
		{
			if (value == null)
			{
				Xpcom.NS_CStringSetIsVoid(ptrAUTF8String, true);
			}
			else
			{
				Byte[] valueUTF8 = Encoding.UTF8.GetBytes(value);
				Xpcom.NS_CStringSetData(ptrAUTF8String, valueUTF8, (UInt32)valueUTF8.Length);
			}
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal abstract class nsStringContainer_base
	{
		protected IntPtr d1;
		protected UInt32 d2;
		protected UInt32 d3;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal abstract class nsStringContainer : nsStringContainer_base
	{ }

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal abstract class nsCStringContainer : nsStringContainer_base
	{ }
}
