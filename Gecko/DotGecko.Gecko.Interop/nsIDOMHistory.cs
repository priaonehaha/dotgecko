using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("208f2af7-9f2e-497c-8a53-9e7803280898"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
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
	}

	[ComImport, Guid("949fcdc1-664b-4a4b-939a-7144c94b48ac"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMHistory_MOZILLA_2_0_BRANCH //: nsISupports
	{
		nsIVariant State { get; }
	}
}
