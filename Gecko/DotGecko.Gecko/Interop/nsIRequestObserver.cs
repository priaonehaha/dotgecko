using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIRequestObserver
	 *
	 * @status FROZEN
	 */
	[ComImport]
	[Guid("fd91e2e0-1481-11d3-9333-00104ba0fd40")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIRequestObserver //: nsISupports
	{
		/**
		 * Called to signify the beginning of an asynchronous request.
		 *
		 * @param aRequest request being observed
		 * @param aContext user defined context
		 *
		 * An exception thrown from onStartRequest has the side-effect of
		 * causing the request to be canceled.
		 */
		void OnStartRequest(nsIRequest aRequest, [MarshalAs(UnmanagedType.IUnknown)] Object aContext);

		/**
		 * Called to signify the end of an asynchronous request.  This
		 * call is always preceded by a call to onStartRequest.
		 *
		 * @param aRequest request being observed
		 * @param aContext user defined context
		 * @param aStatusCode reason for stopping (NS_OK if completed successfully)
		 *
		 * An exception thrown from onStopRequest is generally ignored.
		 */
		void OnStopRequest(nsIRequest aRequest, [MarshalAs(UnmanagedType.IUnknown)] Object aContext, UInt32 aStatusCode);
	}
}
