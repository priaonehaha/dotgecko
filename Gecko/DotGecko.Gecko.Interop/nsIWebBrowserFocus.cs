using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIWebBrowserFocus
	 * Interface that embedders use for controlling and interacting
	 * with the browser focus management. The embedded browser can be focused by
	 * clicking in it or tabbing into it. If the browser is currently focused and
	 * the embedding application's top level window is disabled, deactivate() must
	 * be called, and activate() called again when the top level window is
	 * reactivated for the browser's focus memory to work correctly.
	 */
	[ComImport, Guid("9c5d3c58-1dd1-11b2-a1c9-f3699284657a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWebBrowserFocus //: nsISupports
	{
		/**
		 * MANDATORY
		 * activate() is a mandatory call that must be made to the browser
		 * when the embedding application's window is activated *and* the 
		 * browser area was the last thing in focus.  This method can also be called
		 * if the embedding application wishes to give the browser area focus,
		 * without affecting the currently focused element within the browser.
		 *
		 * @note
		 * If you fail to make this call, mozilla focus memory will not work
		 * correctly.
		 */
		void Activate();

		/**
		 * MANDATORY
		 * deactivate() is a mandatory call that must be made to the browser
		 * when the embedding application's window is deactivated *and* the
		 * browser area was the last thing in focus.  On non-windows platforms,
		 * deactivate() should also be called when focus moves from the browser
		 * to the embedding chrome.
		 *
		 * @note
		 * If you fail to make this call, mozilla focus memory will not work
		 * correctly.
		 */
		void Deactivate();

		/**
		 * Give the first element focus within mozilla
		 * (i.e. TAB was pressed and focus should enter mozilla)
		 */
		void SetFocusAtFirstElement();

		/**
		 * Give the last element focus within mozilla
		 * (i.e. SHIFT-TAB was pressed and focus should enter mozilla)
		 */
		void SetFocusAtLastElement();

		/**
		 * The currently focused nsDOMWindow when the browser is active,
		 * or the last focused nsDOMWindow when the browser is inactive.
		 */
		nsIDOMWindow GetFocusedWindow();
		void SetFocusedWindow(nsIDOMWindow value);

		/**
		 * The currently focused nsDOMElement when the browser is active,
		 * or the last focused nsDOMElement when the browser is inactive.
		 */
		nsIDOMElement GetFocusedElement();
		void SetFocusedElement(nsIDOMElement value);
	}
}
