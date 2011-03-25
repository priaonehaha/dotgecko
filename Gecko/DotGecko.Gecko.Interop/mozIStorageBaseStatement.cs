using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	public static class mozIStorageBaseStatementConstants
	{
		/**
		 * The statement is not usable, either because it failed to initialize or
		 * was explicitly finalized.
		 */
		public const Int32 MOZ_STORAGE_STATEMENT_INVALID = 0;
		/**
		 * The statement is usable.
		 */
		public const Int32 MOZ_STORAGE_STATEMENT_READY = 1;
		/**
		 * Indicates that the statement is executing and the row getters may be used.
		 *
		 * @note This is only relevant for mozIStorageStatement instances being used
		 *       in a synchronous fashion.
		 */
		public const Int32 MOZ_STORAGE_STATEMENT_EXECUTING = 2;
	}

	/**
	 * The base interface for both pure asynchronous storage statements 
	 * (mozIStorageAsyncStatement) and 'classic' storage statements
	 * (mozIStorageStatement) that can be used for both synchronous and asynchronous
	 * purposes.
	 */
	[ComImport, Guid("da2ec336-fbbb-4ba1-9778-8c9825980d01"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageBaseStatement : mozIStorageBindingParams
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

		/**
		 * Finalizes a statement so you can successfully close a database connection.
		 * Once a statement has been finalized it can no longer be used for any
		 * purpose.
		 * 
		 * Statements are implicitly finalized when their reference counts hits zero.
		 * If you are a native (C++) caller this is accomplished by setting all of
		 * your nsCOMPtr instances to be NULL.  If you are operating from JavaScript
		 * code then you cannot rely on this behavior because of the involvement of
		 * garbage collection.
		 *
		 * When finalizing an asynchronous statement you do not need to worry about
		 * whether the statement has actually been executed by the asynchronous
		 * thread; you just need to call finalize after your last call to executeAsync
		 * involving the statement.  However, you do need to use asyncClose instead of
		 * close on the connection if any statements have been used asynchronously.
		 */
		void DoFinalize();

		/**
		 * Bind the given value at the given numeric index.
		 *
		 * @param aParamIndex
		 *        0-based index, 0 corresponding to the first numbered argument or
		 *        "?1".
		 * @param aValue
		 *        Argument value.
		 * @param aValueSize
		 *        Length of aValue in bytes.
		 * @{
		 */
		[Obsolete] void BindUTF8StringParameter(UInt32 aParamIndex, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aValue);
		[Obsolete] void BindStringParameter(UInt32 aParamIndex, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue);
		[Obsolete] void BindDoubleParameter(UInt32 aParamIndex, Double aValue);
		[Obsolete] void BindInt32Parameter(UInt32 aParamIndex, Int32 aValue);
		[Obsolete] void BindInt64Parameter(UInt32 aParamIndex, Int64 aValue);
		[Obsolete] void BindNullParameter(UInt32 aParamIndex);
		[Obsolete] void BindBlobParameter(UInt32 aParamIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] Byte[] aValue, UInt32 aValueSize);
		/**@}*/

		/**
		 * Binds the array of parameters to the statement.  When executeAsync is
		 * called, all the parameters in aParameters are bound and then executed.
		 *
		 * @param aParameters
		 *        The array of parameters to bind to the statement upon execution.
		 *
		 * @note This is only works on statements being used asynchronously.
		 */
		void BindParameters(mozIStorageBindingParamsArray aParameters);

		/**
		 * Creates a new mozIStorageBindingParamsArray that can be used to bind
		 * multiple sets of data to a statement with bindParameters.
		 *
		 * @return a mozIStorageBindingParamsArray that multiple sets of parameters
		 *         can be bound to.
		 *
		 * @note This is only useful for statements being used asynchronously.
		 */
		mozIStorageBindingParamsArray NewBindingParamsArray();

		/**
		 * Execute a query asynchronously using any currently bound parameters.  This
		 * statement can be reused immediately, and reset does not need to be called.
		 *
		 * @note If you have any custom defined functions, they must be re-entrant
		 *       since they can be called on multiple threads.
		 *
		 * @param aCallback [optional]
		 *        The callback object that will be notified of progress, errors, and
		 *        completion.
		 * @return an object that can be used to cancel the statements execution.
		 */
		mozIStoragePendingStatement ExecuteAsync([Optional] mozIStorageStatementCallback aCallback);

		/**
		 * Find out whether the statement is usable (has not been finalized).
		 */
		Int32 State { get; }

		/**
		 * Escape a string for SQL LIKE search.
		 *
		 * @note Consumers will have to use same escape char when doing statements
		 *       such as:   ...LIKE '?1' ESCAPE '/'...
		 *
		 * @param aValue
		 *        The string to escape for SQL LIKE.
		 * @param aEscapeChar
		 *        The escape character.
		 * @return an AString of an escaped version of aValue
		 *         (%, _ and the escape char are escaped with the escape char)
		 *         For example, we will convert "foo/bar_baz%20cheese" 
		 *         into "foo//bar/_baz/%20cheese" (if the escape char is '/').
		 */
		void EscapeStringForLIKE(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue,
			Char aEscapeChar,
			[In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
	}
}
