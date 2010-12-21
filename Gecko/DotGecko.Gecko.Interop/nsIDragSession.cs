using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("15860D52-FE2C-4DDD-AC50-9C23E24916C4"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDragSession //: nsISupports
	{
		/**
		  * Set the current state of the drag, whether it can be dropped or not.
		  * usually the target "frame" sets this so the native system can render the correct feedback
		  */
		Boolean CanDrop { get; set; }

		/**
		  * Sets the action (copy, move, link, et.c) for the current drag 
		  */
		UInt32 DragAction { get; set; }

		/**
		  * Sets the current width and height of the drag target area. 
		  * It will contain the current size of the Frame that the drag is currently in
		  */
		nsSize TargetSize { get; set; }

		/**
		  * Get the number of items that were dropped
		  */
		UInt32 NumDropItems { get; }

		/**
		  * The document where the drag was started, which will be null if the
		  * drag originated outside the application. Useful for determining if a drop
		  * originated in the same document.
		  */
		nsIDOMDocument SourceDocument { get; }

		/**
		  * The dom node that was originally dragged to start the session, which will be null if the
		  * drag originated outside the application.
		  */
		nsIDOMNode SourceNode { get; }

		/**
		 * The data transfer object for the current drag.
		 */
		nsIDOMDataTransfer DataTransfer { get; set; }

		/**
		  * Get data from a Drag&Drop. Can be called while the drag is in process
		  * or after the drop has completed.  
		  *
		  * @param  aTransferable the transferable for the data to be put into
		  * @param  aItemIndex which of multiple drag items, zero-based
		  */
		void GetData(nsITransferable aTransferable, UInt32 aItemIndex);

		/**
		 * Check to set if any of the native data on the clipboard matches this data flavor
		 */
		Boolean IsDataFlavorSupported([MarshalAs(UnmanagedType.LPStr)] String aDataFlavor);
	}

	[ComImport, Guid("fde41f6a-c710-46f8-a0a8-1ff76ca4ff57"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDragSession_1_9_2 //: nsISupports
	{
		/**
		 * Indicates if the drop event should be dispatched only to chrome.
		 */
		Boolean OnlyChromeDrop { get; set; }
	}
}
