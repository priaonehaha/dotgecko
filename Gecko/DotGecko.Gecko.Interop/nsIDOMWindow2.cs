using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("efff0d88-3b94-4375-bdeb-676a847ecd7d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMWindow2 : nsIDOMWindow
	{
		#region nsIDOMWindow Members

		new nsIDOMDocument Document { get; }
		new nsIDOMWindow Parent { get; }
		new nsIDOMWindow Top { get; }
		new nsIDOMBarProp Scrollbars { get; }
		new nsIDOMWindowCollection Frames { get; }
		new void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void SetName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);
		new Single TextZoom { get; set; }
		new Int32 ScrollX { get; }
		new Int32 ScrollY { get; }
		new void ScrollTo(Int32 xScroll, Int32 yScroll);
		new void ScrollBy(Int32 xScrollDif, Int32 yScrollDif);
		new nsISelection GetSelection();
		new void ScrollByLines(Int32 numLines);
		new void ScrollByPages(Int32 numPages);
		new void SizeToContent();

		#endregion

		/**
		 * Get the window root for this window. This is useful for hooking
		 * up event listeners to this window and every other window nested
		 * in the window root.
		 */
		nsIDOMEventTarget WindowRoot { get; }

		/**
		 * Get the application cache object for this window.
		 */
		nsIDOMOfflineResourceList ApplicationCache { get; }

		/**
		 * Deprecated, but can't remove yet since we don't want to change interfaces.
		 */
		void CreateBlobURL(nsIDOMBlob blob, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void RevokeBlobURL([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String URL);
	}
}
