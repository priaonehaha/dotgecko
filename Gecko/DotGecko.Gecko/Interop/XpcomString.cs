using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	internal static class XpcomString
	{
		public static String Get<T>(Action<T> getter) where T : nsStringContainer_base, new()
		{
			using (var xpcomString = new T())
			{
				getter(xpcomString);
				return xpcomString.ToString();
			}
		}

		public static void Set<T>(Action<T> setter, String value) where T : nsStringContainer_base, new()
		{
			using (var xpcomString = new T())
			{
				xpcomString.Assign(value);
				setter(xpcomString);
			}
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal abstract class nsStringContainer_base : IDisposable
	{
		public abstract UInt32 Length();

		public Boolean IsEmpty()
		{
			return Length() == 0;
		}

		public abstract void Assign(String value);

		public virtual void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		~nsStringContainer_base()
		{
			Dispose();
		}

		protected IntPtr d1;
		protected UInt32 d2;
		protected UInt32 d3;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal abstract class nsStringContainer : nsStringContainer_base
	{ }

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal class nsAString : nsStringContainer
	{
		public nsAString()
		{
			Xpcom.NS_StringContainerInit(this);
		}

		public override UInt32 Length()
		{
			IntPtr data;
			return Xpcom.NS_StringGetData(this, out data, IntPtr.Zero);
		}

		public override void Assign(String value)
		{
			Xpcom.NS_StringSetData(this, value, (UInt32)(value == null ? 0 : value.Length));
		}

		public override String ToString()
		{
			IntPtr data;
			UInt32 length = Xpcom.NS_StringGetData(this, out data, IntPtr.Zero);
			return length > 0 ? Marshal.PtrToStringUni(data, (Int32)length) : String.Empty;
		}

		public override void Dispose()
		{
			Xpcom.NS_StringContainerFinish(this);
			base.Dispose();
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal abstract class nsCStringContainer : nsStringContainer_base
	{ }

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal class nsACString : nsCStringContainer
	{
		public nsACString()
		{
			Xpcom.NS_CStringContainerInit(this);
		}

		public override UInt32 Length()
		{
			IntPtr data;
			return Xpcom.NS_CStringGetData(this, out data, IntPtr.Zero);
		}

		public override void Assign(String value)
		{
			Xpcom.NS_CStringSetData(this, value, (UInt32)(value == null ? 0 : value.Length));
		}

		public override String ToString()
		{
			IntPtr data;
			UInt32 length = Xpcom.NS_CStringGetData(this, out data, IntPtr.Zero);
			return length > 0 ? Marshal.PtrToStringAnsi(data, (Int32)length) : String.Empty;
		}

		public override void Dispose()
		{
			Xpcom.NS_CStringContainerFinish(this);
			base.Dispose();
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal class nsAUTF8String : nsACString
	{
		public override void Assign(String value)
		{
			Byte[] valueUtf8 = Encoding.UTF8.GetBytes(value ?? String.Empty);
			Xpcom.NS_CStringSetData(this, valueUtf8, (UInt32)valueUtf8.Length);
		}

		public override String ToString()
		{
			IntPtr data;
			UInt32 length = Xpcom.NS_CStringGetData(this, out data, IntPtr.Zero);
			if (length > 0)
			{
				var result = new Byte[length];
				Marshal.Copy(data, result, 0, (Int32)length);
				return Encoding.UTF8.GetString(result);
			}
			return String.Empty;
		}
	}
}
