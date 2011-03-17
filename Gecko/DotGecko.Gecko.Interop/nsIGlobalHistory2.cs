using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Provides information about global history to gecko. 
	 *
	 * @note  This interface replaces and deprecates nsIGlobalHistory.
	 */
	[ComImport, Guid("cf777d42-1270-4b34-be7b-2931c93feda5"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIGlobalHistory2 //: nsISupports
	{
		/**
		 * Add a URI to global history
		 *
		 * @param aURI      the URI of the page
		 * @param aRedirect whether the URI was redirected to another location;
		 *                  this is 'true' for the original URI which is
		 *                  redirected.
		 * @param aToplevel whether the URI is loaded in a top-level window
		 * @param aReferrer the URI of the referring page
		 *
		 * @note  Docshell will not filter out URI schemes like chrome: data:
		 *        about: and view-source:.  Embedders should consider filtering out
		 *        these schemes and others, e.g. mailbox: for the main URI and the
		 *        referrer.
		 */
		void AddURI(nsIURI aURI, Boolean aRedirect, Boolean aToplevel, nsIURI aReferrer);

		/**
		 * Checks to see whether the given URI is in history.
		 *
		 * @param aURI the uri to the page
		 * @return true if a URI has been visited
		 */
		Boolean IsVisited(nsIURI aURI);

		/**
		 * Set the page title for the given uri. URIs that are not already in
		 * global history will not be added.
		 *
		 * @param aURI    the URI for which to set to the title
		 * @param aTitle  the page title
		 */
		void SetPageTitle(nsIURI aURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aTitle);
	}
}
