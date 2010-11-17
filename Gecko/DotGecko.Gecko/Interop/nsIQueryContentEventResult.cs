using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The result of query content events.  succeeded propery can be used always.
	 * Whether other properties can be used or not depends on the event.
	 * See nsIDOMWindowUtils.idl, which properites can be used was documented.
	 */
	[ComImport, Guid("4b4ba266-b51e-4f0f-8d0e-9f13cb2a0056"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIQueryContentEventResult //: nsISupports
	{
		UInt32 Offset { get; }
		Boolean Reversed { get; }

		Int32 Left { get; }
		Int32 Top { get; }
		Int32 Width { get; }
		Int32 Height { get; }
		void GetText([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		Boolean Succeeded { get; }
		Boolean NotFound { get; }
	}
}
