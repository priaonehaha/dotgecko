using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	public static class nsIDOMCSSValueConstants
	{
		// UnitTypes
		public const UInt16 CSS_INHERIT = 0;
		public const UInt16 CSS_PRIMITIVE_VALUE = 1;
		public const UInt16 CSS_VALUE_LIST = 2;
		public const UInt16 CSS_CUSTOM = 3;
	}

	/**
	 * The nsIDOMCSSValue interface is a datatype for a CSS value in the
	 * Document Object Model.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Style
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("009f7ea5-9e80-41be-b008-db62f10823f2"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMCSSValue //: nsISupports
	{
		void GetCssText([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetCssText([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value); // raises(DOMException) on setting

		UInt16 CssValueType { get; }
	}
}
