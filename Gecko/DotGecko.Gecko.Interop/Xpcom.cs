using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static partial class Xpcom
	{
		/**
		 * Initialises XPCOM. You must call one of the NS_InitXPCOM methods
		 * before proceeding to use xpcom. The one exception is that you may
		 * call NS_NewLocalFile to create a nsIFile.
		 * 
		 * @status FROZEN
		 *
		 * @note Use <CODE>NS_NewLocalFile</CODE> or <CODE>NS_NewNativeLocalFile</CODE> 
		 *       to create the file object you supply as the bin directory path in this
		 *       call. The function may be safely called before the rest of XPCOM or 
		 *       embedding has been initialised.
		 *
		 * @param result           The service manager.  You may pass null.
		 *
		 * @param binDirectory     The directory containing the component
		 *                         registry and runtime libraries;
		 *                         or use <CODE>nsnull</CODE> to use the working
		 *                         directory.
		 *
		 * @param appFileLocationProvider The object to be used by Gecko that specifies
		 *                         to Gecko where to find profiles, the component
		 *                         registry preferences and so on; or use
		 *                         <CODE>nsnull</CODE> for the default behaviour.
		 *
		 * @see NS_NewLocalFile
		 * @see nsILocalFile
		 * @see nsIDirectoryServiceProvider
		 *
		 * @return NS_OK for success;
		 *         NS_ERROR_NOT_INITIALIZED if static globals were not initialized,
		 *         which can happen if XPCOM is reloaded, but did not completly
		 *         shutdown. Other error codes indicate a failure during
		 *         initialisation.
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		[return: MarshalAs(UnmanagedType.U4)]
		public static extern nsResult NS_InitXPCOM2(out nsIServiceManager result, nsIFile binDirectory, nsIDirectoryServiceProvider appFileLocationProvider);

		/**
		 * Shutdown XPCOM. You must call this method after you are finished
		 * using xpcom. 
		 *
		 * @status FROZEN
		 *
		 * @param servMgr           The service manager which was returned by NS_InitXPCOM.
		 *                          This will release servMgr.  You may pass null.
		 *
		 * @return NS_OK for success;
		 *         other error codes indicate a failure during initialisation.
		 *
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		[return: MarshalAs(UnmanagedType.U4)]
		public static extern nsResult NS_ShutdownXPCOM(nsIServiceManager servMgr);

		/**
		 * Public Method to access to the service manager.
		 * 
		 * @status FROZEN
		 * @param result Interface pointer to the service manager 
		 *
		 * @return NS_OK for success;
		 *         other error codes indicate a failure during initialisation.
		 *
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		[return: MarshalAs(UnmanagedType.U4)]
		public static extern nsResult NS_GetServiceManager(out nsIServiceManager result);

		/**
		 * Public Method to access to the component manager.
		 * 
		 * @status FROZEN
		 * @param result Interface pointer to the service 
		 *
		 * @return NS_OK for success;
		 *         other error codes indicate a failure during initialisation.
		 *
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		[return: MarshalAs(UnmanagedType.U4)]
		public static extern nsResult NS_GetComponentManager(out nsIComponentManager result);

		/**
		 * Public Method to access to the component registration manager.
		 * 
		 * @status FROZEN
		 * @param result Interface pointer to the service 
		 *
		 * @return NS_OK for success;
		 *         other error codes indicate a failure during initialisation.
		 *
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		[return: MarshalAs(UnmanagedType.U4)]
		public static extern nsResult NS_GetComponentRegistrar(out nsIComponentRegistrar result);

		/**
		 * Public Method to access to the memory manager.  See nsIMemory
		 * 
		 * @status FROZEN
		 * @param result Interface pointer to the memory manager 
		 *
		 * @return NS_OK for success;
		 *         other error codes indicate a failure during initialisation.
		 *
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		[return: MarshalAs(UnmanagedType.U4)]
		public static extern nsResult NS_GetMemoryManager(out nsIMemory result);

		/**
		 * Public Method to create an instance of a nsILocalFile.  This function
		 * may be called prior to NS_InitXPCOM.
		 * 
		 * @status FROZEN
		 * 
		 *   @param path       
		 *       A string which specifies a full file path to a 
		 *       location.  Relative paths will be treated as an
		 *       error (NS_ERROR_FILE_UNRECOGNIZED_PATH).       
		 *       |NS_NewNativeLocalFile|'s path must be in the 
		 *       filesystem charset.
		 *   @param followLinks
		 *       This attribute will determine if the nsLocalFile will auto
		 *       resolve symbolic links.  By default, this value will be false
		 *       on all non unix systems.  On unix, this attribute is effectively
		 *       a noop.  
		 * @param result Interface pointer to a new instance of an nsILocalFile 
		 *
		 * @return NS_OK for success;
		 *         other error codes indicate a failure.
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		[return: MarshalAs(UnmanagedType.U4)]
		public static extern nsResult NS_NewLocalFile([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String path, Boolean followLinks, out nsILocalFile result);

		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		[return: MarshalAs(UnmanagedType.U4)]
		public static extern nsResult NS_NewNativeLocalFile([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String path, Boolean followLinks, out nsILocalFile result);

		/**
		 * Allocates a block of memory of a particular size. If the memory cannot
		 * be allocated (because of an out-of-memory condition), null is returned.
		 *
		 * @status FROZEN
		 *
		 * @param size   The size of the block to allocate
		 * @result       The block of memory
		 * @note         This function is thread-safe.
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		public static extern IntPtr NS_Alloc(UInt32 size);

		/**
		 * Reallocates a block of memory to a new size.
		 *
		 * @status FROZEN
		 *
		 * @param ptr     The block of memory to reallocate. This block must originally
						  have been allocated by NS_Alloc or NS_Realloc
		 * @param size    The new size. If 0, frees the block like NS_Free
		 * @result        The reallocated block of memory
		 * @note          This function is thread-safe.
		 *
		 * If ptr is null, this function behaves like NS_Alloc.
		 * If s is the size of the block to which ptr points, the first min(s, size)
		 * bytes of ptr's block are copied to the new block. If the allocation
		 * succeeds, ptr is freed and a pointer to the new block is returned. If the
		 * allocation fails, ptr is not freed and null is returned. The returned
		 * value may be the same as ptr.
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		public static extern IntPtr NS_Realloc(IntPtr ptr, UInt32 size);

		/**
		 * Frees a block of memory. Null is a permissible value, in which case no
		 * action is taken.
		 *
		 * @status FROZEN
		 *
		 * @param ptr   The block of memory to free. This block must originally have
		 *              been allocated by NS_Alloc or NS_Realloc
		 * @note        This function is thread-safe.
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		public static extern void NS_Free(IntPtr ptr);

		/**
		 * Support for warnings, assertions, and debugging breaks.
		 */
		public const UInt32 NS_DEBUG_WARNING = 0;
		public const UInt32 NS_DEBUG_ASSERTION = 1;
		public const UInt32 NS_DEBUG_BREAK = 2;
		public const UInt32 NS_DEBUG_ABORT = 3;

		/**
		 * Print a runtime assertion. This function is available in both debug and
		 * release builds.
		 * 
		 * @note Based on the value of aSeverity and the XPCOM_DEBUG_BREAK
		 * environment variable, this function may cause the application to
		 * print the warning, print a stacktrace, break into a debugger, or abort
		 * immediately.
		 *
		 * @param aSeverity A NS_DEBUG_* value
		 * @param aStr   A readable error message (ASCII, may be null)
		 * @param aExpr  The expression evaluated (may be null)
		 * @param aFile  The source file containing the assertion (may be null)
		 * @param aLine  The source file line number (-1 indicates no line number)
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true, CharSet = CharSet.Ansi)]
		public static extern void NS_DebugBreak(UInt32 aSeverity, String aStr, String aExpr, String aFile, Int32 aLine);

		/**
		 * Perform a stack-walk to a debugging log under various
		 * circumstances. Used to aid debugging of leaked object graphs.
		 *
		 * The NS_Log* functions are available in both debug and release
		 * builds of XPCOM, but the output will be useless unless binary
		 * debugging symbols for all modules in the stacktrace are available.
		 */

		/**
		 * By default, refcount logging is enabled at NS_InitXPCOM and
		 * refcount statistics are printed at NS_ShutdownXPCOM. NS_LogInit and
		 * NS_LogTerm allow applications to enable logging earlier and delay
		 * printing of logging statistics. They should always be used as a
		 * matched pair.
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		public static extern void NS_LogInit();

		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		public static extern void NS_LogTerm();

		/**
		 * Log construction and destruction of objects. Processing tools can use the
		 * stacktraces printed by these functions to identify objects that are being
		 * leaked.
		 *
		 * @param aPtr          A pointer to the concrete object.
		 * @param aTypeName     The class name of the type
		 * @param aInstanceSize The size of the type
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true, CharSet = CharSet.Ansi)]
		public static extern void NS_LogCtor(IntPtr aPtr, String aTypeName, UInt32 aInstanceSize);

		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true, CharSet = CharSet.Ansi)]
		public static extern void NS_LogDtor(IntPtr aPtr, String aTypeName, UInt32 aInstanceSize);

		/**
		 * Log a stacktrace when an XPCOM object's refcount is incremented or
		 * decremented. Processing tools can use the stacktraces printed by these
		 * functions to identify objects that were leaked due to XPCOM references.
		 *
		 * @param aPtr          A pointer to the concrete object
		 * @param aNewRefCnt    The new reference count.
		 * @param aTypeName     The class name of the type
		 * @param aInstanceSize The size of the type
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true, CharSet = CharSet.Ansi)]
		public static extern void NS_LogAddRef(IntPtr aPtr, UInt32 aNewRefCnt, String aTypeName, UInt32 aInstanceSize);

		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true, CharSet = CharSet.Ansi)]
		public static extern void NS_LogRelease(IntPtr aPtr, UInt32 aNewRefCnt, String aTypeName);

		/**
		 * Log reference counting performed by COMPtrs. Processing tools can
		 * use the stacktraces printed by these functions to simplify reports
		 * about leaked objects generated from the data printed by
		 * NS_LogAddRef/NS_LogRelease.
		 *
		 * @param aCOMPtr the address of the COMPtr holding a strong reference
		 * @param aObject the object being referenced by the COMPtr
		 */
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		public static extern void NS_LogCOMPtrAddRef(IntPtr aCOMPtr, [MarshalAs(UnmanagedType.IUnknown)] Object aObject);

		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		public static extern void NS_LogCOMPtrRelease(IntPtr aCOMPtr, [MarshalAs(UnmanagedType.IUnknown)] Object aObject);

		//[DllImport(xpcom, ExactSpelling = true)]
		//internal static extern X NS_CycleCollectorSuspect();

		//[DllImport(xpcom, ExactSpelling = true)]
		//internal static extern X NS_CycleCollectorForget();

		//[DllImport(xpcom, ExactSpelling = true)]
		//internal static extern X NS_CycleCollectorSuspect2();

		//[DllImport(xpcom, ExactSpelling = true)]
		//internal static extern X NS_CycleCollectorForget2();

		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		[return: MarshalAs(UnmanagedType.U4)]
		public static extern nsResult NS_GetDebug(out nsIDebug result);

		[Obsolete("Use NS_Log* functions")]
		[DllImport(xpcom, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true)]
		[return: MarshalAs(UnmanagedType.U4)]
		public static extern nsResult NS_GetTraceRefcnt(out nsITraceRefcnt result);

		private const String xpcom = "xpcom.dll";
	}
}
