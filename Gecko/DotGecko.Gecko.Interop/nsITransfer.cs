using System;
using System.Runtime.InteropServices;
using PRTime = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("23c51569-e9a1-4a92-adeb-3723db82ef7c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsITransfer : nsIWebProgressListener2
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

		/**
		 * Initializes the transfer with certain properties.  This function must
		 * be called prior to accessing any properties on this interface.
		 *
		 * @param aSource The source URI of the transfer. Must not be null.
		 *
		 * @param aTarget The target URI of the transfer. Must not be null.
		 *
		 * @param aDisplayName The user-readable description of the transfer.
		 *                     Can be empty.
		 *
		 * @param aMIMEInfo The MIME info associated with the target,
		 *                  including MIME type and helper app when appropriate.
		 *                  This parameter is optional.
		 *
		 * @param startTime Time when the download started (ie, when the first
		 *                  response from the server was received)
		 *                  XXX presumably wbp and exthandler do this differently
		 *
		 * @param aTempFile The location of a temporary file; i.e. a file in which
		 *                  the received data will be stored, but which is not
		 *                  equal to the target file. (will be moved to the real
		 *                  target by the caller, when the download is finished)
		 *                  May be null.
		 *
		 * @param aCancelable An object that can be used to abort the download.
		 *                    Must not be null.
		 *                    Implementations are expected to hold a strong
		 *                    reference to this object until the download is
		 *                    finished, at which point they should release the
		 *                    reference.
		 */
		void Init(nsIURI aSource, nsIURI aTarget,
				  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aDisplayName,
				  nsIMIMEInfo aMIMEInfo, PRTime startTime, nsILocalFile aTempFile, nsICancelable aCancelable);
	}
}
