using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	// Constants for nsIWebBrowserChrome ( "BA434C60-9D52-11d3-AFB0-00A024FFC08C" ) interface
	internal static class nsIWebBrowserChromeConstants
	{
		internal const UInt32 STATUS_SCRIPT = 0x00000001;
		internal const UInt32 STATUS_SCRIPT_DEFAULT = 0x00000002;
		internal const UInt32 STATUS_LINK = 0x00000003;

		/**
		 * Definitions for the chrome flags
		 */
		internal const UInt32 CHROME_DEFAULT = 0x00000001;
		internal const UInt32 CHROME_WINDOW_BORDERS = 0x00000002;
		internal const UInt32 CHROME_WINDOW_CLOSE = 0x00000004;
		internal const UInt32 CHROME_WINDOW_RESIZE = 0x00000008;
		internal const UInt32 CHROME_MENUBAR = 0x00000010;
		internal const UInt32 CHROME_TOOLBAR = 0x00000020;
		internal const UInt32 CHROME_LOCATIONBAR = 0x00000040;
		internal const UInt32 CHROME_STATUSBAR = 0x00000080;
		internal const UInt32 CHROME_PERSONAL_TOOLBAR = 0x00000100;
		internal const UInt32 CHROME_SCROLLBARS = 0x00000200;
		internal const UInt32 CHROME_TITLEBAR = 0x00000400;
		internal const UInt32 CHROME_EXTRA = 0x00000800;

		// createBrowserWindow specific flags
		internal const UInt32 CHROME_WITH_SIZE = 0x00001000;
		internal const UInt32 CHROME_WITH_POSITION = 0x00002000;

		// special cases
		internal const UInt32 CHROME_WINDOW_MIN = 0x00004000;
		internal const UInt32 CHROME_WINDOW_POPUP = 0x00008000;

		internal const UInt32 CHROME_WINDOW_RAISED = 0x02000000;
		internal const UInt32 CHROME_WINDOW_LOWERED = 0x04000000;
		internal const UInt32 CHROME_CENTER_SCREEN = 0x08000000;

		// Make the new window dependent on the parent.  This flag is only
		// meaningful if CHROME_OPENAS_CHROME is set; content windows should not be
		// dependent.
		internal const UInt32 CHROME_DEPENDENT = 0x10000000;

		// Note: The modal style bit just affects the way the window looks and does
		//       mean it's actually modal.
		internal const UInt32 CHROME_MODAL = 0x20000000;
		internal const UInt32 CHROME_OPENAS_DIALOG = 0x40000000;
		internal const UInt32 CHROME_OPENAS_CHROME = 0x80000000;

		internal const UInt32 CHROME_ALL = 0x00000ffe;
	}

	/**
	 * nsIWebBrowserChrome corresponds to the top-level, outermost window
	 * containing an embedded Gecko web browser.
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("BA434C60-9D52-11d3-AFB0-00A024FFC08C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIWebBrowserChrome //: nsISupports
	{
		/**
		 * Called when the status text in the chrome needs to be updated.
		 * @param statusType indicates what is setting the text
		 * @param status status string. null is an acceptable value meaning
		 *               no status.
		 */
		void SetStatus(UInt32 statusType, [MarshalAs(UnmanagedType.LPWStr)] String status);

		/**
		 * The currently loaded WebBrowser.  The browser chrome may be
		 * told to set the WebBrowser object to a new object by setting this
		 * attribute.  In this case the implementer is responsible for taking the 
		 * new WebBrowser object and doing any necessary initialization or setup 
		 * as if it had created the WebBrowser itself.  This includes positioning
		 * setting up listeners etc.
		 */
		nsIWebBrowser WebBrowser { get; set; }

		/**
		 * The chrome flags for this browser chrome. The implementation should
		 * reflect the value of this attribute by hiding or showing its chrome
		 * appropriately.
		 */
		UInt32 ChromeFlags { get; set; }

		/**
		 * Asks the implementer to destroy the window associated with this
		 * WebBrowser object.
		 */
		void DestroyBrowserWindow();

		/**
		 * Tells the chrome to size itself such that the browser will be the 
		 * specified size.
		 * @param aCX new width of the browser
		 * @param aCY new height of the browser
		 */
		void SizeBrowserTo(Int32 aCX, Int32 aCY);

		/**
		 * Shows the window as a modal window.
		 * @return (the function error code) the status value specified by
		 *         in exitModalEventLoop.
		 */
		void ShowAsModal();

		/**
		 * Is the window modal (that is, currently executing a modal loop)?
		 * @return true if it's a modal window
		 */
		Boolean IsWindowModal();

		/**
		 * Exit a modal event loop if we're in one. The implementation
		 * should also exit out of the loop if the window is destroyed.
		 * @param aStatus - the result code to return from showAsModal
		 */
		void ExitModalEventLoop(UInt32 aStatus);
	}
}
