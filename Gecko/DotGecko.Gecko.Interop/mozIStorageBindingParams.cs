using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("a8d4827c-641c-45e3-a9ea-493570b4106b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageBindingParams //: nsISupports
	{
		/**
		 * Binds aValue to the parameter with the name aName.
		 *
		 * @param aName
		 *        The name of the parameter to bind aValue to.
		 * @param aValue
		 *        The value to bind.
		 */
		void BindByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName, nsIVariant aValue);
		void BindUTF8StringByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName,
								  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aValue);
		void BindStringByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName,
							  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue);
		void BindDoubleByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName, Double aValue);
		void BindInt32ByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName, Int32 aValue);
		void BindInt64ByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName, Int64 aValue);
		void BindNullByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName);
		void BindBlobByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName,
							[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] Byte[] aValue,
							UInt32 aValueSize);

		/**
		 * Binds aValue to the parameter with the index aIndex.
		 *
		 * @param aIndex
		 *        The zero-based index of the parameter to bind aValue to.
		 * @param aValue
		 *        The value to bind.
		 */
		void BindByIndex(UInt32 aIndex, nsIVariant aValue);
		void BindUTF8StringByIndex(UInt32 aIndex, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aValue);
		void BindStringByIndex(UInt32 aIndex, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue);
		void BindDoubleByIndex(UInt32 aIndex, Double aValue);
		void BindInt32ByIndex(UInt32 aIndex, Int32 aValue);
		void BindInt64ByIndex(UInt32 aIndex, Int64 aValue);
		void BindNullByIndex(UInt32 aIndex);
		void BindBlobByIndex(UInt32 aIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] Byte[] aValue, UInt32 aValueSize);
	}
}
