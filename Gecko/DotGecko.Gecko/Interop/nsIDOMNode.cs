using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	// Constants for nsIDOMNode ( "a6cf907c-15b3-11d2-932e-00805f8add32" ) interface
	internal static class nsIDOMNodeConstants
	{
		internal const UInt16 ELEMENT_NODE = 1;
		internal const UInt16 ATTRIBUTE_NODE = 2;
		internal const UInt16 TEXT_NODE = 3;
		internal const UInt16 CDATA_SECTION_NODE = 4;
		internal const UInt16 ENTITY_REFERENCE_NODE = 5;
		internal const UInt16 ENTITY_NODE = 6;
		internal const UInt16 PROCESSING_INSTRUCTION_NODE = 7;
		internal const UInt16 COMMENT_NODE = 8;
		internal const UInt16 DOCUMENT_NODE = 9;
		internal const UInt16 DOCUMENT_TYPE_NODE = 10;
		internal const UInt16 DOCUMENT_FRAGMENT_NODE = 11;
		internal const UInt16 NOTATION_NODE = 12;
	}

	/**
	 * The nsIDOMNode interface is the primary datatype for the entire 
	 * Document Object Model.
	 * It represents a single node in the document tree.
	 *
	 * For more information on this interface please see 
	 * http://www.w3.org/TR/DOM-Level-2-Core/
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("a6cf907c-15b3-11d2-932e-00805f8add32"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMNode //: nsISupports
	{
		void GetNodeName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		void GetNodeValue([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result); // raises(DOMException) on retrieval
		void SetNodeValue([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);  // raises(DOMException) on setting

		UInt16 NodeType { get; }

		nsIDOMNode ParentNode { get; }

		nsIDOMNodeList ChildNodes { get; }

		nsIDOMNode FirstChild { get; }

		nsIDOMNode LastChild { get; }

		nsIDOMNode PreviousSibling { get; }

		nsIDOMNode NextSibling { get; }

		nsIDOMNamedNodeMap Attributes { get; }

		// Modified in DOM Level 2:
		nsIDOMDocument OwnerDocument { get; }

		nsIDOMNode InsertBefore(nsIDOMNode newChild, nsIDOMNode refChild); // raises(DOMException);

		nsIDOMNode ReplaceChild(nsIDOMNode newChild, nsIDOMNode oldChild); // raises(DOMException);

		nsIDOMNode RemoveChild(nsIDOMNode oldChild); // raises(DOMException);

		nsIDOMNode AppendChild(nsIDOMNode newChild); // raises(DOMException);

		Boolean HasChildNodes();

		nsIDOMNode CloneNode(Boolean deep);

		// Modified in DOM Level 2:
		void Normalize();

		// Introduced in DOM Level 2:
		Boolean IsSupported([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String feature, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String version);

		// Introduced in DOM Level 2:
		void GetNamespaceURI([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		// Introduced in DOM Level 2:
		void GetPrefix([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void SetPrefix([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value); // raises(DOMException) on setting

		// Introduced in DOM Level 2:
		void GetLocalName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		// Introduced in DOM Level 2:
		Boolean HasAttributes();
	}
}
