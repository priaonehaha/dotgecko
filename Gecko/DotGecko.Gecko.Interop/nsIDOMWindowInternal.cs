using System;
using System.Runtime.InteropServices;
using System.Text;
using nsISupports = System.Object;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("9d6a1157-0719-46a7-b49f-7ffeaa0b5c86"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMWindowInternal : nsIDOMWindow2
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

		#region nsIDOMWindow2 Members

		new nsIDOMEventTarget WindowRoot { get; }
		new nsIDOMOfflineResourceList ApplicationCache { get; }
		new void CreateBlobURL(nsIDOMBlob blob, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		new void RevokeBlobURL([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String URL);

		#endregion

		nsIDOMWindowInternal Window { get; }

		/* [replaceable] self */
		nsIDOMWindowInternal Self { get; }

		nsIDOMNavigator Navigator { get; }
		nsIDOMScreen Screen { get; }
		nsIDOMHistory History { get; }

		/* [replaceable] content */
		nsIDOMWindow Content { get; }

		/* [replaceable] prompter */
		nsIPrompt Prompter { get; }

		/* [replaceable] menubar */
		nsIDOMBarProp Menubar { get; }

		/* [replaceable] toolbar */
		nsIDOMBarProp Toolbar { get; }

		/* [replaceable] locationbar */
		nsIDOMBarProp Locationbar { get; }

		/* [replaceable] personalbar */
		nsIDOMBarProp Personalbar { get; }

		/* [replaceable] statusbar */
		nsIDOMBarProp Statusbar { get; }

		Boolean Closed { get; }
		nsIDOMCrypto Crypto { get; }
		nsIDOMPkcs11 Pkcs11 { get; }

		// XXX Shouldn't this be in nsIDOMChromeWindow?
		/* [replaceable] controllers */
		nsIControllers Controllers { get; }

		nsIDOMWindowInternal Opener { get; set; }

		/* [replaceable] */
		void GetStatus([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void SetStatus([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		void GetDefaultStatus([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void SetDefaultStatus([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		// XXX: The setter that takes a string argument needs to be special
		// cased!
		nsIDOMLocation Location { get; }

		/* [replaceable] */
		Int32 InnerWidth { get; set; }
		Int32 InnerHeight { get; set; }
		Int32 OuterWidth { get; set; }
		Int32 OuterHeight { get; set; }
		Int32 ScreenX { get; set; }
		Int32 ScreenY { get; set; }
		Single MozInnerScreenX { get; }
		Single MozInnerScreenY { get; }

		/* The offset in pixels by which the window is scrolled */
		Int32 PageXOffset { get; }
		Int32 PageYOffset { get; }

		/* The maximum offset that the window can be scrolled to
		   (i.e., the document width/height minus the scrollport width/height) */
		Int32 ScrollMaxX { get; }
		Int32 ScrollMaxY { get; }

		/* [replaceable] length */
		UInt32 Length { get; }

		Boolean FullScreen { get; set; }

		void Alert([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String text);
		Boolean Confirm([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String text);

		// prompt() should return a null string if cancel is pressed
		void Prompt(
			[In, Optional, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String aMessage,
			[In, Optional, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String aInitial,
			[In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		void Focus();
		void Blur();

		void Back();
		void Forward();
		void Home();
		void Stop();

		void Print();

		void MoveTo(Int32 xPos, Int32 yPos);
		void MoveBy(Int32 xDif, Int32 yDif);
		void ResizeTo(Int32 width, Int32 height);
		void ResizeBy(Int32 widthDif, Int32 heightDif);
		void Scroll(Int32 xScroll, Int32 yScroll);

		/**
		 * Open a new window with this one as the parent.  This method will
		 * NOT examine the JS stack for purposes of determining a caller.
		 * This window will be used for security checks during the search by
		 * name and the default character set on the newly opened window
		 * will just be the default character set of this window.
		 */
		nsIDOMWindow Open(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String url,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String options);

		/**
		 * This method works like open except that aExtraArgument gets
		 * converted into the array window.arguments in JS, if
		 * aExtraArgument is a nsISupportsArray then the individual items in
		 * the array are inserted into window.arguments, and primitive
		 * nsISupports (nsISupportsPrimitives) types are converted to native
		 * JS types when possible.
		 */
		nsIDOMWindow OpenDialog(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String url,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String name,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String options,
			[MarshalAs(UnmanagedType.Interface)] nsISupports aExtraArgument);
		void Close();

		// XXX Should this be in nsIDOMChromeWindow?
		void UpdateCommands([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String action);

		/* Find in page.
		 * @param str: the search pattern
		 * @param caseSensitive: is the search caseSensitive
		 * @param backwards: should we search backwards
		 * @param wrapAround: should we wrap the search
		 * @param wholeWord: should we search only for whole words
		 * @param searchInFrames: should we search through all frames
		 * @param showDialog: should we show the Find dialog
		 */
		Boolean Find([In, Optional, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String str,
					 [Optional] Boolean caseSensitive,
					 [Optional] Boolean backwards,
					 [Optional] Boolean wrapAround,
					 [Optional] Boolean wholeWord,
					 [Optional] Boolean searchInFrames,
					 [Optional] Boolean showDialog);

		// Ascii base64 data to binary data and vice versa...
		void AtoB(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String aAsciiString,
			[In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void BtoA(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String aBase64Data,
			[In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		nsIDOMElement FrameElement { get; }

		nsIVariant ShowModalDialog(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String aURI,
			[Optional] nsIVariant aArgs,
			[In, Optional, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String aOptions);

		/**
		 * Implements a safe message-passing system which can cross same-origin
		 * boundaries.
		 *
		 * This method, when called, causes a MessageEvent to be asynchronously
		 * dispatched at the primary document for the window upon which this method is
		 * called.  (Note that the postMessage property on windows is allAccess and
		 * thus is readable cross-origin.)  The dispatched event will have message as
		 * its data, the calling context's window as its source, and an origin
		 * determined by the calling context's main document URI.  The targetOrigin
		 * argument specifies a URI and is used to restrict the message to be sent
		 * only when the target window has the same origin as targetOrigin (since,
		 * when the sender and the target have different origins, neither can read the
		 * location of the other).
		 * 
		 * See the WHATWG HTML5 specification, section 6.4, for more details.
		 */
		void PostMessage([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String message, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String targetOrigin);

		/**
		 * Returns the number of times this document for this window has
		 * been painted to the screen.
		 */
		UInt64 MozPaintCount { get; }

		/**
		 * Request a refresh of this browser window.
		 */
		void MozRequestAnimationFrame([Optional] nsIAnimationFrameListener aListener);

		/**
		 * The current animation start time in milliseconds since the epoch.
		 */
		Int64 MozAnimationStartTime { get; }
	}

	[ComImport, Guid("8fc58f56-f769-4368-a098-edd08550cf1a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMMozURLProperty //: nsISupports
	{
		void CreateObjectURL(nsIDOMBlob blob, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void RevokeObjectURL([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String URL);
	}

	[ComImport, Guid("05563c0c-b74c-41ad-91d1-bc22d580a581"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMWindow_2_0_BRANCH //: nsISupports
	{
		nsIDOMMozURLProperty URL { get; }
	}
}
