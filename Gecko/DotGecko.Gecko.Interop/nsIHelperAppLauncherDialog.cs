using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	public static class nsIHelperAppLauncherDialogConstants
	{
		/**
		 * This request is passed to the helper app dialog because Gecko can not
		 * handle content of this type.
		 */
		public const Int32 REASON_CANTHANDLE = 0;

		/**
		 * The server requested external handling.
		 */
		public const Int32 REASON_SERVERREQUEST = 1;

		/**
		 * Gecko detected that the type sent by the server (e.g. text/plain) does
		 * not match the actual type.
		 */
		public const Int32 REASON_TYPESNIFFED = 2;
	}

	/**
	 * This interface is used to display a confirmation dialog before
	 * launching a "helper app" to handle content not handled by
	 * Mozilla.
	 *
	 * Usage:  Clients (of which there is one: the nsIExternalHelperAppService
	 * implementation in mozilla/uriloader/exthandler) create an instance of
	 * this interface (using the contract ID) and then call the show() method.
	 *
	 * The dialog is shown non-modally.  The implementation of the dialog
	 * will access methods of the nsIHelperAppLauncher passed in to show()
	 * in order to cause a "save to disk" or "open using" action.
	 */
	[ComImport, Guid("f3704fdc-8ae6-4eba-a3c3-f02958ac0649"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIHelperAppLauncherDialog //: nsISupports
	{
		/**
		 * Show confirmation dialog for launching application (or "save to
		 * disk") for content specified by aLauncher.
		 *
		 * @param aLauncher
		 *        A nsIHelperAppLauncher to be invoked when a file is selected.
		 * @param aWindowContext
		 *        Window associated with action.
		 * @param aReason
		 *        One of the constants from above. It indicates why the dialog is
		 *        shown. Implementors should treat unknown reasons like
		 *        REASON_CANTHANDLE.
		 */
		void Show(nsIHelperAppLauncher aLauncher, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aWindowContext, UInt32 aReason);

		/**
		 * Invoke a save-to-file dialog instead of the full fledged helper app dialog.
		 * Returns the a nsILocalFile for the file name/location selected.
		 *
		 * @param aLauncher
		 *        A nsIHelperAppLauncher to be invoked when a file is selected.
		 * @param aWindowContext
		 *        Window associated with action.
		 * @param aDefaultFileName
		 *        Default file name to provide (can be null)
		 * @param aSuggestedFileExtension
		 *        Sugested file extension
		 * @param aForcePrompt
		 *        Set to true to force prompting the user for thet file
		 *        name/location, otherwise perferences may control if the user is
		 *        prompted.
		 */
		nsILocalFile PromptForSaveToFile(nsIHelperAppLauncher aLauncher,
										 [MarshalAs(UnmanagedType.IUnknown)] nsISupports aWindowContext,
										 [MarshalAs(UnmanagedType.LPWStr)] String aDefaultFileName,
										 [MarshalAs(UnmanagedType.LPWStr)] String aSuggestedFileExtension,
										 Boolean aForcePrompt);
	}
}
