using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIXULBrowserWindow supplies the methods that may be called from the
	 * internals of the browser area to tell the containing xul window to update
	 * its ui. 
	 */
	[ComImport, Guid("67a601df-f091-4894-a2e2-2e6cfebb35ea"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXULBrowserWindow //: nsISupports
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

		/**
		 * Determines the appropriate target for a link.
		 */
		void OnBeforeLinkTraversal([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String originalTarget,
									nsIURI linkURI,
									nsIDOMNode linkNode,
									Boolean isAppTab,
									[In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
	}
}
