using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("ce572460-b0f2-4650-a9e7-c53a99d3b6ad"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIBoxObject //: nsISupports
	{
		nsIDOMElement Element { get; }

		Int32 X { get; }
		Int32 Y { get; }
		Int32 ScreenX { get; }
		Int32 ScreenY { get; }
		Int32 Width { get; }
		Int32 Height { get; }

		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports GetPropertyAsSupports([MarshalAs(UnmanagedType.LPWStr)] String propertyName);
		void SetPropertyAsSupports([MarshalAs(UnmanagedType.LPWStr)] String propertyName, [MarshalAs(UnmanagedType.IUnknown)] nsISupports value);
		[return: MarshalAs(UnmanagedType.LPWStr)]
		String GetProperty([MarshalAs(UnmanagedType.LPWStr)] String propertyName);
		void SetProperty([MarshalAs(UnmanagedType.LPWStr)] String propertyName, [MarshalAs(UnmanagedType.LPWStr)] String propertyValue);
		void RemoveProperty([MarshalAs(UnmanagedType.LPWStr)] String propertyName);

		// for stepping through content in the expanded dom with box-ordinal-group order
		nsIDOMElement ParentBox { get; }
		nsIDOMElement FirstChild { get; }
		nsIDOMElement LastChild { get; }
		nsIDOMElement NextSibling { get; }
		nsIDOMElement PreviousSibling { get; }
	}
}
