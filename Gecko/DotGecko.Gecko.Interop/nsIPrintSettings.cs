using System;
using System.Runtime.InteropServices;
using nsIPrintSession = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	public static class nsIPrintSettingsConstants
	{
		/**
		 * PrintSettings to be Saved Navigation Constants
		 */
		public const UInt32 kInitSaveOddEvenPages = 0x00000001;
		public const UInt32 kInitSaveHeaderLeft = 0x00000002;
		public const UInt32 kInitSaveHeaderCenter = 0x00000004;
		public const UInt32 kInitSaveHeaderRight = 0x00000008;
		public const UInt32 kInitSaveFooterLeft = 0x00000010;
		public const UInt32 kInitSaveFooterCenter = 0x00000020;
		public const UInt32 kInitSaveFooterRight = 0x00000040;
		public const UInt32 kInitSaveBGColors = 0x00000080;
		public const UInt32 kInitSaveBGImages = 0x00000100;
		public const UInt32 kInitSavePaperSize = 0x00000200;
		/* Flag 0x00000400 is unused */
		/* Flag 0x00000800 is unused */
		/* Flag 0x00001000 is unused */
		public const UInt32 kInitSavePaperData = 0x00002000;
		public const UInt32 kInitSaveUnwriteableMargins = 0x00004000;
		public const UInt32 kInitSaveEdges = 0x00008000;

		public const UInt32 kInitSaveReversed = 0x00010000;
		public const UInt32 kInitSaveInColor = 0x00020000;
		public const UInt32 kInitSaveOrientation = 0x00040000;
		public const UInt32 kInitSavePrintCommand = 0x00080000;
		public const UInt32 kInitSavePrinterName = 0x00100000;
		public const UInt32 kInitSavePrintToFile = 0x00200000;
		public const UInt32 kInitSaveToFileName = 0x00400000;
		public const UInt32 kInitSavePageDelay = 0x00800000;
		public const UInt32 kInitSaveMargins = 0x01000000;
		public const UInt32 kInitSaveNativeData = 0x02000000;
		public const UInt32 kInitSavePlexName = 0x04000000;
		public const UInt32 kInitSaveShrinkToFit = 0x08000000;
		public const UInt32 kInitSaveScaling = 0x10000000;
		public const UInt32 kInitSaveColorspace = 0x20000000;
		public const UInt32 kInitSaveResolutionName = 0x40000000;
		public const UInt32 kInitSaveDownloadFonts = 0x80000000;
		public const UInt32 kInitSaveAll = 0xFFFFFFFF;

		/* Print Option Flags for Bit Field*/
		public const Int32 kPrintOddPages = 0x00000001;
		public const Int32 kPrintEvenPages = 0x00000002;
		public const Int32 kEnableSelectionRB = 0x00000004;

		/* Print Range Enums */
		public const Int32 kRangeAllPages = 0;
		public const Int32 kRangeSpecifiedPageRange = 1;
		public const Int32 kRangeSelection = 2;
		public const Int32 kRangeFocusFrame = 3;

		/* Justification Enums */
		public const Int32 kJustLeft = 0;
		public const Int32 kJustCenter = 1;
		public const Int32 kJustRight = 2;

		/**
		 * FrameSet Default Type Constants
		 */
		public const Int16 kUseInternalDefault = 0;
		public const Int16 kUseSettingWhenPossible = 1;

		/**
		 * Page Size Type Constants
		 */
		public const Int16 kPaperSizeNativeData = 0;
		public const Int16 kPaperSizeDefined = 1;

		/**
		 * Page Size Unit Constants
		 */
		public const Int16 kPaperSizeInches = 0;
		public const Int16 kPaperSizeMillimeters = 1;

		/**
		 * Orientation Constants
		 */
		public const Int16 kPortraitOrientation = 0;
		public const Int16 kLandscapeOrientation = 1;

		/**
		 * Print Frame Constants
		 */
		public const Int16 kNoFrames = 0;
		public const Int16 kFramesAsIs = 1;
		public const Int16 kSelectedFrame = 2;
		public const Int16 kEachFrameSep = 3;

		/**
		 * How to Enable Frame Set Printing Constants
		 */
		public const Int16 kFrameEnableNone = 0;
		public const Int16 kFrameEnableAll = 1;
		public const Int16 kFrameEnableAsIsAndEach = 2;

		/**
		 * Output file format
		 */
		public const Int16 kOutputFormatNative = 0;
		public const Int16 kOutputFormatPS = 1;
		public const Int16 kOutputFormatPDF = 2;
	}

	/**
	 * Simplified graphics interface for JS rendering.
	 */
	[ComImport, Guid("343700dd-078b-42b6-a809-b9c1d7e951d0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIPrintSettings //: nsISupports
	{
		/**
		 * Set PrintOptions 
		 */
		void SetPrintOptions(Int32 aType, Boolean aTurnOnOff);

		/**
		 * Get PrintOptions 
		 */
		Boolean GetPrintOptions(Int32 aType);

		/**
		 * Set PrintOptions Bit field
		 */
		Int32 GetPrintOptionsBits();

		/**
		 * Get the page size in twips, considering the
		 * orientation (portrait or landscape).
		 */
		void GetEffectivePageSize(out Double aWidth, out Double aHeight);

		/**
		 * Makes a new copy
		 */
		nsIPrintSettings Clone();

		/**
		 * Assigns the internal values from the "in" arg to the current object
		 */
		void Assign(nsIPrintSettings aPS);

		/**
		 * Data Members
		 */
		nsIPrintSession PrintSession { get; set; } /* We hold a weak reference */

		Int32 StartPageRange { get; set; }
		Int32 EndPageRange { get; set; }

		/**
		 * The edge measurements define the positioning of the headers
		 * and footers on the page. They're measured as an offset from
		 * the "unwriteable margin" (described below).
		 */
		Double EdgeTop { get; set; }     /*  these are in inches */
		Double EdgeLeft { get; set; }
		Double EdgeBottom { get; set; }
		Double EdgeRight { get; set; }

		/**
		 * The margins define the positioning of the content on the page.
		 * They're treated as an offset from the "unwriteable margin"
		 * (described below).
		 */
		Double MarginTop { get; set; }     /*  these are in inches */
		Double MarginLeft { get; set; }
		Double MarginBottom { get; set; }
		Double MarginRight { get; set; }
		/**
		 * The unwriteable margin defines the printable region of the paper, creating
		 * an invisible border from which the edge and margin attributes are measured.
		 */
		Double UnwriteableMarginTop { get; set; }     /*  these are in inches */
		Double UnwriteableMarginLeft { get; set; }
		Double UnwriteableMarginBottom { get; set; }
		Double UnwriteableMarginRight { get; set; }

		Double Scaling { get; set; }      /* values 0.0 - 1.0 */
		Boolean PrintBGColors { get; set; } /* Print Background Colors */
		Boolean PrintBGImages { get; set; } /* Print Background Images */

		Int16 PrintRange { get; set; }

		String Title { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }
		String DocURL { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }

		String HeaderStrLeft { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }
		String HeaderStrCenter { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }
		String HeaderStrRight { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }

		String FooterStrLeft { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }
		String FooterStrCenter { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }
		String FooterStrRight { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }

		Int16 HowToEnableFrameUI { get; set; }  /* indicates how to enable the frameset UI            */
		Boolean IsCancelled { get; set; }         /* indicates whether the print job has been cancelled */
		Int16 PrintFrameTypeUsage { get; set; } /* indicates whether to use the interal value or not  */
		Int16 PrintFrameType { get; set; }
		Boolean PrintSilent { get; set; }	     /* print without putting up the dialog */
		Boolean ShrinkToFit { get; set; }	     /* shrinks content to fit on page      */
		Boolean ShowPrintProgress { get; set; }   /* indicates whether the progress dialog should be shown */

		/* Additional XP Related */
		String PaperName { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }     /* name of paper */
		Int16 PaperSizeType { get; set; } /* use native data or is defined here */
		Int16 PaperData { get; set; }     /* native data value */
		Double PaperWidth { get; set; }    /* width of the paper in inches or mm */
		Double PaperHeight { get; set; }   /* height of the paper in inches or mm */
		Int16 PaperSizeUnit { get; set; } /* paper is in inches or mm */

		String PlexName { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }      /* name of plex mode (like "simplex", "duplex",
                                    * "tumble" and various custom values) */

		String Colorspace { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }    /* device-specific name of colorspace, overrides |printInColor| */
		String ResolutionName { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }/* device-specific identifer of resolution or quality
                                    * (like "600", "600x300", "600x300x12", "high-res",
                                    * "med-res". "low-res", etc.) */
		Boolean DownloadFonts { get; set; } /* enable font download to printer? */

		Boolean PrintReversed { get; set; }
		Boolean PrintInColor { get; set; }  /* a false means grayscale */
		Int32 Orientation { get; set; }   /*  see orientation consts */
		String PrintCommand { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }
		Int32 NumCopies { get; set; }

		String PrinterName { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }   /* name of destination printer */

		Boolean PrintToFile { get; set; }
		String ToFileName { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }
		Int16 OutputFormat { get; set; }

		Int32 PrintPageDelay { get; set; } /* in milliseconds */

		/* initialize helpers */
		/**
		 * This attribute tracks whether the PS has been initialized 
		 * from a printer specified by the "printerName" attr. 
		 * If a different name is set into the "printerName" 
		 * attribute than the one it was initialized with the PS
		 * will then get intialized from that printer.
		 */
		Boolean IsInitializedFromPrinter { get; set; }

		/**
		 * This attribute tracks whether the PS has been initialized 
		 * from prefs. If a different name is set into the "printerName" 
		 * attribute than the one it was initialized with the PS
		 * will then get intialized from prefs again.
		 */
		Boolean IsInitializedFromPrefs { get; set; }

		/* C++ Helper Functions */
		void SetMarginInTwips([In] ref nsIntMargin aMargin);
		void SetEdgeInTwips([In] ref nsIntMargin aEdge);
		/* Purposely made this an "in" arg */
		void GetMarginInTwips(ref nsIntMargin aMargin);
		void GetEdgeInTwips(ref nsIntMargin aEdge);

		/**
		 * We call this function so that anything that requires a run of the event loop
		 * can do so safely. The print dialog runs the event loop but in silent printing
		 * that doesn't happen.
		 *
		 * Either this or ShowPrintDialog (but not both) MUST be called by the print engine
		 * before printing, otherwise printing can fail on some platforms.
		 */
		void SetupSilentPrinting();

		/**
		 * Sets/Gets the "unwriteable margin" for the page format.  This defines
		 * the boundary from which we'll measure the EdgeInTwips and MarginInTwips 
		 * attributes, to place the headers and content, respectively.
		 *
		 * Note: Implementations of SetUnwriteableMarginInTwips should handle
		 * negative margin values by falling back on the system default for
		 * that margin.
		 */
		void SetUnwriteableMarginInTwips([In] ref nsIntMargin aEdge);
		void GetUnwriteableMarginInTwips(ref nsIntMargin aEdge);
	}
}
