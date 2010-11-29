using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIWebNavigationConstants
	{
		/****************************************************************************
		 * The following flags may be bitwise combined to form the load flags
		 * parameter passed to either the loadURI or reload method.  Some of these
		 * flags are only applicable to loadURI.
		 */

		/**
		 * This flags defines the range of bits that may be specified.  Flags
		 * outside this range may be used, but may not be passed to Reload().
		 */
		public const UInt32 LOAD_FLAGS_MASK = 0xffff;

		/**
		 * This is the default value for the load flags parameter.
		 */
		public const UInt32 LOAD_FLAGS_NONE = 0x0000;

		/**
		 * Flags 0x1, 0x2, 0x4, 0x8 are reserved for internal use by
		 * nsIWebNavigation implementations for now.
		 */

		/**
		 * This flag specifies that the load should have the semantics of an HTML
		 * Meta-refresh tag (i.e., that the cache should be bypassed).  This flag
		 * is only applicable to loadURI.
		 * XXX the meaning of this flag is poorly defined.
		 * XXX no one uses this, so we should probably deprecate and remove it.
		 */
		public const UInt32 LOAD_FLAGS_IS_REFRESH = 0x0010;

		/**
		 * This flag specifies that the load should have the semantics of a link
		 * click.  This flag is only applicable to loadURI.
		 * XXX the meaning of this flag is poorly defined.
		 */
		public const UInt32 LOAD_FLAGS_IS_LINK = 0x0020;

		/**
		 * This flag specifies that history should not be updated.  This flag is only
		 * applicable to loadURI.
		 */
		public const UInt32 LOAD_FLAGS_BYPASS_HISTORY = 0x0040;

		/**
		 * This flag specifies that any existing history entry should be replaced.
		 * This flag is only applicable to loadURI.
		 */
		public const UInt32 LOAD_FLAGS_REPLACE_HISTORY = 0x0080;

		/**
		 * This flag specifies that the local web cache should be bypassed, but an
		 * intermediate proxy cache could still be used to satisfy the load.
		 */
		public const UInt32 LOAD_FLAGS_BYPASS_CACHE = 0x0100;

		/**
		 * This flag specifies that any intermediate proxy caches should be bypassed
		 * (i.e., that the content should be loaded from the origin server).
		 */
		public const UInt32 LOAD_FLAGS_BYPASS_PROXY = 0x0200;

		/**
		 * This flag specifies that a reload was triggered as a result of detecting
		 * an incorrect character encoding while parsing a previously loaded
		 * document.
		 */
		public const UInt32 LOAD_FLAGS_CHARSET_CHANGE = 0x0400;

		/**
		 * If this flag is set, Stop() will be called before the load starts
		 * and will stop both content and network activity (the default is to
		 * only stop network activity).  Effectively, this passes the
		 * STOP_CONTENT flag to Stop(), in addition to the STOP_NETWORK flag.
		 */
		public const UInt32 LOAD_FLAGS_STOP_CONTENT = 0x0800;

		/**
		 * A hint this load was prompted by an external program: take care!
		 */
		public const UInt32 LOAD_FLAGS_FROM_EXTERNAL = 0x1000;

		/**
		 * This flag specifies that the URI may be submitted to a third-party
		 * server for correction. This should only be applied to non-sensitive
		 * URIs entered by users.  This flag must not be passed to Reload.
		 */
		public const UInt32 LOAD_FLAGS_ALLOW_THIRD_PARTY_FIXUP = 0x2000;

		/**
		 * This flag specifies that this is the first load in this object.
		 * Set with care, since setting incorrectly can cause us to assume that
		 * nothing was actually loaded in this object if the load ends up being 
		 * handled by an external application.  This flag must not be passed to
		 * Reload.
		 */
		public const UInt32 LOAD_FLAGS_FIRST_LOAD = 0x4000;

		/**
		 * This flag specifies that the load should not be subject to popup
		 * blocking checks.  This flag must not be passed to Reload.
		 */
		public const UInt32 LOAD_FLAGS_ALLOW_POPUPS = 0x8000;

		/**
		 * This flag specifies that the URI classifier should not be checked for
		 * this load.  This flag must not be passed to Reload.
		 */
		public const UInt32 LOAD_FLAGS_BYPASS_CLASSIFIER = 0x10000;

		/**
		 * Force relevant cookies to be sent with this load even if normally they
		 * wouldn't be.
		 */
		public const UInt32 LOAD_FLAGS_FORCE_ALLOW_COOKIES = 0x20000;

		/****************************************************************************
		 * The following flags may be passed as the stop flags parameter to the stop
		 * method defined on this interface.
		 */

		/**
		 * This flag specifies that all network activity should be stopped.  This
		 * includes both active network loads and pending META-refreshes.
		 */
		public const UInt32 STOP_NETWORK = 0x01;

		/**
		 * This flag specifies that all content activity should be stopped.  This
		 * includes animated images, plugins and pending Javascript timeouts.
		 */
		public const UInt32 STOP_CONTENT = 0x02;

		/**
		 * This flag specifies that all activity should be stopped.
		 */
		public const UInt32 STOP_ALL = 0x03;
	}

	/**
	 * The nsIWebNavigation interface defines an interface for navigating the web.
	 * It provides methods and attributes to direct an object to navigate to a new
	 * location, stop or restart an in process load, or determine where the object
	 * has previously gone.
	 *
	 * @status UNDER_REVIEW
	 */
	[ComImport, Guid("F5D9E7B0-D930-11d3-B057-00A024FFC08C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWebNavigation //: nsISupports
	{
		/**
		 * Indicates if the object can go back.  If true this indicates that
		 * there is back session history available for navigation.
		 */
		Boolean CanGoBack { get; }

		/**
		 * Indicates if the object can go forward.  If true this indicates that
		 * there is forward session history available for navigation
		 */
		Boolean CanGoForward { get; }

		/**
		 * Tells the object to navigate to the previous session history item.  When a
		 * page is loaded from session history, all content is loaded from the cache
		 * (if available) and page state (such as form values and scroll position) is
		 * restored.
		 *
		 * @throw NS_ERROR_UNEXPECTED
		 *        Indicates that the call was unexpected at this time, which implies
		 *        that canGoBack is false.
		 */
		void GoBack();

		/**
		 * Tells the object to navigate to the next session history item.  When a
		 * page is loaded from session history, all content is loaded from the cache
		 * (if available) and page state (such as form values and scroll position) is
		 * restored.
		 *
		 * @throw NS_ERROR_UNEXPECTED
		 *        Indicates that the call was unexpected at this time, which implies
		 *        that canGoForward is false.
		 */
		void GoForward();

		/**
		 * Tells the object to navigate to the session history item at a given index.
		 *
		 * @throw NS_ERROR_UNEXPECTED
		 *        Indicates that the call was unexpected at this time, which implies
		 *        that session history entry at the given index does not exist.
		 */
		void GotoIndex(Int32 index);

		/**
		 * Loads a given URI.  This will give priority to loading the requested URI
		 * in the object implementing	this interface.  If it can't be loaded here
		 * however, the URI dispatcher will go through its normal process of content
		 * loading.
		 *
		 * @param aURI
		 *        The URI string to load.  For HTTP and FTP URLs and possibly others,
		 *        characters above U+007F will be converted to UTF-8 and then URL-
		 *        escaped per the rules of RFC 2396.
		 * @param aLoadFlags
		 *        Flags modifying load behaviour.  This parameter is a bitwise
		 *        combination of the load flags defined above.  (Undefined bits are
		 *        reserved for future use.)  Generally you will pass LOAD_FLAGS_NONE
		 *        for this parameter.
		 * @param aReferrer
		 *        The referring URI.  If this argument is null, then the referring
		 *        URI will be inferred internally.
		 * @param aPostData
		 *        If the URI corresponds to a HTTP request, then this stream is
		 *        appended directly to the HTTP request headers.  It may be prefixed
		 *        with additional HTTP headers.  This stream must contain a "\r\n"
		 *        sequence separating any HTTP headers from the HTTP request body.
		 *        This parameter is optional and may be null.
		 * @param aHeaders
		 *        If the URI corresponds to a HTTP request, then any HTTP headers
		 *        contained in this stream are set on the HTTP request.  The HTTP
		 *        header stream is formatted as:
		 *            ( HEADER "\r\n" )*
		 *        This parameter is optional and may be null.
		 */
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		nsResult LoadURI([MarshalAs(UnmanagedType.LPWStr)] String aURI, UInt32 aLoadFlags, nsIURI aReferrer, nsIInputStream aPostData, nsIInputStream aHeaders);

		/**
		 * Tells the Object to reload the current page.  There may be cases where the
		 * user will be asked to confirm the reload (for example, when it is
		 * determined that the request is non-idempotent).
		 *
		 * @param aReloadFlags
		 *        Flags modifying load behaviour.  This parameter is a bitwise
		 *        combination of the Load Flags defined above.  (Undefined bits are
		 *        reserved for future use.)  Generally you will pass LOAD_FLAGS_NONE
		 *        for this parameter.
		 *
		 * @throw NS_BINDING_ABORTED
		 *        Indicating that the user canceled the reload.
		 */
		void Reload(UInt32 aReloadFlags);

		/**
		 * Stops a load of a URI.
		 *
		 * @param aStopFlags
		 *        This parameter is one of the stop flags defined above.
		 */
		void Stop(UInt32 aStopFlags);

		/**
		 * Retrieves the current DOM document for the frame, or lazily creates a
		 * blank document if there is none.  This attribute never returns null except
		 * for unexpected error situations.
		 */
		nsIDOMDocument Document { get; }

		/**
		 * The currently loaded URI or null.
		 */
		nsIURI CurrentURI { get; }

		/**
		 * The referring URI for the currently loaded URI or null.
		 */
		nsIURI ReferringURI { get; }

		/**
		 * The session history object used by this web navigation instance.
		 */
		nsISHistory SessionHistory { get; set; }
	}
}
