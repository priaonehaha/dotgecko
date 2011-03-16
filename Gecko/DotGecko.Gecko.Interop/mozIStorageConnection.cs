using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	public static class mozIStorageConnectionConstants
	{
		/**
		 * The default size for SQLite database pages used by mozStorage for new
		 * databases.
		 * This value must stay in sync with the SQLITE_DEFAULT_PAGE_SIZE define in
		 * /db/sqlite3/src/Makefile.in
		 */
		public const Int32 DEFAULT_PAGE_SIZE = 32768;

		public const Int32 TRANSACTION_DEFERRED = 0;
		public const Int32 TRANSACTION_IMMEDIATE = 1;
		public const Int32 TRANSACTION_EXCLUSIVE = 2;
	}

	/**
	 * mozIStorageConnection represents a database connection attached to
	 * a specific file or to the in-memory data storage.  It is the
	 * primary interface for interacting with a database, including
	 * creating prepared statements, executing SQL, and examining database
	 * errors.
	 *
	 * @threadsafe
	 */
	[ComImport, Guid("ad035628-4ffb-42ff-a256-0ed9e410b859"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageConnection //: nsISupports
	{
		/**
		 * Closes a database connection.  Callers must finalize all statements created
		 * for this connection prior to calling this method.  It is illegal to use
		 * call this method if any asynchronous statements have been executed on this
		 * connection.
		 *
		 * @throws NS_ERROR_UNEXPECTED
		 *         If any statement has been executed asynchronously on this object.
		 * @throws NS_ERROR_UNEXPECTED
		 *         If is called on a thread other than the one that opened it.
		 */
		void Close();

		/**
		 * Asynchronously closes a database connection, allowing all pending
		 * asynchronous statements to complete first.
		 *
		 * @param aCallback [optional]
		 *        A callback that will be notified when the close is completed.
		 *
		 * @throws NS_ERROR_UNEXPECTED
		 *         If is called on a thread other than the one that opened it.
		 */
		void AsyncClose([In, Optional] mozIStorageCompletionCallback aCallback);

		/**
		 * Clones a database and makes the clone read only if needed.
		 *
		 * @note If your connection is already read-only, you will get a read-only
		 *       clone.
		 * @note Due to a bug in SQLite, if you use the shared cache (openDatabase),
		 *       you end up with the same privileges as the first connection opened
		 *       regardless of what is specified in aReadOnly.
		 *
		 * @throws NS_ERROR_UNEXPECTED
		 *         If this connection is a memory database.
		 *
		 * @param aReadOnly
		 *        If true, the returned database should be put into read-only mode.
		 *        Defaults to false.
		 * @return the cloned database connection.
		 */
		mozIStorageConnection Clone([In, Optional] Boolean aReadOnly);

		/**
		 * Indicates if the connection is open and ready to use.  This will be false
		 * if the connection failed to open, or it has been closed.
		 */
		Boolean ConnectionReady { get; }

		/**
		 * The current database nsIFile.  Null if the database
		 * connection refers to an in-memory database.
		 */
		nsIFile DatabaseFile { get; }

		/**
		 * lastInsertRowID returns the row ID from the last INSERT
		 * operation.
		 */
		Int64 LastInsertRowID { get; }

		/**
		 * The last error SQLite error code.
		 */
		Int32 LastError { get; }

		/**
		 * The last SQLite error as a string (in english, straight from the
		 * sqlite library).
		 */
		void GetLastErrorString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * The schema version of the database.  This should not be used until the 
		 * database is ready.  The schema will be reported as zero if it is not set.
		 */
		Int32 SchemaVersion { get; set; }

		//////////////////////////////////////////////////////////////////////////////
		//// Statement creation

		/**
		 * Create a mozIStorageStatement for the given SQL expression.  The
		 * expression may use ? to indicate sequential numbered arguments,
		 * ?1, ?2 etc. to indicate specific numbered arguments or :name and 
		 * $var to indicate named arguments.
		 *
		 * @param aSQLStatement
		 *        The SQL statement to execute.
		 * @return a new mozIStorageStatement
		 */
		mozIStorageStatement CreateStatement([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aSQLStatement);

		/**
		 * Create an asynchronous statement (mozIStorageAsyncStatement) for the given
		 * SQL expression.  An asynchronous statement can only be used to dispatch
		 * asynchronous requests to the asynchronous execution thread and cannot be
		 * used to take any synchronous actions on the database.
		 *
		 * The expression may use ? to indicate sequential numbered arguments,
		 * ?1, ?2 etc. to indicate specific numbered arguments or :name and
		 * $var to indicate named arguments.
		 *
		 * @param aSQLStatement
		 *        The SQL statement to execute.
		 * @return a new mozIStorageAsyncStatement
		 */
		mozIStorageAsyncStatement CreateAsyncStatement([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aSQLStatement);

		/**
		 * Execute a SQL expression, expecting no arguments.
		 *
		 * @param aSQLStatement  The SQL statement to execute
		 */
		void ExecuteSimpleSQL([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aSQLStatement);

		/**
		 * Execute an array of queries created with this connection asynchronously
		 * using any currently bound parameters.  The statements are ran wrapped in a
		 * transaction.  These statements can be reused immediately, and reset does
		 * not need to be called.
		 *
		 * Note:  If you have any custom defined functions, they must be re-entrant
		 *        since they can be called on multiple threads.
		 *
		 * @param aStatements
		 *        The array of statements to execute asynchronously, in the order they
		 *        are given in the array.
		 * @param aNumStatements
		 *        The number of statements in aStatements.
		 * @param aCallback [optional]
		 *        The callback object that will be notified of progress, errors, and
		 *        completion.
		 * @return an object that can be used to cancel the statements execution.
		 */
		mozIStoragePendingStatement ExecuteAsync(
		  [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 1)] mozIStorageBaseStatement aStatements,
		  UInt32 aNumStatements,
		  [Optional]  mozIStorageStatementCallback aCallback);

		/**
		 * Check if the given table exists.
		 *
		 * @param aTableName
		 *        The table to check
		 * @return TRUE if table exists, FALSE otherwise.
		 */
		Boolean TableExists([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aTableName);

		/**
		 * Check if the given index exists.
		 *
		 * @param aIndexName   The index to check
		 * @return TRUE if the index exists, FALSE otherwise.
		 */
		Boolean IndexExists([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aIndexName);

		//////////////////////////////////////////////////////////////////////////////
		//// Transactions

		/**
		 * Returns true if a transaction is active on this connection.
		 */
		Boolean TransactionInProgress { get; }

		/**
		 * Begin a new transaction.  sqlite default transactions are deferred.
		 * If a transaction is active, throws an error.
		 */
		void BeginTransaction();

		/**
		 * Begins a new transaction with the given type.
		 */
		void BeginTransactionAs(Int32 transactionType);

		/**
		 * Commits the current transaction.  If no transaction is active,
		 * @throws NS_ERROR_UNEXPECTED.
		 * @throws NS_ERROR_NOT_INITIALIZED.
		 */
		void CommitTransaction();

		/**
		 * Rolls back the current transaction.  If no transaction is active,
		 * @throws NS_ERROR_UNEXPECTED.
		 * @throws NS_ERROR_NOT_INITIALIZED.
		 */
		void RollbackTransaction();

		//////////////////////////////////////////////////////////////////////////////
		//// Tables

		/**
		 * Create the table with the given name and schema.
		 *
		 * If the table already exists, NS_ERROR_FAILURE is thrown.
		 * (XXX at some point in the future it will check if the schema is
		 * the same as what is specified, but that doesn't happen currently.)
		 *
		 * @param aTableName
		 *        The table name to be created, consisting of [A-Za-z0-9_], and
		 *        beginning with a letter.
		 * @param aTableSchema
		 *        The schema of the table; what would normally go between the parens
		 *        in a CREATE TABLE statement: e.g., "foo  INTEGER, bar STRING".
		 *
		 * @throws NS_ERROR_FAILURE
		 *         If the table already exists or could not be created for any other
		 *         reason.
		 */
		void CreateTable([MarshalAs(UnmanagedType.LPStr)] String aTableName,
						 [MarshalAs(UnmanagedType.LPStr)] String aTableSchema);

		//////////////////////////////////////////////////////////////////////////////
		//// Functions

		/**
		 * Create a new SQL function.  If you use your connection on multiple threads,
		 * your function needs to be threadsafe, or it should only be called on one
		 * thread.
		 *
		 * @param aFunctionName
		 *        The name of function to create, as seen in SQL.
		 * @param aNumArguments
		 *        The number of arguments the function takes. Pass -1 for
		 *        variable-argument functions.
		 * @param aFunction
		 *        The instance of mozIStorageFunction, which implements the function
		 *        in question.
		 */
		void CreateFunction([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aFunctionName,
							Int32 aNumArguments,
							mozIStorageFunction aFunction);

		/**
		 * Create a new SQL aggregate function.  If you use your connection on
		 * multiple threads, your function needs to be threadsafe, or it should only
		 * be called on one thread.
		 *
		 * @param aFunctionName
		 *        The name of aggregate function to create, as seen in SQL.
		 * @param aNumArguments
		 *        The number of arguments the function takes. Pass -1 for
		 *        variable-argument functions.
		 * @param aFunction
		 *        The instance of mozIStorageAggreagteFunction, which implements the
		 *        function in question.
		 */
		void CreateAggregateFunction([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aFunctionName,
									 Int32 aNumArguments,
									 mozIStorageAggregateFunction aFunction);
		/**
		 * Delete custom SQL function (simple or aggregate one).
		 *
		 * @param aFunctionName
		 *        The name of function to remove.
		 */
		void RemoveFunction([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aFunctionName);

		/**
		 * Sets a progress handler. Only one handler can be registered at a time.
		 * If you need more than one, you need to chain them yourself.  This progress
		 * handler should be threadsafe if you use this connection object on more than
		 * one thread.
		 *
		 * @param aGranularity
		 *        The number of SQL virtual machine steps between progress handler
		 *        callbacks.
		 * @param aHandler
		 *        The instance of mozIStorageProgressHandler.
		 * @return previous registered handler.
		 */
		mozIStorageProgressHandler SetProgressHandler(Int32 aGranularity, mozIStorageProgressHandler aHandler);

		/**
		 * Remove a progress handler.
		 *
		 * @return previous registered handler.
		 */
		mozIStorageProgressHandler RemoveProgressHandler();

		/**
		 * Controls SQLITE_FCNTL_CHUNK_SIZE setting in sqlite. This helps avoid fragmentation
		 * by growing/shrinking the database file in SQLITE_FCNTL_CHUNK_SIZE increments.
		 *
		 * @param aIncrement
		 *        The database file will grow in multiples of chunkSize.
		 * @param aDatabaseName
		 *        Sqlite database name. "" means pass NULL for zDbName to sqlite3_file_control.
		 *        See http://sqlite.org/c3ref/file_control.html for more details.
		 */
		void SetGrowthIncrement(Int32 aIncrement, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aDatabaseName);
	}
}
