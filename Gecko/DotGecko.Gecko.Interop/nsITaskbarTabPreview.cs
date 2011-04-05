using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/*
	 * nsITaskbarTabPreview
	 *
	 * This interface controls tab preview-specific behavior. Creating an
	 * nsITaskbarTabPreview for a window will hide that window's
	 * nsITaskbarWindowPreview in the taskbar - the native API performs this
	 * unconditionally. When there are no more tab previews for a window, the
	 * nsITaskbarWindowPreview will automatically become visible again.
	 *
	 * An application may have as many tab previews per window as memory allows.
	 *
	 */
	[ComImport, Guid("11E4C8BD-5C2D-4E1A-A9A1-79DD5B0FE544"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsITaskbarTabPreview : nsITaskbarPreview
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
		 * The title displayed above the thumbnail
		 *
		 * Default: an empty string
		 */
		void GetTitle([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetTitle([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		/**
		 * The icon displayed next to the title in the preview
		 *
		 * Default: null
		 */
		imgIContainer Icon { get; set; }

		/**
		 * Rearranges the preview relative to another tab preview from the same window
		 * @param aNext The preview to the right of this one. A value of null
		 *              indicates that the preview is the rightmost one.
		 */
		void Move(nsITaskbarTabPreview aNext);

		/**
		 * Used internally to grab the handle to the proxy window.
		 */
		IntPtr GetHWND();

		/**
		 * Used internally to ensure that the taskbar knows about this preview. If a
		 * preview is not registered, then the API call to set its sibling (via move)
		 * will silently fail.
		 *
		 * This method is only invoked when it is safe to make taskbar API calls.
		 */
		void EnsureRegistration();
	}
}
