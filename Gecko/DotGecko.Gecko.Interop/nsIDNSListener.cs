using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIDNSListener
	 */
	[ComImport, Guid("41466a9f-f027-487d-a96c-af39e629b8d2"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDNSListener //: nsISupports
	{
		/**
		 * called when an asynchronous host lookup completes.
		 *
		 * @param aRequest
		 *        the value returned from asyncResolve.
		 * @param aRecord
		 *        the DNS record corresponding to the hostname that was resolved.
		 *        this parameter is null if there was an error.
		 * @param aStatus
		 *        if the lookup failed, this parameter gives the reason.
		 */
		void OnLookupComplete(nsICancelable aRequest, nsIDNSRecord aRecord, [MarshalAs(UnmanagedType.U4)] nsResult aStatus);
	}
}
