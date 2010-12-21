using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class mozIStorageStatementCallbackConstants
	{
		public const UInt16 REASON_FINISHED = 0;
		public const UInt16 REASON_CANCELED = 1;
		public const UInt16 REASON_ERROR = 2;
	}

	[ComImport, Guid("29383d00-d8c4-4ddd-9f8b-c2feb0f2fcfa"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageStatementCallback //: nsISupports
	{
		/**
		 * Called when some result is obtained from the database.  This function can
		 * be called more than once with a different storageIResultSet each time for
		 * any given asynchronous statement.
		 *
		 * @param aResultSet
		 *        The result set containing the data from the database.
		 */
		void HandleResult(mozIStorageResultSet aResultSet);

		/**
		 * Called when some error occurs while executing the statement.  This function
		 * may be called more than once with a different storageIError each time for
		 * any given asynchronous statement.
		 *
		 * @param aError
		 *        An object containing information about the error.
		 */
		void HandleError(mozIStorageError aError);

		/**
		 * Called when the statement has finished executing.  This function will only
		 * be called once for any given asynchronous statement.
		 *
		 * @param aReason
		 *        Indicates if the statement is no longer executing because it either
		 *        finished (REASON_FINISHED), was canceled (REASON_CANCELED), or
		 *        a fatal error occurred (REASON_ERROR).
		 */

		void HandleCompletion(UInt16 aReason);
	}
}
