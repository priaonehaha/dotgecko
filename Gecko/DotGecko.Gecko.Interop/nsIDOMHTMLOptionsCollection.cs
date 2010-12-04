using System;
using System.Runtime.InteropServices;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMHTMLOptionsCollection interface is the interface to a
	 * collection of [X]HTML option elements.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-HTML/
	 *
	 * @status FROZEN
	 */
	// Introduced in DOM Level 2:
	[ComImport, Guid("bce0213c-f70f-488f-b93f-688acca55d63"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMHTMLOptionsCollection //: nsISupports
	{
		UInt32 Length { get; set; } // raises(DOMException) on setting

		nsIDOMNode Item(UInt32 index);
		nsIDOMNode NamedItem([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name);
	}
}
