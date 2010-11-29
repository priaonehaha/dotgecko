using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	internal abstract class XpcomStringMarshaler : ICustomMarshaler
	{
		public const String AString = "AString";
		public const String ACString = "ACString";
		public const String AUTF8String = "AUTF8String";
		public const String DOMString = "DOMString";

		public static ICustomMarshaler GetInstance(String cookie)
		{
			switch (cookie)
			{
				case AString:
				case DOMString:
					return AStringMarshaler.GetInstance(cookie);
				case ACString:
					return ACStringMarshaler.GetInstance(cookie);
				case AUTF8String:
					return AUTF8StringMarshaler.GetInstance(cookie);
				default:
					throw new ArgumentException("Invalid XPCOM string type", "cookie");
			}
		}

		public virtual Object MarshalNativeToManaged(IntPtr pNativeData)
		{
			String stringData = GetString(pNativeData);
			StringBuilder stringBuilder = GetStringBuilder(pNativeData);
			if (stringBuilder != null)
			{
				stringBuilder.Clear();
				if (stringData != null)
				{
					stringBuilder.Append(stringData);
				}
			}

			return stringData;
		}

		public virtual IntPtr MarshalManagedToNative(Object ManagedObj)
		{
			return AllocStringBuffer(ManagedObj as StringBuilder);
		}

		public virtual void CleanUpNativeData(IntPtr pNativeData)
		{
			FreeStringBuffer(pNativeData);
		}

		public virtual void CleanUpManagedData(Object ManagedObj)
		{ }

		public abstract Int32 GetNativeDataSize();

		protected IntPtr AllocStringBuffer(StringBuilder stringBuilder)
		{
			var bufferSize = (UInt32)(GetNativeDataSize() + IntPtr.Size);
			IntPtr bufferPtr = Xpcom.NS_Alloc(bufferSize);

			if (stringBuilder != null)
			{
				GCHandle stringBuilderHandle = GCHandle.Alloc(stringBuilder, GCHandleType.Normal);
				IntPtr stringBuilderPtr = GCHandle.ToIntPtr(stringBuilderHandle);
				Marshal.WriteIntPtr(bufferPtr, stringBuilderPtr);
			}
			else
			{
				Marshal.WriteIntPtr(bufferPtr, IntPtr.Zero);
			}

			bufferPtr = IntPtr.Add(bufferPtr, IntPtr.Size);
			return bufferPtr;
		}

		protected void FreeStringBuffer(IntPtr stringBuffer)
		{
			stringBuffer = IntPtr.Add(stringBuffer, -IntPtr.Size);
			IntPtr stringBuilderPtr = Marshal.ReadIntPtr(stringBuffer);
			if (stringBuilderPtr != IntPtr.Zero)
			{
				GCHandle stringBuilderHandle = GCHandle.FromIntPtr(stringBuilderPtr);
				stringBuilderHandle.Free();
			}
			Xpcom.NS_Free(stringBuffer);
		}

		protected abstract String GetString(IntPtr stringBuffer);

		protected StringBuilder GetStringBuilder(IntPtr stringBuffer)
		{
			IntPtr stringBuilderPtr = Marshal.ReadIntPtr(stringBuffer, -IntPtr.Size);
			if (stringBuilderPtr != IntPtr.Zero)
			{
				GCHandle stringBuilderHandle = GCHandle.FromIntPtr(stringBuilderPtr);
				return stringBuilderHandle.IsAllocated ? (StringBuilder)stringBuilderHandle.Target : null;
			}
			return null;
		}
	}

	internal sealed class AStringMarshaler : XpcomStringMarshaler
	{
		private AStringMarshaler()
		{ }

		new public static ICustomMarshaler GetInstance(String cookie)
		{
			return new AStringMarshaler();
		}

		public override IntPtr MarshalManagedToNative(Object ManagedObj)
		{
			IntPtr nsAStringPtr = base.MarshalManagedToNative(ManagedObj);
			if (ManagedObj == null)
			{
				Xpcom.NS_StringContainerInit(nsAStringPtr);
				Xpcom.NS_StringSetIsVoid(nsAStringPtr, true);
			}
			else
			{
				String data = ManagedObj.ToString();
				Xpcom.NS_StringContainerInit2(nsAStringPtr, data, (UInt32)data.Length, 0);
			}
			return nsAStringPtr;
		}

		public override void CleanUpNativeData(IntPtr pNativeData)
		{
			Xpcom.NS_StringContainerFinish(pNativeData);
			base.CleanUpNativeData(pNativeData);
		}

		public override Int32 GetNativeDataSize()
		{
			return Marshal.SizeOf(typeof(nsStringContainer));
		}

		protected override String GetString(IntPtr stringBuffer)
		{
			return stringBuffer.GetString();
		}
	}

	internal sealed class ACStringMarshaler : XpcomStringMarshaler
	{
		private ACStringMarshaler()
		{ }

		new public static ICustomMarshaler GetInstance(String cookie)
		{
			return new ACStringMarshaler();
		}

		public override IntPtr MarshalManagedToNative(Object ManagedObj)
		{
			IntPtr nsACStringPtr = base.MarshalManagedToNative(ManagedObj);
			if (ManagedObj == null)
			{
				Xpcom.NS_CStringContainerInit(nsACStringPtr);
				Xpcom.NS_CStringSetIsVoid(nsACStringPtr, true);
			}
			else
			{
				String data = ManagedObj.ToString();
				Xpcom.NS_CStringContainerInit2(nsACStringPtr, data, (UInt32)data.Length, 0);
			}
			return nsACStringPtr;
		}

		public override void CleanUpNativeData(IntPtr pNativeData)
		{
			Xpcom.NS_CStringContainerFinish(pNativeData);
			base.CleanUpNativeData(pNativeData);
		}

		public override Int32 GetNativeDataSize()
		{
			return Marshal.SizeOf(typeof(nsCStringContainer));
		}

		protected override String GetString(IntPtr stringBuffer)
		{
			return stringBuffer.GetCString();
		}
	}

	internal sealed class AUTF8StringMarshaler : XpcomStringMarshaler
	{
		private AUTF8StringMarshaler()
		{ }

		new public static ICustomMarshaler GetInstance(String cookie)
		{
			return new AUTF8StringMarshaler();
		}

		public override IntPtr MarshalManagedToNative(Object ManagedObj)
		{
			IntPtr nsAUTF8StringPtr = base.MarshalManagedToNative(ManagedObj);
			if (ManagedObj == null)
			{
				Xpcom.NS_CStringContainerInit(nsAUTF8StringPtr);
				Xpcom.NS_CStringSetIsVoid(nsAUTF8StringPtr, true);
			}
			else
			{
				Byte[] utf8Data = Encoding.UTF8.GetBytes(ManagedObj.ToString());
				Xpcom.NS_CStringContainerInit2(nsAUTF8StringPtr, utf8Data, (UInt32)utf8Data.Length, 0);
			}
			return nsAUTF8StringPtr;
		}

		public override void CleanUpNativeData(IntPtr pNativeData)
		{
			Xpcom.NS_CStringContainerFinish(pNativeData);
			base.CleanUpNativeData(pNativeData);
		}

		public override Int32 GetNativeDataSize()
		{
			return Marshal.SizeOf(typeof(nsCStringContainer));
		}

		protected override String GetString(IntPtr stringBuffer)
		{
			return stringBuffer.GetUTF8String();
		}
	}
}
