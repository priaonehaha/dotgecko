using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	public static class nsIWebBrowserPersistConstants
	{
		/** No special persistence behaviour. */
		public const UInt32 PERSIST_FLAGS_NONE = 0;
		/** Only use cached data (could result in failure if data is not cached). */
		public const UInt32 PERSIST_FLAGS_FROM_CACHE = 1;
		/** Bypass the cached data. */
		public const UInt32 PERSIST_FLAGS_BYPASS_CACHE = 2;
		/** Ignore any redirected data (usually adverts). */
		public const UInt32 PERSIST_FLAGS_IGNORE_REDIRECTED_DATA = 4;
		/** Ignore IFRAME content (usually adverts). */
		public const UInt32 PERSIST_FLAGS_IGNORE_IFRAMES = 8;
		/** Do not run the incoming data through a content converter e.g. to decompress it */
		public const UInt32 PERSIST_FLAGS_NO_CONVERSION = 16;
		/** Replace existing files on the disk (use with due diligence!) */
		public const UInt32 PERSIST_FLAGS_REPLACE_EXISTING_FILES = 32;
		/** Don't modify or add base tags */
		public const UInt32 PERSIST_FLAGS_NO_BASE_TAG_MODIFICATIONS = 64;
		/** Make changes to original dom rather than cloning nodes */
		public const UInt32 PERSIST_FLAGS_FIXUP_ORIGINAL_DOM = 128;
		/** Fix links relative to destination location (not origin) */
		public const UInt32 PERSIST_FLAGS_FIXUP_LINKS_TO_DESTINATION = 256;
		/** Don't make any adjustments to links */
		public const UInt32 PERSIST_FLAGS_DONT_FIXUP_LINKS = 512;
		/** Force serialization of output (one file at a time; not concurrent) */
		public const UInt32 PERSIST_FLAGS_SERIALIZE_OUTPUT = 1024;
		/** Don't make any adjustments to filenames */
		public const UInt32 PERSIST_FLAGS_DONT_CHANGE_FILENAMES = 2048;
		/** Fail on broken inline links */
		public const UInt32 PERSIST_FLAGS_FAIL_ON_BROKEN_LINKS = 4096;
		/**
		 * Automatically cleanup after a failed or cancelled operation, deleting all
		 * created files and directories. This flag does nothing for failed upload
		 * operations to remote servers.
		 */
		public const UInt32 PERSIST_FLAGS_CLEANUP_ON_FAILURE = 8192;
		/**
		 * Let the WebBrowserPersist decide whether the incoming data is encoded
		 * and whether it needs to go through a content converter e.g. to
		 * decompress it.
		 */
		public const UInt32 PERSIST_FLAGS_AUTODETECT_APPLY_CONVERSION = 16384;
		/**
		 * Append the downloaded data to the target file.
		 * This can only be used when persisting to a local file.
		 */
		public const UInt32 PERSIST_FLAGS_APPEND_TO_FILE = 32768;

		/**
		 * Force relevant cookies to be sent with this load even if normally they
		 * wouldn't be.
		 */
		public const UInt32 PERSIST_FLAGS_FORCE_ALLOW_COOKIES = 65536;

		/** Persister is ready to save data */
		public const UInt32 PERSIST_STATE_READY = 1;
		/** Persister is saving data */
		public const UInt32 PERSIST_STATE_SAVING = 2;
		/** Persister has finished saving data */
		public const UInt32 PERSIST_STATE_FINISHED = 3;

		/** Output only the current selection as opposed to the whole document. */
		public const UInt32 ENCODE_FLAGS_SELECTION_ONLY = 1;
		/**
		 * For plaintext output. Convert html to plaintext that looks like the html.
		 * Implies wrap (except inside &lt;pre&gt;), since html wraps.
		 * HTML output: always do prettyprinting, ignoring existing formatting.
		 */
		public const UInt32 ENCODE_FLAGS_FORMATTED = 2;
		/**
		 * Output without formatting or wrapping the content. This flag
		 * may be used to preserve the original formatting as much as possible.
		 */
		public const UInt32 ENCODE_FLAGS_RAW = 4;
		/** Output only the body section, no HTML tags. */
		public const UInt32 ENCODE_FLAGS_BODY_ONLY = 8;
		/** Wrap even if when not doing formatted output (e.g. for text fields). */
		public const UInt32 ENCODE_FLAGS_PREFORMATTED = 16;
		/** Wrap documents at the specified column. */
		public const UInt32 ENCODE_FLAGS_WRAP = 32;
		/**
		 * For plaintext output. Output for format flowed (RFC 2646). This is used
		 * when converting to text for mail sending. This differs just slightly
		 * but in an important way from normal formatted, and that is that
		 * lines are space stuffed. This can't (correctly) be done later.
		 */
		public const UInt32 ENCODE_FLAGS_FORMAT_FLOWED = 64;
		/** Convert links to absolute links where possible. */
		public const UInt32 ENCODE_FLAGS_ABSOLUTE_LINKS = 128;

		/** 
		 * Attempt to encode entities standardized at W3C (HTML, MathML, etc).
		 * This is a catch-all flag for documents with mixed contents. Beware of
		 * interoperability issues. See below for other flags which might likely
		 * do what you want.
		 */
		public const UInt32 ENCODE_FLAGS_ENCODE_W3C_ENTITIES = 256;

		/**
		 * Output with carriage return line breaks. May also be combined with
		 * ENCODE_FLAGS_LF_LINEBREAKS and if neither is specified, the platform
		 * default format is used.
		 */
		public const UInt32 ENCODE_FLAGS_CR_LINEBREAKS = 512;
		/**
		 * Output with linefeed line breaks. May also be combined with
		 * ENCODE_FLAGS_CR_LINEBREAKS and if neither is specified, the platform
		 * default format is used.
		 */
		public const UInt32 ENCODE_FLAGS_LF_LINEBREAKS = 1024;
		/** For plaintext output. Output the content of noscript elements. */
		public const UInt32 ENCODE_FLAGS_NOSCRIPT_CONTENT = 2048;
		/** For plaintext output. Output the content of noframes elements. */
		public const UInt32 ENCODE_FLAGS_NOFRAMES_CONTENT = 4096;

		/**
		 * Encode basic entities, e.g. output &nbsp; instead of character code 0xa0. 
		 * The basic set is just &nbsp; &amp; &lt; &gt; &quot; for interoperability
		 * with older products that don't support &alpha; and friends.
		 */
		public const UInt32 ENCODE_FLAGS_ENCODE_BASIC_ENTITIES = 8192;
		/**
		 * Encode Latin1 entities. This includes the basic set and
		 * accented letters between 128 and 255.
		 */
		public const UInt32 ENCODE_FLAGS_ENCODE_LATIN1_ENTITIES = 16384;
		/**
		 * Encode HTML4 entities. This includes the basic set, accented
		 * letters, greek letters and certain special markup symbols.
		 */
		public const UInt32 ENCODE_FLAGS_ENCODE_HTML_ENTITIES = 32768;
	}

	/**
	 * Interface for persisting DOM documents and URIs to local or remote storage.
	 */
	[ComImport, Guid("dd4e0a6a-210f-419a-ad85-40e8543b9465"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWebBrowserPersist : nsICancelable
	{
		#region nsICancelable Members

		new void Cancel([MarshalAs(UnmanagedType.U4)] nsResult aReason);

		#endregion

		/**
		 * Flags governing how data is fetched and saved from the network. 
		 * It is best to set this value explicitly unless you are prepared
		 * to accept the default values.
		 */
		UInt32 PersistFlags { get; set; }

		/**
		 * Current state of the persister object.
		 */
		UInt32 CurrentState { get; }

		/**
		 * Value indicating the success or failure of the persist
		 * operation.
		 *
		 * @return NS_OK Operation was successful or is still ongoing.
		 * @return NS_BINDING_ABORTED Operation cancelled.
		 * @return NS_ERROR_FAILURE Non-specific failure.
		 */
		UInt32 Result { get; }

		/**
		 * Callback listener for progress notifications. The object that the
		 * embbedder supplies may also implement nsIInterfaceRequestor and be
		 * prepared to return nsIAuthPrompt or other interfaces that may be required
		 * to download data.
		 *
		 * @see nsIAuthPrompt
		 * @see nsIInterfaceRequestor
		 */
		nsIWebProgressListener ProgressListener { get; set; }

		/**
		 * Save the specified URI to file.
		 *
		 * @param aURI       URI to save to file. Some implementations of this interface
		 *                   may also support <CODE>nsnull</CODE> to imply the currently
		 *                   loaded URI.
		 * @param aCacheKey  An object representing the URI in the cache or
		 *                   <CODE>nsnull</CODE>.  This can be a necko cache key,
		 *                   an nsIWebPageDescriptor, or the currentDescriptor of an
		 *                   nsIWebPageDescriptor.
		 * @param aReferrer  The referrer URI to pass with an HTTP request or
		 *                   <CODE>nsnull</CODE>.
		 * @param aPostData  Post data to pass with an HTTP request or
		 *                   <CODE>nsnull</CODE>.
		 * @param aExtraHeaders Additional headers to supply with an HTTP request
		 *                   or <CODE>nsnull</CODE>.
		 * @param aFile      Target file. This may be a nsILocalFile object or an
		 *                   nsIURI object with a file scheme or a scheme that
		 *                   supports uploading (e.g. ftp).
		 *
		 * @see nsILocalFile
		 * @see nsIURI
		 * @see nsIInputStream
		 *
		 * @return NS_OK Operation has been started.
		 * @return NS_ERROR_INVALID_ARG One or more arguments was invalid.
		 */
		void SaveURI(nsIURI aURI, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aCacheKey,
			nsIURI aReferrer, nsIInputStream aPostData, [MarshalAs(UnmanagedType.LPStr)] String aExtraHeaders, nsISupports aFile);

		/**
		 * Save a channel to a file. It must not be opened yet.
		 * @see saveURI
		 */
		void CaveChannel(nsIChannel aChannel, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aFile);

		/**
		 * Save the specified DOM document to file and optionally all linked files
		 * (e.g. images, CSS, JS & subframes). Do not call this method until the
		 * document has finished loading!
		 *
		 * @param aDocument          Document to save to file. Some implementations of
		 *                           this interface may also support <CODE>nsnull</CODE>
		 *                           to imply the currently loaded document.
		 * @param aFile              Target local file. This may be a nsILocalFile object or an
		 *                           nsIURI object with a file scheme or a scheme that
		 *                           supports uploading (e.g. ftp).
		 * @param aDataPath          Path to directory where URIs linked to the document
		 *                           are saved or nsnull if no linked URIs should be saved.
		 *                           This may be a nsILocalFile object or an nsIURI object
		 *                           with a file scheme.
		 * @param aOutputContentType The desired MIME type format to save the 
		 *                           document and all subdocuments into or nsnull to use
		 *                           the default behaviour.
		 * @param aEncodingFlags     Flags to pass to the encoder.
		 * @param aWrapColumn        For text documents, indicates the desired width to
		 *                           wrap text at. Parameter is ignored if wrapping is not
		 *                           specified by the encoding flags.
		 *
		 * @see nsILocalFile
		 * @see nsIURI
		 *
		 * @return NS_OK Operation has been started.
		 * @return NS_ERROR_INVALID_ARG One or more arguments was invalid.
		 */
		void SaveDocument(nsIDOMDocument aDocument,
		   [MarshalAs(UnmanagedType.IUnknown)] nsISupports aFile, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aDataPath,
		   [MarshalAs(UnmanagedType.LPStr)] String aOutputContentType, UInt32 aEncodingFlags,
		   UInt32 aWrapColumn);

		/**
		 * Cancels the current operation. The caller is responsible for cleaning up
		 * partially written files or directories. This has the same effect as calling
		 * cancel with an argument of NS_BINDING_ABORTED.
		 */
		void CancelSave();
	}
}
