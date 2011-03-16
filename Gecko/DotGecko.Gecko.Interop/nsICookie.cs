using System;
using System.Runtime.InteropServices;
using System.Text;
using nsCookieStatus = System.Int32;
using nsCookiePolicy = System.Int32;

namespace DotGecko.Gecko.Interop
{
	public static class nsICookieConstants
	{
		public const nsCookieStatus STATUS_UNKNOWN = 0;
		public const nsCookieStatus STATUS_ACCEPTED = 1;
		public const nsCookieStatus STATUS_DOWNGRADED = 2;
		public const nsCookieStatus STATUS_FLAGGED = 3;
		public const nsCookieStatus STATUS_REJECTED = 4;

		public const nsCookiePolicy POLICY_UNKNOWN = 0;
		public const nsCookiePolicy POLICY_NONE = 1;
		public const nsCookiePolicy POLICY_NO_CONSENT = 2;
		public const nsCookiePolicy POLICY_IMPLICIT_CONSENT = 3;
		public const nsCookiePolicy POLICY_EXPLICIT_CONSENT = 4;
		public const nsCookiePolicy POLICY_NO_II = 5;
	}

	/** 
	 * An optional interface for accessing the HTTP or
	 * javascript cookie object
	 */
	[ComImport, Guid("E9FCB9A4-D376-458f-B720-E65E7DF593BC"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsICookie //: nsISupports
	{
		/**
		 * the name of the cookie
		 */
		void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);

		/**
		 * the cookie value
		 */
		void GetValue([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);

		/**
		 * true if the cookie is a domain cookie, false otherwise
		 */
		Boolean IsDomain { get; }

		/**
		 * the host (possibly fully qualified) of the cookie
		 */
		void GetHost([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * the path pertaining to the cookie
		 */
		void GetPath([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * true if the cookie was transmitted over ssl, false otherwise
		 */
		Boolean IsSecure { get; }

		/**
		 * @DEPRECATED use nsICookie2.expiry and nsICookie2.isSession instead.
		 *
		 * expiration time in seconds since midnight (00:00:00), January 1, 1970 UTC.
		 * expires = 0 represents a session cookie.
		 * expires = 1 represents an expiration time earlier than Jan 1, 1970.
		 */
		UInt64 Expires { get; }

		/**
		 * @DEPRECATED status implementation will return STATUS_UNKNOWN in all cases.
		 */
		nsCookieStatus Status { get; }

		/**
		 * @DEPRECATED policy implementation will return POLICY_UNKNOWN in all cases.
		 */
		nsCookiePolicy Policy { get; }
	}
}
