using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("f6134682-f28b-11d2-8360-c90899049c3c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMMimeType //: nsISupports
	{
		void GetDescription([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		nsIDOMPlugin EnabledPlugin { get; }
		void GetSuffixes([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
	}
}
