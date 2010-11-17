using System;
using System.Runtime.InteropServices;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("f6134680-f28b-11d2-8360-c90899049c3c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMPluginArray //: nsISupports
	{
		UInt32 Length { get; }

		nsIDOMPlugin Item(UInt32 index);
		nsIDOMPlugin NamedItem([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name);

		void Refresh([Optional] Boolean reloadDocuments);
	}
}
