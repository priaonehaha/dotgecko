using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIStreamListener
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("1a637020-1482-11d3-9333-00104ba0fd40"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIStreamListener : nsIRequestObserver
	{
		#region nsIRequestObserver Members

		new void OnStartRequest(nsIRequest aRequest, [MarshalAs(UnmanagedType.IUnknown)] Object aContext);
		new void OnStopRequest(nsIRequest aRequest, [MarshalAs(UnmanagedType.IUnknown)] Object aContext, UInt32 aStatusCode);

		#endregion

		/**
		 * Called when the next chunk of data (corresponding to the request) may
		 * be read without blocking the calling thread.  The onDataAvailable impl
		 * must read exactly |aCount| bytes of data before returning.
		 *
		 * @param aRequest request corresponding to the source of the data
		 * @param aContext user defined context
		 * @param aInputStream input stream containing the data chunk
		 * @param aOffset
		 *        Number of bytes that were sent in previous onDataAvailable calls
		 *        for this request. In other words, the sum of all previous count
		 *        parameters.
		 *        If that number is greater than or equal to 2^32, this parameter
		 *        will be PR_UINT32_MAX (2^32 - 1).
		 * @param aCount number of bytes available in the stream
		 *
		 * NOTE: The aInputStream parameter must implement readSegments.
		 *
		 * An exception thrown from onDataAvailable has the side-effect of
		 * causing the request to be canceled.
		 */
		void OnDataAvailable(nsIRequest aRequest, [MarshalAs(UnmanagedType.IUnknown)] Object aContext, nsIInputStream aInputStream, UInt32 aOffset, UInt32 aCount);
	}
}
