using System;
using System.Runtime.InteropServices;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMDocumentEvent interface is the interface to the event
	 * factory method on a DOM document object.
	 *
	 * For more information on this interface please see 
	 * http://www.w3.org/TR/DOM-Level-2-Events/
	 */
	[ComImport, Guid("46b91d66-28e2-11d4-ab1e-0010830123b4"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMDocumentEvent //: nsISupports
	{
		nsIDOMEvent CreateEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String eventType); // raises(DOMException);
	}
}
