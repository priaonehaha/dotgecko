using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Each Document has a doctype attribute whose value is either null 
	 * or a DocumentType object. 
	 * The nsIDOMDocumentType interface in the DOM Core provides an 
	 * interface to the list of entities that are defined for the document.
	 *
	 * For more information on this interface please see 
	 * http://www.w3.org/TR/DOM-Level-2-Core/
	 */
	[ComImport, Guid("5725a7de-2883-45a5-878e-30773cd82478"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMDocumentType : nsIDOMNode
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

		void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		nsIDOMNamedNodeMap Entities { get; }
		nsIDOMNamedNodeMap Notations { get; }
		void GetPublicId([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetSystemId([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetInternalSubset([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
	}
}
