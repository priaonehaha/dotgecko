using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	public static class nsStringExtensions
	{
		public static String GetString(this IntPtr ptrAString)
		{
			if (Xpcom.NS_StringGetIsVoid(ptrAString))
			{
				return null;
			}
			IntPtr data;
			UInt32 length = Xpcom.NS_StringGetData(ptrAString, out data, IntPtr.Zero);
			return length > 0 ? Marshal.PtrToStringUni(data, (Int32)length) : String.Empty;
		}

		public static void SetString(this IntPtr ptrAString, String value)
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

		public static String GetCString(this IntPtr ptrACString)
		{
			if (Xpcom.NS_CStringGetIsVoid(ptrACString))
			{
				return null;
			}
			IntPtr data;
			UInt32 length = Xpcom.NS_CStringGetData(ptrACString, out data, IntPtr.Zero);
			return length > 0 ? Marshal.PtrToStringAnsi(data, (Int32)length) : String.Empty;
		}

		public static void SetCString(this IntPtr ptrACString, String value)
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

		public static String GetUTF8String(this IntPtr ptrAUTF8String)
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

		public static void SetUTF8String(this IntPtr ptrAUTF8String, String value)
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
}
