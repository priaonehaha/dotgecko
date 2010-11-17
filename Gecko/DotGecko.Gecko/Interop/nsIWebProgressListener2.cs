using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * An extended version of nsIWebProgressListener.
	 */
	[ComImport, Guid("dde39de0-e4e0-11da-8ad9-0800200c9a66"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIWebProgressListener2 : nsIWebProgressListener
	{
		#region nsIWebProgressListener Members

		new void OnStateChange(nsIWebProgress aWebProgress, nsIRequest aRequest, UInt32 aStateFlags, UInt32 aStatus);
		new void OnProgressChange(nsIWebProgress aWebProgress, nsIRequest aRequest, Int32 aCurSelfProgress, Int32 aMaxSelfProgress, Int32 aCurTotalProgress, Int32 aMaxTotalProgress);
		new void OnLocationChange(nsIWebProgress aWebProgress, nsIRequest aRequest, nsIURI aLocation);
		new void OnStatusChange(nsIWebProgress aWebProgress, nsIRequest aRequest, UInt32 aStatus, [MarshalAs(UnmanagedType.LPWStr)] String aMessage);
		new void OnSecurityChange(nsIWebProgress aWebProgress, nsIRequest aRequest, UInt32 aState);

		#endregion

		/**
		 * Notification that the progress has changed for one of the requests
		 * associated with aWebProgress.  Progress totals are reset to zero when all
		 * requests in aWebProgress complete (corresponding to onStateChange being
		 * called with aStateFlags including the STATE_STOP and STATE_IS_WINDOW
		 * flags).
		 *
		 * This function is identical to nsIWebProgressListener::onProgressChange,
		 * except that this function supports 64-bit values.
		 *
		 * @param aWebProgress
		 *        The nsIWebProgress instance that fired the notification.
		 * @param aRequest
		 *        The nsIRequest that has new progress.
		 * @param aCurSelfProgress
		 *        The current progress for aRequest.
		 * @param aMaxSelfProgress
		 *        The maximum progress for aRequest.
		 * @param aCurTotalProgress
		 *        The current progress for all requests associated with aWebProgress.
		 * @param aMaxTotalProgress
		 *        The total progress for all requests associated with aWebProgress.
		 *
		 * NOTE: If any progress value is unknown, then its value is replaced with -1.
		 *
		 * @see nsIWebProgressListener2::onProgressChange64
		 */
		void OnProgressChange64(nsIWebProgress aWebProgress,
								nsIRequest aRequest,
								Int64 aCurSelfProgress,
								Int64 aMaxSelfProgress,
								Int64 aCurTotalProgress,
								Int64 aMaxTotalProgress);

		/**
		 * Notification that a refresh or redirect has been requested in aWebProgress
		 * For example, via a <meta http-equiv="refresh"> or an HTTP Refresh: header
		 *
		 * @param aWebProgress
		 *        The nsIWebProgress instance that fired the notification.
		 * @param aRefreshURI
		 *        The new URI that aWebProgress has requested redirecting to.
		 * @param aMillis
		 *        The delay (in milliseconds) before refresh.
		 * @param aSameURI
		 *        True if aWebProgress is requesting a refresh of the
		 *        current URI.
		 *        False if aWebProgress is requesting a redirection to
		 *        a different URI.
		 *
		 * @return True if the refresh may proceed.
		 *         False if the refresh should be aborted.
		 */
		Boolean OnRefreshAttempted(nsIWebProgress aWebProgress, nsIURI aRefreshURI, Int32 aMillis, Boolean aSameURI);
	}
}
