using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsICookieService
	 *
	 * Provides methods for setting and getting cookies in the context of a
	 * page load.  See nsICookieManager for methods to manipulate the cookie
	 * database directly.  This separation of interface is mainly historical.
	 *
	 * This service broadcasts the following notifications when the cookie
	 * list is changed, or a cookie is rejected:
	 *
	 * topic  : "cookie-changed"
	 *          broadcast whenever the cookie list changes in some way. there
	 *          are four possible data strings for this notification; one
	 *          notification will be broadcast for each change, and will involve
	 *          a single cookie.
	 * subject: an nsICookie2 interface pointer representing the cookie object
	 *          that changed.
	 * data   : "deleted"
	 *          a cookie was deleted. the subject is the deleted cookie.
	 *          "added"
	 *          a cookie was added. the subject is the added cookie.
	 *          "changed"
	 *          a cookie was changed. the subject is the new cookie.
	 *          "cleared"
	 *          the entire cookie list was cleared. the subject is null.
	 *          "reload"
	 *          the entire cookie list should be reloaded.  the subject is null.
	 *
	 * topic  : "cookie-rejected"
	 *          broadcast whenever a cookie was rejected from being set as a
	 *          result of user prefs.
	 * subject: an nsIURI interface pointer representing the URI that attempted
	 *          to set the cookie.
	 * data   : none.
	 */
	[ComImport, Guid("2aaa897a-293c-4d2b-a657-8c9b7136996d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsICookieService //: nsISupports
	{
		/*
		 * Get the complete cookie string associated with the URI.
		 *
		 * @param aURI
		 *        the URI of the document for which cookies are being queried.
		 * @param aChannel
		 *        the channel used to load the document.  this parameter should not
		 *        be null, otherwise the cookies will not be returned if third-party
		 *        cookies have been disabled by the user. (the channel is used
		 *        to determine the originating URI of the document; if it is not
		 *        provided, the cookies will be assumed third-party.)
		 *
		 * @return the resulting cookie string
		 */
		[return: MarshalAs(UnmanagedType.LPStr)]
		String GetCookieString(nsIURI aURI, nsIChannel aChannel);

		/*
		 * Get the complete cookie string associated with the URI.
		 *
		 * This function is NOT redundant with getCookieString, as the result
		 * will be different based on httponly (see bug 178993)
		 *
		 * @param aURI
		 *        the URI of the document for which cookies are being queried.
		 * @param aFirstURI
		 *        the URI that the user originally typed in or clicked on to initiate
		 *        the load of the document referenced by aURI.
		 * @param aChannel
		 *        the channel used to load the document.  this parameter should not
		 *        be null, otherwise the cookies will not be returned if third-party
		 *        cookies have been disabled by the user. (the channel is used
		 *        to determine the originating URI of the document; if it is not
		 *        provided, the cookies will be assumed third-party.)
		 *
		 * @return the resulting cookie string
		 */
		[return: MarshalAs(UnmanagedType.LPStr)]
		String GetCookieStringFromHttp(nsIURI aURI, nsIURI aFirstURI, nsIChannel aChannel);

		/*
		 * Set the cookie string associated with the URI.
		 *
		 * @param aURI
		 *        the URI of the document for which cookies are being set.
		 * @param aPrompt
		 *        the prompt to use for all user-level cookie notifications.
		 * @param aCookie
		 *        the cookie string to set.
		 * @param aChannel
		 *        the channel used to load the document.  this parameter should not
		 *        be null, otherwise the cookies will not be set if third-party
		 *        cookies have been disabled by the user. (the channel is used
		 *        to determine the originating URI of the document; if it is not
		 *        provided, the cookies will be assumed third-party.)
		 *
		 * XXX should be able to allow null aPrompt, since nsIPrompt can be queryied
		 * from aChannel.
		 */
		void SetCookieString(nsIURI aURI, nsIPrompt aPrompt, [MarshalAs(UnmanagedType.LPStr)] String aCookie, nsIChannel aChannel);

		/*
		 * Set the cookie string and expires associated with the URI.
		 *
		 * This function is NOT redundant with setCookieString, as the result
		 * will be different based on httponly (see bug 178993)
		 *
		 * @param aURI
		 *        the URI of the document for which cookies are being set.
		 * @param aFirstURI
		 *        the URI that the user originally typed in or clicked on to initiate
		 *        the load of the document referenced by aURI.
		 * @param aPrompt
		 *        the prompt to use for all user-level cookie notifications.
		 * @param aCookie
		 *        the cookie string to set.
		 * @param aServerTime
		 *        the expiry information of the cookie (the Date header from the HTTP
		 *        response).
		 * @param aChannel
		 *        the channel used to load the document.  this parameter should not
		 *        be null, otherwise the cookies will not be set if third-party
		 *        cookies have been disabled by the user. (the channel is used
		 *        to determine the originating URI of the document; if it is not
		 *        provided, the cookies will be assumed third-party.)
		 */
		void SetCookieStringFromHttp(nsIURI aURI, nsIURI aFirstURI, nsIPrompt aPrompt, [MarshalAs(UnmanagedType.LPStr)] String aCookie, [MarshalAs(UnmanagedType.LPStr)] String aServerTime, nsIChannel aChannel);
	}
}
