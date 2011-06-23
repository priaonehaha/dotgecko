using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * DocumentFragment is a "lightweight" or "minimal" Document object.
	 * nsIDOMDocumentFragment is used in situations where the Document
	 * interface can potentially be a heavyweight interface.
	 *
	 * For more information on this interface please see 
	 * http://www.w3.org/TR/DOM-Level-2-Core/
	 */
	[ComImport, Guid("06376ec5-7c91-45ad-a346-30a06a125935"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMDocumentFragment : nsIDOMNode
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
	}
}
