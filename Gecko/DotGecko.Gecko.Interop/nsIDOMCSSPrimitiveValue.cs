using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	public static class nsIDOMCSSPrimitiveValueConstants
	{
		// UnitTypes
		public const UInt16 CSS_UNKNOWN = 0;
		public const UInt16 CSS_NUMBER = 1;
		public const UInt16 CSS_PERCENTAGE = 2;
		public const UInt16 CSS_EMS = 3;
		public const UInt16 CSS_EXS = 4;
		public const UInt16 CSS_PX = 5;
		public const UInt16 CSS_CM = 6;
		public const UInt16 CSS_MM = 7;
		public const UInt16 CSS_IN = 8;
		public const UInt16 CSS_PT = 9;
		public const UInt16 CSS_PC = 10;
		public const UInt16 CSS_DEG = 11;
		public const UInt16 CSS_RAD = 12;
		public const UInt16 CSS_GRAD = 13;
		public const UInt16 CSS_MS = 14;
		public const UInt16 CSS_S = 15;
		public const UInt16 CSS_HZ = 16;
		public const UInt16 CSS_KHZ = 17;
		public const UInt16 CSS_DIMENSION = 18;
		public const UInt16 CSS_STRING = 19;
		public const UInt16 CSS_URI = 20;
		public const UInt16 CSS_IDENT = 21;
		public const UInt16 CSS_ATTR = 22;
		public const UInt16 CSS_COUNTER = 23;
		public const UInt16 CSS_RECT = 24;
		public const UInt16 CSS_RGBCOLOR = 25;
	}

	/**
	 * The nsIDOMCSSPrimitiveValue interface is a datatype for a primitive
	 * CSS value in the Document Object Model.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Style
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("e249031f-8df9-4e7a-b644-18946dce0019"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMCSSPrimitiveValue : nsIDOMCSSValue
	{
		#region nsIDOMCSSValue Members

		new void GetCssText([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		new void SetCssText([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value); // raises(DOMException) on setting
		new UInt16 CssValueType { get; }

		#endregion

		UInt16 PrimitiveType { get; }

		void SetFloatValue(UInt16 unitType, Single floatValue); //raises(DOMException);
		Single GetFloatValue(UInt16 unitType); //raises(DOMException);
		void SetStringValue(UInt16 stringType, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String stringValue); //raises(DOMException);
		void GetStringValue([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval); //raises(DOMException);
		nsIDOMCounter GetCounterValue(); //raises(DOMException);
		nsIDOMRect GetRectValue(); //raises(DOMException);
		nsIDOMRGBColor GetRGBColorValue(); //raises(DOMException);
	}
}
