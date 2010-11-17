using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("896d1d20-b4c4-11d2-bd93-00805f8ae3f4"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMHistory //: nsISupports
	{
		Int32 Length { get; }
		void GetCurrent([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetPrevious([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetNext([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		void Back();
		void Forward();

		void Go([Optional] Int32 aDelta);
		void Item(UInt32 index, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
	}
}
