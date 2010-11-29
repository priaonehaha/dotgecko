using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * @status DEPRECATED  Replaced by the NS_DebugBreak function.
	 * @status FROZEN
	 */
	[ComImport, Guid("3bf0c3d7-3bd9-4cf2-a971-33572c503e1e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDebug //: nsISupports
	{
		void Assertion([MarshalAs(UnmanagedType.LPStr)] String aStr, [MarshalAs(UnmanagedType.LPStr)] String aExpr, [MarshalAs(UnmanagedType.LPStr)] String aFile, Int32 aLine);

		void Warning([MarshalAs(UnmanagedType.LPStr)] String aStr, [MarshalAs(UnmanagedType.LPStr)] String aFile, Int32 aLine);

		void Break([MarshalAs(UnmanagedType.LPStr)] String aFile, Int32 aLine);

		void Abort([MarshalAs(UnmanagedType.LPStr)] String aFile, Int32 aLine);
	}
}
