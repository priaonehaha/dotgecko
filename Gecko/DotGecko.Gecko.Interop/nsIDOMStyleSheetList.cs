using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMStyleSheetList interface is a datatype for a style sheet
	 * list in the Document Object Model.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Style
	 */
	[ComImport, Guid("a6cf9081-15b3-11d2-932e-00805f8add32"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMStyleSheetList //: nsISupports
	{
		UInt32 Length { get; }
		nsIDOMStyleSheet Item(UInt32 index);
	}
}
