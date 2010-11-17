using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("77947960-b4af-11d2-bd93-00805f8ae3f4"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMScreen //: nsISupports
	{
		Int32 Top { get; }
		Int32 Left { get; }
		Int32 Width { get; }
		Int32 Height { get; }
		Int32 PixelDepth { get; }
		Int32 ColorDepth { get; }
		Int32 AvailWidth { get; }
		Int32 AvailHeight { get; }
		Int32 AvailLeft { get; }
		Int32 AvailTop { get; }
	}
}
