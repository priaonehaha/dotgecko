using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/* THIS IS A PUBLIC EMBEDDING API */

	// Constants for nsIEmbeddingSiteWindow ( "3E5432CD-9568-4bd1-8CBE-D50ABA110743" ) interface
	public static class nsIEmbeddingSiteWindowConstants
	{
		/**
		 * Flag indicates that position of the top left corner of the outer area
		 * is required/specified.
		 *
		 * @see setDimensions
		 * @see getDimensions
		 */
		public const UInt32 DIM_FLAGS_POSITION = 1;

		/**
		 * Flag indicates that the size of the inner area is required/specified.
		 *
		 * @note The inner and outer flags are mutually exclusive and it is
		 *       invalid to combine them.
		 *
		 * @see setDimensions
		 * @see getDimensions
		 * @see DIM_FLAGS_SIZE_OUTER
		 */
		public const UInt32 DIM_FLAGS_SIZE_INNER = 2;

		/**
		 * Flag indicates that the size of the outer area is required/specified.
		 *
		 * @see setDimensions
		 * @see getDimensions
		 * @see DIM_FLAGS_SIZE_INNER
		 */
		public const UInt32 DIM_FLAGS_SIZE_OUTER = 4;
	}

	/**
	 * The nsIEmbeddingSiteWindow is implemented by the embedder to provide
	 * Gecko with the means to call up to the host to resize the window,
	 * hide or show it and set/get its title.
	 */
	[ComImport, Guid("3E5432CD-9568-4bd1-8CBE-D50ABA110743"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIEmbeddingSiteWindow //: nsISupports
	{
		/**
		 * Sets the dimensions for the window; the position & size. The
		 * flags to indicate what the caller wants to set and whether the size
		 * refers to the inner or outer area. The inner area refers to just
		 * the embedded area, wheras the outer area can also include any 
		 * surrounding chrome, window frame, title bar, and so on.
		 *
		 * @param flags  Combination of position, inner and outer size flags.
		 * @param x      Left hand corner of the outer area.
		 * @param y      Top corner of the outer area.
		 * @param cx     Width of the inner or outer area.
		 * @param cy     Height of the inner or outer area.
		 *
		 * @return <code>NS_OK</code> if operation was performed correctly;
		 *         <code>NS_ERROR_UNEXPECTED</code> if window could not be
		 *           destroyed;
		 *         <code>NS_ERROR_INVALID_ARG</code> for bad flag combination
		 *           or illegal dimensions.
		 *
		 * @see getDimensions
		 * @see DIM_FLAGS_POSITION
		 * @see DIM_FLAGS_SIZE_OUTER
		 * @see DIM_FLAGS_SIZE_INNER
		 */
		void SetDimensions(UInt32 flags, Int32 x, Int32 y, Int32 cx, Int32 cy);

		/**
		 * Gets the dimensions of the window. The caller may pass
		 * <CODE>nsnull</CODE> for any value it is uninterested in receiving.
		 *
		 * @param flags  Combination of position, inner and outer size flag .
		 * @param x      Left hand corner of the outer area; or <CODE>nsnull</CODE>.
		 * @param y      Top corner of the outer area; or <CODE>nsnull</CODE>.
		 * @param cx     Width of the inner or outer area; or <CODE>nsnull</CODE>.
		 * @param cy     Height of the inner or outer area; or <CODE>nsnull</CODE>.
		 *
		 * @see setDimensions
		 * @see DIM_FLAGS_POSITION
		 * @see DIM_FLAGS_SIZE_OUTER
		 * @see DIM_FLAGS_SIZE_INNER
		 */
		void GetDimensions(UInt32 flags, out Int32 x, out Int32 y, out Int32 cx, out Int32 cy);

		/**
		 * Give the window focus.
		 */
		void SetFocus();

		/**
		 * Visibility of the window.
		 */
		Boolean Visibility { get; set; }

		/**
		 * Title of the window.
		 */
		String Title { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }

		/**
		 * Native window for the site's window. The implementor should copy the
		 * native window object into the address supplied by the caller. The
		 * type of the native window that the address refers to is  platform
		 * and OS specific as follows:
		 *
		 * <ul>
		 *   <li>On Win32 it is an <CODE>HWND</CODE>.</li>
		 *   <li>On MacOS this is a <CODE>WindowPtr</CODE>.</li>
		 *   <li>On GTK this is a <CODE>GtkWidget*</CODE>.</li>
		 * </ul>
		 */
		IntPtr SiteWindow { get; }
	}
}
