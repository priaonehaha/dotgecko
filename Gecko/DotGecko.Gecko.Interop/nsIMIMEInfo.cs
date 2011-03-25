using System;
using System.Runtime.InteropServices;
using System.Text;
using nsHandlerInfoAction = System.Int32;

namespace DotGecko.Gecko.Interop
{
	public static class nsIHandlerInfoConstants
	{
		public const Int32 saveToDisk = 0;
		/**
		 * Used to indicate that we know nothing about what to do with this.  You
		 * could consider this to be not initialized.
		 */
		public const Int32 alwaysAsk = 1;
		public const Int32 useHelperApp = 2;
		public const Int32 handleInternally = 3;
		public const Int32 useSystemDefault = 4;
	}

	/**
	 * nsIHandlerInfo gives access to the information about how a given protocol
	 * scheme or MIME-type is handled.
	 */
	[ComImport, Guid("325e56a7-3762-4312-aec7-f1fcf84b4145"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIHandlerInfo //: nsISupports
	{
		/**
		 * The type of this handler info.  For MIME handlers, this is the MIME type.
		 * For protocol handlers, it's the scheme.
		 * 
		 * @return String representing the type.
		 */
		void GetType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);

		/**
		 * A human readable description of the handler type
		 */
		void GetDescription([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		void SetDescription([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);

		/**
		 * The application the user has said they want associated with this content
		 * type. This is not always guaranteed to be set!!
		 */
		nsIHandlerApp PreferredApplicationHandler { get; set; }

		/**
		 * Applications that can handle this content type.
		 *
		 * The list will include the preferred handler, if any.  Elements of this
		 * array are nsIHandlerApp objects, and this attribute will always reference
		 * an array, whether or not there are any possible handlers.  If there are
		 * no possible handlers, the array will contain no elements, so just check
		 * its length (nsIArray::length) to see if there are any possible handlers.
		 */
		nsIMutableArray PossibleApplicationHandlers { get; }

		/**
		 * Indicates whether a default application handler exists,
		 * i.e. whether launchWithFile with action = useSystemDefault is possible
		 * and defaultDescription will contain usable information.
		 */
		Boolean HasDefaultHandler { get; }

		/**
		 * A pretty name description of the associated default application. Only
		 * usable if hasDefaultHandler is true.
		 */
		void GetDefaultDescription([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		void SetDefaultDescription([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);

		/**
		 * Launches the application with the specified URI, in a way that
		 * depends on the value of preferredAction. preferredAction must be
		 * useHelperApp or useSystemDefault.
		 *  
		 * @note Only the URI scheme is used to determine how to launch.  This is
		 * essentially a pass-by-value operation.  This means that in the case of
		 * a file: URI, the handler that is registered for file: will be launched
		 * and our code will not make any decision based on the content-type or
		 * extension, though the invoked file: handler is free to do so. 
		 *
		 * @param aURI
		 *        The URI to launch this application with
		 *
		 * @param aWindowContext 
		 *        The window to parent the dialog against, and, if a web handler
		 *        is chosen, it is loaded in this window as well.  See 
		 *        nsIHandlerApp.launchWithURI for more details.
		 *
		 * @throw NS_ERROR_INVALID_ARG if preferredAction is not valid for this
		 * call. Other exceptions may be thrown.
		 */
		void LaunchWithURI(nsIURI aURI, [Optional] nsIInterfaceRequestor aWindowContext);

		/**
		 * preferredAction is how the user specified they would like to handle
		 * this content type: save to disk, use specified helper app, use OS
		 * default handler or handle using navigator; possible value constants
		 * listed below
		 */
		nsHandlerInfoAction PreferredAction { get; set; }

		/**
		 * alwaysAskBeforeHandling: if true, we should always give the user a
		 * dialog asking how to dispose of this content.
		 */
		Boolean AlwaysAskBeforeHandling { get; set; }
	}

	/**
	 * nsIMIMEInfo extends nsIHandlerInfo with a bunch of information specific to
	 * MIME content-types. There is a one-to-many relationship between MIME types
	 * and file extensions. This means that a MIMEInfo object may have multiple
	 * file extensions associated with it.  However, the reverse is not true.
	 *
	 * MIMEInfo objects are generally retrieved from the MIME Service
	 * @see nsIMIMEService
	 */
	[ComImport, Guid("1c21acef-c7a1-40c6-9d40-a20480ee53a1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIMIMEInfo : nsIHandlerInfo
	{
		#region nsIHandlerInfo Members

		new void GetType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);
		new void GetDescription([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		new void SetDescription([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
		new nsIHandlerApp PreferredApplicationHandler { get; set; }
		new nsIMutableArray PossibleApplicationHandlers { get; }
		new Boolean HasDefaultHandler { get; }
		new void GetDefaultDescription([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		new void SetDefaultDescription([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
		new void LaunchWithURI(nsIURI aURI, [Optional] nsIInterfaceRequestor aWindowContext);
		new nsHandlerInfoAction PreferredAction { get; set; }
		new Boolean AlwaysAskBeforeHandling { get; set; }

		#endregion

		/**
		 * Gives you an array of file types associated with this type.
		 *
		 * @return Number of elements in the array.
		 * @return Array of extensions.
		 */
		nsIUTF8StringEnumerator GetFileExtensions();

		/**
		 * Set File Extensions. Input is a comma delimited list of extensions.
		 */
		void SetFileExtensions([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aExtensions);

		/**
		 * Returns whether or not the given extension is
		 * associated with this MIME info.
		 *
		 * @return TRUE if the association exists. 
		 */
		Boolean ExtensionExists([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aExtension);

		/**
		 * Append a given extension to the set of extensions
		 */
		void AppendExtension([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aExtension);

		/**
		 * Returns the first extension association in
		 * the internal set of extensions.
		 *
		 * @return The first extension.
		 */
		void GetPrimaryExtension([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		void SetPrimaryExtension([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);

		/**
		 * The MIME type of this MIMEInfo.
		 * 
		 * @return String representing the MIME type.
		 * 
		 * @deprecated  use nsIHandlerInfo::type instead.
		 */
		void GetMIMEType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);

		/**
		 * Returns whether or not these two nsIMIMEInfos are logically
		 * equivalent.
		 *
		 * @returns PR_TRUE if the two are considered equal
		 */
		Boolean Equals(nsIMIMEInfo aMIMEInfo);

		/** 
		 * Returns a list of nsILocalHandlerApp objects containing
		 * handlers associated with this mimeinfo. Implemented per 
		 * platform using information in this object to generate the
		 * best list. Typically used for an "open with" style user 
		 * option.
		 * 
		 * @return nsIArray of nsILocalHandlerApp
		 */
		nsIArray PossibleLocalHandlers { get; }

		/**
		 * Launches the application with the specified file, in a way that
		 * depends on the value of preferredAction. preferredAction must be
		 * useHelperApp or useSystemDefault.
		 *
		 * @param aFile The file to launch this application with.
		 *
		 * @throw NS_ERROR_INVALID_ARG if action is not valid for this function.
		 * Other exceptions may be thrown.
		 */
		void LaunchWithFile(nsIFile aFile);
	}

	/**
	 * nsIHandlerApp represents an external application that can handle content
	 * of some sort (either a MIME type or a protocol).
	 *
	 * FIXME: now that we've made nsIWebHandlerApp inherit from nsIHandlerApp,
	 * we should also try to make nsIWebContentHandlerInfo inherit from or possibly
	 * be replaced by nsIWebHandlerApp (bug 394710).
	 */
	[ComImport, Guid("8BDF20A4-9170-4548-AF52-78311A44F920"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIHandlerApp //: nsISupports
	{
		/**
		 * Human readable name for the handler
		 */
		void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		void SetName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);

		/**
		 * Detailed description for this handler. Suitable for
		 * a tooltip or short informative sentence.
		 */
		void GetDetailedDescription([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		void SetDetailedDescription([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);

		/**
		 * Whether or not the given handler app is logically equivalent to the
		 * invokant (i.e. they represent the same app).
		 * 
		 * Two apps are the same if they are both either local or web handlers
		 * and their executables/URI templates and command line parameters are
		 * the same.
		 *
		 * @param aHandlerApp the handler app to compare to the invokant
		 *
		 * @returns true if the two are logically equivalent, false otherwise
		 */
		Boolean Equals(nsIHandlerApp aHandlerApp);

		/**
		 * Launches the application with the specified URI.
		 *
		 * @param aURI
		 *        The URI to launch this application with
		 *
		 * @param aWindowContext 
		 *
		 *        Currently only relevant to web-handler apps.  If given, this
		 *        represents the docshell to load the handler in and is passed
		 *        through to nsIURILoader.openURI.  If this parameter is null or
		 *        not present, the web handler app implementation will attempt to 
		 *        find/create a place to load the handler and do so.  As of this
		 *        writing, it tries to load the web handler in a new window using
		 *        nsIBrowserDOMWindow.openURI.  In the future, it may attempt to 
		 *        have a more comprehensive strategy which could include handing
		 *        off to the system default browser (bug 394479).
		 */
		void LaunchWithURI(nsIURI aURI, [Optional] nsIInterfaceRequestor aWindowContext);
	}

	/**
	 * nsILocalHandlerApp is a local OS-level executable
	 */
	[ComImport, Guid("D36B6329-52AE-4f45-80F4-B2536AE5F8B2"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsILocalHandlerApp : nsIHandlerApp
	{
		#region nsIHandlerApp Members

		new void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		new void SetName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
		new void GetDetailedDescription([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		new void SetDetailedDescription([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
		new Boolean Equals(nsIHandlerApp aHandlerApp);
		new void LaunchWithURI(nsIURI aURI, [Optional] nsIInterfaceRequestor aWindowContext);

		#endregion

		/**
		 * Pointer to the executable file used to handle content
		 */
		nsIFile Executable { get; set; }

		/**
		 * Returns the current number of command line parameters.
		 */
		UInt32 ParameterCount { get; }

		/**
		 * Clears the current list of command line parameters.
		 */
		void ClearParameters();

		/**
		 * Appends a command line parameter to the command line
		 * parameter list.
		 *
		 * @param param the parameter to add.
		 */
		void AppendParameter([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String param);

		/**
		 * Retrieves a specific command line parameter.
		 *
		 * @param param the index of the parameter to return.
		 *
		 * @return the parameter string.
		 *
		 * @throw NS_ERROR_INVALID_ARG if the index is out of range.
		 */
		void GetParameter(UInt32 parameterIndex, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * Checks to see if a parameter exists in the command line
		 * parameter list.
		 *
		 * @param param the parameter to search for.
		 *
		 * @return TRUE if the parameter exists in the current list. 
		 */
		Boolean ParameterExists([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String param);
	}

	/**
	 * nsIWebHandlerApp is a web-based handler, as speced by the WhatWG HTML5
	 * draft.  Currently, only GET-based handlers are supported.  At some point, 
	 * we probably want to work with WhatWG to spec out and implement POST-based
	 * handlers as well.
	 */
	[ComImport, Guid("7521a093-c498-45ce-b462-df7ba0d882f6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWebHandlerApp : nsIHandlerApp
	{
		#region nsIHandlerApp Members

		new void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		new void SetName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
		new void GetDetailedDescription([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		new void SetDetailedDescription([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
		new Boolean Equals(nsIHandlerApp aHandlerApp);
		new void LaunchWithURI(nsIURI aURI, [Optional] nsIInterfaceRequestor aWindowContext);

		#endregion

		/**
		 * Template used to construct the URI to GET.  Template is expected to have
		 * a %s in it, and the escaped URI to be handled is inserted in place of 
		 * that %s, as per the HTML5 spec.
		 */
		void GetUriTemplate([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		void SetUriTemplate([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);
	}

	/**
	 * nsIDBusHandlerApp represents local applications launched by DBus a message
	 * invoking a method taking a single string argument descibing a URI
	 */
	[ComImport, Guid("1ffc274b-4cbf-4bb5-a635-05ad2cbb6534"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDBusHandlerApp : nsIHandlerApp
	{
		#region nsIHandlerApp Members

		new void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		new void SetName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
		new void GetDetailedDescription([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		new void SetDetailedDescription([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
		new Boolean Equals(nsIHandlerApp aHandlerApp);
		new void LaunchWithURI(nsIURI aURI, [Optional] nsIInterfaceRequestor aWindowContext);

		#endregion

		/**
		 * Service defines the dbus service that should handle this protocol.
		 * If its not set,  NS_ERROR_FAILURE will be returned by LaunchWithURI
		 */
		void GetService([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		void SetService([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);

		/**
		 * Objpath defines the object path of the dbus service that should handle 
		 * this protocol. If its not set,  NS_ERROR_FAILURE will be returned 
		 * by LaunchWithURI
		 */
		void GetObjectPath([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		void SetObjectPath([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);

		/**
		 * DBusInterface defines the interface of the dbus service that should 
		 * handle this protocol. If its not set,  NS_ERROR_FAILURE will be  
		 * returned by LaunchWithURI
		 */
		void GetDBusInterface([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		void SetDBusInterface([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);

		/**
		 * Method defines the dbus method that should be invoked to handle this 
		 * protocol. If its not set,  NS_ERROR_FAILURE will be returned by 
		 * LaunchWithURI
		 */
		void GetMethod([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		void SetMethod([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);
	}
}
