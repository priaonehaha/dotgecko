using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsITaskbarPreviewButton
	 *
	 * Provides access to a window preview's toolbar button's properties.
	 */
	[ComImport, Guid("CED8842D-FE37-4767-9A8E-FDFA56510C75"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsITaskbarPreviewButton //: nsISupports
	{
		/**
		 * The button's tooltip.
		 *
		 * Default: an empty string
		 */
		void GetTooltip([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetTooltip([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		/**
		 * True if the array of previews should be dismissed when this button is clicked.
		 *
		 * Default: false
		 */
		Boolean DismissOnClick { get; set; }

		/**
		 * True if the taskbar should draw a border around this button's image.
		 *
		 * Default: true
		 */
		Boolean HasBorder { get; set; }

		/**
		 * True if the button is disabled. This is not the same as visible.
		 *
		 * Default: false
		 */
		Boolean Disabled { get; set; }

		/**
		 * The icon used for the button.
		 *
		 * Default: null
		 */
		imgIContainer Image { get; set; }

		/**
		 * True if the button is shown. Buttons that are invisible do not
		 * participate in the layout of buttons underneath the preview.
		 *
		 * Default: false
		 */
		Boolean Visible { get; set; }
	}
}
