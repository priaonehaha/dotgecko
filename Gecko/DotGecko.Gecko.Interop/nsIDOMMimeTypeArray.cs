using System;
using System.Runtime.InteropServices;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("f6134683-f28b-11d2-8360-c90899049c3c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMMimeTypeArray //: nsISupports
	{
		UInt32 Length { get; }

		nsIDOMMimeType Item(UInt32 index);
		nsIDOMMimeType NamedItem([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name);
	}
}
