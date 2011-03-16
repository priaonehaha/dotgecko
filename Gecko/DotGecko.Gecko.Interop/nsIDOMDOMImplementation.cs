using System;
using System.Runtime.InteropServices;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMDOMImplementation interface provides a number of methods for 
	 * performing operations that are independent of any particular instance 
	 * of the document object model.
	 *
	 * For more information on this interface please see 
	 * http://www.w3.org/TR/DOM-Level-2-Core/
	 */
	[ComImport, Guid("03a6f574-99ec-42f8-9e6c-812a4a9bcbf7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMDOMImplementation //: nsISupports
	{
		Boolean HasFeature([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String feature, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String version);

		nsIDOMDocumentType CreateDocumentType(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String qualifiedName,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String publicId,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String systemId); // raises(DOMException);

		nsIDOMDocument CreateDocument([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String namespaceURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String qualifiedName, nsIDOMDocumentType doctype); // raises(DOMException);

		/**
		 * Returns an HTML document with a basic DOM already constructed and with an
		 * appropriate title element.
		 *
		 * @param title the title of the Document
		 * @see <http://www.whatwg.org/html/#creating-documents>
		 */
		nsIDOMDocument SreateHTMLDocument([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String title);
	}
}
