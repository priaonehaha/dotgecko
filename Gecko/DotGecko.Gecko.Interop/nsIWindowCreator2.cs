using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIWindowCreator2Constants
	{
		/**
		 * Definitions for contextFlags
		 */

		// Likely that the window is an advertising popup. 
		public const UInt32 PARENT_IS_LOADING_OR_RUNNING_TIMEOUT = 0x00000001;
	}

	/**
	 * nsIWindowCreator2 is an extension of nsIWindowCreator which allows
	 * additional information about the context of the window creation to
	 * be passed.
	 *
	 * @see nsIWindowCreator
	 * @see nsIWindowWatcher
	 *
	 * @status
	 */
	[ComImport, Guid("f673ec81-a4b0-11d6-964b-eb5a2bf216fc"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWindowCreator2 : nsIWindowCreator
	{
		#region nsIWindowCreator Members

		new nsIWebBrowserChrome CreateChromeWindow(nsIWebBrowserChrome parent, UInt32 chromeFlags);

		#endregion

		/** Create a new window. Gecko will/may call this method, if made
			available to it, to create new windows.
			@param parent Parent window, if any. Null if not. The newly created
			              window should be made a child/dependent window of
			              the parent, if any (and if the concept applies
			              to the underlying OS).
			@param chromeFlags Chrome features from nsIWebBrowserChrome
			@param contextFlags Flags about the context of the window being created.
			@param uri The URL for which this window is intended. It can be null
			           or zero-length. The implementation of this interface
					   may use the URL to help determine what sort of window
					   to open or whether to cancel window creation. It will not
					   load the URL.
			@param cancel Return |true| to reject window creation. If true the
							implementation has determined the window should not
							be created at all. The caller should not default
							to any possible backup scheme for creating the window.
			@return the new window. Will be null if canceled or an error occurred.
		*/
		nsIWebBrowserChrome CreateChromeWindow2(nsIWebBrowserChrome parent, UInt32 chromeFlags, UInt32 contextFlags, nsIURI uri, out Boolean cancel);
	}
}
