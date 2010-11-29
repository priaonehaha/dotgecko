using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDocShellTreeNode supplies the methods for interacting with children
	 * of a docshell.  These are essentially the methods that turn a single docshell
	 * into a docshell tree. 
	 */

	/*
	 * Long-term, we probably want to merge this interface into
	 * nsIDocShellTreeItem.  Need to eliminate uses of this interface
	 * first.
	 */
	[ComImport, Guid("37f1ab73-f224-44b1-82f0-d2834ab1cec0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDocShellTreeNode //: nsISupports
	{
		/*
		The current number of DocShells which are immediate children of the 
		this object.
		*/
		Int32 ChildCount { get; }

		/*
		Add a new child DocShellTreeItem.  Adds to the end of the list.
		Note that this does NOT take a reference to the child.  The child stays
		alive only as long as it's referenced from outside the docshell tree.
		@throws NS_ERROR_ILLEGAL_VALUE if child corresponds to the same
				object as this treenode or an ancestor of this treenode
		@throws NS_ERROR_UNEXPECTED if this node is a leaf in the tree.
		*/
		void AddChild(nsIDocShellTreeItem child);

		/*
		Removes a child DocShellTreeItem.
		@throws NS_ERROR_UNEXPECTED if this node is a leaf in the tree.
		*/
		void RemoveChild(nsIDocShellTreeItem child);

		/**
		 * Return the child at the index requested.  This is 0-based.
		 *
		 * @throws NS_ERROR_UNEXPECTED if the index is out of range
		 */
		nsIDocShellTreeItem GetChildAt(Int32 index);

		/*
		Return the child DocShellTreeItem with the specified name.
		aName - This is the name of the item that is trying to be found.
		aRecurse - Is used to tell the function to recurse through children.
			Note, recursion will only happen through items of the same type.
		aSameType - If this is set only children of the same type will be returned.
		aRequestor - This is the docshellTreeItem that is requesting the find.  This
			parameter is used when recursion is being used to avoid searching the same
			tree again when a child has asked a parent to search for children.
		aOriginalRequestor - The original treeitem that made the request, if any.
			This is used to ensure that we don't run into cross-site issues.

		Note the search is depth first when recursing.
		*/
		nsIDocShellTreeItem FindChildWithName([MarshalAs(UnmanagedType.LPWStr)] String aName,
											  Boolean aRecurse,
											  Boolean aSameType,
											  nsIDocShellTreeItem aRequestor,
											  nsIDocShellTreeItem aOriginalRequestor);
	}
}
