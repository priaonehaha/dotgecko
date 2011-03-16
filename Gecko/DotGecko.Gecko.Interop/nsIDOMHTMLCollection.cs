using System;
using System.Runtime.InteropServices;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMHTMLCollection interface is an interface to a collection
	 * of [X]HTML elements.
	 *
	 * This interface is trying to follow the DOM Level 2 HTML specification:
	 * http://www.w3.org/TR/DOM-Level-2-HTML/
	 *
	 * with changes from the work-in-progress WHATWG HTML specification:
	 * http://www.whatwg.org/specs/web-apps/current-work/
	 */
	[ComImport, Guid("a6cf9083-15b3-11d2-932e-00805f8add32"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMHTMLCollection //: nsISupports
	{
		UInt32 Length { get; }

		nsIDOMNode Item(UInt32 index);
		nsIDOMNode NamedItem([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name);
	}
}
