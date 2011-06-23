using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMDocument interface represents the entire HTML or XML document.
	 * Conceptually, it is the root of the document tree, and provides the 
	 * primary access to the document's data.
	 * Since elements, text nodes, comments, processing instructions, etc. 
	 * cannot exist outside the context of a Document, the nsIDOMDocument 
	 * interface also contains the factory methods needed to create these 
	 * objects.
	 *
	 * For more information on this interface please see 
	 * http://dvcs.w3.org/hg/domcore/raw-file/tip/Overview.html
	 */
	[ComImport, Guid("3e7421c4-9964-4184-8c75-d291eecdba35"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMDocument : nsIDOMNode
	{
		#region nsIDOMNode Members

		new void GetNodeName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void GetNodeValue([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void SetNodeValue([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		new UInt16 NodeType { get; }
		new nsIDOMNode ParentNode { get; }
		new nsIDOMNodeList ChildNodes { get; }
		new nsIDOMNode FirstChild { get; }
		new nsIDOMNode LastChild { get; }
		new nsIDOMNode PreviousSibling { get; }
		new nsIDOMNode NextSibling { get; }
		new nsIDOMNamedNodeMap Attributes { get; }
		new nsIDOMDocument OwnerDocument { get; }
		new nsIDOMNode InsertBefore(nsIDOMNode newChild, nsIDOMNode refChild);
		new nsIDOMNode ReplaceChild(nsIDOMNode newChild, nsIDOMNode oldChild);
		new nsIDOMNode RemoveChild(nsIDOMNode oldChild);
		new nsIDOMNode AppendChild(nsIDOMNode newChild);
		new Boolean HasChildNodes();
		new nsIDOMNode CloneNode(Boolean deep);
		new void Normalize();
		new Boolean IsSupported([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String feature, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String version);
		new void GetNamespaceURI([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void GetPrefix([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void GetLocalName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new Boolean HasAttributes();

		#endregion

		nsIDOMDocumentType GetDoctype();

		nsIDOMDOMImplementation GetImplementation();

		nsIDOMElement GetDocumentElement();

		nsIDOMElement CreateElement([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String tagName); // raises(DOMException);

		nsIDOMDocumentFragment CreateDocumentFragment();

		nsIDOMText CreateTextNode([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String data);

		nsIDOMComment CreateComment([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String data);

		nsIDOMCDATASection CreateCDATASection([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String data); // raises(DOMException);

		nsIDOMProcessingInstruction CreateProcessingInstruction([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String target, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String data); // raises(DOMException);

		nsIDOMAttr CreateAttribute([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name); // raises(DOMException);

		nsIDOMEntityReference CreateEntityReference([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name); // raises(DOMException);

		nsIDOMNodeList GetElementsByTagName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String tagname);

		// Introduced in DOM Level 2:
		nsIDOMNode ImportNode(nsIDOMNode importedNode, Boolean deep); // raises(DOMException);

		// Introduced in DOM Level 2:
		nsIDOMElement CreateElementNS([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String namespaceURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String qualifiedName); // raises(DOMException);

		// Introduced in DOM Level 2:
		nsIDOMAttr CreateAttributeNS([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String namespaceURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String qualifiedName); // raises(DOMException);

		// Introduced in DOM Level 2:
		nsIDOMNodeList GetElementsByTagNameNS([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String namespaceURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String localName);

		// Introduced in DOM Level 2:
		nsIDOMElement GetElementById([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String elementId);

		// Introduced in DOM Level 3:
		void GetInputEncoding([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		// Introduced in DOM Level 3:
		void GetXmlEncoding([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		// Introduced in DOM Level 3:
		Boolean XmlStandalone { get; set; } // raises(DOMException) on setting
		// Introduced in DOM Level 3:
		void GetXmlVersion([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void SetXmlVersion([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value); // raises(DOMException) on setting
		// Introduced in DOM Level 3:
		Boolean StrictErrorChecking { get; set; }
		// Introduced in DOM Level 3:
		void GetDocumentURI([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void SetDocumentURI([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		// Introduced in DOM Level 3:
		nsIDOMNode AdoptNode(nsIDOMNode source); // raises(DOMException);
		// Introduced in DOM Level 3:
		nsIDOMDOMConfiguration DomConfig { get; }
		// Introduced in DOM Level 3:
		void NormalizeDocument();
		// Introduced in DOM Level 3:
		nsIDOMNode RenameNode(nsIDOMNode node,
							  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String namespaceURI,
							  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String qualifiedName); // raises(DOMException);
	}
}
