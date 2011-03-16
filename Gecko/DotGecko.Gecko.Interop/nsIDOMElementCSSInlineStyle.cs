using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMElementCSSInlineStyle interface allows access to the inline
	 * style information for elements.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Style
	 */
	[ComImport, Guid("99715845-95fc-4a56-aa53-214b65c26e22"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMElementCSSInlineStyle //: nsISupports
	{
		nsIDOMCSSStyleDeclaration Style { get; }
	}
}
