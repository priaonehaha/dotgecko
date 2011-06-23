using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;
using nsPresContext = System.IntPtr;
using nsIPresShell = System.IntPtr;
using nsILayoutHistoryState = System.IntPtr;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	public static class nsIDocShellConstants
	{
		public const Int32 INTERNAL_LOAD_FLAGS_NONE = 0x0;
		public const Int32 INTERNAL_LOAD_FLAGS_INHERIT_OWNER = 0x1;
		public const Int32 INTERNAL_LOAD_FLAGS_DONT_SEND_REFERRER = 0x2;
		public const Int32 INTERNAL_LOAD_FLAGS_ALLOW_THIRD_PARTY_FIXUP = 0x4;

		// This flag marks the first load in this object
		// @see nsIWebNavigation::LOAD_FLAGS_FIRST_LOAD
		public const Int32 INTERNAL_LOAD_FLAGS_FIRST_LOAD = 0x8;

		public const Int32 INTERNAL_LOAD_FLAGS_BYPASS_CLASSIFIER = 0x10;
		public const Int32 INTERNAL_LOAD_FLAGS_FORCE_ALLOW_COOKIES = 0x20;

		public const Int32 ENUMERATE_FORWARDS = 0;
		public const Int32 ENUMERATE_BACKWARDS = 1;

		/**
		 * The type of application that created this window
		 */
		public const UInt32 APP_TYPE_UNKNOWN = 0;
		public const UInt32 APP_TYPE_MAIL = 1;
		public const UInt32 APP_TYPE_EDITOR = 2;

		/**
		 * Current busy state for DocShell
		 */
		public const UInt32 BUSY_FLAGS_NONE = 0;
		public const UInt32 BUSY_FLAGS_BUSY = 1;
		public const UInt32 BUSY_FLAGS_BEFORE_PAGE_LOAD = 2;
		public const UInt32 BUSY_FLAGS_PAGE_LOADING = 4;

		/**
		 * Load commands for the document 
		 */
		public const UInt32 LOAD_CMD_NORMAL = 0x1; // Normal load
		public const UInt32 LOAD_CMD_RELOAD = 0x2; // Reload
		public const UInt32 LOAD_CMD_HISTORY = 0x4; // Load from history
		public const UInt32 LOAD_CMD_PUSHSTATE = 0x8; // History.pushState()
	}

	/**
	 * The nsIDocShell interface.
	 */
	[ComImport, Guid("f77271a1-0b22-4581-af6d-529125f1901d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDocShell //: nsISupports
	{
		/**
		 * Loads a given URI.  This will give priority to loading the requested URI
		 * in the object implementing	this interface.  If it can't be loaded here
		 * however, the URL dispatcher will go through its normal process of content
		 * loading.
		 *
		 * @param uri        - The URI to load.
		 * @param loadInfo   - This is the extended load info for this load.  This
		 *                     most often will be null, but if you need to do 
		 *                     additional setup for this load you can get a loadInfo
		 *                     object by calling createLoadInfo.  Once you have this
		 *                     object you can set the needed properties on it and
		 *                     then pass it to loadURI.
		 * @param aLoadFlags - Flags to modify load behaviour. Flags are defined in
		 *                     nsIWebNavigation.  Note that using flags outside
		 *                     LOAD_FLAGS_MASK is only allowed if passing in a
		 *                     non-null loadInfo.  And even some of those might not
		 *                     be allowed.  Use at your own risk.
		 */
		void LoadURI(nsIURI uri, nsIDocShellLoadInfo loadInfo, UInt32 aLoadFlags, Boolean firstParty);

		/**
		 * Loads a given stream. This will give priority to loading the requested
		 * stream in the object implementing this interface. If it can't be loaded
		 * here however, the URL dispatched will go through its normal process of
		 * content loading.
		 *
		 * @param aStream         - The input stream that provides access to the data
		 *                          to be loaded.  This must be a blocking, threadsafe
		 *                          stream implementation.
		 * @param aURI            - The URI representing the stream, or null.
		 * @param aContentType    - The type (MIME) of data being loaded (empty if unknown).
		 * @param aContentCharset - The charset of the data being loaded (empty if unknown).
		 * @param aLoadInfo       - This is the extended load info for this load.  This
		 *                          most often will be null, but if you need to do 
		 *                          additional setup for this load you can get a
		 *                          loadInfo object by calling createLoadInfo.  Once
		 *                          you have this object you can set the needed 
		 *                          properties on it and then pass it to loadStream.
		 */
		void LoadStream(
			nsIInputStream aStream, nsIURI aURI,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String aContentType,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String aContentCharset,
			nsIDocShellLoadInfo aLoadInfo);

		/**
		 * Loads the given URI.  This method is identical to loadURI(...) except
		 * that its parameter list is broken out instead of being packaged inside
		 * of an nsIDocShellLoadInfo object...
		 *
		 * @param aURI            - The URI to load.
		 * @param aReferrer       - Referring URI
		 * @param aOwner          - Owner (security principal) 
		 * @param aInheritOwner   - Flag indicating whether the owner of the current
		 *                          document should be inherited if aOwner is null.
		 * @param aStopActiveDoc  - Flag indicating whether loading the current
		 *                          document should be stopped.
		 * @param aWindowTarget   - Window target for the load.
		 * @param aTypeHint       - A hint as to the content-type of the resulting
		 *                          data.  May be null or empty if no hint.
		 * @param aPostDataStream - Post data stream (if POSTing)
		 * @param aHeadersStream  - Stream containing "extra" request headers...
		 * @param aLoadFlags      - Flags to modify load behaviour. Flags are defined
		 *                          in nsIWebNavigation.
		 * @param aSHEntry        - Active Session History entry (if loading from SH)
		 */
		void InternalLoad(
			nsIURI aURI,
			nsIURI aReferrer,
			[MarshalAs(UnmanagedType.IUnknown)] nsISupports aOwner,
			UInt32 aFlags,
			[MarshalAs(UnmanagedType.LPWStr)] String aWindowTarget,
			[MarshalAs(UnmanagedType.LPStr)] String aTypeHint,
			nsIInputStream aPostDataStream,
			nsIInputStream aHeadersStream,
			UInt32 aLoadFlags,
			nsISHEntry aSHEntry,
			Boolean firstParty,
			out nsIDocShell aDocShell,
			out nsIRequest aRequest);

		/**
		 * Do either a history.pushState() or history.replaceState() operation,
		 * depending on the value of aReplace.
		 */
		void AddState(nsIVariant aData,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String aTitle,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String aURL,
			Boolean aReplace);

		/**
		 * Creates a DocShellLoadInfo object that you can manipulate and then pass
		 * to loadURI.
		 */
		void CreateLoadInfo(out nsIDocShellLoadInfo loadInfo);

		/**
		 * Reset state to a new content model within the current document and the document
		 * viewer.  Called by the document before initiating an out of band document.write().
		 */
		void PrepareForNewContentModel();

		/**
		 * For editors and suchlike who wish to change the URI associated with the
		 * document. Note if you want to get the current URI, use the read-only
		 * property on nsIWebNavigation.
		 */
		void SetCurrentURI(nsIURI aURI);

		/**
		 * Notify the associated content viewer and all child docshells that they are
		 * about to be hidden.  If |isUnload| is true, then the document is being
		 * unloaded as well.
		 *
		 * @param isUnload if true, fire the unload event in addition to the pagehide
		 *                 event.
		 */
		void FirePageHideNotification(Boolean isUnload);

		/**
		 * Presentation context for the currently loaded document.  This may be null.
		 */
		nsPresContext PresContext { get; }

		/**
		 * Presentation shell for the currently loaded document.  This may be null.
		 */
		nsIPresShell PresShell { get; }

		/**
		 * Presentation shell for the oldest document, if this docshell is
		 * currently transitioning between documents.
		 */
		nsIPresShell EldestPresShell { get; }

		/**
		 * Content Viewer that is currently loaded for this DocShell.  This may
		 * change as the underlying content changes.
		 */
		nsIContentViewer ContentViewer { get; }

		/**
		 * This attribute allows chrome to tie in to handle DOM events that may
		 * be of interest to chrome.
		 */
		nsIDOMEventTarget ChromeEventHandler { get; set; }

		/**
		 * The document charset info.  This is used by a load to determine priorities
		 * for charset detection etc.
		 */
		nsIDocumentCharsetInfo DocumentCharsetInfo { get; set; }

		/**
		 * Whether to allow plugin execution
		 */
		Boolean AllowPlugins { get; set; }

		/**
		 * Whether to allow Javascript execution
		 */
		Boolean AllowJavascript { get; set; }

		/**
		 * Attribute stating if refresh based redirects can be allowed
		 */
		Boolean AllowMetaRedirects { get; set; }

		/**
		 * Attribute stating if it should allow subframes (framesets/iframes) or not
		 */
		Boolean AllowSubframes { get; set; }

		/**
		 * Attribute stating whether or not images should be loaded.
		 */
		Boolean AllowImages { get; set; }

		/**
		 * Attribute that determines whether DNS prefetch is allowed for this subtree
		 * of the docshell tree.  Defaults to true.  Setting this will make it take
		 * effect starting with the next document loaded in the docshell.
		 */
		Boolean AllowDNSPrefetch { get; set; }

		/**
		 * Get an enumerator over this docShell and its children.
		 *
		 * @param aItemType  - Only include docShells of this type, or if typeAll,
		 *                     include all child shells.
		 *                     Uses types from nsIDocShellTreeItem.
		 * @param aDirection - Whether to enumerate forwards or backwards.
		 */
		nsISimpleEnumerator GetDocShellEnumerator(Int32 aItemType, Int32 aDirection);

		UInt32 AppType { get; set; }

		/**
		 * certain dochshells (like the message pane)
		 * should not throw up auth dialogs
		 * because it can act as a password trojan
		 */
		Boolean AllowAuth { get; set; }

		/**
		 * Set/Get the document scale factor.  When setting this attribute, a
		 * NS_ERROR_NOT_IMPLEMENTED error may be returned by implementations
		 * not supporting zoom.  Implementations not supporting zoom should return
		 * 1.0 all the time for the Get operation.  1.0 by the way is the default
		 * of zoom.  This means 100% of normal scaling or in other words normal size
		 * no zoom. 
		 */
		Single Zoom { get; set; }

		/*
		 * The size, in CSS pixels, of the horizontal margins for the <body> of an
		 * HTML document in this docshel; used to implement the marginwidth attribute
		 * on HTML <frame>/<iframe> elements.  A value smaller than zero indicates
		 * that the attribute was not set.
		 */
		Int32 MarginWidth { get; set; }

		/*
		 * The size, in CSS pixels, of the vertical margins for the <body> of an HTML
		 * document in this docshel; used to implement the marginheight attribute on
		 * HTML <frame>/<iframe> elements.  A value smaller than zero indicates that
		 * the attribute was not set.
		 */
		Int32 MarginHeight { get; set; }

		/*
		 * Tells the docshell to offer focus to its tree owner.
		 * This is currently only necessary for embedding chrome.
		 */
		void TabToTreeOwner(Boolean forward, out Boolean tookFocus);

		UInt32 BusyFlags { get; }

		/* 
		 * attribute to access the loadtype  for the document
		 */
		UInt32 LoadType { get; set; }

		/*
		 * returns true if the docshell is being destroyed, false otherwise
		 */
		Boolean IsBeingDestroyed();

		/*
		 * Returns true if the docshell is currently executing the onLoad Handler
		 */
		Boolean IsExecutingOnLoadHandler { get; }

		nsILayoutHistoryState LayoutHistoryState { get; set; }

		Boolean ShouldSaveLayoutState { get; }

		/**
		 * The SecureBrowserUI object for this docshell.  This is set by XUL
		 * <browser> or nsWebBrowser for their root docshell.
		 */
		nsISecureBrowserUI SecurityUI { get; set; }

		/**
		 * Cancel the XPCOM timers for each meta-refresh URI in this docshell,
		 * and this docshell's children, recursively. The meta-refresh timers can be
		 * restarted using resumeRefreshURIs().  If the timers are already suspended,
		 * this has no effect.
		 */
		void SuspendRefreshURIs();

		/**
		 * Restart the XPCOM timers for each meta-refresh URI in this docshell,
		 * and this docshell's children, recursively.  If the timers are already
		 * running, this has no effect.
		 */
		void ResumeRefreshURIs();

		/**
		 * Begin firing WebProgressListener notifications for restoring a page
		 * presentation. |viewer| is the content viewer whose document we are
		 * starting to load.  If null, it defaults to the docshell's current content
		 * viewer, creating one if necessary.  |top| should be true for the toplevel
		 * docshell that is being restored; it will be set to false when this method
		 * is called for child docshells.  This method will post an event to
		 * complete the simulated load after returning to the event loop.
		 */
		void BeginRestore(nsIContentViewer viewer, Boolean top);

		/**
		 * Finish firing WebProgressListener notifications and DOM events for
		 * restoring a page presentation.  This should only be called via
		 * beginRestore().
		 */
		void FinishRestore();

		/* Track whether we're currently restoring a document presentation. */
		Boolean RestoringDocument { get; }

		/* attribute to access whether error pages are enabled */
		Boolean UseErrorPages { get; set; }

		/**
		 * Keeps track of the previous SHTransaction index and the current
		 * SHTransaction index at the time that the doc shell begins to load.
		 * Used for ContentViewer eviction.
		 */
		Int32 PreviousTransIndex { get; }
		Int32 LoadedTransIndex { get; }

		/**
		 * Notification that entries have been removed from the beginning of a
		 * nsSHistory which has this as its rootDocShell.
		 *
		 * @param numEntries - The number of entries removed
		 */
		void HistoryPurged(Int32 numEntries);

		/*
		 * Retrieves the WebApps session storage object for the supplied domain.
		 * If it doesn't already exist, a new one will be created.
		 *
		 * @param uri the uri of the storage object to retrieve
		 * @param documentURI new storage will be created with reference to this
		 *                    document.documentURI that will appear in storage event
		 */
		nsIDOMStorage GetSessionStorageForURI(nsIURI uri, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String documentURI);

		/*
		 * Retrieves the WebApps session storage object for the supplied principal.
		 *
		 * @param principal returns a storage for this principal
		 * @param documentURI new storage will be created with reference to this
		 *                    document.documentURI that will appear in storage event
		 * @param create If true and a session storage object doesn't
		 *               already exist, a new one will be created.
		 */
		nsIDOMStorage GetSessionStorageForPrincipal(nsIPrincipal principal,
													[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String documentURI,
													Boolean create);

		/*
		 * Add a WebApps session storage object to the docshell.
		 *
		 * @param principal the principal the storage object is associated with
		 * @param storage the storage object to add
		 */
		void AddSessionStorage(nsIPrincipal principal, nsIDOMStorage storage);

		/**
		 * Gets the channel for the currently loaded document, if any. 
		 * For a new document load, this will be the channel of the previous document
		 * until after OnLocationChange fires.
		 */
		nsIChannel CurrentDocumentChannel { get; }

		/**
		 * Set the offset of this child in its container.
		 */
		void SetChildOffset(UInt32 offset);

		/**
		 * Find out whether the docshell is currently in the middle of a page
		 * transition. This is set just before the pagehide/unload events fire.
		 */
		Boolean IsInUnload { get; }

		/**
		 * Find out if the currently loaded document came from a suspicious channel
		 * (such as a JAR channel where the server-returned content type isn't a
		 * known JAR type).
		 */
		Boolean ChannelIsUnsafe { get; }

		/**
		 * Disconnects this docshell's editor from its window, and stores the
		 * editor data in the open document's session history entry.  This
		 * should be called only during page transitions.
		 */
		void DetachEditorFromWindow();

		/**
		 * If true, this browser is not visible in the traditional sense, but
		 * is actively being rendered to the screen (ex. painted on a canvas)
		 * and should be treated accordingly.
		 **/
		Boolean IsOffScreenBrowser { get; set; }

		/**
		 * If the current content viewer isn't initialized for print preview,
		 * it is replaced with one which is and to which an about:blank document
		 * is loaded.
		 */
		nsIWebBrowserPrint PrintPreview { get; }

		/**
		 * Whether this docshell can execute scripts based on its hierarchy.
		 * The rule of thumb here is that we disable js if this docshell or any
		 * of its parents disallow scripting, unless the only reason for js being
		 * disabled in this docshell is a parent docshell having a document that
		 * is in design mode.  In that case, we explicitly allow scripting on the
		 * current docshell.
		 */
		Boolean CanExecuteScripts { get; }

		/**
		 * Sets whether a docshell is active. An active docshell is one that is
		 * visible, and thus is not a good candidate for certain optimizations
		 * like image frame discarding. Docshells are active unless told otherwise.
		 */
		Boolean IsActive { get; set; }

		/**
		 * The ID of the docshell in the session history.
		 */
		UInt64 HistoryID { get; }

		/**
		 * Sets whether a docshell is an app tab. An app tab docshell may behave
		 * differently than a non-app tab docshell in some cases, such as when
		 * handling link clicks. Docshells are not app tabs unless told otherwise.
		 */
		Boolean IsAppTab { get; set; }

		/**
		 * Create a new about:blank document and content viewer.
		 * @param aPrincipal the principal to use for the new document.
		 */
		void CreateAboutBlankContentViewer(nsIPrincipal aPrincipal);
	}
}
