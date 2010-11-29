using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIXULWindowConstants
	{
		public const UInt32 lowestZ = 0;
		public const UInt32 loweredZ = 4;  /* "alwaysLowered" attribute */
		public const UInt32 normalZ = 5;
		public const UInt32 raisedZ = 6;   /* "alwaysRaised" attribute */
		public const UInt32 highestZ = 9;
	}

	/**
	 * The nsIXULWindow
	 *
	 * When the window is destroyed, it will fire a "xul-window-destroyed"
	 * notification through the global observer service.
	 */
	[ComImport, Guid("c175a596-ee13-420a-aa74-13ad3a14deb1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXULWindow //: nsISupports
	{
		/**
		 * The docshell owning the XUL for this window.
		 */
		nsIDocShell DocShell { get; }

		/**
		 * Indicates if this window is instrinsically sized.	
		 */
		Boolean IntrinsicallySized { get; set; }

		/**
		 * The primary content shell.  
		 *
		 * Note that this is a docshell tree item and therefore can not be assured of
		 * what object it is. It could be an editor, a docshell, or a browser object.
		 * Or down the road any other object that supports being a DocShellTreeItem
		 * Query accordingly to determine the capabilities.
		 */
		nsIDocShellTreeItem PrimaryContentShell { get; }

		/**
		 * The content shell specified by the supplied id.
		 *
		 * Note that this is a docshell tree item and therefore can not be assured of
		 * what object it is.  It could be an editor, a docshell, or a browser object.
		 * Or down the road any other object that supports being a DocShellTreeItem
		 * Query accordingly to determine the capabilities.
		 */
		nsIDocShellTreeItem GetContentShellById([MarshalAs(UnmanagedType.LPWStr)] String ID);

		/**
		 * Tell this window that it has picked up a child XUL window
		 * @param aChild the child window being added
		 */
		void AddChildWindow(nsIXULWindow aChild);

		/**
		 * Tell this window that it has lost a child XUL window
		 * @param aChild the child window being removed
		 */
		void RemoveChildWindow(nsIXULWindow aChild);

		/**
		 * Move the window to a centered position.
		 * @param aRelative the window relative to which the window is moved.
		 *                  See screen parameter for details. if null, the
		 *                  window is centered relative to the main screen.
		 * @param aScreen   PR_TRUE to center the window relative to the screen
		 *                  containing aRelative. PR_FALSE to center it relative
		 *                  to aRelative itself.
		 * @param aAlert    PR_TRUE to move the window to an alert position,
		 *                  generally centered horizontally and 1/3 down from the top.
		 */
		void Center(nsIXULWindow aRelative, Boolean aScreen, Boolean aAlert);

		/**
		 * Shows the window as a modal window. That is, ensures that it is visible
		 * and runs a local event loop, exiting only once the window has been closed.
		 */
		void ShowModal();

		UInt32 ZLevel { get; set; }

		/**
		 * contextFlags are from nsIWindowCreator2
		 */
		UInt32 ContextFlags { get; set; }

		UInt32 ChromeFlags { get; set; }

		/**
		 * Create a new window.
		 * @param aChromeFlags see nsIWebBrowserChrome
		 * @return the newly minted window
		 */
		nsIXULWindow CreateNewWindow(Int32 aChromeFlags, nsIAppShell aAppShell);

		nsIXULBrowserWindow XULBrowserWindow { get; set; }

		/**
		 * Back-door method to force application of chrome flags at a particular
		 * time.  Do NOT call this unless you know what you're doing!  In particular,
		 * calling this when this XUL window doesn't yet have a document in its
		 * docshell could cause problems.
		 */
		void ApplyChromeFlags();
	}
}
