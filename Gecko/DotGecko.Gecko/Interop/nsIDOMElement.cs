using System;
using System.Runtime.InteropServices;
using DOMString = DotGecko.Gecko.Interop.nsAString;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMElement interface represents an element in an HTML or 
	 * XML document. 
	 *
	 * For more information on this interface please see 
	 * http://www.w3.org/TR/DOM-Level-2-Core/
	 *
	 * @status FROZEN
	 */
	[ComImport]
	[Guid("a6cf9078-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMElement : nsIDOMNode
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

		void GetTagName(DOMString result);

		void GetAttribute(DOMString name, DOMString result);

		void SetAttribute(DOMString name, DOMString value); // raises(DOMException);

		void RemoveAttribute(DOMString name); // raises(DOMException);

		nsIDOMAttr GetAttributeNode(DOMString name);

		nsIDOMAttr SetAttributeNode(nsIDOMAttr newAttr); // raises(DOMException);

		nsIDOMAttr RemoveAttributeNode(nsIDOMAttr oldAttr); // raises(DOMException);

		nsIDOMNodeList GetElementsByTagName(DOMString name);

		// Introduced in DOM Level 2:
		void GetAttributeNS(DOMString namespaceURI, DOMString localName, DOMString result);

		// Introduced in DOM Level 2:
		void SetAttributeNS(DOMString namespaceURI, DOMString qualifiedName, DOMString value); // raises(DOMException);

		// Introduced in DOM Level 2:
		void RemoveAttributeNS(DOMString namespaceURI, DOMString localName); // raises(DOMException);

		// Introduced in DOM Level 2:
		nsIDOMAttr GetAttributeNodeNS(DOMString namespaceURI, DOMString localName);

		// Introduced in DOM Level 2:
		nsIDOMAttr SetAttributeNodeNS(nsIDOMAttr newAttr); // raises(DOMException);

		// Introduced in DOM Level 2:
		nsIDOMNodeList GetElementsByTagNameNS(DOMString namespaceURI, DOMString localName);

		// Introduced in DOM Level 2:
		Boolean HasAttribute(DOMString name);

		// Introduced in DOM Level 2:
		Boolean HasAttributeNS(DOMString namespaceURI, DOMString localName);
	}
}
