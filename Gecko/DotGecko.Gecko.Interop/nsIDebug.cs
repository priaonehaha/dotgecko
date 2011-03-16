using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 *   For use by consumers in scripted languages (JavaScript, Java, Python,
	 *   Perl, ...).
	 *
	 * @note C/C++ consumers who are planning to use the nsIDebug interface with
	 *   the "@mozilla.org/xpcom;1" contract should use NS_DebugBreak from xpcom
	 *   glue instead.
	 *
	 */
	[ComImport, Guid("3bf0c3d7-3bd9-4cf2-a971-33572c503e1e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDebug //: nsISupports
	{
		/**
		 * Show an assertion and trigger nsIDebug.break().
		 *
		 * @param aStr assertion message
		 * @param aExpr expression that failed
		 * @param aFile file containing assertion
		 * @param aLine line number of assertion
		 *
		 */
		void Assertion([MarshalAs(UnmanagedType.LPStr)] String aStr, [MarshalAs(UnmanagedType.LPStr)] String aExpr, [MarshalAs(UnmanagedType.LPStr)] String aFile, Int32 aLine);

		/**
		 * Show a warning.
		 *
		 * @param aStr warning message
		 * @param aFile file containing assertion
		 * @param aLine line number of assertion
		 */
		void Warning([MarshalAs(UnmanagedType.LPStr)] String aStr, [MarshalAs(UnmanagedType.LPStr)] String aFile, Int32 aLine);

		/**
		 * Request to break into a debugger.
		 *
		 * @param aFile file containing break request
		 * @param aLine line number of break request
		 */
		void Break([MarshalAs(UnmanagedType.LPStr)] String aFile, Int32 aLine);

		/**
		 * Request the process to trigger a fatal abort.
		 *
		 * @param aFile file containing abort request
		 * @param aLine line number of abort request
		 */
		void Abort([MarshalAs(UnmanagedType.LPStr)] String aFile, Int32 aLine);
	}
}
