using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMWindow interface is the primary interface for a DOM
	 * window object. It represents a single window object that may
	 * contain child windows if the document in the window contains a
	 * HTML frameset document or if the document contains iframe elements.
	 *
	 * This interface is not officially defined by any standard bodies, it
	 * originates from the defacto DOM Level 0 standard.
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("a6cf906b-15b3-11d2-932e-00805f8add32"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMWindow //: nsISupports
	{
		/**
		 * Accessor for the document in this window.
		 */
		nsIDOMDocument Document { get; }

		/**
		 * Accessor for this window's parent window, or the window itself if
		 * there is no parent, or if the parent is of different type
		 * (i.e. this does not cross chrome-content boundaries).
		 */
		nsIDOMWindow Parent { get; }

		/**
		 * Accessor for the root of this hierarchy of windows. This root may
		 * be the window itself if there is no parent, or if the parent is
		 * of different type (i.e. this does not cross chrome-content
		 * boundaries).
		 *
		 * This property is "replaceable" in JavaScript */
		nsIDOMWindow Top { get; }

		/**
		 * Accessor for the object that controls whether or not scrollbars
		 * are shown in this window.
		 *
		 * This attribute is "replaceable" in JavaScript
		 */
		nsIDOMBarProp Scrollbars { get; }

		/**
		 * Accessor for the child windows in this window.
		 */
		nsIDOMWindowCollection Frames { get; }

		/**
		 * Set/Get the name of this window.
		 *
		 * This attribute is "replaceable" in JavaScript
		 */
		void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void SetName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		/**
		 * Set/Get the document scale factor as a multiplier on the default
		 * size. When setting this attribute, a NS_ERROR_NOT_IMPLEMENTED
		 * error may be returned by implementations not supporting
		 * zoom. Implementations not supporting zoom should return 1.0 all
		 * the time for the Get operation. 1.0 is equals normal size,
		 * i.e. no zoom.
		 */
		Single TextZoom { get; set; }

		/**
		 * Accessor for the current x scroll position in this window in
		 * pixels.
		 *
		 * This attribute is "replaceable" in JavaScript
		 */
		Int32 ScrollX { get; }

		/**
		 * Accessor for the current y scroll position in this window in
		 * pixels.
		 *
		 * This attribute is "replaceable" in JavaScript
		 */
		Int32 ScrollY { get; }

		/**
		 * Method for scrolling this window to an absolute pixel offset.
		 */
		void ScrollTo(Int32 xScroll, Int32 yScroll);

		/**
		 * Method for scrolling this window to a pixel offset relative to
		 * the current scroll position.
		 */
		void ScrollBy(Int32 xScrollDif, Int32 yScrollDif);

		/**
		 * Method for accessing this window's selection object.
		 */
		nsISelection GetSelection();

		/**
		 * Method for scrolling this window by a number of lines.
		 */
		void ScrollByLines(Int32 numLines);

		/**
		 * Method for scrolling this window by a number of pages.
		 */
		void ScrollByPages(Int32 numPages);

		/**
		 * Method for sizing this window to the content in the window.
		 */
		void SizeToContent();
	}
}
