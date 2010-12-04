using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsICookiePromptServiceConstants
	{
		public const UInt32 DENY_COOKIE = 0;
		public const UInt32 ACCEPT_COOKIE = 1;
		public const UInt32 ACCEPT_SESSION_COOKIE = 2;
	}

	/**
	 * An interface to open a dialog to ask to permission to accept the cookie.
	 */
	[ComImport, Guid("72f8bb14-2810-4f38-8d0d-290c5401f54e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsICookiePromptService //: nsISupports
	{
		/* Open a dialog that asks for permission to accept a cookie
		 * 
		 * @param parent
		 * @param cookie
		 * @param hostname          the host that wants to set the cookie, 
		 *                           not the domain: part of the cookie
		 * @param cookiesFromHost   the number of cookies there are already for this host
		 * @param changingCookie    are we changing this cookie?
		 * @param rememberDecision  should we set the matching permission for this host?
		 * @returns                 0 == deny cookie
		 *                          1 == accept cookie
		 *                          2 == accept cookie for current session
		 */
		Int32 CookieDialog(nsIDOMWindow parent,
						   nsICookie cookie,
						   [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String hostname,
						   Int32 cookiesFromHost,
						   Boolean changingCookie,
						   out Boolean rememberDecision);
	}
}
