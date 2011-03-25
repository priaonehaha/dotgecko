using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIWebBrowserPrintConstants
	{
		/**
		 * PrintPreview Navigation Constants
		 */
		public const Int16 PRINTPREVIEW_GOTO_PAGENUM = 0;
		public const Int16 PRINTPREVIEW_PREV_PAGE = 1;
		public const Int16 PRINTPREVIEW_NEXT_PAGE = 2;
		public const Int16 PRINTPREVIEW_HOME = 3;
		public const Int16 PRINTPREVIEW_END = 4;
	}

	/**
	 * nsIWebBrowserPrint corresponds to the main interface
	 * for printing an embedded Gecko web browser window/document
	 */
	[ComImport, Guid("9A7CA4B0-FBBA-11d4-A869-00105A183419"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWebBrowserPrint //: nsISupports
	{
		/**
		 * Returns a "global" PrintSettings object 
		 * Creates a new the first time, if one doesn't exist.
		 *
		 * Then returns the same object each time after that.
		 *
		 * Initializes the globalPrintSettings from the default printer
		 */
		nsIPrintSettings GlobalPrintSettings { get; }

		/**
		 * Returns a pointer to the PrintSettings object that
		 * that was passed into either "print" or "print preview"
		 *
		 * This enables any consumers of the interface to have access
		 * to the "current" PrintSetting at later points in the execution
		 */
		nsIPrintSettings CurrentPrintSettings { get; }

		/**
		 * Returns a pointer to the current child DOMWindow
		 * that is being print previewed. (FrameSet Frames)
		 *
		 * Returns null if parent document is not a frameset or the entire FrameSet 
		 * document is being print previewed
		 *
		 * This enables any consumers of the interface to have access
		 * to the "current" child DOMWindow at later points in the execution
		 */
		nsIDOMWindow CurrentChildDOMWindow { get; }

		/**
		 * Returns whether it is in Print mode
		 */
		Boolean DoingPrint { get; }

		/**
		 * Returns whether it is in Print Preview mode
		 */
		Boolean DoingPrintPreview { get; }

		/**
		 * This returns whether the current document is a frameset document
		 */
		Boolean IsFramesetDocument { get; }

		/**
		 * This returns whether the current document is a frameset document
		 */
		Boolean IsFramesetFrameSelected { get; }

		/**
		 * This returns whether there is an IFrame selected
		 */
		Boolean IsIFrameSelected { get; }

		/**
		 * This returns whether there is a "range" selection
		 */
		Boolean IsRangeSelection { get; }

		/**
		 * This returns the total number of pages for the Print Preview
		 */
		Int32 PrintPreviewNumPages { get; }

		/**
		 * Print the specified DOM window
		 *
		 * @param aThePrintSettings - Printer Settings for the print job, if aThePrintSettings is null
		 *                            then the global PS will be used.
		 * @param aWPListener - is updated during the print
		 * @return void
		 */
		void Print(nsIPrintSettings aThePrintSettings, nsIWebProgressListener aWPListener);

		/**
		 * Print Preview the specified DOM window
		 *
		 * @param aThePrintSettings - Printer Settings for the print preview, if aThePrintSettings is null
		 *                            then the global PS will be used.
		 * @param aChildDOMWin - DOM Window to be print previewed.
		 * @param aWPListener - is updated during the printpreview
		 * @return void
		 */
		void PrintPreview(nsIPrintSettings aThePrintSettings, nsIDOMWindow aChildDOMWin, nsIWebProgressListener aWPListener);

		/**
		 * Print Preview - Navigates within the window
		 *
		 * @param aNavType - navigation enum
		 * @param aPageNum - page num to navigate to when aNavType = ePrintPreviewGoToPageNum
		 * @return void
		 */
		void PrintPreviewNavigate(Int16 aNavType, Int32 aPageNum);

		/**
		 * Cancels the current print 
		 * @return void
		 */
		void Cancel();

		/**
		 * Returns an array of the names of all documents names (Title or URL)
		 * and sub-documents. This will return a single item if the attr "isFramesetDocument" is false
		 * and may return any number of items is "isFramesetDocument" is true
		 *
		 * @param  aCount - returns number of printers returned
		 * @param  aResult - returns array of names
		 * @return void
		 */
		[return: MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)]
		String[] EnumerateDocumentNames(out UInt32 aCount);

		/**
		 * This exists PrintPreview mode and returns browser window to galley mode
		 * @return void
		 */
		void ExitPrintPreview();
	}
}
