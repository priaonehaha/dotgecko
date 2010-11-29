using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDocShellTreeOwner
	 */
	[ComImport, Guid("bc0eb30e-656e-491e-a7ae-7f460b660c8d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDocShellTreeOwner //: nsISupports
	{
		/*
		Return the child DocShellTreeItem with the specified name.
		name - This is the name of the item that is trying to be found.
		aRequestor - This is the docshellTreeItem that is requesting the find.  This
		parameter is used to identify when the child is asking its parent to find
		a child with the specific name.  The parent uses this parameter to ensure
		a resursive state does not occur by not again asking the requestor for find
		a shell by the specified name.  Inversely the child uses it to ensure it
		does not ask its parent to do the search if its parent is the one that
		asked it to search.
		aOriginalRequestor - The original treeitem that made the request, if any.
		This is used to ensure that we don't run into cross-site issues.

		*/
		nsIDocShellTreeItem FindItemWithName([MarshalAs(UnmanagedType.LPWStr)] String name, nsIDocShellTreeItem aRequestor, nsIDocShellTreeItem aOriginalRequestor);

		/**
		 * Called when a content shell is added to the docshell tree.  This is
		 * _only_ called for "root" content shells (that is, ones whose parent is a
		 * chrome shell).
		 *
		 * @param aContentShell the shell being added.
		 * @param aPrimary whether the shell is primary.
		 * @param aTargetable whether the shell can be a target for named window
		 *					targeting.
		 * @param aID the "id" of the shell.  What this actually means is
		 *			undefined. Don't rely on this for anything.
		 */
		void ContentShellAdded(
			nsIDocShellTreeItem aContentShell, Boolean aPrimary, Boolean aTargetable,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aID);

		/**
		 * Called when a content shell is removed from the docshell tree.  This is
		 * _only_ called for "root" content shells (that is, ones whose parent is a
		 * chrome shell).  Note that if aContentShell was never added,
		 * contentShellRemoved should just do nothing.
		 *
		 * @param aContentShell the shell being removed.
		 */
		void ContentShellRemoved(nsIDocShellTreeItem aContentShell);

		/*
		Returns the Primary Content Shell
		*/
		nsIDocShellTreeItem PrimaryContentShell { get; }

		/*
		Tells the tree owner to size its window or parent window in such a way
		that the shell passed along will be the size specified.
		*/
		void SizeShellTo(nsIDocShellTreeItem shell, Int32 cx, Int32 cy);

		/*
		Sets the persistence of different attributes of the window.
		*/
		void SetPersistence(Boolean aPersistPosition, Boolean aPersistSize, Boolean aPersistSizeMode);

		/*
		Gets the current persistence states of the window.
		*/
		void GetPersistence(out Boolean aPersistPosition, out Boolean aPersistSize, out Boolean aPersistSizeMode);
	}
}
