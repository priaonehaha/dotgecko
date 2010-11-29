using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/* THIS IS A PUBLIC INTERFACE */

	/**
	 * Interface for manipulating and querying the current selected range
	 * of nodes within the document.
	 *
	 * @status FROZEN
	 * @version 1.0
	 */
	[ComImport, Guid("B2C7ED59-8634-4352-9E37-5484C8B6E4E1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISelection //: nsISupports
	{
		/**
		 * Returns the node in which the selection begins.
		 */
		nsIDOMNode AnchorNode { get; }

		/**
		 * The offset within the (text) node where the selection begins.
		 */
		Int32 AnchorOffset { get; }

		/**
		 * Returns the node in which the selection ends.
		 */
		nsIDOMNode FocusNode { get; }

		/**
		 * The offset within the (text) node where the selection ends.
		 */
		Int32 FocusOffset { get; }

		/**
		 * Indicates if the selection is collapsed or not.
		 */
		Boolean IsCollapsed { get; }

		/**
		 * Returns the number of ranges in the selection.
		 */
		Int32 RangeCount { get; }

		/**
		 * Returns the range at the specified index.
		 */
		nsIDOMRange GetRangeAt(Int32 index);

		/**
		 * Collapses the selection to a single point, at the specified offset
		 * in the given DOM node. When the selection is collapsed, and the content
		 * is focused and editable, the caret will blink there.
		 * @param parentNode      The given dom node where the selection will be set
		 * @param offset          Where in given dom node to place the selection (the offset into the given node)
		 */
		void Collapse(nsIDOMNode parentNode, Int32 offset);

		/**
		 * Extends the selection by moving the selection end to the specified node and offset,
		 * preserving the selection begin position. The new selection end result will always
		 * be from the anchorNode to the new focusNode, regardless of direction.
		 * @param parentNode      The node where the selection will be extended to
		 * @param offset          Where in node to place the offset in the new selection end
		 */
		void Extend(nsIDOMNode parentNode, Int32 offset);

		/**
		 * Collapses the whole selection to a single point at the start
		 * of the current selection (irrespective of direction).  If content
		 * is focused and editable, the caret will blink there.
		 */
		void CollapseToStart();

		/**
		 * Collapses the whole selection to a single point at the end
		 * of the current selection (irrespective of direction).  If content
		 * is focused and editable, the caret will blink there.
		 */
		void CollapseToEnd();

		/**
		 * Indicates whether the node is part of the selection. If partlyContained 
		 * is set to PR_TRUE, the function returns true when some part of the node 
		 * is part of the selection. If partlyContained is set to PR_FALSE, the
		 * function only returns true when the entire node is part of the selection.
		 */
		Boolean ContainsNode(nsIDOMNode node, Boolean partlyContained);

		/**
		 * Adds all children of the specified node to the selection.
		 * @param parentNode  the parent of the children to be added to the selection.
		 */
		void SelectAllChildren(nsIDOMNode parentNode);

		/**
		 * Adds a range to the current selection.
		 */
		void AddRange(nsIDOMRange range);

		/**
		 * Removes a range from the current selection.
		 */
		void RemoveRange(nsIDOMRange range);

		/**
		 * Removes all ranges from the current selection.
		 */
		void RemoveAllRanges();

		/**
		 * Deletes this selection from document the nodes belong to.
		 */
		void DeleteFromDocument();

		/**
		 * Modifies the cursor Bidi level after a change in keyboard direction
		 * @param langRTL is PR_TRUE if the new language is right-to-left or
		 *                PR_FALSE if the new language is left-to-right.
		 */
		void SelectionLanguageChange(Boolean langRTL);

		/**
		 * Returns the whole selection into a plain text string.
		 */
		[return: MarshalAs(UnmanagedType.LPWStr)]
		String ToString();
	}
}
