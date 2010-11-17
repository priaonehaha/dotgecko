using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	internal static class nsIDOMWindowUtilsConstants
	{
		/**
		 * WARNING: These values must be same as nsIWidget's values.
		 */

		/**
		 * DISABLED means users cannot use IME completely.
		 * Note that this state is *not* same as |ime-mode: disabled;|.
		 */
		internal const UInt32 IME_STATUS_DISABLED = 0;

		/**
		 * ENABLED means users can use all functions of IME. This state is same as
		 * |ime-mode: normal;|.
		 */
		internal const UInt32 IME_STATUS_ENABLED = 1;

		/**
		 * PASSWORD means users cannot use most functions of IME. But on GTK2,
		 * users can use "Simple IM" which only supports dead key inputting.
		 * The behavior is same as the behavior of the native password field.
		 * This state is same as |ime-mode: disabled;|.
		 */
		internal const UInt32 IME_STATUS_PASSWORD = 2;

		/**
		 * PLUGIN means a plug-in has focus. At this time we should not touch to
		 * controlling the IME state.
		 */
		internal const UInt32 IME_STATUS_PLUGIN = 3;
	}

	/**
	 * nsIDOMWindowUtils is intended for infrequently-used methods related
	 * to the current nsIDOMWindow.  Some of the methods may require
	 * elevated privileges; the method implementations should contain the
	 * necessary security checks.  Access this interface by calling
	 * getInterface on a DOMWindow.
	 */
	[ComImport, Guid("6a60fde5-a00a-4732-bbea-2787c174c04f"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMWindowUtils //: nsISupports
	{
		/**
		 * Image animation mode of the window. When this attribute's value
		 * is changed, the implementation should set all images in the window
		 * to the given value. That is, when set to kDontAnimMode, all images
		 * will stop animating. The attribute's value must be one of the
		 * animationMode values from imgIContainer.
		 * @note Images may individually override the window's setting after
		 *       the window's mode is set. Therefore images given different modes
		 *       since the last setting of the window's mode may behave
		 *       out of line with the window's overall mode.
		 * @note The attribute's value is the window's overall mode. It may
		 *       for example continue to report kDontAnimMode after all images
		 *       have subsequently been individually animated.
		 * @note Only images immediately in this window are affected;
		 *       this is not recursive to subwindows.
		 * @see imgIContainer
		 */
		UInt16 ImageAnimationMode { get; set; }

		/**
		 * Whether the charset of the window's current document has been forced by
		 * the user.
		 * Cannot be accessed from unprivileged context (not content-accessible)
		 */
		Boolean DocCharsetIsForced { get; }

		/**
		 * Function to get metadata associated with the window's current document
		 * @param aName the name of the metadata.  This should be all lowercase.
		 * @return the value of the metadata, or the empty string if it's not set
		 *
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges.
		 */
		void GetDocumentMetadata(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aName,
			[In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * Force an immediate redraw of this window.  The parameter specifies
		 * the number of times to redraw, and the return value is the length,
		 * in milliseconds, that the redraws took.  If aCount is not specified
		 * or is 0, it is taken to be 1.
		 */
		UInt32 Redraw([Optional] UInt32 aCount);

		/** Synthesize a mouse event for a window. The event types supported
		 *  are: 
		 *    mousedown, mouseup, mousemove, mouseover, mouseout, contextmenu
		 *
		 * Events are sent in coordinates offset by aX and aY from the window.
		 *
		 * Note that additional events may be fired as a result of this call. For
		 * instance, typically a click event will be fired as a result of a
		 * mousedown and mouseup in sequence.
		 *
		 * Normally at this level of events, the mouseover and mouseout events are
		 * only fired when the window is entered or exited. For inter-element
		 * mouseover and mouseout events, a movemove event fired on the new element
		 * should be sufficient to generate the correct over and out events as well.
		 *
		 * Cannot be accessed from unprivileged context (not content-accessible)
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges.
		 *
		 * @param aType event type
		 * @param aX x offset in CSS pixels
		 * @param aY y offset in CSS pixels
		 * @param aButton button to synthesize
		 * @param aClickCount number of clicks that have been performed
		 * @param aModifiers modifiers pressed, using constants defined in nsIDOMNSEvent
		 * @param aIgnoreRootScrollFrame whether the event should ignore viewport bounds
		 *                           during dispatch
		 */
		void SendMouseEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aType,
							Single aX,
							Single aY,
							Int32 aButton,
							Int32 aClickCount,
							Int32 aModifiers,
							[Optional] Boolean aIgnoreRootScrollFrame);

		/** Synthesize a mouse scroll event for a window. The event types supported
		 *  are: 
		 *    DOMMouseScroll
		 *    MozMousePixelScroll
		 *
		 * Events are sent in coordinates offset by aX and aY from the window.
		 *
		 * Cannot be accessed from unprivileged context (not content-accessible)
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges.
		 *
		 * @param aType event type
		 * @param aX x offset in CSS pixels
		 * @param aY y offset in CSS pixels
		 * @param aButton button to synthesize
		 * @param aScrollFlags flag bits --- see nsMouseScrollFlags in nsGUIEvent.h
		 * @param aDelta the direction and amount to scroll (in lines or pixels,
		 * depending on the event type)
		 * @param aModifiers modifiers pressed, using constants defined in nsIDOMNSEvent
		 */
		void SendMouseScrollEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aType,
								  Single aX,
								  Single aY,
								  Int32 aButton,
								  Int32 aScrollFlags,
								  Int32 aDelta,
								  Int32 aModifiers);

		/**
		 * Synthesize a key event to the window. The event types supported are:
		 *   keydown, keyup, keypress
		 *
		 * Key events generally end up being sent to the focused node.
		 *
		 * Cannot be accessed from unprivileged context (not content-accessible)
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges.
		 *
		 * @param aType event type
		 * @param aKeyCode key code
		 * @param aCharCode character code
		 * @param aModifiers modifiers pressed, using constants defined in nsIDOMNSEvent
		 * @param aPreventDefault if true, preventDefault() the event before dispatch
		 *
		 * @return false if the event had preventDefault() called on it,
		 *               true otherwise.  In other words, true if and only if the
		 *               default action was taken.
		 */
		Boolean SendKeyEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aType,
							 Int32 aKeyCode,
							 Int32 aCharCode,
							 Int32 aModifiers,
							 [Optional] Boolean aPreventDefault);

		/**
		 * See nsIWidget::SynthesizeNativeKeyEvent
		 *
		 * Cannot be accessed from unprivileged context (not content-accessible)
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges.
		 */
		void SendNativeKeyEvent(Int32 aNativeKeyboardLayout,
								Int32 aNativeKeyCode,
								Int32 aModifierFlags,
								[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aCharacters,
								[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aUnmodifiedCharacters);

		/**
		 * See nsIWidget::ActivateNativeMenuItemAt
		 *
		 * Cannot be accessed from unprivileged context (not content-accessible)
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges.
		 */
		void ActivateNativeMenuItemAt([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String indexString);

		/**
		 * See nsIWidget::ForceUpdateNativeMenuAt
		 *
		 * Cannot be accessed from unprivileged context (not content-accessible)
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges.
		 */
		void ForceUpdateNativeMenuAt([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String indexString);

		/**
		 * Focus the element aElement. The element should be in the same document
		 * that the window is displaying. Pass null to blur the element, if any,
		 * that currently has focus, and focus the document.
		 *
		 * Cannot be accessed from unprivileged context (not content-accessible)
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges.
		 *
		 * @param aElement the element to focus
		 *
		 * Do not use this method. Just use element.focus if available or
		 * nsIFocusManager::SetFocus instead.
		 *
		 */
		void Focus(nsIDOMElement aElement);

		/**
		 * Force a garbage collection. This will run the cycle-collector twice to
		 * make sure all garbage is collected.
		 *
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges in non-debug builds. Available to all callers in debug builds.
		 */
		void GarbageCollect();

		/**
		 * Force processing of any queued paints
		 */
		void ProcessUpdates();

		/** Synthesize a simple gesture event for a window. The event types
		 *  supported are: MozSwipeGesture, MozMagnifyGestureStart,
		 *  MozMagnifyGestureUpdate, MozMagnifyGesture, MozRotateGestureStart,
		 *  MozRotateGestureUpdate, MozRotateGesture, MozPressTapGesture, and
		 *  MozTapGesture.
		 *
		 * Cannot be accessed from unprivileged context (not
		 * content-accessible) Will throw a DOM security error if called
		 * without UniversalXPConnect privileges.
		 *
		 * @param aType event type
		 * @param aX x offset in CSS pixels
		 * @param aY y offset in CSS pixels
		 * @param aDirection direction, using constants defined in nsIDOMSimpleGestureEvent
		 * @param aDelta  amount of magnification or rotation for magnify and rotation events
		 * @param aModifiers modifiers pressed, using constants defined in nsIDOMNSEvent
		 */
		void SendSimpleGestureEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aType,
									Single aX,
									Single aY,
									UInt32 aDirection,
									Double aDelta,
									Int32 aModifiers);

		/**
		 * Retrieve the element at point aX, aY in the window's document.
		 *
		 * @param aIgnoreRootScrollFrame whether or not to ignore the root scroll
		 *        frame when retrieving the element. If false, this method returns
		 *        null for coordinates outside of the viewport.
		 * @param aFlushLayout flushes layout if true. Otherwise, no flush occurs.
		 */
		nsIDOMElement ElementFromPoint(Int32 aX, Int32 aY, Boolean aIgnoreRootScrollFrame, Boolean aFlushLayout);

		/**
		 * Compare the two canvases, returning the number of differing pixels and
		 * the maximum difference in a channel.  This will throw an error if
		 * the dimensions of the two canvases are different.
		 *
		 * This method requires UniversalXPConnect privileges.
		 */
		UInt32 CompareCanvases(nsIDOMHTMLCanvasElement aCanvas1, nsIDOMHTMLCanvasElement aCanvas2, out UInt32 aMaxDifference);

		/**
		 * Returns true if a MozAfterPaint event has been queued but not yet
		 * fired.
		 */
		Boolean IsMozAfterPaintPending { get; }

		/**
		 * Suppresses/unsuppresses user initiated event handling in window's document
		 * and subdocuments.
		 *
		 * @throw NS_ERROR_DOM_SECURITY_ERR if called without UniversalXPConnect
		 *        privileges and NS_ERROR_FAILURE if window doesn't have a document.
		 */
		void SuppressEventHandling(Boolean aSuppress);

		void ClearMozAfterPaintEvents();

		/**
		 * Disable or enable non synthetic test mouse events on *all* windows.
		 *
		 * Cannot be accessed from unprivileged context (not content-accessible).
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges.
		 *
		 * @param aDisable  If true, disable all non synthetic test mouse events
		 *               on all windows.  Otherwise, enable them.
		 */
		void DisableNonTestMouseEvents(Boolean aDisable);

		/**
		 * Returns the scroll position of the window's currently loaded document.
		 *
		 * @param aFlushLayout flushes layout if true. Otherwise, no flush occurs.
		 * @see nsIDOMWindow::scrollX/Y
		 */
		void GetScrollXY(Boolean aFlushLayout, out Int32 aScrollX, out Int32 aScrollY);

		/**
		 * Creates a ChromeObjectWrapper for the object and returns it.
		 *
		 * @param scope The JavaScript object whose scope we'll use as the
		 *        parent of the wrapper.
		 * @param objToWrap The JavaScript object to wrap.
		 * @return the wrapped object.
		 */
		void GetCOWForObject(/* scope, objToWrap */);

		/**
		 * Get IME open state. TRUE means 'Open', otherwise, 'Close'.
		 * This property works only when IMEEnabled is IME_STATUS_ENABLED.
		 */
		Boolean IMEIsOpen { get; }

		/**
		 * Get IME status, see above IME_STATUS_* definitions.
		 */
		UInt32 IMEStatus { get; }

		/**
		 * Get the number of screen pixels per CSS pixel.
		 */
		Single ScreenPixelsPerCSSPixel { get; }
	}

	[ComImport, Guid("b0f803f7-98c0-4152-812c-d6678ba23049"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMWindowUtils_1_9_2 //: nsISupports
	{
		/**
		 * Dispatches aEvent via the nsIPresShell object of the window's document.
		 * The event is dispatched to aTarget, which should be an object
		 * which implements nsIContent interface (#element, #text, etc).
		 *
		 * Cannot be accessed from unprivileged context (not
		 * content-accessible) Will throw a DOM security error if called
		 * without UniversalXPConnect privileges.
		 *
		 * @note Event handlers won't get aEvent as parameter, but a similar event.
		 *       Also, aEvent should not be reused.
		 */
		Boolean DispatchDOMEventViaPresShell(nsIDOMNode aTarget, nsIDOMEvent aEvent, Boolean aTrusted);
	}

	internal static class nsIDOMWindowUtils_1_9_2_5Constants
	{
		// NOTE: following values are same as NS_QUERY_* in nsGUIEvent.h

		/**
		 * QUERY_SELECTED_TEXT queries the first selection range's information.
		 *
		 * @param aOffset   Not used.
		 * @param aLength   Not used.
		 * @param aX        Not used.
		 * @param aY        Not used.
		 *
		 * @return offset, reversed and text properties of the result are available.
		 */
		internal const UInt32 QUERY_SELECTED_TEXT = 3200;

		/**
		 * QUERY_TEXT_CONTENT queries the text at the specified range.
		 *
		 * @param aOffset   The first character's offset.  0 is the first character.
		 * @param aLength   The length of getting text.  If the aLength is too long,
		 *                  the result text is shorter than this value.
		 * @param aX        Not used.
		 * @param aY        Not used.
		 *
		 * @return text property of the result is available.
		 */
		internal const UInt32 QUERY_TEXT_CONTENT = 3201;

		/**
		 * QUERY_CARET_RECT queries the (collapsed) caret rect of the offset.
		 * If the actual caret is there at the specified offset, this returns the
		 * actual caret rect.  Otherwise, this guesses the caret rect from the
		 * metrics of the text.
		 *
		 * @param aOffset   The caret offset.  0 is the left side of the first
		 *                  caracter in LTR text.
		 * @param aLength   Not used.
		 * @param aX        Not used.
		 * @param aY        Not used.
		 *
		 * @return left, top, width and height properties of the result are available.
		 *         The left and the top properties are offset in the client area of
		 *         the DOM window.
		 */
		internal const UInt32 QUERY_CARET_RECT = 3203;

		/**
		 * QUERY_TEXT_RECT queries the specified text's rect.
		 *
		 * @param aOffset   The first character's offset.  0 is the first character.
		 * @param aLength   The length of getting text.  If the aLength is too long,
		 *                  the extra length is ignored.
		 * @param aX        Not used.
		 * @param aY        Not used.
		 *
		 * @return left, top, width and height properties of the result are available.
		 *         The left and the top properties are offset in the client area of
		 *         the DOM window.
		 */
		internal const UInt32 QUERY_TEXT_RECT = 3204;

		/**
		 * QUERY_TEXT_RECT queries the focused editor's rect.
		 *
		 * @param aOffset   Not used.
		 * @param aLength   Not used.
		 * @param aX        Not used.
		 * @param aY        Not used.
		 *
		 * @return left, top, width and height properties of the result are available.
		 */
		internal const UInt32 QUERY_EDITOR_RECT = 3205;

		/**
		 * QUERY_CHARACTER_AT_POINT queries the character information at the
		 * specified point.  The point is offset in the window.
		 * NOTE: If there are some panels at the point, this method send the query
		 * event to the panel's widget automatically.
		 *
		 * @param aOffset   Not used.
		 * @param aLength   Not used.
		 * @param aX        X offset in the widget.
		 * @param aY        Y offset in the widget.
		 *
		 * @return offset, notFound, left, top, width and height properties of the
		 *         result are available.
		 */
		internal const UInt32 QUERY_CHARACTER_AT_POINT = 3208;
	}

	[ComImport, Guid("915abb48-66d4-4135-a0d8-153fb87b99e6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMWindowUtils_1_9_2_5 //: nsISupports
	{
		/**
		 * Retrieve all nodes that intersect a rect in the window's document.
		 *
		 * @param aX x reference for the rectangle in CSS pixels
		 * @param aY y reference for the rectangle in CSS pixels
		 * @param aTopSize How much to expand up the rectangle
		 * @param aRightSize How much to expand right the rectangle
		 * @param aBottomSize How much to expand down the rectangle
		 * @param aLeftSize How much to expand left the rectangle
		 * @param aIgnoreRootScrollFrame whether or not to ignore the root scroll
		 *        frame when retrieving the element. If false, this method returns
		 *        null for coordinates outside of the viewport.
		 * @param aFlushLayout flushes layout if true. Otherwise, no flush occurs.
		 */
		nsIDOMNodeList NodesFromRect(Single aX,
									 Single aY,
									 Single aTopSize,
									 Single aRightSize,
									 Single aBottomSize,
									 Single aLeftSize,
									 Boolean aIgnoreRootScrollFrame,
									 Boolean aFlushLayout);

		/**
		 * Synthesize a query content event.
		 *
		 * @param aType  On of the following const values.  And see also each comment
		 *               for the other parameters and the result.
		 */
		nsIQueryContentEventResult SendQueryContentEvent(UInt32 aType,
														 UInt32 aOffset,
														 UInt32 aLength,
														 Int32 aX,
														 Int32 aY);

		/**
		 * Exposes the CSS parser's "initial syntax is valid" heuristic for
		 * testing.
		 */
		Boolean CssInitialSyntaxIsValid([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aSheet);
	}
}
