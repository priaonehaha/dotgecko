using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Corresponds to http://www.w3.org/TR/2004/REC-DOM-Level-3-Core-20040407
	 */
	[ComImport, Guid("0bbae65c-1dde-11d9-8c46-000a95dc234c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMDOMStringList //: nsISupports
	{
		void Item(UInt32 index, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		UInt32 Length { get; }
		Boolean Contains([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String str);
	}
}
