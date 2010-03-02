using System;
using System.Runtime.InteropServices;
using DOMString = DotGecko.Gecko.Interop.nsAString;

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
	 * http://www.w3.org/TR/DOM-Level-2-Core/
	 *
	 * @status FROZEN
	 */
	[ComImport]
	[Guid("a6cf9075-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMDocument : nsIDOMNode
	{
		#region nsIDOMNode Members

		new void GetNodeName(DOMString result);
		new void GetNodeValue(DOMString result);
		new void SetNodeValue(DOMString value);
		new UInt16 GetNodeType();
		new nsIDOMNode GetParentNode();
		new nsIDOMNodeList GetChildNodes();
		new nsIDOMNode GetFirstChild();
		new nsIDOMNode GetLastChild();
		new nsIDOMNode GetPreviousSibling();
		new nsIDOMNode GetNextSibling();
		new nsIDOMNamedNodeMap GetAttributes();
		new nsIDOMDocument GetOwnerDocument();
		new nsIDOMNode InsertBefore(nsIDOMNode newChild, nsIDOMNode refChild);
		new nsIDOMNode ReplaceChild(nsIDOMNode newChild, nsIDOMNode oldChild);
		new nsIDOMNode RemoveChild(nsIDOMNode oldChild);
		new nsIDOMNode AppendChild(nsIDOMNode newChild);
		new Boolean HasChildNodes();
		new nsIDOMNode CloneNode(Boolean deep);
		new void Normalize();
		new Boolean IsSupported(DOMString feature, DOMString version);
		new void GetNamespaceURI(DOMString result);
		new void GetPrefix(DOMString result);
		new void SetPrefix(DOMString value);
		new void GetLocalName(DOMString result);
		new Boolean HasAttributes();

		#endregion

		nsIDOMDocumentType GetDoctype();

		nsIDOMDOMImplementation GetImplementation();

		nsIDOMElement GetDocumentElement();

		nsIDOMElement CreateElement(DOMString tagName); // raises(DOMException);

		nsIDOMDocumentFragment CreateDocumentFragment();

		nsIDOMText CreateTextNode(DOMString data);

		nsIDOMComment CreateComment(DOMString data);

		nsIDOMCDATASection CreateCDATASection(DOMString data); // raises(DOMException);

		nsIDOMProcessingInstruction CreateProcessingInstruction(DOMString target, DOMString data); // raises(DOMException);

		nsIDOMAttr CreateAttribute(DOMString name); // raises(DOMException);

		nsIDOMEntityReference CreateEntityReference(DOMString name); // raises(DOMException);

		nsIDOMNodeList GetElementsByTagName(DOMString tagname);

		// Introduced in DOM Level 2:
		nsIDOMNode ImportNode(nsIDOMNode importedNode, Boolean deep); // raises(DOMException);

		// Introduced in DOM Level 2:
		nsIDOMElement CreateElementNS(DOMString namespaceURI, DOMString qualifiedName); // raises(DOMException);

		// Introduced in DOM Level 2:
		nsIDOMAttr CreateAttributeNS(DOMString namespaceURI, DOMString qualifiedName); // raises(DOMException);

		// Introduced in DOM Level 2:
		nsIDOMNodeList GetElementsByTagNameNS(DOMString namespaceURI, DOMString localName);

		// Introduced in DOM Level 2:
		nsIDOMElement GetElementById(DOMString elementId);
	}
}
