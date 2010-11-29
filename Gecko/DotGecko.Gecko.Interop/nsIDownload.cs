using System;
using System.Runtime.InteropServices;
using System.Text;
using PRTime = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Represents a download object.
	 *
	 * @note This object is no longer updated once it enters a completed state.
	 *       Completed states are the following:  
	 *       nsIDownloadManager::DOWNLOAD_FINISHED  
	 *       nsIDownloadManager::DOWNLOAD_FAILED  
	 *       nsIDownloadManager::DOWNLOAD_CANCELED 
	 *       nsIDownloadManager::DOWNLOAD_BLOCKED_PARENTAL 
	 *       nsIDownloadManager::DOWNLOAD_DIRTY 
	 *       nsIDownloadManager::DOWNLOAD_BLOCKED_POLICY 
	 */
	[ComImport, Guid("c891111e-92a6-47b8-bc46-874ebb61ac9d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDownload : nsITransfer
	{
		#region nsIWebProgressListener Members

		new void OnStateChange(nsIWebProgress aWebProgress, nsIRequest aRequest, UInt32 aStateFlags, UInt32 aStatus);
		new void OnProgressChange(nsIWebProgress aWebProgress, nsIRequest aRequest, Int32 aCurSelfProgress, Int32 aMaxSelfProgress, Int32 aCurTotalProgress, Int32 aMaxTotalProgress);
		new void OnLocationChange(nsIWebProgress aWebProgress, nsIRequest aRequest, nsIURI aLocation);
		new void OnStatusChange(nsIWebProgress aWebProgress, nsIRequest aRequest, UInt32 aStatus, [MarshalAs(UnmanagedType.LPWStr)] String aMessage);
		new void OnSecurityChange(nsIWebProgress aWebProgress, nsIRequest aRequest, UInt32 aState);

		#endregion

		#region nsIWebProgressListener2 Members

		new void OnProgressChange64(nsIWebProgress aWebProgress, nsIRequest aRequest, Int64 aCurSelfProgress, Int64 aMaxSelfProgress, Int64 aCurTotalProgress, Int64 aMaxTotalProgress);
		new Boolean OnRefreshAttempted(nsIWebProgress aWebProgress, nsIURI aRefreshURI, Int32 aMillis, Boolean aSameURI);

		#endregion

		#region nsITransfer Members

		new void Init(nsIURI aSource, nsIURI aTarget,
				  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aDisplayName,
				  nsIMIMEInfo aMIMEInfo, PRTime startTime, nsILocalFile aTempFile, nsICancelable aCancelable);

		#endregion

		/**
		 * The target of a download is always a file on the local file system.
		 */
		nsILocalFile TargetFile { get; }

		/**
		 * The percentage of transfer completed.
		 * If the file size is unknown it'll be -1 here.
		 */
		Int32 PercentComplete { get; }

		/**
		 * The amount of bytes downloaded so far.
		 */
		Int64 AmountTransferred { get; }

		/**
		 * The size of file in bytes.
		 * Unknown size is represented by -1.
		 */
		Int64 Size { get; }

		/**
		 * The source of the transfer.
		 */
		nsIURI Source { get; }

		/**
		 * The target of the transfer.
		 */
		nsIURI Target { get; }

		/**
		 * Object that can be used to cancel the download.
		 * Will be null after the download is finished.
		 */
		nsICancelable Cancelable { get; }

		/**
		 * The user-readable description of the transfer.
		 */
		void GetDisplayName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * The time a transfer was started.
		 */
		Int64 StartTime { get; }

		/**
		 * The speed of the transfer in bytes/sec.
		 */
		Double Speed { get; }

		/**
		 * Optional. If set, it will contain the target's relevant MIME information.
		 * This includes its MIME Type, helper app, and whether that helper should be
		 * executed.
		 */
		nsIMIMEInfo MIMEInfo { get; }

		/**
		 * The id of the download that is stored in the database.
		 */
		UInt32 Id { get; }

		/**
		 * The state of the download.
		 * @see nsIDownloadManager and nsIXPInstallManagerUI
		 */
		Int16 State { get; }

		/**
		 * The referrer uri of the download.  This is only valid for HTTP downloads,
		 * and can be null.
		 */
		nsIURI Referrer { get; }

		/**
		 * Indicates if the download can be resumed after being paused or not.  This
		 * is only the case if the download is over HTTP/1.1 or FTP and if the
		 * server supports it.
		 */
		Boolean Resumable { get; }
	}
}
