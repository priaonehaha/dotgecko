using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	public static class mozIStorageStatementConstants
	{
		public const Int32 MOZ_STORAGE_STATEMENT_INVALID = 0;
		public const Int32 MOZ_STORAGE_STATEMENT_READY = 1;
		public const Int32 MOZ_STORAGE_STATEMENT_EXECUTING = 2;
	}

	[ComImport, Guid("20c45bdd-51d4-4f07-b70e-5feaa6302197"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageStatement : mozIStorageValueArray
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
		 * Finalizes a statement so you can successfully close a database connection.
		 * This method does not need to be used from native callers since you can just
		 * set the statement to null, but is extremely useful to JS callers.
		 */
		void DoFinalize();

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
		 * @returns the index of the named parameter.
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
		 * @param aName The name of the column.
		 * @return The index of the column with the specified name.
		 */
		UInt32 GetColumnIndex([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName);

		/**
		 * Obtains the declared column type of a prepared statement.
		 *
		 * @param aParamIndex The zero-based index of the column who's declared type
		 *                    we are interested in.
		 * @returns the declared index type.
		 */
		void GetColumnDecltype(UInt32 aParamIndex, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * Reset parameters/statement execution
		 */
		void Reset();

		/**
		 * Bind the given value to the parameter at aParamIndex. Index 0
		 * denotes the first numbered argument or ?1.
		 */
		void BindUTF8StringParameter(UInt32 aParamIndex,
									 [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aValue);
		void BindStringParameter(UInt32 aParamIndex, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue);
		void BindDoubleParameter(UInt32 aParamIndex, Double aValue);
		void BindInt32Parameter(UInt32 aParamIndex, Int32 aValue);
		void BindInt64Parameter(UInt32 aParamIndex, Int64 aValue);
		void BindNullParameter(UInt32 aParamIndex);
		void BindBlobParameter(UInt32 aParamIndex,
							   [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] Byte[] aValue,
							   UInt32 aValueSize);

		/**
		 * Binds the array of parameters to the statement.  When executeAsync is
		 * called, all the parameters in aParameters are bound and then executed.
		 *
		 * @param aParameters
		 *        The array of parameters to bind to the statement upon execution.
		 */
		void BindParameters(mozIStorageBindingParamsArray aParameters);

		/**
		 * Creates a new mozIStorageBindingParamsArray that can be used to bind
		 * multiple sets of data to a statement.
		 *
		 * @returns a mozIStorageBindingParamsArray that multiple sets of parameters
		 *          can be bound to.
		 */
		mozIStorageBindingParamsArray NewBindingParamsArray();

		/**
		 * Execute the query, ignoring any results.  This is accomplished by
		 * calling step() once, and then calling reset().
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
		 * @returns a boolean indicating whether there are more rows or not;
		 * row data may be accessed using mozIStorageValueArray methods on
		 * the statement.
		 *
		 */
		Boolean ExecuteStep();

		/**
		 * Execute a query, using any currently-bound parameters.  Reset is called
		 * when no more data is returned.  This method is only available to JavaScript
		 * consumers.
		 *
		 * @returns a boolean indicating whether there are more rows or not.
		 *
		 * boolean step();
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

		/**
		 * Execute a query asynchronously using any currently bound parameters.  This
		 * statement can be reused immediately, and reset does not need to be called.
		 *
		 * Note:  If you have any custom defined functions, they must be re-entrant
		 *        since they can be called on multiple threads.
		 *
		 * @param aCallback [optional]
		 *        The callback object that will be notified of progress, errors, and
		 *        completion.
		 * @returns an object that can be used to cancel the statements execution.
		 */
		mozIStoragePendingStatement ExecuteAsync([Optional] mozIStorageStatementCallback aCallback);

		/**
		 * The current state.  Row getters are only valid while
		 * the statement is in the "executing" state.
		 */
		Int32 State { get; }

		/**
		 * Escape a string for SQL LIKE search.
		 *
		 * @param     aValue the string to escape for SQL LIKE 
		 * @param     aEscapeChar the escape character
		 * @returns   an AString of an escaped version of aValue
		 *            (%, _ and the escape char are escaped with the escape char)
		 *            For example, we will convert "foo/bar_baz%20cheese" 
		 *            into "foo//bar/_baz/%20cheese" (if the escape char is '/').
		 * @note      consumers will have to use same escape char
		 *            when doing statements such as:   ...LIKE '?1' ESCAPE '/'...
		 */
		void EscapeStringForLIKE([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue, Char aEscapeChar,
								 [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
	}
}
