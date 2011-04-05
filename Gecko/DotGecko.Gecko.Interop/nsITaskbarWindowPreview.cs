using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	public static class nsITaskbarWindowPreviewConstants
	{
		/**
		 * Max 7 buttons per preview per the Windows Taskbar API
		 */
		public const Int32 NUM_TOOLBAR_BUTTONS = 7;
	}

	/*
	 * nsITaskbarWindowPreview
	 *
	 * This interface represents the preview for a window in the taskbar. By
	 * default, Windows implements much of the behavior for applications by
	 * default. The primary purpose of this interface is to allow Gecko
	 * applications to take control over parts of the preview. Some parts are not
	 * controlled through this interface: the title and icon of the preview match
	 * the title and icon of the window always.
	 *
	 * By default, Windows takes care of drawing the thumbnail and preview for the
	 * application however if enableCustomDrawing is set to true, then the
	 * controller will start to receive drawPreview and drawThumbnail calls as well
	 * as reads on the thumbnailAspectRatio, width and height properties.
	 *
	 * By default, nsITaskbarWindowPreviews are visible. When made invisible, the
	 * window disappears from the list of windows in the taskbar for the
	 * application.
	 *
	 * If the window has any visible nsITaskbarTabPreviews, then the
	 * nsITaskbarWindowPreview for the corresponding window is automatically
	 * hidden. This is not reflected in the visible property. Note that other parts
	 * of the system (such as alt-tab) may still request thumbnails and/or previews
	 * through the nsITaskbarWindowPreview's controller.
	 *
	 * nsITaskbarWindowPreview will never invoke the controller's onClose or
	 * onActivate methods since handling them may conflict with other internal
	 * Gecko state and there is existing infrastructure in place to allow clients
	 * to handle those events 
	 *
	 * Window previews may have a toolbar with up to 7 buttons. See
	 * nsITaskbarPreviewButton for more information about button properties.
	 */
	[ComImport, Guid("EC67CC57-342D-4064-B4C6-74A375E07B10"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsITaskbarWindowPreview : nsITaskbarPreview
	{
		#region nsITaskbarPreview Members

		new nsITaskbarPreviewController Controller { get; set; }
		new void GetTooltip([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		new void SetTooltip([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		new Boolean Visible { get; set; }
		new Boolean Active { get; set; }
		new void Invalidate();

		#endregion

		/**
		 * Gets the nth button for the preview image. By default, all of the buttons
		 * are invisible.
		 *
		 * @see nsITaskbarPreviewButton
		 *
		 * @param index The index into the button array. Must be >= 0 and <
		 *              MAX_TOOLBAR_BUTTONS.
		 */
		nsITaskbarPreviewButton GetButton(UInt32 index);

		/**
		 * Enables/disables custom drawing of thumbnails and previews
		 *
		 * Default value: false
		 */
		Boolean EnableCustomDrawing { get; set; }
	}
}
