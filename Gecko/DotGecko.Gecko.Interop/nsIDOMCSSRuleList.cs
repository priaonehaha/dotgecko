using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMCSSRuleList interface is a datatype for a list of CSS
	 * style rules in the Document Object Model.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Style
	 */
	[ComImport, Guid("a6cf90c0-15b3-11d2-932e-00805f8add32"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMCSSRuleList //: nsISupports
	{
		UInt32 Length { get; }
		nsIDOMCSSRule Item(UInt32 index);
	}
}
