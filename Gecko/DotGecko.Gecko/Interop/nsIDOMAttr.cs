using System;
using System.Runtime.InteropServices;
using DOMString = DotGecko.Gecko.Interop.nsAString;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMAttr interface represents an attribute in an "Element" object. 
	 * Typically the allowable values for the attribute are defined in a document 
	 * type definition.
	 * 
	 * For more information on this interface please see 
	 * http://www.w3.org/TR/DOM-Level-2-Core/
	 *
	 * @status FROZEN
	 */
	[ComImport]
	[Guid("a6cf9070-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	interface nsIDOMAttr : nsIDOMNode
	{
		void GetName(DOMString result);

		Boolean GetSpecified();

		void GetValue(DOMString result);
		void SetValue(DOMString value); // raises(DOMException) on setting

		// Introduced in DOM Level 2:
		nsIDOMElement GetOwnerElement();
	}
}
