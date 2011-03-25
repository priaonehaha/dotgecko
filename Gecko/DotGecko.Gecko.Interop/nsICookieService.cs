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
	 * This service broadcasts the notifications detailed below when the cookie
	 * list is changed, or a cookie is rejected.
	 *
	 * NOTE: observers of these notifications *must* not attempt to change profile
	 *       or switch into or out of private browsing mode from within the
	 *       observer. Doing so will cause undefined behavior. Mutating the cookie
	 *       list (e.g. by calling methods on nsICookieService and friends) is
	 *       allowed, but beware that there may be pending notifications you haven't
	 *       seen yet -- for instance, a "batch-deleted" notification will likely be
	 *       immediately followed by "added". You may check the state of the cookie
	 *       list to determine if this is the case.
	 *
	 * topic  : "cookie-changed"
	 *          broadcast whenever the cookie list changes in some way. see
	 *          explanation of data strings below.
	 * subject: see below.
	 * data   : "deleted"
	 *          a cookie was deleted. the subject is an nsICookie2 representing
	 *          the deleted cookie.
	 *          "added"
	 *          a cookie was added. the subject is an nsICookie2 representing
	 *          the added cookie.
	 *          "changed"
	 *          a cookie was changed. the subject is an nsICookie2 representing
	 *          the new cookie. (note that host, path, and name are invariant
	 *          for a given cookie; other parameters may change.)
	 *          "batch-deleted"
	 *          a set of cookies was purged (typically, because they have either
	 *          expired or because the cookie list has grown too large). The subject
	 *          is an nsIArray of nsICookie2's representing the deleted cookies.
	 *          Note that the array could contain a single cookie.
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
		 *        The URI of the document for which cookies are being queried.
		 *        file:// URIs (i.e. with an empty host) are allowed, but any other
		 *        scheme must have a non-empty host. A trailing dot in the host
		 *        is acceptable, and will be stripped. This argument must not be null.
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
		 *        The URI of the document for which cookies are being queried.
		 *        file:// URIs (i.e. with an empty host) are allowed, but any other
		 *        scheme must have a non-empty host. A trailing dot in the host
		 *        is acceptable, and will be stripped. This argument must not be null.
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
		 *        The URI of the document for which cookies are being queried.
		 *        file:// URIs (i.e. with an empty host) are allowed, but any other
		 *        scheme must have a non-empty host. A trailing dot in the host
		 *        is acceptable, and will be stripped. This argument must not be null.
		 * @param aPrompt
		 *        the prompt to use for all user-level cookie notifications. This is
		 *        presently ignored and can be null. (Prompt information is determined
		 *        from the channel if necessary.)
		 * @param aCookie
		 *        the cookie string to set.
		 * @param aChannel
		 *        the channel used to load the document.  this parameter should not
		 *        be null, otherwise the cookies will not be set if third-party
		 *        cookies have been disabled by the user. (the channel is used
		 *        to determine the originating URI of the document; if it is not
		 *        provided, the cookies will be assumed third-party.)
		 */
		void SetCookieString(nsIURI aURI, nsIPrompt aPrompt, [MarshalAs(UnmanagedType.LPStr)] String aCookie, nsIChannel aChannel);

		/*
		 * Set the cookie string and expires associated with the URI.
		 *
		 * This function is NOT redundant with setCookieString, as the result
		 * will be different based on httponly (see bug 178993)
		 *
		 * @param aURI
		 *        The URI of the document for which cookies are being queried.
		 *        file:// URIs (i.e. with an empty host) are allowed, but any other
		 *        scheme must have a non-empty host. A trailing dot in the host
		 *        is acceptable, and will be stripped. This argument must not be null.
		 * @param aFirstURI
		 *        the URI that the user originally typed in or clicked on to initiate
		 *        the load of the document referenced by aURI.
		 * @param aPrompt
		 *        the prompt to use for all user-level cookie notifications. This is
		 *        presently ignored and can be null. (Prompt information is determined
		 *        from the channel if necessary.)
		 * @param aCookie
		 *        the cookie string to set.
		 * @param aServerTime
		 *        the current time reported by the server, if available. This should
		 *        be the string from the Date header in an HTTP response. If the
		 *        string is empty or null, server time is assumed to be the current
		 *        local time. If provided, it will be used to calculate the expiry
		 *        time of the cookie relative to the server's local time.
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
