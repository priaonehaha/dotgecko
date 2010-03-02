using System;
using System.Runtime.InteropServices;
using DOMString = DotGecko.Gecko.Interop.nsAString;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Objects implementing the nsIDOMNamedNodeMap interface are used to 
	 * represent collections of nodes that can be accessed by name.
	 *
	 * For more information on this interface please see 
	 * http://www.w3.org/TR/DOM-Level-2-Core/
	 *
	 * @status FROZEN
	 */
	[ComImport]
	[Guid("a6cf907b-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMNamedNodeMap //: nsISupports
	{
		nsIDOMNode GetNamedItem(DOMString name);

		nsIDOMNode SetNamedItem(nsIDOMNode arg); // raises(DOMException);

		nsIDOMNode RemoveNamedItem(DOMString name); // raises(DOMException);

		nsIDOMNode Item(UInt32 index);

		UInt32 GetLength();

		// Introduced in DOM Level 2:
		nsIDOMNode GetNamedItemNS(DOMString namespaceURI, DOMString localName);

		// Introduced in DOM Level 2:
		nsIDOMNode SetNamedItemNS(nsIDOMNode arg); // raises(DOMException);

		// Introduced in DOM Level 2:
		nsIDOMNode RemoveNamedItemNS(DOMString namespaceURI, DOMString localName); // raises(DOMException);
	}
}
