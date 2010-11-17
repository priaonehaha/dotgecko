using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("16da46c0-208d-11d4-8a7c-006008c844c3"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMCRMFObject //: nsISupports
	{
		void GetRequest([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
	}
}
