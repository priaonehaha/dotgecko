using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("2F276982-0D60-4377-A595-D350BA516395"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWindowMediatorListener //: nsISupports
	{
		void OnWindowTitleChange(nsIXULWindow window, [MarshalAs(UnmanagedType.LPWStr)] String newTitle);

		void OnOpenWindow(nsIXULWindow window);
		void OnCloseWindow(nsIXULWindow window);
	}
}
