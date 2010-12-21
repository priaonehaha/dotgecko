using System;
using System.Runtime.InteropServices;
using JSContext = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	public static class nsIAppShellServiceConstants
	{
		public const Int32 SIZE_TO_CONTENT = -1;
	}

	[ComImport, Guid("361facd0-6e9a-4ff1-a0d4-450744cf0023"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIAppShellService //: nsISupports
	{
		/**
		 * Create a window, which will be initially invisible.
		 * @param aParent the parent window.  Can be null.
		 * @param aUrl the contents of the new window.
		 * @param aChromeMask chrome flags affecting the kind of OS border
		 *                    given to the window. see nsIBrowserWindow for
		 *                    bit/flag definitions.
		 * @param aCallbacks interface providing C++ hooks for window initialization
		 *                   before the window is made visible.  Can be null.
		 *                   Deprecated.
		 * @param aInitialWidth width, in pixels, of the window.  Width of window
		 *                      at creation.  Can be overridden by the "width"
		 *                      tag in the XUL.  Set to NS_SIZETOCONTENT to force
		 *                      the window to wrap to its contents.
		 * @param aInitialHeight like aInitialWidth, but subtly different.
		 * @param aAppShell a widget "appshell" (event processor) to associate
		 *        with the new window
		 * @param aResult the newly created window is returned here.
		 */
		nsIXULWindow CreateTopLevelWindow(nsIXULWindow aParent,
										  nsIURI aUrl,
										  UInt32 aChromeMask,
										  Int32 aInitialWidth,
										  Int32 aInitialHeight,
										  nsIAppShell aAppShell);

		void CreateHiddenWindow(nsIAppShell aAppShell);

		void DestroyHiddenWindow();

		/**
		 * Return the (singleton) application hidden window, automatically created
		 * and maintained by this AppShellService.
		 * @param aResult the hidden window.  Do not unhide hidden window.
		 *                Do not taunt hidden window.
		 */
		nsIXULWindow HiddenWindow { get; }

		/**
		 * Return the (singleton) application hidden window, automatically created
		 * and maintained by this AppShellService.
		 * @param aResult the hidden window.  Do not unhide hidden window.
		 *                Do not taunt hidden window.
		 */
		nsIDOMWindowInternal HiddenDOMWindow { get; }

		/**
		 * Return the (singleton) application hidden window as an nsIDOMWindowInternal,
		 * and, the corresponding JavaScript context pointer.  This is useful
		 * if you'd like to subsequently call OpenDialog on the hidden window.
		 * @aHiddenDOMWindow the hidden window QI'd to type nsIDOMWindowInternal
		 * @aJSContext       the corresponding JavaScript context
		 */
		void GetHiddenWindowAndJSContext(out nsIDOMWindowInternal aHiddenDOMWindow, out JSContext aJSContext);

		/**
		 * Return true if the application hidden window was provided by the
		 * application. If it wasn't, the default hidden window was used. This will
		 * usually be false on all non-mac platforms.
		 */
		Boolean ApplicationProvidedHiddenWindow { get; }

		/**
		 * Add a window to the application's registry of windows.  These windows
		 * are generally shown in the Windows taskbar, and the application
		 * knows it can't quit until it's out of registered windows.
		 * @param aWindow the window to register
		 * @note When this method is successful, it fires the global notification
		 *       "xul-window-registered"
		 */
		void RegisterTopLevelWindow(nsIXULWindow aWindow);

		/**
		 * Remove a window from the application's window registry. Note that
		 * this method won't automatically attempt to quit the app when
		 * the last window is unregistered. For that, see Quit().
		 * @param aWindow you see the pattern
		 */
		void UnregisterTopLevelWindow(nsIXULWindow aWindow);
	}
}
