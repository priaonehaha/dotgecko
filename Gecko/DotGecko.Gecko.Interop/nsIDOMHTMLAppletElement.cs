using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMHTMLAppletElement interface is the interface to a [X]HTML
	 * applet element.
	 *
	 * This interface is trying to follow the DOM Level 2 HTML specification:
	 * http://www.w3.org/TR/DOM-Level-2-HTML/
	 *
	 * with changes from the work-in-progress WHATWG HTML specification:
	 * http://www.whatwg.org/specs/web-apps/current-work/
	 */
	[ComImport, Guid("1840a15d-618e-4e22-a53e-56a3624b8ae3"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMHTMLAppletElement : nsIDOMHTMLElement
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

		#region nsIDOMElement Members

		new void GetTagName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void GetAttribute([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void SetAttribute([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		new void RemoveAttribute([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name);
		new nsIDOMAttr GetAttributeNode([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name);
		new nsIDOMAttr SetAttributeNode(nsIDOMAttr newAttr);
		new nsIDOMAttr RemoveAttributeNode(nsIDOMAttr oldAttr);
		new nsIDOMNodeList GetElementsByTagName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name);
		new void GetAttributeNS([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String namespaceURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String localName, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void SetAttributeNS([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String namespaceURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String qualifiedName, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		new void RemoveAttributeNS([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String namespaceURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String localName);
		new nsIDOMAttr GetAttributeNodeNS([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String namespaceURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String localName);
		new nsIDOMAttr SetAttributeNodeNS(nsIDOMAttr newAttr);
		new nsIDOMNodeList GetElementsByTagNameNS([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String namespaceURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String localName);
		new Boolean HasAttribute([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name);
		new Boolean HasAttributeNS([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String namespaceURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String localName);

		#endregion

		#region nsIDOMHTMLElement Members

		new void GetId([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void SetId([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		new void GetTitle([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void SetTitle([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		new void GetLang([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void SetLang([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		new void GetDir([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void SetDir([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		new void GetClassName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void SetClassName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		new void GetAccessKey([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void SetAccessKey([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		new void Blur();
		new void Focus();
		new void Click();

		#endregion

		void GetAlign([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetAlign([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		void GetAlt([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetAlt([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		void GetArchive([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetArchive([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		void GetCode([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetCode([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		void GetCodeBase([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetCodeBase([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		void GetHeight([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetHeight([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		// Modified in DOM Level 2:
		Int32 HSpace { get; set; }
		void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		// Modified in DOM Level 2:
		void GetObject([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetObject([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		// Modified in DOM Level 2:
		Int32 VSpace { get; set; }
		void GetWidth([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetWidth([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
	}
}
