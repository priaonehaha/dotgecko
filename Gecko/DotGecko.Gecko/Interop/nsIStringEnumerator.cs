using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Used to enumerate over an ordered list of strings.
	 */

	[ComImport, Guid("50d3ef6c-9380-4f06-9fb2-95488f7d141c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIStringEnumerator //: nsISupports
	{
		Boolean HasMore();
		void GetNext([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
	}

	[ComImport, Guid("9bdf1010-3695-4907-95ed-83d0410ec307"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIUTF8StringEnumerator //: nsISupports
	{
		Boolean HasMore();
		void GetNext([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
	}
}
