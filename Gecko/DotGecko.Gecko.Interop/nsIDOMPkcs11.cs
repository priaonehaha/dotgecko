using System;
using System.Runtime.InteropServices;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("9fd42950-25e7-11d4-8a7d-006008c844c3"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMPkcs11 //: nsISupports
	{
		Int32 DeleteModule([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String moduleName);
		Int32 DddModule([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String moduleName, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String libraryFullPath, Int32 cryptoMechanismFlags, Int32 cipherFlags);
	}
}
