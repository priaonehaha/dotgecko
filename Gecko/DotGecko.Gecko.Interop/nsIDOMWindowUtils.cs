using System;
using System.Runtime.InteropServices;
using System.Text;
using nscolor = System.UInt32;
using gfxContext = System.IntPtr;
using nsViewID = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	public static class nsIDOMWindowUtilsConstants
	{
		/**
		 * WARNING: These values must be same as nsIWidget's values.
		 */

		/**
		 * DISABLED means users cannot use IME completely.
		 * Note that this state is *not* same as |ime-mode: disabled;|.
		 */
		public const UInt32 IME_STATUS_DISABLED = 0;

		/**
		 * ENABLED means users can use all functions of IME. This state is same as
		 * |ime-mode: normal;|.
		 */
		public const UInt32 IME_STATUS_ENABLED = 1;

		/**
		 * PASSWORD means users cannot use most functions of IME. But on GTK2,
		 * users can use "Simple IM" which only supports dead key inputting.
		 * The behavior is same as the behavior of the native password field.
		 * This state is same as |ime-mode: disabled;|.
		 */
		public const UInt32 IME_STATUS_PASSWORD = 2;

		/**
		 * PLUGIN means a plug-in has focus. At this time we should not touch to
		 * controlling the IME state.
		 */
		public const UInt32 IME_STATUS_PLUGIN = 3;

		// NOTE: These values must be same to NS_TEXTRANGE_* in nsGUIEvent.h

		public const UInt32 COMPOSITION_ATTR_RAWINPUT = 0x02;
		public const UInt32 COMPOSITION_ATTR_SELECTEDRAWTEXT = 0x03;
		public const UInt32 COMPOSITION_ATTR_CONVERTEDTEXT = 0x04;
		public const UInt32 COMPOSITION_ATTR_SELECTEDCONVERTEDTEXT = 0x05;

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
		public const UInt32 QUERY_SELECTED_TEXT = 3200;

		/**
		 * QUERY_TEXT_CONTENT queries the text at the specified range.
		 *
		 * @param aOffset   The first character's offset.  0 is the first character.
		 * @param aLength   The length of getting text.  If the aLength is too Int32,
		 *                  the result text is shorter than this value.
		 * @param aX        Not used.
		 * @param aY        Not used.
		 *
		 * @return text property of the result is available.
		 */
		public const UInt32 QUERY_TEXT_CONTENT = 3201;

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
		public const UInt32 QUERY_CARET_RECT = 3203;

		/**
		 * QUERY_TEXT_RECT queries the specified text's rect.
		 *
		 * @param aOffset   The first character's offset.  0 is the first character.
		 * @param aLength   The length of getting text.  If the aLength is too Int32,
		 *                  the extra length is ignored.
		 * @param aX        Not used.
		 * @param aY        Not used.
		 *
		 * @return left, top, width and height properties of the result are available.
		 *         The left and the top properties are offset in the client area of
		 *         the DOM window.
		 */
		public const UInt32 QUERY_TEXT_RECT = 3204;

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
		public const UInt32 QUERY_EDITOR_RECT = 3205;

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
		public const UInt32 QUERY_CHARACTER_AT_POINT = 3208;
	}

	/**
	 * nsIDOMWindowUtils is intended for infrequently-used methods related
	 * to the current nsIDOMWindow.  Some of the methods may require
	 * elevated privileges; the method implementations should contain the
	 * necessary security checks.  Access this interface by calling
	 * getInterface on a DOMWindow.
	 */
	[ComImport, Guid("85fa978a-fc91-4513-9f11-8911e671577f"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMWindowUtils //: nsISupports
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
		 * Get current cursor type from this window
		 * @return the current value of nsCursor
		 */
		Int16 GetCursorType();

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

		/**
		 * Set the CSS viewport to be |widthPx| x |heightPx| in units of CSS
		 * pixels, regardless of the size of the enclosing widget/view.
		 * This will trigger reflow.
		 *
		 * The caller of this method must have UniversalXPConnect
		 * privileges.
		 */
		void SetCSSViewport(Single aWidthPx, Single aHeightPx);

		/**
		 * Set the "displayport" to be <xPx, yPx, widthPx, heightPx> in
		 * units of CSS pixels, regardless of the size of the enclosing
		 * widget/view.  This will *not* trigger reflow.
		 *
		 * <x, y> is relative to the top-left of the CSS viewport.  This
		 * means that the pixels rendered to the displayport take scrolling
		 * into account, for example.
		 *
		 * The displayport will be used as the window's visible region for
		 * the purposes of invalidation and painting.  The displayport can
		 * approximately be thought of as a "persistent" drawWindow()
		 * (albeit with coordinates relative to the CSS viewport): the
		 * bounds are remembered by the platform, and layer pixels are
		 * retained and updated inside the viewport bounds.
		 *
		 * It's legal to set a displayport that extends beyond the CSS
		 * viewport in any direction (left/right/top/bottom).
		 * 
		 * It's also legal to set a displayport that extends beyond the
		 * document's bounds.  The value of the pixels rendered outside the
		 * document bounds is not yet defined.
		 *
		 * The caller of this method must have UniversalXPConnect
		 * privileges.
		 */
		void SetDisplayPort(Single aXPx, Single aYPx,
							Single aWidthPx, Single aHeightPx);

		/**
		 * Get/set the resolution at which rescalable web content is drawn.
		 * Currently this is only (some) thebes content.
		 *
		 * Setting a new resolution does *not* trigger reflow.  This API is
		 * entirely separate from textZoom and fullZoom; a resolution scale
		 * can be applied together with both textZoom and fullZoom.
		 *
		 * The effect of is API for gfx code to allocate more or fewer
		 * pixels for rescalable content by a factor of |resolution| in
		 * either or both dimensions.  setResolution() together with
		 * setDisplayport() can be used to implement a non-reflowing
		 * scale-zoom in concert with another entity that can draw with a
		 * scale.  For example, to scale a content |window| inside a
		 * <browser> by a factor of 2.0
		 *
		 *   window.setDisplayport(x, y, oldW / 2.0, oldH / 2.0);
		 *   window.setResolution(2.0, 2.0);
		 *   // elsewhere
		 *   browser.setViewportScale(2.0, 2.0);
		 *
		 * The caller of this method must have UniversalXPConnect
		 * privileges.
		 */
		void SetResolution(Single aXResolution, Single aYResolution);

		/** Synthesize a mouse event. The event types supported are:
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
		 * The event is dispatched via the toplevel window, so it could go to any
		 * window under the toplevel window, in some cases it could never reach this
		 * window at all.
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

		/** The same as sendMouseEvent but ensures that the event is dispatched to
		 *  this DOM window or one of its children.
		 */
		void SendMouseEventToWindow([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aType,
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
		 * See nsIWidget::SynthesizeNativeMouseEvent
		 *
		 * Will be called on the widget that contains aElement.
		 * Cannot be accessed from unprivileged context (not content-accessible)
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges.
		 */
		void SendNativeMouseEvent(Int32 aScreenX,
								  Int32 aScreenY,
								  Int32 aNativeMessage,
								  Int32 aModifierFlags,
								  nsIDOMElement aElement);

		/**
		 * See nsIWidget::ActivateNativeMenuItemAt
		 *
		 * Cannot be accessed from unprivileged context (not content-accessible)
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges.
		 */
		void SctivateNativeMenuItemAt([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String indexString);

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
		 * Force a garbage collection followed by a cycle collection.
		 *
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges in non-debug builds. Available to all callers in debug builds.
		 *
		 * @param aListener listener that receives information about the CC graph
		 *                  (see @mozilla.org/cycle-collector-logger;1 for a logger
		 *                   component)
		 */
		void GarbageCollect([Optional] nsICycleCollectorListener aListener);

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
		nsIDOMElement ElementFromPoint(Single aX,
									   Single aY,
									   Boolean aIgnoreRootScrollFrame,
									   Boolean aFlushLayout);

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
		 * Compare the two canvases, returning the number of differing pixels and
		 * the maximum difference in a channel.  This will throw an error if
		 * the dimensions of the two canvases are different.
		 *
		 * This method requires UniversalXPConnect privileges.
		 */
		UInt32 CompareCanvases(nsIDOMHTMLCanvasElement aCanvas1,
							   nsIDOMHTMLCanvasElement aCanvas2,
							   out UInt32 aMaxDifference);

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
		Boolean DispatchDOMEventViaPresShell(nsIDOMNode aTarget,
											 nsIDOMEvent aEvent,
											 Boolean aTrusted);

		/**
		 * Returns the real classname (possibly of the mostly-transparent security
		 * wrapper) of aObj.
		 */
		[return: MarshalAs(UnmanagedType.LPStr)]
		String GetClassName(/*in JSObjectPtr aObj*/);

		/**
		 * Generate a content command event.
		 *
		 * Cannot be accessed from unprivileged context (not content-accessible)
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges.
		 *
		 * @param aType Type of command content event to send.  Can be one of "cut",
		 *        "copy", "paste", "delete", "undo", "redo", or "pasteTransferable".
		 * @param aTransferable an instance of nsITransferable when aType is
		 *        "pasteTransferable"
		 */
		void SendContentCommandEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aType,
									 [Optional] nsITransferable aTransferable);

		/**
		 * Synthesize a composition event to the window.
		 *
		 * Cannot be accessed from unprivileged context (not content-accessible)
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges.
		 *
		 * @param aType The event type: "compositionstart" or "compositionend".
		 */
		void SendCompositionEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aType);

		/**
		 * Synthesize a text event to the window.
		 *
		 * Cannot be accessed from unprivileged context (not content-accessible)
		 * Will throw a DOM security error if called without UniversalXPConnect
		 * privileges.
		 *
		 * Currently, this method doesn't support 4 or more clauses composition
		 * string.
		 *
		 * @param aCompositionString  composition string
		 * @param a*ClauseLengh       the length of nth clause, set 0 when you
		 *                            don't need second or third clause.
		 * @param a*ClauseAttr        the attribute of nth clause, uese following
		 *                            const values.
		 * @param aCaretStart         the caret position in the composition string,
		 *                            if you set negative value, this method don't
		 *                            set the caret position to the event.
		 * @param aCaretLength        the caret length, if this is one or more,
		 *                            the caret will be wide caret, otherwise,
		 *                            it's collapsed.
		 *                            XXX nsEditor doesn't support wide caret yet.
		 */
		void SendTextEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aCompositionString,
						   Int32 aFirstClauseLength,
						   UInt32 aFirstClauseAttr,
						   Int32 aSecondClauseLength,
						   UInt32 aSecondClauseAttr,
						   Int32 aThirdClauseLength,
						   UInt32 aThirdClauseAttr,
						   Int32 aCaretStart,
						   Int32 aCaretLength);

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
		 * Synthesize a selection set event to the window.
		 *
		 * This sets the selection as the specified information.
		 *
		 * @param aOffset  The caret offset of the selection start.
		 * @param aLength  The length of the selection.  If this is too Int32, the
		 *                 extra length is ignored.
		 * @param aReverse If true, the selection set from |aOffset + aLength| to
		 *                 |aOffset|.  Otherwise, set from |aOffset| to
		 *                 |aOffset + aLength|.
		 * @return True, if succeeded.  Otherwise, false.
		 */
		Boolean SendSelectionSetEvent(UInt32 aOffset,
									  UInt32 aLength,
									  Boolean aReverse);

		/**
		 * Perform the equivalent of:
		 *   window.getComputedStyle(aElement, aPseudoElement).
		 *     getPropertyValue(aPropertyName)
		 * except that, when the link whose presence in history is allowed to
		 * influence aElement's style is visited, get the value the property
		 * would have if allowed all properties to change as a result of
		 * :visited selectors (except for cases where getComputedStyle uses
		 * data from the frame).
		 *
		 * This is easier to implement than adding our property restrictions
		 * to this API, and is sufficient for the present testing
		 * requirements (which are essentially testing 'color').
		 */
		void GetVisitedDependentComputedStyle(nsIDOMElement aElement,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aPseudoElement,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aPropertyName,
			[In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * Returns the parent of obj.
		 *
		 * @param obj The JavaScript object whose parent is to be gotten.
		 * @return the parent.
		 */
		void GetParent(/* obj */);

		/**
		 * Get the id of the outer window of this window.  This will never throw.
		 */
		UInt64 OuterWindowID { get; }

		/**
		 * Get the id of the current inner window of this window.  If there
		 * is no current inner window, throws NS_ERROR_NOT_AVAILABLE.
		 */
		UInt64 CurrentInnerWindowID { get; }

		/**
		 * Put the window into a state where scripts are frozen and events
		 * suppressed, for use when the window has launched a modal prompt.
		 */
		void EnterModalState();

		/**
		 * Resume normal window state, where scripts can run and events are
		 * delivered.
		 */
		void LeaveModalState();

		/**
		 * Is the window is in a modal state? [See enterModalState()]
		 */
		Boolean IsInModalState();

		/**
		 * Suspend/resume timeouts on this window and its descendant windows.
		 */
		void SuspendTimeouts();
		void ResumeTimeouts();

		/**
		 * What type of layer manager the widget associated with this window is
		 * using. "Basic" is unaccelerated; other types are accelerated. Throws an
		 * error if there is no widget associated with this window.
		 */
		void GetLayerManagerType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * The DPI of the display
		 */
		Single DisplayDPI { get; }

		/**
		 * Return the outer window with the given ID, if any.  Can return null.
		 */
		nsIDOMWindow GetOuterWindowWithId(UInt64 aOuterWindowID);

		void RenderDocument([In] ref nsIntRect aRect,
							UInt32 aFlags,
							nscolor aBackgroundColor,
							gfxContext aThebesContext);

		/**
		 * Method for testing nsStyleAnimation::ComputeDistance.
		 *
		 * Returns the distance between the two values as reported by
		 * nsStyleAnimation::ComputeDistance for the given element and
		 * property.
		 */
		Double ComputeAnimationDistance(nsIDOMElement element,
										[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String property,
										[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value1,
										[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value2);
	}

	[ComImport, Guid("be2e28c8-64f8-4100-906d-8a451ddd6835"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMWindowUtils_MOZILLA_2_0_BRANCH //: nsISupports
	{
		/**
		 * Get the type of the currently focused html input, if any.
		 */
		String FocusedInputType { [return: MarshalAs(UnmanagedType.LPStr)] get; }

		/**
		 * Given a view ID from the compositor process, retrieve the element
		 * associated with a view. For scrollpanes for documents, the root
		 * element of the document is returned.
		 */
		nsIDOMElement FindElementWithViewId(nsViewID aId);

		/**
		 * Same as enterModalState, but returns the window associated with the
		 * current JS context.
		 */
		nsIDOMWindow EnterModalStateWithWindow();

		/**
		 * Same as leaveModalState, but takes a window associated with the active
		 * context when enterModalStateWithWindow was called. The currently context
		 * might be different at the moment (see bug 621764).
		 */
		void LeaveModalStateWithWindow(nsIDOMWindow aWindow);

		/**
		 * Checks the layer tree for this window and returns true
		 * if all layers have transforms that are translations by integers,
		 * no leaf layers overlap, and the union of the leaf layers is exactly
		 * the bounds of the window. Always returns true in non-DEBUG builds.
		 */
		Boolean LeafLayersPartitionWindow();
	}
}
