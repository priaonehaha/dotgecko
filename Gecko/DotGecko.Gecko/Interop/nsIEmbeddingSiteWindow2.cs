using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/* THIS IS A PUBLIC EMBEDDING API */

	/**
	 * The nsIEmbeddingSiteWindow is implemented by the embedder to provide
	 * Gecko with the means to call up to the host to perform basic windowing
	 * operations such as resizing and showing.
	 *
	 * Changes from version 1 to version 2:
	 * A new method: blur()
	 */
	[ComImport, Guid("e932bf55-0a64-4beb-923a-1f32d3661044"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIEmbeddingSiteWindow2 : nsIEmbeddingSiteWindow
	{
		#region nsIEmbeddingSiteWindow Members

		new void SetDimensions(UInt32 flags, Int32 x, Int32 y, Int32 cx, Int32 cy);
		new void GetDimensions(UInt32 flags, out Int32 x, out Int32 y, out Int32 cx, out Int32 cy);
		new void SetFocus();
		new Boolean Visibility { get; set; }
		new String Title { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }
		new IntPtr SiteWindow { get; }

		#endregion

		/**
		 * Blur the window. This should unfocus the window and send an onblur event.
		 */
		void Blur();
	}
}
