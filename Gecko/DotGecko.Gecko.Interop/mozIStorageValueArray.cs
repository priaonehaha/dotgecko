using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	public static class mozIStorageValueArrayConstants
	{
		/**
		 * These type values are returned by getTypeOfIndex
		 * to indicate what type of value is present at
		 * a given column.
		 */
		public const Int32 VALUE_TYPE_NULL = 0;
		public const Int32 VALUE_TYPE_INTEGER = 1;
		public const Int32 VALUE_TYPE_FLOAT = 2;
		public const Int32 VALUE_TYPE_TEXT = 3;
		public const Int32 VALUE_TYPE_BLOB = 4;
	}

	/**
	 * mozIStorageValueArray wraps an array of SQL values,
	 * such as a single database row.
	 */
	[ComImport, Guid("07b5b93e-113c-4150-863c-d247b003a55d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageValueArray //: nsISupports
	{
		/**
		 * numEntries
		 *
		 * number of entries in the array (each corresponding to a column
		 * in the database row)
		 */
		UInt32 NumEntries { get; }

		/**
		 * Returns the type of the value at the given column index;
		 * one of VALUE_TYPE_NULL, VALUE_TYPE_INTEGER, VALUE_TYPE_FLOAT,
		 * VALUE_TYPE_TEXT, VALUE_TYPE_BLOB.
		 */
		Int32 GetTypeOfIndex(UInt32 aIndex);

		/**
		 * Obtain a value for the given entry (column) index.
		 * Due to SQLite's type conversion rules, any of these are valid
		 * for any column regardless of the column's data type.  However,
		 * if the specific type matters, getTypeOfIndex should be used
		 * first to identify the column type, and then the appropriate
		 * get method should be called.
		 *
		 * If you ask for a string value for a NULL column, you will get an empty
		 * string with IsVoid set to distinguish it from an explicitly set empty
		 * string.
		 */
		Int32 GetInt32(UInt32 aIndex);
		Int64 GetInt64(UInt32 aIndex);
		Double GetDouble(UInt32 aIndex);
		void GetUTF8String(UInt32 aIndex, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void GetString(UInt32 aIndex, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		// data will be NULL if dataSize = 0
		void GetBlob(UInt32 aIndex, out UInt32 aDataSize, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] out Byte[] aData);
		Boolean GetIsNull(UInt32 aIndex);

		/**
		 * Returns a shared string pointer
		 */
		[return: MarshalAs(UnmanagedType.LPStr)]
		String GetSharedUTF8String(UInt32 aIndex, out UInt32 aLength);
		[return: MarshalAs(UnmanagedType.LPWStr)]
		String GetSharedString(UInt32 aIndex, out UInt32 aLength);
		IntPtr GetSharedBlob(UInt32 aIndex, out UInt32 aLength);
	}
}
