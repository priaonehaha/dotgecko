using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	public static class nsIDOMCSSRuleConstants
	{
		// RuleType
		public const UInt16 UNKNOWN_RULE = 0;
		public const UInt16 STYLE_RULE = 1;
		public const UInt16 CHARSET_RULE = 2;
		public const UInt16 IMPORT_RULE = 3;
		public const UInt16 MEDIA_RULE = 4;
		public const UInt16 FONT_FACE_RULE = 5;
		public const UInt16 PAGE_RULE = 6;
	}

	/**
	 * The nsIDOMCSSRule interface is a datatype for a CSS style rule in
	 * the Document Object Model.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Style
	 */
	[ComImport, Guid("a6cf90c1-15b3-11d2-932e-00805f8add32"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMCSSRule //: nsISupports
	{
		UInt16 Type { get; }
		void GetCssText([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetCssText([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value); // raises(DOMException) on setting

		nsIDOMCSSStyleSheet ParentStyleSheet { get; }
		nsIDOMCSSRule ParentRule { get; }
	}
}
