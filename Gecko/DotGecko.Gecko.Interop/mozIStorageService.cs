using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The mozIStorageService interface is intended to be implemented by
	 * a service that can create storage connections (mozIStorageConnection)
	 * to either a well-known profile database or to a specific database file.
	 *
	 * This is the only way to open a database connection.
	 */
	[ComImport, Guid("fe8e95cb-b377-4c8d-bccb-d9198c67542b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageService //: nsISupports
	{
		/**
		 * Get a connection to a named special database storage.
		 *
		 * @param aStorageKey a string key identifying the type of storage
		 * requested.  Valid values include: "profile", "memory".
		 *
		 * @see openDatabase for restrictions on how database connections may be
		 * used. For the profile database, you should only access it from the main
		 * thread since other callers may also have connections.
		 *
		 * @returns a new mozIStorageConnection for the requested
		 * storage database.
		 *
		 * @throws NS_ERROR_INVALID_ARG if aStorageKey is invalid.
		 */
		mozIStorageConnection OpenSpecialDatabase([MarshalAs(UnmanagedType.LPStr)] String aStorageKey);

		/**
		 * Open a connection to the specified file.
		 *
		 * Consumers should check mozIStorageConnection::connectionReady to ensure
		 * that they can use the database.  If this value is false, it is strongly
		 * recommended that the database be backed up with
		 * mozIStorageConnection::backupDB so user data is not lost.
		 *
		 * ==========
		 *   DANGER
		 * ==========
		 *
		 * If you have more than one connection to a file, you MUST use the EXACT
		 * SAME NAME for the file each time, including case. The sqlite code uses
		 * a simple string compare to see if there is already a connection. Opening
		 * a connection to "Foo.sqlite" and "foo.sqlite" will CORRUPT YOUR DATABASE.
		 *
		 * The connection object returned by this function is not threadsafe. You must
		 * use it only from the thread you created it from.
		 *
		 * If your database contains virtual tables (f.e. for full-text indexes), you
		 * must open it with openUnsharedDatabase, as those tables are incompatible
		 * with a shared cache.  If you attempt to use this method to open a database
		 * containing virtual tables, it will think the database is corrupted and
		 * throw NS_ERROR_FILE_CORRUPTED.
		 *
		 * @param aDatabaseFile
		 *        A nsIFile that represents the database that is to be opened..
		 *
		 * @returns a mozIStorageConnection for the requested database file.
		 *
		 * @throws NS_ERROR_OUT_OF_MEMORY
		 *         If allocating a new storage object fails.
		 * @throws NS_ERROR_FILE_CORRUPTED
		 *         If the database file is corrupted.
		 */
		mozIStorageConnection OpenDatabase(nsIFile aDatabaseFile);

		/**
		 * Open a connection to the specified file that doesn't share a sqlite cache.
		 *
		 * Each connection uses its own sqlite cache, which is inefficient, so you
		 * should use openDatabase instead of this method unless you need a feature
		 * of SQLite that is incompatible with a shared cache, like virtual table
		 * and full text indexing support. If cache contention is expected, for
		 * instance when operating on a database from multiple threads, using
		 * unshared connections may be a performance win.
		 *
		 * ==========
		 *   DANGER
		 * ==========
		 *
		 * If you have more than one connection to a file, you MUST use the EXACT
		 * SAME NAME for the file each time, including case. The sqlite code uses
		 * a simple string compare to see if there is already a connection. Opening
		 * a connection to "Foo.sqlite" and "foo.sqlite" will CORRUPT YOUR DATABASE.
		 *
		 * The connection object returned by this function is not threadsafe. You must
		 * use it only from the thread you created it from.
		 *
		 * @param aDatabaseFile
		 *        A nsIFile that represents the database that is to be opened..
		 *
		 * @returns a mozIStorageConnection for the requested database file.
		 *
		 * @throws NS_ERROR_OUT_OF_MEMORY
		 *         If allocating a new storage object fails.
		 * @throws NS_ERROR_FILE_CORRUPTED
		 *         If the database file is corrupted.
		 */
		mozIStorageConnection OpenUnsharedDatabase(nsIFile aDatabaseFile);

		/*
		 * Utilities
		 */

		/**
		 * Copies the specified database file to the specified parent directory with
		 * the specified file name.  If the parent directory is not specified, it
		 * places the backup in the same directory as the current file.  This function
		 * ensures that the file being created is unique.
		 *
		 * @param aDBFile
		 *        The database file that will be backed up.
		 * @param aBackupFileName
		 *        The name of the new backup file to create.
		 * @param [optional] aBackupParentDirectory
		 *        The directory you'd like the backup file to be placed.
		 * @return The nsIFile representing the backup file.
		 */
		nsIFile BackupDatabaseFile(
			nsIFile aDBFile,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aBackupFileName,
			[Optional] nsIFile aBackupParentDirectory);
	}
}
