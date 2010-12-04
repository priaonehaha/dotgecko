using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMCSSStyleSheet interface is a datatype for a CSS style
	 * sheet in the Document Object Model.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Style
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("a6cf90c2-15b3-11d2-932e-00805f8add32"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMCSSStyleSheet : nsIDOMStyleSheet
	{
		#region nsIDOMStyleSheet Members

		new void GetType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		new Boolean Disabled { get; set; }
		new nsIDOMNode OwnerNode { get; }
		new nsIDOMStyleSheet ParentStyleSheet { get; }
		new void GetHref([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		new void GetTitle([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		new nsIDOMMediaList Media { get; }

		#endregion

		nsIDOMCSSRule OwnerRule { get; }
		nsIDOMCSSRuleList CssRules { get; }

		UInt32 InsertRule([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String rule, UInt32 index); //raises(DOMException);
		void DeleteRule(UInt32 index); //raises(DOMException);
	}
}
