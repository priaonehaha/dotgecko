using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/* A minimally extended progress listener used by download manager
	 * to update its default UI.  This is implemented in nsDownloadProgressListener.js.
	 * See nsIWebProgressListener for documentation, and use its constants.  This isn't
	 * too pretty, but the alternative is having this extend nsIWebProgressListener and
	 * adding an |item| attribute, which would mean a separate nsIDownloadProgressListener
	 * for every nsIDownloadItem, which is a waste...
	 */
	[ComImport, Guid("7acb07ea-cac2-4c15-a3ad-23aaa789ed51"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDownloadProgressListener //: nsISupports
	{
		/**
		 * document
		 * The document of the download manager frontend.
		 */
		nsIDOMDocument Document { get; set; }

		/**
		 * Dispatched whenever the state of the download changes.
		 *
		 * @param aState The previous download sate.
		 * @param aDownload The download object.
		 * @see nsIDownloadManager for download states.
		 */
		void OnDownloadStateChange(Int16 aState, nsIDownload aDownload);

		void OnStateChange(nsIWebProgress aWebProgress,
						   nsIRequest aRequest,
						   UInt32 aStateFlags,
						   [MarshalAs(UnmanagedType.U4)] nsResult aStatus,
						   nsIDownload aDownload);

		void OnProgressChange(nsIWebProgress aWebProgress,
							  nsIRequest aRequest,
							  Int64 aCurSelfProgress,
							  Int64 aMaxSelfProgress,
							  Int64 aCurTotalProgress,
							  Int64 aMaxTotalProgress,
							  nsIDownload aDownload);

		void OnSecurityChange(nsIWebProgress aWebProgress,
							  nsIRequest aRequest,
							  UInt32 aState,
							  nsIDownload aDownload);
	}
}
