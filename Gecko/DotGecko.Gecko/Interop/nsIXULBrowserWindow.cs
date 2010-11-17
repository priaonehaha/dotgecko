using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIXULBrowserWindow supplies the methods that may be called from the
	 * internals of the browser area to tell the containing xul window to update
	 * its ui. 
	 */
	[ComImport, Guid("67a601df-f091-4894-a2e2-2e6cfebb35ea"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIXULBrowserWindow //: nsISupports
	{
		/**
		 * Sets the status according to JS' version of status.
		 */
		void SetJSStatus([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String status);

		/**
		 * Sets the default status according to JS' version of default status.
		 */
		void SetJSDefaultStatus([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String status);

		/**
		 * Tells the object implementing this function what link we are currently
		 * over.
		 */
		void SetOverLink([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String link, nsIDOMElement element);
	}
}
