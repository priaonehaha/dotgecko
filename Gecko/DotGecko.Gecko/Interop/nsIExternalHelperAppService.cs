using System;
using System.Runtime.InteropServices;
using System.Text;
using PRTime = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The external helper app service is used for finding and launching
	 * platform specific external applications for a given mime content type.
	 */
	[ComImport, Guid("9e456297-ba3e-42b1-92bd-b7db014268cb"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIExternalHelperAppService //: nsISupports
	{
		/**
		 * Binds an external helper application to a stream listener. The caller
		 * should pump data into the returned stream listener. When the OnStopRequest
		 * is issued, the stream listener implementation will launch the helper app
		 * with this data.
		 * @param aMimeContentType The content type of the incoming data
		 * @param aRequest The request corresponding to the incoming data
		 * @param aWindowContext Use GetInterface to retrieve properties like the
		 *                       dom window or parent window...
		 *                       The service might need this in order to bring up dialogs.
		 * @param aForceSave True to always save this content to disk, regardless of
		 *                   nsIMIMEInfo and other such influences.
		 * @return A nsIStreamListener which the caller should pump the data into.
		 */
		nsIStreamListener DoContent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String aMimeContentType,
									nsIRequest aRequest,
									nsIInterfaceRequestor aWindowContext,
									Boolean aForceSave);

		/**
		 * Returns true if data from a URL with this extension combination
		 * is to be decoded from aEncodingType prior to saving or passing
		 * off to helper apps, false otherwise.
		 */
		Boolean ApplyDecodingForExtension([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aExtension,
										  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String aEncodingType);
	}

	/**
	 * This is a private interface shared between external app handlers and the platform specific
	 * external helper app service
	 */
	[ComImport, Guid("d0b5d7d3-9565-403d-9fb5-e5089c4567c6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsPIExternalAppLauncher //: nsISupports
	{
		/**
		 * mscott --> eventually I should move this into a new service so other
		 * consumers can add temporary files they want deleted on exit.
		 * @param aTemporaryFile A temporary file we should delete on exit.
		 */
		void DeleteTemporaryFileOnExit(nsIFile aTemporaryFile);
	}

	/**
	 * A helper app launcher is a small object created to handle the launching
	 * of an external application.
	 *
	 * Note that cancelling the load via the nsICancelable interface will release
	 * the reference to the launcher dialog.
	 */
	[ComImport, Guid("d9a19faf-497b-408c-b995-777d956b72c0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIHelperAppLauncher : nsICancelable
	{
		#region nsICancelable Members

		new void Cancel([MarshalAs(UnmanagedType.U4)] nsResult aReason);

		#endregion

		/**
		 * The mime info object associated with the content type this helper app
		 * launcher is currently attempting to load
		 */
		nsIMIMEInfo MIMEInfo { get; }

		/**
		 * The source uri
		 */
		nsIURI Source { get; }

		/**
		 * The suggested name for this file
		 */
		void GetSuggestedFileName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * Called when we want to just save the content to a particular file.
		 * NOTE: This will release the reference to the nsIHelperAppLauncherDialog.
		 * @param aNewFileLocation Location where the content should be saved
		 */
		void SaveToDisk(nsIFile aNewFileLocation, Boolean aRememberThisPreference);

		/**
		 * Use aApplication to launch with this content.
		 * NOTE: This will release the reference to the nsIHelperAppLauncherDialog.
		 * @param aApplication nsIFile corresponding to the location of the application to use.
		 * @param aRememberThisPreference TRUE if we should remember this choice.
		 */
		void LaunchWithApplication(nsIFile aApplication, Boolean aRememberThisPreference);

		/**
		 * The following methods are used by the progress dialog to get or set
		 * information on the current helper app launcher download.
		 * This reference will be released when the download is finished (after the
		 * listener receives the STATE_STOP notification).
		 */
		void SetWebProgressListener(nsIWebProgressListener2 aWebProgressListener);

		/**
		 * when the stand alone progress window actually closes, it calls this method
		 * so we can release any local state...
		 */
		void CloseProgressWindow();

		/**
		 * The file we are saving to
		 */
		nsIFile TargetFile { get; }

		/**
		 * The executable-ness of the target file
		 */
		Boolean TargetFileIsExecutable { get; }

		/**
		 * Time when the download started
		 */
		PRTime TimeDownloadStarted { get; }

		/**
		 * The download content length, or -1 if the length is not available.
		 */
		Int64 ContentLength { get; }
	}
}
