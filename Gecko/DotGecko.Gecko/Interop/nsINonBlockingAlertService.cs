using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * This is the interface to the embeddable non-blocking alert
	 * service.  A non-blocking alert is a less serious informative alert
	 * that does not need to block the program's execution to get the
	 * user's response.
	 *
	 * The way to present the alert is left to the implementations.  It
	 * may be a dialog separate from the parent window, or a window-modal
	 * sheet (as the ones in Mac OS X) attached to the parent.
	 */
	[ComImport, Guid("E800EF97-AE37-46B7-A46C-31FBE79657EA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsINonBlockingAlertService //: nsISupports
	{
		/**
		 * This shows a non-blocking alert with the specified title and
		 * message text. This function requires a valid parent window with
		 * which the alert is associated.
		 *
		 * @param aParent
		 *        The parent window. This must not be null.
		 * @param aDialogTitle
		 *        Text to appear in the title of the alert.
		 * @param aText
		 *        Text to appear in the body of the alert.
		 */
		void ShowNonBlockingAlert(nsIDOMWindow aParent, [MarshalAs(UnmanagedType.LPWStr)] String aDialogTitle, [MarshalAs(UnmanagedType.LPWStr)] String aText);
	}
}
