using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIWebBrowserChromeFocus is implemented by the same object as the
	 * nsIEmbeddingSiteWindow. It represents the focus up-calls from mozilla
	 * to the embedding chrome. See mozilla bug #70224 for gratuitous info.
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("d2206418-1dd1-11b2-8e55-acddcd2bcfb8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWebBrowserChromeFocus //: nsISupports
	{
		/**
		 * Set the focus at the next focusable element in the chrome.
		 */
		void FocusNextElement();

		/**
		 * Set the focus at the previous focusable element in the chrome.
		 */
		void FocusPrevElement();
	}
}
