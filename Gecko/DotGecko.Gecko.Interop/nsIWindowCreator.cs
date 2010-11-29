using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIWindowCreator is a callback interface used by Gecko to create
	 * new browser windows. The application, either Mozilla or an embedding app,
	 * must provide an implementation of the Window Watcher component and
	 * notify the WindowWatcher during application initialization.
	 * @see nsIWindowWatcher
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("30465632-A777-44cc-90F9-8145475EF999"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWindowCreator //: nsISupports
	{
		/** Create a new window. Gecko will/may call this method, if made
			available to it, to create new windows.
			@param parent parent window, if any. null if not. the newly created
						  window should be made a child/dependent window of
						  the parent, if any (and if the concept applies
						  to the underlying OS).
			@param chromeFlags chrome features from nsIWebBrowserChrome
			@return the new window
		*/
		nsIWebBrowserChrome CreateChromeWindow(nsIWebBrowserChrome parent, UInt32 chromeFlags);
	}
}
