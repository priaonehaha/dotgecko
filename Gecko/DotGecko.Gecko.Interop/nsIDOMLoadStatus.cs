using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	public static class nsIDOMLoadStatusConstants
	{
		public const UInt16 UNINITIALIZED = 0;
		public const UInt16 REQUESTED = 1;
		public const UInt16 RECEIVING = 2;
		public const UInt16 LOADED = 3;
	}

	[ComImport, Guid("2cb53a8a-d2f4-4ddf-874f-3bc2d595c41a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMLoadStatus //: nsISupports
	{
		nsIDOMNode Source { get; }
		void GetUri([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		Int32 TotalSize { get; }
		Int32 LoadedSize { get; }
		UInt16 ReadyState { get; }
		UInt16 Status { get; }
	}
}
