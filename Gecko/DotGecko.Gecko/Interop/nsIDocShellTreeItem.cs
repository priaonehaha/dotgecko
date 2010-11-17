using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	internal static class nsIDocShellTreeItemConstants
	{
		/*
		 Definitions for the item types.
		 */
		internal const Int32 typeChrome = 0;            // typeChrome must equal 0
		internal const Int32 typeContent = 1;           // typeContent must equal 1
		internal const Int32 typeContentWrapper = 2;    // typeContentWrapper must equal 2
		internal const Int32 typeChromeWrapper = 3;     // typeChromeWrapper must equal 3

		internal const Int32 typeAll = 0x7FFFFFFF;
	}

	/**
	 * The nsIDocShellTreeItem supplies the methods that are required of any item
	 * that wishes to be able to live within the docshell tree either as a middle
	 * node or a leaf. 
	 */
	[ComImport, Guid("09b54ec1-d98a-49a9-bc95-3219e8b55089"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDocShellTreeItem : nsIDocShellTreeNode
	{
		#region nsIDocShellTreeNode Members

		new Int32 ChildCount { get; }
		new void AddChild(nsIDocShellTreeItem child);
		new void RemoveChild(nsIDocShellTreeItem child);
		new nsIDocShellTreeItem GetChildAt(Int32 index);
		new nsIDocShellTreeItem FindChildWithName([MarshalAs(UnmanagedType.LPWStr)] String aName, Boolean aRecurse, Boolean aSameType, nsIDocShellTreeItem aRequestor, nsIDocShellTreeItem aOriginalRequestor);

		#endregion

		/*
		 name of the DocShellTreeItem
		 */
		String Name { [return: MarshalAs(UnmanagedType.LPWStr)]get; [param: MarshalAs(UnmanagedType.LPWStr)]set; }

		/**
		 * Compares the provided name against the item's name and
		 * returns the appropriate result.
		 *
		 * @return <CODE>PR_TRUE</CODE> if names match;
		 *         <CODE>PR_FALSE</CODE> otherwise.
		 */
		Boolean NameEquals([MarshalAs(UnmanagedType.LPWStr)] String name);

		/*
		The type this item is.  
		*/
		Int32 ItemType { get; set; }

		/*
		Parent DocShell.
		*/
		nsIDocShellTreeItem Parent { get; }

		/*
		This is call returns the same thing parent does however if the parent is
		of a different itemType, it will instead return nsnull.  This call is a
		convience function for those wishing to not cross the boundaries at which
		item types change.
		*/
		nsIDocShellTreeItem SameTypeParent { get; }

		/*
		Returns the root DocShellTreeItem.  This is a convience equivalent to 
		getting the parent and its parent until there isn't a parent.
		*/
		nsIDocShellTreeItem RootTreeItem { get; }

		/*
		Returns the root DocShellTreeItem of the same type.  This is a convience 
		equivalent to getting the parent of the same type and its parent until 
		there isn't a parent.
		*/
		nsIDocShellTreeItem SameTypeRootTreeItem { get; }

		/*
		Returns the docShellTreeItem with the specified name.  Search order is as 
		follows...
		1.)  Check name of self, if it matches return it.
		2.)  For each immediate child.
			a.) Check name of child and if it matches return it.
			b.)  Ask the child to perform the check
				i.) Do not ask a child if it is the aRequestor
				ii.) Do not ask a child if it is of a different item type.
		3.)  If there is a parent of the same item type ask parent to perform the check
			a.) Do not ask parent if it is the aRequestor
		4.)  If there is a tree owner ask the tree owner to perform the check
			a.)  Do not ask the tree owner if it is the aRequestor
			b.)  This should only be done if there is no parent of the same type.

		Return the child DocShellTreeItem with the specified name.
		name - This is the name of the item that is trying to be found.
		aRequestor - This is the object that is requesting the find.  This
			parameter is used to identify when the child is asking its parent to find
			a child with the specific name.  The parent uses this parameter to ensure
			a resursive state does not occur by not again asking the requestor to find
			a shell by the specified name.  Inversely the child uses it to ensure it
			does not ask its parent to do the search if its parent is the one that
			asked it to search.  Children also use this to test against the treeOwner;
		aOriginalRequestor - The original treeitem that made the request, if any.
			This is used to ensure that we don't run into cross-site issues.
		*/
		nsIDocShellTreeItem FindItemWithName([MarshalAs(UnmanagedType.LPWStr)] String name, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aRequestor, nsIDocShellTreeItem aOriginalRequestor);

		/*
		The owner of the DocShell Tree.  This interface will be called upon when
		the docshell has things it needs to tell to the owner of the docshell.
		Note that docShell tree ownership does not cross tree types.  Meaning
		setting ownership on a chrome tree does not set ownership on the content 
		sub-trees.  A given tree's boundaries are identified by the type changes.
		Trees of different types may be connected, but should not be traversed
		for things such as ownership.
	
		Note implementers of this interface should NOT effect the lifetime of the 
		parent DocShell by holding this reference as it creates a cycle.  Owners
		when releasing this interface should set the treeOwner to nsnull.
		Implementers of this interface are guaranteed that when treeOwner is
		set that the poitner is valid without having to addref.
	
		Further note however when others try to get the interface it should be 
		addref'd before handing it to them. 
		*/
		nsIDocShellTreeOwner TreeOwner { get; }
		void SetTreeOwner(nsIDocShellTreeOwner treeOwner);
	}
}
