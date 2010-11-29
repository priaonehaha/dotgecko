using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * An interface for embedding clients who wish to interact with
	 * the system-wide OS clipboard. Mozilla does not use a private
	 * clipboard, instead it places its data directly onto the system 
	 * clipboard. The webshell implements this interface.
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("b8100c90-73be-11d2-92a5-00105a1b0d64"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIClipboardCommands //: nsISupports
	{
		/**
		 * Returns whether there is a selection and it is not read-only.
		 *
		 * @return <code>true</code> if the current selection can be cut,
		 *          <code>false</code> otherwise.
		 */
		Boolean CanCutSelection();

		/**
		 * Returns whether there is a selection and it is copyable.
		 *
		 * @return <code>true</code> if there is a selection,
		 *          <code>false</code> otherwise.
		 */
		Boolean CanCopySelection();

		/**
		 * Returns whether we can copy a link location.
		 *
		 * @return <code>true</code> if a link is selected,
		 *           <code>false</code> otherwise.
		 */
		Boolean CanCopyLinkLocation();

		/**
		 * Returns whether we can copy an image location.
		 *
		 * @return <code>true</code> if an image is selected,
					<code>false</code> otherwise.
		 */
		Boolean CanCopyImageLocation();

		/**
		 * Returns whether we can copy an image's contents.
		 *
		 * @return <code>true</code> if an image is selected,
		 *          <code>false</code> otherwise
		 */
		Boolean CanCopyImageContents();

		/**
		 * Returns whether the current contents of the clipboard can be
		 * pasted and if the current selection is not read-only.
		 *
		 * @return <code>true</code> there is data to paste on the clipboard
		 *          and the current selection is not read-only,
		 *          <code>false</code> otherwise
		 */
		Boolean CanPaste();

		/**
		 * Cut the current selection onto the clipboard.
		 */
		void CutSelection();

		/**
		 * Copy the current selection onto the clipboard.
		 */
		void CopySelection();

		/**
		 * Copy the link location of the current selection (e.g.,
		 * the |href| attribute of a selected |a| tag).
		 */
		void CopyLinkLocation();

		/**
		 * Copy the location of the selected image.
		 */
		void CopyImageLocation();

		/**
		 * Copy the contents of the selected image.
		 */
		void CopyImageContents();

		/**
		 * Paste the contents of the clipboard into the current selection.
		 */
		void Paste();

		/**
		 * Select the entire contents.
		 */
		void SelectAll();

		/**
		 * Clear the current selection (if any). Insertion point ends up
		 * at beginning of current selection.
		 */
		void SelectNone();
	}
}
