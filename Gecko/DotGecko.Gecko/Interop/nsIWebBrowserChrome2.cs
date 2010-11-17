using System;
using System.Runtime.InteropServices;
using AString = System.IntPtr;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIWebBrowserChrome2 is an extension to nsIWebBrowserChrome.
	 */
	[ComImport, Guid("2585a7b1-7b47-43c4-bf17-c6bf84e09b7b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIWebBrowserChrome2 : nsIWebBrowserChrome
	{
		#region nsIWebBrowserChrome

		new void SetStatus(UInt32 statusType, [MarshalAs(UnmanagedType.LPWStr)] String status);
		new nsIWebBrowser WebBrowser { get; set; }
		new UInt32 ChromeFlags { get; set; }
		new void DestroyBrowserWindow();
		new void SizeBrowserTo(Int32 aCX, Int32 aCY);
		new void ShowAsModal();
		new Boolean IsWindowModal();
		new void ExitModalEventLoop(UInt32 aStatus);

		#endregion

		/**
		 * Called when the status text in the chrome needs to be updated.  This
		 * method may be called instead of nsIWebBrowserChrome::SetStatus.  An
		 * implementor of this method, should still implement SetStatus.
		 *
		 * @param statusType
		 *        Indicates what is setting the text.
		 * @param status
		 *        Status string.  Null is an acceptable value meaning no status.
		 * @param contextNode 
		 *        An object that provides context pertaining to the status type.
		 *        If statusType is STATUS_LINK, then statusContext may be a DOM
		 *        node corresponding to the source of the link.  This value can
		 *        be null if there is no context.
		 */
		void SetStatusWithContext(UInt32 statusType, AString statusText, [MarshalAs(UnmanagedType.IUnknown)] nsISupports statusContext);
	}
}
