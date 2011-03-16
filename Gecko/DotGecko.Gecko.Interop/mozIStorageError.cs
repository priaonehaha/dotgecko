using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	public static class mozIStorageErrorConstants
	{
		/**
		 * General SQL error or missing database.
		 */
		public const Int32 ERROR = 1;

		/**
		 * Internal logic error.
		 */
		public const Int32 INTERNAL = 2;

		/**
		 * Access permission denied.
		 */
		public const Int32 PERM = 3;

		/**
		 * A callback routine requested an abort.
		 */
		public const Int32 ABORT = 4;

		/**
		 * The database file is locked.
		 */
		public const Int32 BUSY = 5;

		/**
		 * A table in the database is locked.
		 */
		public const Int32 LOCKED = 6;

		/**
		 * An allocation failed.
		 */
		public const Int32 NOMEM = 7;

		/**
		 * Attempt to write to a readonly database.
		 */
		public const Int32 READONLY = 8;

		/**
		 * Operation was terminated by an interrupt.
		 */
		public const Int32 INTERRUPT = 9;

		/**
		 * Some kind of disk I/O error occurred.
		 */
		public const Int32 IOERR = 10;

		/**
		 * The database disk image is malformed.
		 */
		public const Int32 CORRUPT = 11;

		/**
		 * An insertion failed because the database is full.
		 */
		public const Int32 FULL = 13;

		/**
		 * Unable to open the database file.
		 */
		public const Int32 CANTOPEN = 14;

		/**
		 * The database is empty.
		 */
		public const Int32 EMPTY = 16;

		/**
		 * The database scheme changed.
		 */
		public const Int32 SCHEMA = 17;

		/**
		 * A string or blob exceeds the size limit.
		 */
		public const Int32 TOOBIG = 18;

		/**
		 * Abort due to a constraint violation.
		 */
		public const Int32 CONSTRAINT = 19;

		/**
		 * Data type mismatch.
		 */
		public const Int32 MISMATCH = 20;

		/**
		 * Library used incorrectly.
		 */
		public const Int32 MISUSE = 21;

		/**
		 * Uses OS features not supported on the host system.
		 */
		public const Int32 NOLFS = 22;

		/**
		 * Authorization denied.
		 */
		public const Int32 AUTH = 23;

		/**
		 * Auxiliary database format error.
		 */
		public const Int32 FORMAT = 24;

		/**
		 * Attempt to bind a parameter using an out-of-range index or nonexistent
		 * named parameter name.
		 */
		public const Int32 RANGE = 25;

		/**
		 * File opened that is not a database file.
		 */
		public const Int32 NOTADB = 26;
	}

	[ComImport, Guid("1f350f96-7023-434a-8864-40a1c493aac1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageError //: nsISupports
	{
		/**
		 * Indicates what type of error occurred.
		 */
		Int32 Result { get; }

		/**
		 * An error string the gives more details, if available.
		 */
		void GetMessage([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
	}
}
