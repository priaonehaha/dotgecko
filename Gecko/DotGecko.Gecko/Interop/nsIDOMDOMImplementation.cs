using System;
using System.Runtime.InteropServices;
using DOMString = DotGecko.Gecko.Interop.nsAString;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMDOMImplementation interface provides a number of methods for 
	 * performing operations that are independent of any particular instance 
	 * of the document object model.
	 *
	 * For more information on this interface please see 
	 * http://www.w3.org/TR/DOM-Level-2-Core/
	 *
	 * @status FROZEN
	 */
	[ComImport]
	[Guid("a6cf9074-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMDOMImplementation //: nsISupports
	{
		Boolean HasFeature(DOMString feature, DOMString version);

		nsIDOMDocumentType CreateDocumentType(DOMString qualifiedName, DOMString publicId, DOMString systemId); // raises(DOMException);

		nsIDOMDocument CreateDocument(DOMString namespaceURI, DOMString qualifiedName, nsIDOMDocumentType doctype); // raises(DOMException);
	}
}
