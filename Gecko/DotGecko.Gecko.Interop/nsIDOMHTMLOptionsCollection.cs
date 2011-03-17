using System;
using System.Runtime.InteropServices;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMHTMLOptionsCollection interface is the interface to a
	 * collection of [X]HTML option elements.
	 *
	 * This interface is trying to follow the DOM Level 2 HTML specification:
	 * http://www.w3.org/TR/DOM-Level-2-HTML/
	 *
	 * with changes from the work-in-progress WHATWG HTML specification:
	 * http://www.whatwg.org/specs/web-apps/current-work/
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
