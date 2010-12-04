using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 *
	 * The interface to global history.
	 *
	 * @status FROZEN & DEPRECATED. This interface is still accepted, but new
	 *         implementations should use nsIGlobalHistory2.
	 * @version 1.0
	 */
	[Obsolete("Use nsIGlobalHistory2", false)]
	[ComImport, Guid("9491C383-E3C4-11d2-BDBE-0050040A9B44"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIGlobalHistory //: nsISupports
	{
		/**
		 * addPage
		 * Add a page to the history
		 *
		 * @param aURL the url to the page
		 */
		void AddPage([MarshalAs(UnmanagedType.LPStr)] String aURL);

		/**
		 * isVisited
		 * Checks to see if the given page is in history
		 *
		 * @return true if a page has been passed into addPage().
		 * @param aURL the url to the page
		 */
		Boolean IsVisited([MarshalAs(UnmanagedType.LPStr)] String aURL);
	}
}
