using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	// Constants for nsIRequest ( "ef6bfbd2-fd46-48d8-96b7-9f8f0fd387fe" ) interface
	internal static class nsIRequestConstants
	{
		/**************************************************************************
		 * Listed below are the various load flags which may be or'd together.
		 */

		/**
		 * No special load flags:
		 */
		internal const UInt32 LOAD_NORMAL = 0;

		/** 
		 * Don't deliver status notifications to the nsIProgressEventSink, or keep 
		 * this load from completing the nsILoadGroup it may belong to.
		 */
		internal const UInt32 LOAD_BACKGROUND = 1 << 0;

		/**************************************************************************
		 * The following flags control the flow of data into the cache.
		 */

		/**
		 * This flag prevents caching of any kind.  It does not, however, prevent
		 * cached content from being used to satisfy this request.
		 */
		internal const UInt32 INHIBIT_CACHING = 1 << 7;

		/**
		 * This flag prevents caching on disk (or other persistent media), which
		 * may be needed to preserve privacy.  For HTTPS, this flag is set auto-
		 * matically.
		 */
		internal const UInt32 INHIBIT_PERSISTENT_CACHING = 1 << 8;

		/**************************************************************************
		 * The following flags control what happens when the cache contains data
		 * that could perhaps satisfy this request.  They are listed in descending
		 * order of precidence.
		 */

		/**
		 * Force an end-to-end download of content data from the origin server.
		 * This flag is used for a shift-reload.
		 */
		internal const UInt32 LOAD_BYPASS_CACHE = 1 << 9;

		/**
		 * Load from the cache, bypassing protocol specific validation logic.  This
		 * flag is used when browsing via history.  It is not recommended for normal
		 * browsing as it may likely violate reasonable assumptions made by the 
		 * server and confuse users.
		 */
		internal const UInt32 LOAD_FROM_CACHE = 1 << 10;

		/**
		 * The following flags control the frequency of cached content validation
		 * when neither LOAD_BYPASS_CACHE or LOAD_FROM_CACHE are set.  By default,
		 * cached content is automatically validated if necessary before reuse.
		 * 
		 * VALIDATE_ALWAYS forces validation of any cached content independent of
		 * its expiration time.
		 * 
		 * VALIDATE_NEVER disables validation of expired content.
		 *
		 * VALIDATE_ONCE_PER_SESSION disables validation of expired content, 
		 * provided it has already been validated (at least once) since the start 
		 * of this session.
		 *
		 * NOTE TO IMPLEMENTORS:
		 *   These flags are intended for normal browsing, and they should therefore
		 *   not apply to content that must be validated before each use.  Consider,
		 *   for example, a HTTP response with a "Cache-control: no-cache" header.
		 *   According to RFC2616, this response must be validated before it can
		 *   be taken from a cache.  Breaking this requirement could result in 
		 *   incorrect and potentially undesirable side-effects.
		 */
		internal const UInt32 VALIDATE_ALWAYS = 1 << 11;
		internal const UInt32 VALIDATE_NEVER = 1 << 12;
		internal const UInt32 VALIDATE_ONCE_PER_SESSION = 1 << 13;

		/**
		 * When set, this flag indicates that no user-specific data should be added
		 * to the request when opened. This means that things like authorization
		 * tokens or cookie headers should not be added.
		 */
		internal const UInt32 LOAD_ANONYMOUS = 1 << 14;
	}

	/**
	 * nsIRequest
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("ef6bfbd2-fd46-48d8-96b7-9f8f0fd387fe"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIRequest //: nsISupports
	{
		/**
		 * The name of the request.  Often this is the URI of the request.
		 */
		void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);

		/**
		 * Indicates whether the request is pending. nsIRequest::isPending is
		 * true when there is an outstanding asynchronous event that will make
		 * the request no longer be pending.  Requests do not necessarily start
		 * out pending; in some cases, requests have to be explicitly initiated
		 * (e.g. nsIChannel implementations are only pending once asyncOpen
		 * returns successfully).
		 *
		 * Requests can become pending multiple times during their lifetime.
		 *
		 * @return TRUE if the request has yet to reach completion.
		 * @return FALSE if the request has reached completion (e.g., after
		 *   OnStopRequest has fired).
		 * @note Suspended requests are still considered pending.
		 */
		Boolean IsPending();

		/**
		 * The error status associated with the request.
		 */
		nsResult Status { [return: MarshalAs(UnmanagedType.U4)] get; }

		/**
		 * Cancels the current request.  This will close any open input or
		 * output streams and terminate any async requests.  Users should 
		 * normally pass NS_BINDING_ABORTED, although other errors may also
		 * be passed.  The error passed in will become the value of the 
		 * status attribute.
		 *
		 * Implementations must not send any notifications (e.g. via
		 * nsIRequestObserver) synchronously from this function. Similarly,
		 * removal from the load group (if any) must also happen asynchronously.
		 *
		 * Requests that use nsIStreamListener must not call onDataAvailable
		 * anymore after cancel has been called.
		 *
		 * @param aStatus the reason for canceling this request.
		 *
		 * NOTE: most nsIRequest implementations expect aStatus to be a
		 * failure code; however, some implementations may allow aStatus to
		 * be a success code such as NS_OK.  In general, aStatus should be
		 * a failure code.
		 */
		void Cancel([MarshalAs(UnmanagedType.U4)] nsResult aStatus);

		/**
		 * Suspends the current request.  This may have the effect of closing
		 * any underlying transport (in order to free up resources), although
		 * any open streams remain logically opened and will continue delivering
		 * data when the transport is resumed.
		 *
		 * Calling cancel() on a suspended request must not send any
		 * notifications (such as onstopRequest) until the request is resumed.
		 *
		 * NOTE: some implementations are unable to immediately suspend, and
		 * may continue to deliver events already posted to an event queue. In
		 * general, callers should be capable of handling events even after 
		 * suspending a request.
		 */
		void Suspend();

		/**
		 * Resumes the current request.  This may have the effect of re-opening
		 * any underlying transport and will resume the delivery of data to 
		 * any open streams.
		 */
		void Resume();

		/**
		 * The load group of this request.  While pending, the request is a 
		 * member of the load group.  It is the responsibility of the request
		 * to implement this policy.
		 */
		nsILoadGroup LoadGroup { get; set; }

		/**
		 * The load flags of this request.  Bits 0-15 are reserved.
		 *
		 * When added to a load group, this request's load flags are merged with
		 * the load flags of the load group.
		 */
		UInt32 LoadFlags { get; set; }
	}
}
