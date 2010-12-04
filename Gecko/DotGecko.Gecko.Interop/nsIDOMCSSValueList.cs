using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMCSSValueList interface is a datatype for a list of CSS
	 * values in the Document Object Model.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Style
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("8f09fa84-39b9-4dca-9b2f-db0eeb186286"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMCSSValueList : nsIDOMCSSValue
	{
		#region nsIDOMCSSValue Members

		new void GetCssText([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		new void SetCssText([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value); // raises(DOMException) on setting
		new UInt16 CssValueType { get; }

		#endregion

		UInt32 Length { get; }
		nsIDOMCSSValue Item(UInt32 index);
	}
}
