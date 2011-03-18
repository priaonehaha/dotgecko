using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIUnicharStreamListener is very similar to nsIStreamListener with
	 * the difference being that this interface gives notifications about
	 * data being available after the raw data has been converted to
	 * UTF-16.
	 *
	 * nsIUnicharStreamListener
	 */
	[ComImport, Guid("4a7e9b62-fef8-400d-9865-d6820f630b4c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIUnicharStreamListener : nsIRequestObserver
	{
		#region nsIRequestObserver Members

		new void OnStartRequest(nsIRequest aRequest, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aContext);
		new void OnStopRequest(nsIRequest aRequest, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aContext, UInt32 aStatusCode);

		#endregion

		/**
		 * Called when the next chunk of data (corresponding to the
		 * request) is available.
		 *
		 * @param aRequest request corresponding to the source of the data
		 * @param aContext user defined context
		 * @param aData the data chunk
		 *
		 * An exception thrown from onUnicharDataAvailable has the
		 * side-effect of causing the request to be canceled.
		 */
		void OnUnicharDataAvailable(nsIRequest aRequest,
									[MarshalAs(UnmanagedType.IUnknown)] nsISupports aContext,
									[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aData);
	}
}
