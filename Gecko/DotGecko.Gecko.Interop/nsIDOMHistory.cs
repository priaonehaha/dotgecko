using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("d5a3006b-dd6b-4ba3-81be-6559f8889e60"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMHistory //: nsISupports
	{
		Int32 Length { get; }
		void GetCurrent([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetPrevious([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetNext([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		void Back();
		void Forward();

		void Go([Optional] Int32 aDelta);
		void Item(UInt32 index, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		void PushState(nsIVariant aData,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String aTitle,
			[Optional, In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String aURL);
		void ReplaceState(nsIVariant aData,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String aTitle,
			[Optional, In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String aURL);

		nsIVariant State { get; }
	}
}
