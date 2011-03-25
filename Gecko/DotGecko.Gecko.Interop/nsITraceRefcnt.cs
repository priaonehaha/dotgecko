using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**  
	 * nsITraceRefcnt is an interface between XPCOM Glue and XPCOM.
	 *
	 * @status DEPRECATED  Replaced by the NS_Log* functions.
	 */
	[Obsolete("Use NS_Log* functions")]
	[ComImport, Guid("273dc92f-0fe6-4545-96a9-21be77828039"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsITraceRefcnt //: nsISupports
	{
		void LogAddRef(IntPtr aPtr, UInt32 aNewRefcnt, [MarshalAs(UnmanagedType.LPStr)] String aTypeName, UInt32 aInstanceSize);

		void LogRelease(IntPtr aPtr, UInt32 aNewRefcnt, [MarshalAs(UnmanagedType.LPStr)] String aTypeName);

		void LogCtor(IntPtr aPtr, [MarshalAs(UnmanagedType.LPStr)] String aTypeName, UInt32 aInstanceSize);

		void LogDtor(IntPtr aPtr, [MarshalAs(UnmanagedType.LPStr)] String aTypeName, UInt32 aInstanceSize);

		void LogAddCOMPtr(IntPtr aPtr, [MarshalAs(UnmanagedType.IUnknown)] Object aObject);

		void LogReleaseCOMPtr(IntPtr aPtr, [MarshalAs(UnmanagedType.IUnknown)] Object aObject);
	}
}
