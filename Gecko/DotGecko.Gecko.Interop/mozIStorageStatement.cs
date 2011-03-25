using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	public static class mozIStorageStatementConstants
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

	//public static class mozIStorageStatementExtensions
	//{
	//    /**
	//     * Getters for native code that return their values as
	//     * the return type, for convenience and sanity.
	//     *
	//     * Not virtual; no vtable bloat.
	//     */
	//    public static Int32 AsInt32(this mozIStorageStatement storageStatement, UInt32 idx)
	//    {
	//        Int32 v = 0;
	//        nsresult rv = storageStatement.GetInt32(idx, &v);
	//        NS_ABORT_IF_FALSE(NS_SUCCEEDED(rv) || IsNull(idx), "Getting value failed, wrong column index?");
	//        return v;
	//    }

	//    public static Int64 AsInt64(this mozIStorageStatement storageStatement, UInt32 idx)
	//    {
	//        Int64 v = 0;
	//        nsresult rv = storageStatement.GetInt64(idx, &v);
	//        NS_ABORT_IF_FALSE(NS_SUCCEEDED(rv) || IsNull(idx), "Getting value failed, wrong column index?");
	//        return v;
	//    }

	//    public static Double AsDouble(this mozIStorageStatement storageStatement, UInt32 idx)
	//    {
	//        Double v = 0.0;
	//        nsresult rv = storageStatement.GetDouble(idx, &v);
	//        NS_ABORT_IF_FALSE(NS_SUCCEEDED(rv) || IsNull(idx), "Getting value failed, wrong column index?");
	//        return v;
	//    }

	//    public static String AsSharedUTF8String(this mozIStorageStatement storageStatement, UInt32 idx, out UInt32 len)
	//    {
	//        String str = null;
	//        len = 0;
	//        nsresult rv = storageStatement.GetSharedUTF8String(idx, len, &str);
	//        NS_ABORT_IF_FALSE(NS_SUCCEEDED(rv) || IsNull(idx), "Getting value failed, wrong column index?");
	//        return str;
	//    }

	//    public static String AsSharedWString(this mozIStorageStatement storageStatement, UInt32 idx, out UInt32 len)
	//    {
	//        String str = null;
	//        len = 0;
	//        nsresult rv = storageStatement.GetSharedString(idx, len, &str);
	//        NS_ABORT_IF_FALSE(NS_SUCCEEDED(rv) || IsNull(idx), "Getting value failed, wrong column index?");
	//        return str;
	//    }

	//    public static Byte[] AsSharedBlob(this mozIStorageStatement storageStatement, UInt32 idx, out UInt32 len)
	//    {
	//        Byte[] blob = null;
	//        len = 0;
	//        nsresult rv = storageStatement.GetSharedBlob(idx, len, &blob);
	//        NS_ABORT_IF_FALSE(NS_SUCCEEDED(rv) || IsNull(idx), "Getting value failed, wrong column index?");
	//        return blob;
	//    }

	//    public static Boolean IsNull(this mozIStorageStatement storageStatement, UInt32 idx)
	//    {
	//        Boolean b = false;
	//        nsresult rv = storageStatement.GetIsNull(idx, &b);
	//        NS_ABORT_IF_FALSE(NS_SUCCEEDED(rv), "Getting value failed, wrong column index?");
	//        return b;
	//    }
	//}

	/**
	 * A SQL statement that can be used for both synchronous and asynchronous
	 * purposes.
	 */
	[ComImport, Guid("57ec7be1-36cf-4510-b938-7d1c9ee8cec5"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageStatement : mozIStorageBaseStatement
	{
		#region mozIStorageBindingParams Members

		new void BindByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName, nsIVariant aValue);
		new void BindUTF8StringByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName,
								  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aValue);
		new void BindStringByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName,
							  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue);
		new void BindDoubleByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName, Double aValue);
		new void BindInt32ByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName, Int32 aValue);
		new void BindInt64ByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName, Int64 aValue);
		new void BindNullByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName);
		new void BindBlobByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName,
							[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] Byte[] aValue,
							UInt32 aValueSize);
		new void BindByIndex(UInt32 aIndex, nsIVariant aValue);
		new void BindUTF8StringByIndex(UInt32 aIndex, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aValue);
		new void BindStringByIndex(UInt32 aIndex, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue);
		new void BindDoubleByIndex(UInt32 aIndex, Double aValue);
		new void BindInt32ByIndex(UInt32 aIndex, Int32 aValue);
		new void BindInt64ByIndex(UInt32 aIndex, Int64 aValue);
		new void BindNullByIndex(UInt32 aIndex);
		new void BindBlobByIndex(UInt32 aIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] Byte[] aValue, UInt32 aValueSize);

		#endregion

		#region mozIStorageBaseStatement Members

		new void DoFinalize();
		[Obsolete] new void BindUTF8StringParameter(UInt32 aParamIndex, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aValue);
		[Obsolete] new void BindStringParameter(UInt32 aParamIndex, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue);
		[Obsolete] new void BindDoubleParameter(UInt32 aParamIndex, Double aValue);
		[Obsolete] new void BindInt32Parameter(UInt32 aParamIndex, Int32 aValue);
		[Obsolete] new void BindInt64Parameter(UInt32 aParamIndex, Int64 aValue);
		[Obsolete] new void BindNullParameter(UInt32 aParamIndex);
		[Obsolete] new void BindBlobParameter(UInt32 aParamIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] Byte[] aValue, UInt32 aValueSize);
		new void BindParameters(mozIStorageBindingParamsArray aParameters);
		new mozIStorageBindingParamsArray NewBindingParamsArray();
		new mozIStoragePendingStatement ExecuteAsync([Optional] mozIStorageStatementCallback aCallback);
		new Int32 State { get; }
		new void EscapeStringForLIKE(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue,
			Char aEscapeChar,
			[In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		#endregion

		/**
		 * Create a clone of this statement, by initializing a new statement
		 * with the same connection and same SQL statement as this one.  It
		 * does not preserve statement state; that is, if a statement is
		 * being executed when it is cloned, the new statement will not be
		 * executing.
		 */
		mozIStorageStatement Clone();

		/*
		 * Number of parameters
		 */
		UInt32 ParameterCount { get; }

		/**
		 * Name of nth parameter, if given
		 */
		void GetParameterName(UInt32 aParamIndex, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * Returns the index of the named parameter.
		 *
		 * @param aName
		 *        The name of the parameter you want the index for.  This does not
		 *        include the leading ':'.
		 * @return the index of the named parameter.
		 */
		UInt32 GetParameterIndex([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName);

		/**
		 * Number of columns returned
		 */
		UInt32 ColumnCount { get; }

		/**
		 * Name of nth column
		 */
		void GetColumnName(UInt32 aColumnIndex, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * Obtains the index of the column with the specified name.
		 *
		 * @param aName
		 *        The name of the column.
		 * @return The index of the column with the specified name.
		 */
		UInt32 GetColumnIndex([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName);

		/**
		 * Obtains the declared column type of a prepared statement.
		 *
		 * @param aParamIndex
		 *        The zero-based index of the column who's declared type we are
		 *        interested in.
		 * @return the declared index type.
		 */
		void GetColumnDecltype(UInt32 aParamIndex, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * Reset parameters/statement execution
		 */
		void Reset();

		/**
		 * Execute the query, ignoring any results.  This is accomplished by
		 * calling executeStep() once, and then calling reset().
		 *
		 * Error and last insert info, etc. are available from
		 * the mozStorageConnection.
		 */
		void Execute();

		/**
		 * Execute a query, using any currently-bound parameters.  Reset
		 * must be called on the statement after the last call of
		 * executeStep.
		 *
		 * @return a boolean indicating whether there are more rows or not;
		 *         row data may be accessed using mozIStorageValueArray methods on
		 *         the statement.

		 */
		Boolean ExecuteStep();

		/**
		 * Execute a query, using any currently-bound parameters.  Reset is called
		 * when no more data is returned.  This method is only available to JavaScript
		 * consumers.
		 *
		 * @deprecated As of Mozilla 1.9.2 in favor of executeStep().
		 *
		 * @return a boolean indicating whether there are more rows or not.
		 *
		 * [deprecated] boolean step();
		 */

		/**
		 * Obtains the current list of named parameters, which are settable.  This
		 * property is only available to JavaScript consumers.
		 *
		 * readonly attribute mozIStorageStatementParams params;
		 */

		/**
		 * Obtains the current row, with access to all the data members by name.  This
		 * property is only available to JavaScript consumers.
		 *
		 * readonly attribute mozIStorageStatementRow row;
		 */

		//////////////////////////////////////////////////////////////////////////////
		//// Copied contents of mozIStorageValueArray

		/**
		 * The number of entries in the array (each corresponding to a column in the
		 * database row)
		 */
		UInt32 NumEntries { get; }

		/**
		 * Indicate the data type of the current result row for the the given column.
		 * SQLite will perform type conversion if you ask for a value as a different
		 * type than it is stored as.
		 *
		 * @param aIndex
		 *        0-based column index.
		 * @return The type of the value at the given column index; one of
		 *         VALUE_TYPE_NULL, VALUE_TYPE_INTEGER, VALUE_TYPE_FLOAT,
		 *         VALUE_TYPE_TEXT, VALUE_TYPE_BLOB.
		 */
		Int32 GetTypeOfIndex(UInt32 aIndex);

		/**
		 * Retrieve the contents of a column from the current result row as an
		 * integer.
		 *
		 * @param aIndex
		 *        0-based colummn index.
		 * @return Column value interpreted as an integer per type conversion rules.
		 * @{
		 */
		Int32 GetInt32(UInt32 aIndex);
		Int64 GetInt64(UInt32 aIndex);
		/** @} */
		/**
		 * Retrieve the contents of a column from the current result row as a
		 * floating point Double.
		 *
		 * @param aIndex
		 *        0-based colummn index.
		 * @return Column value interpreted as a Double per type conversion rules.
		 */
		Double GetDouble(UInt32 aIndex);
		/**
		 * Retrieve the contents of a column from the current result row as a
		 * string.
		 *
		 * @param aIndex
		 *        0-based colummn index.
		 * @return The value for the result column interpreted as a string.  If the
		 *         stored value was NULL, you will get an empty string with IsVoid set
		 *         to distinguish it from an explicitly set empty string.
		 * @{
		 */
		void GetUTF8String(UInt32 aIndex, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void GetString(UInt32 aIndex, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		/** @} */

		/**
		 * Retrieve the contents of a column from the current result row as a
		 * blob.
		 *
		 * @param aIndex
		 *        0-based colummn index.
		 * @param[out] aDataSize
		 *             The number of bytes in the blob.
		 * @param[out] aData
		 *             The contents of the BLOB.  This will be NULL if aDataSize == 0.
		 */
		void GetBlob(UInt32 aIndex, out UInt32 aDataSize, [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] out Byte[] aData);

		/**
		 * Check whether the given column in the current result row is NULL.
		 *
		 * @param aIndex
		 *        0-based colummn index.
		 * @return true if the value for the result column is null.
		 */
		Boolean GetIsNull(UInt32 aIndex);

		/**
		 * Returns a shared string pointer
		 */
		[return: MarshalAs(UnmanagedType.LPStr)]
		String GetSharedUTF8String(UInt32 aIndex, out UInt32 aLength);
		[return: MarshalAs(UnmanagedType.LPWStr)]
		String GetSharedString(UInt32 aIndex, out UInt32 aLength);
		[return: MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)]
		Byte[] GetSharedBlob(UInt32 aIndex, out UInt32 aLength);
	}
}
