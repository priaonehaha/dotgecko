using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIErrorService: This is an interim service that allows nsresult codes to be mapped to 
	 * string bundles that can be used to look up error messages. String bundle keys can also
	 * be mapped. 
	 *
	 * This service will eventually get replaced by extending xpidl to allow errors to be defined.
	 * (http://bugzilla.mozilla.org/show_bug.cgi?id=13423).
	 */
	[ComImport, Guid("e72f94b2-5f85-11d4-9877-00c04fa0cf4a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIErrorService //: nsISupports 
	{
		/**
		 * Registers a string bundle URL for an error module. Error modules are obtained from
		 * nsresult code with NS_ERROR_GET_MODULE.
		 */
		void RegisterErrorStringBundle(Int16 errorModule, [MarshalAs(UnmanagedType.LPStr)] String stringBundleURL);

		/**
		 * Registers a string bundle URL for an error module.
		 */
		void UnregisterErrorStringBundle(Int16 errorModule);

		/**
		 * Retrieves a string bundle URL for an error module.
		 */
		[return: MarshalAs(UnmanagedType.LPStr)]
		String GetErrorStringBundle(Int16 errorModule);

		/**
		 * Registers a key in a string bundle for an nsresult error code. Only the code portion
		 * of the nsresult is used (obtained with NS_ERROR_GET_CODE) in this registration. The
		 * string bundle key is used to look up internationalized messages in the string bundle.
		 */
		void RegisterErrorStringBundleKey([MarshalAs(UnmanagedType.U4)] nsResult error, [MarshalAs(UnmanagedType.LPStr)] String stringBundleKey);

		/**
		 * Unregisters a key in a string bundle for an nsresult error code. 
		 */
		void UnregisterErrorStringBundleKey([MarshalAs(UnmanagedType.U4)] nsResult error);

		/**
		 * Retrieves a key in a string bundle for an nsresult error code. If no key is registered
		 * for the specified nsresult's code (obtained with NS_ERROR_GET_CODE), then the stringified
		 * version of the nsresult code is returned.
		 */
		[return: MarshalAs(UnmanagedType.LPStr)]
		string GetErrorStringBundleKey([MarshalAs(UnmanagedType.U4)] nsResult error);
	}
}
