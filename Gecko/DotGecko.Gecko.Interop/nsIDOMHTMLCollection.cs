using System;
using System.Runtime.InteropServices;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMHTMLCollection interface is an interface to a collection
	 * of [X]HTML elements.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-HTML/
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("a6cf9083-15b3-11d2-932e-00805f8add32"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMHTMLCollection //: nsISupports
	{
		UInt32 Length { get; }

		nsIDOMNode Item(UInt32 index);
		nsIDOMNode NamedItem([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name);
	}
}
