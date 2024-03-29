using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIJumpListBuilderConstants
	{
		/**
		 * List Types
		 */

		/**
		 * Task List
		 * 
		 * Tasks are common actions performed by users within the application. A task
		 * can be represented by an application shortcut and associated command line
		 * parameters or a URI. Task lists should generally be static lists that do not
		 * change often, if at all - similar to an application menu.
		 *
		 * Tasks are given the highest priority of all lists when space is limited.
		 */
		public const Int16 JUMPLIST_CATEGORY_TASKS = 0;

		/**
		 * Recent or Frequent list
		 * 
		 * Recent and frequent lists are based on Window's recent document lists. The
		 * lists are generated automatically by Windows. Applications that use recent
		 * or frequent lists should keep document use tracking up to date by calling
		 * the SHAddToRecentDocs shell api.
		 */
		public const Int16 JUMPLIST_CATEGORY_RECENT = 1;
		public const Int16 JUMPLIST_CATEGORY_FREQUENT = 2;

		/**
		 * Custom Lists
		 *
		 * Custom lists can be made up of tasks, links, and separators. The title of
		 * of the list is passed through the optional string parameter of addBuildList.
		 */
		public const Int16 JUMPLIST_CATEGORY_CUSTOMLIST = 3;
	}

	[ComImport, Guid("1FE6A9CD-2B18-4dd5-A176-C2B32FA4F683"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIJumpListBuilder //: nsISupports
	{
		/**
		 * JumpLists
		 *
		 * Jump lists are built and then applied. Modifying an applied jump list is not
		 * permitted. Callers should begin the creation of a new jump list using
		 * initListBuild, add sub lists using addListBuild, then commit the jump list
		 * using commitListBuild. Lists are built in real-time during the sequence of
		 * build calls, make sure to check for errors on each individual step.
		 *
		 * The default number of allowed items in a jump list is ten. Users can change
		 * the number through system preferences. User may also pin items to jump lists,
		 * which take up additional slots. Applications do not have control over the
		 * number of items allowed in jump lists; excess items added are dropped by the
		 * system. Item insertion priority is defined as first to last added. 
		 *
		 * Users may remove items from jump lists after they are commited. The system
		 * tracks removed items between commits. A list of these items is returned by
		 * a call to initListBuild. nsIJumpListBuilder does not filter entries added that
		 * have been removed since the last commit. To prevent repeatedly adding entires
		 * users have removed, applications are encoraged to track removed items 
		 * internally.
		 *
		 * Each list is made up of an array of nsIWinJumpListItems representing items
		 * such as shortcuts, links, and separators. See nsIJumpListItem for information
		 * on adding additional jump list types.
		 */

		/**
		 * Indicates whether jump list taskbar features are supported by the current
		 * host.
		 */
		Int16 Available { get; }

		/**
		 * JumpList management
		 *
		 * @throw NS_ERROR_NOT_AVAILABLE on all calls if taskbar functionality
		 * is not supported by the operating system.
		 */

		/**
		 * Indicates if a commit has already occurred in this session.
		 */
		Boolean IsListCommitted { get; }

		/**
		 * The maximum number of jump list items the current desktop can support.
		 */
		Int16 MaxListItems { get; }

		/**
		 * Initializes a jump list build and returns a list of items the user removed
		 * since the last time a jump list was committed. Removed items can become state
		 * after initListBuild is called, lists should be built in single-shot fasion.
		 *
		 * @param removedItems
		 *        A list of items that were removed by the user since the last commit.
		 *
		 * @returns true if the operation completed successfully.
		 */
		Boolean InitListBuild(nsIMutableArray removedItems);

		/**
		 * Adds a list and if required, a set of items for the list.
		 *
		 * @param aCatType
		 *        The type of list to add.
		 * @param items
		 *        An array of nsIJumpListItem items to add to the list.
		 * @param catName
		 *        For custom lists, the title of the list.
		 *
		 * @returns true if the operation completed successfully.
		 *
		 * @throw NS_ERROR_INVALID_ARG if incorrect parameters are passed for
		 * a particular category or item type.
		 * @throw NS_ERROR_ILLEGAL_VALUE if an item is added that was removed
		 * since the last commit.
		 * @throw NS_ERROR_UNEXPECTED on internal errors.
		 */
		Boolean AddListToBuild(Int16 aCatType, [Optional] nsIArray items, [In, Optional, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String catName);

		/**
		 * Aborts and clears the current jump list build.
		 */
		void AbortListBuild();

		/**
		 * Commits the current jump list build to the Taskbar.
		 *
		 * @returns true if the operation completed successfully.
		 */
		Boolean CommitListBuild();

		/**
		 * Deletes any currently applied taskbar jump list for this application.
		 * Common uses would be the enabling of a privacy mode and uninstallation.
		 *
		 * @returns true if the operation completed successfully.
		 *
		 * @throw NS_ERROR_UNEXPECTED on internal errors.
		 */
		Boolean DeleteActiveList();
	}
}
