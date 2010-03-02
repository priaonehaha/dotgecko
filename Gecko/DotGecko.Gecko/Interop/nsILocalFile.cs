using System;
using System.Runtime.InteropServices;
using PRFileDescStar = System.IntPtr;
using PRLibraryStar = System.IntPtr;
using FILE = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	//[ptr] native PRFileDescStar(PRFileDesc);
	//[ptr] native PRLibraryStar(PRLibrary);
	//[ptr] native FILE(FILE);

	// Constants for nsILocalFile ( "aa610f20-a889-11d3-8c81-000064657374" ) interface
	internal static class nsILocalFileConstants
	{
		internal const UInt32 DELETE_ON_CLOSE = 0x80000000;
	}

	/**
	 * This interface adds methods to nsIFile that are particular to a file
	 * that is accessible via the local file system.
	 *
	 * It follows the same string conventions as nsIFile.
	 *
	 * @status FROZEN
	 */
	[ComImport]
	[Guid("aa610f20-a889-11d3-8c81-000064657374")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsILocalFile : nsIFile
	{
		#region nsIFile Members

		new void Append(nsAString node);
		new void AppendNative(nsACString node);
		new void Normalize();
		new void Create(UInt32 type, UInt32 permissions);
		new void GetLeafName(nsAString result);
		new void SetLeafName(nsAString value);
		new void GetNativeLeafName(nsACString result);
		new void SetNativeLeafName(nsACString value);
		new void CopyTo(nsIFile newParentDir, nsAString newName);
		new void CopyToNative(nsIFile newParentDir, nsACString newName);
		new void CopyToFollowingLinks(nsIFile newParentDir, nsAString newName);
		new void CopyToFollowingLinksNative(nsIFile newParentDir, nsACString newName);
		new void MoveTo(nsIFile newParentDir, nsAString newName);
		new void MoveToNative(nsIFile newParentDir, nsACString newName);
		new void Remove(Boolean recursive);
		new UInt32 GetPermissions();
		new void SetPermissions(UInt32 value);
		new UInt32 GetPermissionsOfLink();
		new void SetPermissionsOfLink(UInt32 value);
		new Int64 GetLastModifiedTime();
		new void SetLastModifiedTime(Int64 value);
		new Int64 GetLastModifiedTimeOfLink();
		new void SetLastModifiedTimeOfLink(Int64 value);
		new Int64 GetFileSize();
		new void SetFileSize(Int64 value);
		new Int64 GetFileSizeOfLink();
		new void GetTarget(nsAString result);
		new void GetNativeTarget(nsACString result);
		new void GetPath(nsAString result);
		new void GetNativePath(nsACString result);
		new Boolean Exists();
		new Boolean IsWritable();
		new Boolean IsReadable();
		new Boolean IsExecutable();
		new Boolean IsHidden();
		new Boolean IsDirectory();
		new Boolean IsFile();
		new Boolean IsSymlink();
		new Boolean IsSpecial();
		new void CreateUnique(UInt32 type, UInt32 permissions);
		new nsIFile Clone();
		new Boolean Equals(nsIFile inFile);
		new Boolean Contains(nsIFile inFile, Boolean recur);
		new nsIFile GetParent();
		new nsISimpleEnumerator GetDirectoryEntries();

		#endregion

		/**
		 *  initWith[Native]Path
		 *
		 *  This function will initialize the nsILocalFile object.  Any
		 *  internal state information will be reset.  
		 *
		 *   @param filePath       
		 *       A string which specifies a full file path to a 
		 *       location.  Relative paths will be treated as an
		 *       error (NS_ERROR_FILE_UNRECOGNIZED_PATH).  For 
		 *       initWithNativePath, the filePath must be in the native
		 *       filesystem charset.
		 */
		void InitWithPath(nsAString filePath);

		void InitWithNativePath(nsACString filePath);

		/**
		 *  initWithFile
		 *
		 *  Initialize this object with another file
		 *
		 *   @param aFile
		 *       the file this becomes equivalent to
		 */
		void InitWithFile(nsILocalFile aFile);

		/**
		 *  followLinks
		 *
		 *  This attribute will determine if the nsLocalFile will auto
		 *  resolve symbolic links.  By default, this value will be false
		 *  on all non unix systems.  On unix, this attribute is effectively
		 *  a noop.  
		 */
		Boolean GetFollowLinks();
		void SetFollowLinks(Boolean value);

		/**
		 * Return the result of PR_Open on the file.  The caller is
		 * responsible for calling PR_Close on the result.
		 *
		 * @param flags the PR_Open flags from prio.h, plus optionally
		 * DELETE_ON_CLOSE. DELETE_ON_CLOSE may be implemented by removing
		 * the file (by path name) immediately after opening it, so beware
		 * of possible races; the file should be exclusively owned by this
		 * process.
		 */
		PRFileDescStar OpenNSPRFileDesc(Int32 flags, Int32 mode);

		/**
		 * Return the result of fopen on the file.  The caller is
		 * responsible for calling fclose on the result.
		 */
		FILE OpenANSIFileDesc([MarshalAs(UnmanagedType.LPStr)] String mode);

		/**
		 * Return the result of PR_LoadLibrary on the file.  The caller is
		 * responsible for calling PR_UnloadLibrary on the result.
		 */
		PRLibraryStar Load();

		// number of bytes available on disk to non-superuser
		Int64 GetDiskSpaceAvailable();

		/**
		 *  appendRelative[Native]Path
		 *
		 *  Append a relative path to the current path of the nsILocalFile object.
		 *
		 *   @param relativeFilePath
		 *       relativeFilePath is a native relative path. For security reasons,
		 *       this cannot contain .. or cannot start with a directory separator.
		 *       For the |appendRelativeNativePath| method, the relativeFilePath 
		 *       must be in the native filesystem charset.
		 */
		void AppendRelativePath(nsAString relativeFilePath);

		void AppendRelativeNativePath(nsACString relativeFilePath);

		/**
		 *  Accessor to a null terminated string which will specify
		 *  the file in a persistent manner for disk storage.
		 *
		 *  The character set of this attribute is undefined.  DO NOT TRY TO
		 *  INTERPRET IT AS HUMAN READABLE TEXT!
		 */
		void GetPersistentDescriptor(nsACString result);
		void SetPersistentDescriptor(nsACString value);

		/** 
		 *  reveal
		 *
		 *  Ask the operating system to open the folder which contains
		 *  this file or folder. This routine only works on platforms which 
		 *  support the ability to open a folder...
		 */
		void Reveal();

		/** 
		 *  launch
		 *
		 *  Ask the operating system to attempt to open the file. 
		 *  this really just simulates "double clicking" the file on your platform.
		 *  This routine only works on platforms which support this functionality.
		 */
		void Launch();

		/**
		 *  getRelativeDescriptor
		 *
		 *  Returns a relative file path in an opaque, XP format. It is therefore
		 *  not a native path.
		 *
		 *  The character set of the string returned from this function is
		 *  undefined.  DO NOT TRY TO INTERPRET IT AS HUMAN READABLE TEXT!
		 *
		 *   @param fromFile
		 *       the file from which the descriptor is relative.
		 *       There is no defined result if this param is null.
		 */
		void GetRelativeDescriptor(nsILocalFile fromFile, nsACString result);

		/**
		 *  setRelativeDescriptor
		 *
		 *  Initializes the file to the location relative to fromFile using
		 *  a string returned by getRelativeDescriptor.
		 *
		 *   @param fromFile
		 *       the file to which the descriptor is relative
		 *   @param relative
		 *       the relative descriptor obtained from getRelativeDescriptor
		 */
		void SetRelativeDescriptor(nsILocalFile fromFile, nsACString relativeDesc);
	}
}
