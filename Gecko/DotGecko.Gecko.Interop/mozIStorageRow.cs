using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("62d1b6bd-cbfe-4f9b-aee1-0ead4af4e6dc"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageRow : mozIStorageValueArray
	{
		#region mozIStorageValueArray Members

		new UInt32 NumEntries { get; }
		new Int32 GetTypeOfIndex(UInt32 aIndex);
		new Int32 GetInt32(UInt32 aIndex);
		new Int64 GetInt64(UInt32 aIndex);
		new Double GetDouble(UInt32 aIndex);
		new void GetUTF8String(UInt32 aIndex, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new void GetString(UInt32 aIndex, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		new void GetBlob(UInt32 aIndex, out UInt32 aDataSize, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] out Byte[] aData);
		new Boolean GetIsNull(UInt32 aIndex);
		[return: MarshalAs(UnmanagedType.LPStr)]
		new String GetSharedUTF8String(UInt32 aIndex, out UInt32 aLength);
		[return: MarshalAs(UnmanagedType.LPWStr)]
		new String GetSharedString(UInt32 aIndex, out UInt32 aLength);
		new IntPtr GetSharedBlob(UInt32 aIndex, out UInt32 aLength);

		#endregion

		/**
		 * Obtains the result of a given column specified by aIndex.
		 *
		 * @param aIndex
		 *        Zero-based index of the result to get from the tuple.
		 * @returns the result of the specified column.
		 */
		nsIVariant GetResultByIndex(UInt32 aIndex);

		/**
		 * Obtains the result of a given column specified by aIndex.
		 *
		 * @param aName
		 *        Name of the result to get from the tuple.
		 * @returns the result of the specified column.
		 */
		nsIVariant GetResultByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName);
	}
}
