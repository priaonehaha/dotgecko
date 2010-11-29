using System;
using System.Runtime.InteropServices;
using PRTime = System.UInt64;
using mozIStorageConnection = System.Object;

namespace DotGecko.Gecko.Interop
{
	public static class nsIDownloadManagerUIConstants
	{
		/**
		 * The reason that should be passed when the user requests to show the
		 * download manager's UI.
		 */
		public const Int16 REASON_USER_INTERACTED = 0;

		/**
		 * The reason that should be passed to the show method when we are displaying
		 * the UI because a new download is being added to it.
		 */
		public const Int16 REASON_NEW_DOWNLOAD = 1;
	}

	[ComImport, Guid("ca7663d5-69e3-4c4a-b754-f462bd36b05f"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDownloadManagerUI //: nsISupports
	{
		/**
		 * Shows the Download Manager's UI to the user.
		 *
		 * @param [optional] aWindowContext
		 *        The parent window context to show the UI.
		 * @param [optional] aID
		 *        The id of the download to be preselected upon opening.
		 * @param [optional] aReason
		 *        The reason to show the download manager's UI.  This defaults to
		 *        REASON_USER_INTERACTED, and should be one of the previously listed
		 *        constants.
		 */
		void Show([Optional] nsIInterfaceRequestor aWindowContext, [Optional] UInt32 aID, [Optional] Int16 aReason);

		/**
		 * Indicates if the UI is visible or not.
		 */
		Boolean Visible { get; }

		/**
		 * Brings attention to the UI if it is already visible
		 *
		 * @throws NS_ERROR_UNEXPECTED if the UI is not visible.
		 */
		void GetAttention();
	}
}
