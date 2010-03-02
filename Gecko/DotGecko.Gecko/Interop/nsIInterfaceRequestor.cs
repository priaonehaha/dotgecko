using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIInterfaceRequestor interface defines a generic interface for 
	 * requesting interfaces that a given object might provide access to.
	 * This is very similar to QueryInterface found in nsISupports.  
	 * The main difference is that interfaces returned from GetInterface()
	 * are not required to provide a way back to the object implementing this 
	 * interface.  The semantics of QI() dictate that given an interface A that 
	 * you QI() on to get to interface B, you must be able to QI on B to get back 
	 * to A.  This interface however allows you to obtain an interface C from A 
	 * that may or most likely will not have the ability to get back to A. 
	 *
	 * @status FROZEN
	 */
	[ComImport]
	[Guid("033A1470-8B2A-11d3-AF88-00A024FFC08C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIInterfaceRequestor //: nsISupports
	{
		/**
		 * Retrieves the specified interface pointer.
		 *
		 * @param uuid The IID of the interface being requested.
		 * @param result [out] The interface pointer to be filled in if
		 *               the interface is accessible.
		 * @return NS_OK - interface was successfully returned.
		 *         NS_NOINTERFACE - interface not accessible.
		 *         NS_ERROR* - method failure.
		 */
		[PreserveSig]
		UInt32 GetInterface(ref Guid uuid, [MarshalAs(UnmanagedType.Interface, IidParameterIndex = 0)] out Object result);
	}
}
