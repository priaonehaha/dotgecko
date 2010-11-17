using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("73c5fa35-3add-4c87-a303-a850ccf4d65a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMWindow2 : nsIDOMWindow
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
	}
}
