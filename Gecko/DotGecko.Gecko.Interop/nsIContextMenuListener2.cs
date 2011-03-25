using System;
using System.Runtime.InteropServices;
using System.Text;
using imgIContainer = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	public static class nsIContextMenuListener2Constants
	{
		/** Flag. No context. */
		public const UInt32 CONTEXT_NONE = 0;
		/** Flag. Context is a link element. */
		public const UInt32 CONTEXT_LINK = 1;
		/** Flag. Context is an image element. */
		public const UInt32 CONTEXT_IMAGE = 2;
		/** Flag. Context is the whole document. */
		public const UInt32 CONTEXT_DOCUMENT = 4;
		/** Flag. Context is a text area element. */
		public const UInt32 CONTEXT_TEXT = 8;
		/** Flag. Context is an input element. */
		public const UInt32 CONTEXT_INPUT = 16;
		/** Flag. Context is a background image. */
		public const UInt32 CONTEXT_BACKGROUND_IMAGE = 32;
	}

	/* THIS IS A PUBLIC EMBEDDING API */

	/**
	 * nsIContextMenuListener2
	 *
	 * This is an extended version of nsIContextMenuListener
	 * It provides a helper class, nsIContextMenuInfo, to allow access to
	 * background images as well as various utilities.
	 *
	 * @see nsIContextMenuListener
	 * @see nsIContextMenuInfo
	 */
	[ComImport, Guid("7fb719b3-d804-4964-9596-77cf924ee314"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIContextMenuListener2 //: nsISupports
	{
		/**
		 * Called when the browser receives a context menu event (e.g. user is right-mouse
		 * clicking somewhere on the document). The combination of flags, along with the
		 * attributes of <CODE>aUtils</CODE>, indicate where and what was clicked on.
		 *
		 * The following table describes what context flags and node combinations are
		 * possible.
		 *
		 * aContextFlags                  aUtils.targetNode
		 *
		 * CONTEXT_LINK                   <A>
		 * CONTEXT_IMAGE                  <IMG>
		 * CONTEXT_IMAGE | CONTEXT_LINK   <IMG> with <A> as an ancestor
		 * CONTEXT_INPUT                  <INPUT>
		 * CONTEXT_INPUT | CONTEXT_IMAGE  <INPUT> with type=image
		 * CONTEXT_TEXT                   <TEXTAREA>
		 * CONTEXT_DOCUMENT               <HTML>
		 * CONTEXT_BACKGROUND_IMAGE       <HTML> with background image
		 *
		 * @param aContextFlags           Flags indicating the kind of context.
		 * @param aUtils                  Context information and helper utilities.
		 *
		 * @see nsIContextMenuInfo
		 */
		void OnShowContextMenu(UInt32 aContextFlags, nsIContextMenuInfo aUtils);
	}

	/**
	 * nsIContextMenuInfo
	 *
	 * A helper object for implementors of nsIContextMenuListener2.
	 */
	[ComImport, Guid("2f977d56-5485-11d4-87e2-0010a4e75ef2"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIContextMenuInfo //: nsISupports
	{
		/**
		 * The DOM context menu event.
		 */
		nsIDOMEvent MouseEvent { get; }

		/**
		 * The DOM node most relevant to the context.
		 */
		nsIDOMNode TargetNode { get; }

		/**
		 * Given the <CODE>CONTEXT_LINK</CODE> flag, <CODE>targetNode</CODE> may not
		 * nescesarily be a link. This returns the anchor from <CODE>targetNode</CODE>
		 * if it has one or that of its nearest ancestor if it does not.
		 */
		void GetAssociatedLink([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);

		/**
		 * Given the <CODE>CONTEXT_IMAGE</CODE> flag, these methods can be
		 * used in order to get the image for viewing, saving, or for the clipboard.
		 *
		 * @return <CODE>NS_OK</CODE> if successful, otherwise <CODE>NS_ERROR_FAILURE</CODE> if no
		 * image was found, or NS_ERROR_NULL_POINTER if an internal error occurs where we think there 
		 * is an image, but for some reason it cannot be returned.
		 */
		imgIContainer ImageContainer { get; }
		nsIURI ImageSrc { get; }

		/**
		 * Given the <CODE>CONTEXT_BACKGROUND_IMAGE</CODE> flag, these methods can be
		 * used in order to get the image for viewing, saving, or for the clipboard.
		 *
		 * @return <CODE>NS_OK</CODE> if successful, otherwise <CODE>NS_ERROR_FAILURE</CODE> if no background
		 * image was found, or NS_ERROR_NULL_POINTER if an internal error occurs where we think there is a 
		 * background image, but for some reason it cannot be returned.
		 */
		imgIContainer BackgroundImageContainer { get; }
		nsIURI BackgroundImageSrc { get; }
	}
}
