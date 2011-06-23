using System;
using System.Runtime.InteropServices;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMDOMConfiguration interface represents the configuration
	 * of a document and maintains a table of recognized parameters.
	 *
	 * For more information on this interface, please see
	 * http://www.w3.org/TR/DOM-Level-3-Core/
	 */
	// Introduced in DOM Level 3:
	[ComImport, Guid("cfb5b821-9016-4a79-9d98-87b57c3ea0c7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMDOMConfiguration //: nsISupports
	{
		void SetParameter([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name, nsIVariant value); // raises(DOMException);
		nsIVariant GetParameter([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name); // raises(DOMException);
		Boolean CanSetParameter([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name, nsIVariant value);
	}
}
