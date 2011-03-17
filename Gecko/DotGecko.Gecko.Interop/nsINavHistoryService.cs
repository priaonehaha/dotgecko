using System;
using System.Runtime.InteropServices;
using System.Text;
using nsISupports = System.Object;
using PRTime = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	public static class nsINavHistoryResultNodeConstants
	{
		public const UInt32 RESULT_TYPE_URI = 0;               // nsINavHistoryResultNode
		public const UInt32 RESULT_TYPE_VISIT = 1;             // nsINavHistoryVisitResultNode
		public const UInt32 RESULT_TYPE_FULL_VISIT = 2;        // nsINavHistoryFullVisitResultNode
		public const UInt32 RESULT_TYPE_DYNAMIC_CONTAINER = 4; // nsINavHistoryContainerResultNode
		public const UInt32 RESULT_TYPE_QUERY = 5;             // nsINavHistoryQueryResultNode
		public const UInt32 RESULT_TYPE_FOLDER = 6;            // nsINavHistoryQueryResultNode
		public const UInt32 RESULT_TYPE_SEPARATOR = 7;         // nsINavHistoryResultNode
		public const UInt32 RESULT_TYPE_FOLDER_SHORTCUT = 9;   // nsINavHistoryQueryResultNode
	}

	[ComImport, Guid("081452e5-be5c-4038-a5ea-f1f34cb6fd81"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsINavHistoryResultNode //: nsISupports
	{
		/**
		 * Indentifies the parent result node in the result set. This is null for
		 * top level nodes.
		 */
		nsINavHistoryContainerResultNode Parent { get; }

		/**
		 * The history-result to which this node belongs.
		 */
		nsINavHistoryResult ParentResult { get; }

		/**
		 * URI of the resource in question. For visits and URLs, this is the URL of
		 * the page. For folders and queries, this is the place: URI of the
		 * corresponding folder or query. This may be empty for other types of
		 * objects like host containers.
		 */
		void GetUri([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * Identifies the type of this node. This node can then be QI-ed to the
		 * corresponding specialized result node interface.
		 */
		UInt32 Type { get; }

		/**
		 * Title of the web page, or of the node's query (day, host, folder, etc)
		 */
		void GetTitle([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * Total number of times the URI has ever been accessed. For hosts, this
		 * is the total of the children under it, NOT the total times the host has
		 * been accessed (this would require an additional query, so is not given
		 * by default when most of the time it is never needed).
		 */
		UInt32 AccessCount { get; }

		/**
		 * This is the time the user accessed the page.
		 *
		 * If this is a visit, it is the exact time that the page visit occurred.
		 *
		 * If this is a URI, it is the most recent time that the URI was visited.
		 * Even if you ask for all URIs for a given date range Int32 ago, this might
		 * contain today's date if the URI was visited today.
		 *
		 * For hosts, or other node types with children, this is the most recent
		 * access time for any of the children.
		 *
		 * For days queries this is the respective endTime - a maximum possible
		 * visit time to fit in the day range.
		 */
		PRTime Time { get; }

		/**
		 * This URI can be used as an image source URI and will give you the favicon
		 * for the page. It is *not* the URI of the favicon, but rather something
		 * that will resolve to the actual image.
		 *
		 * In most cases, this is an annotation URI that will query the favicon
		 * service. If the entry has no favicon, this is the chrome URI of the
		 * default favicon. If the favicon originally lived in chrome, this will
		 * be the original chrome URI of the icon.
		 */
		void GetIcon([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * This is the number of levels between this node and the top of the
		 * hierarchy. The members of result.children have indentLevel = 0, their
		 * children have indentLevel = 1, etc. The indent level of the root node is
		 * set to -1.
		 */
		Int32 IndentLevel { get; }

		/**
		 * When this item is in a bookmark folder (parent is of type folder), this is
		 * the index into that folder of this node. These indices start at 0 and
		 * increase in the order that they appear in the bookmark folder. For items
		 * that are not in a bookmark folder, this value is -1.
		 */
		Int32 BookmarkIndex { get; }

		/**
		 * If the node is an item (bookmark, folder or a separator) this value is the
		 * row ID of that bookmark in the database. For other nodes, this value is
		 * set to -1.
		 */
		Int64 ItemId { get; }

		/**
		 * If the node is an item (bookmark, folder or a separator) this value is the 
		 * time that the item was created. For other nodes, this value is 0.
		 */
		PRTime DateAdded { get; }

		/**
		 * If the node is an item (bookmark, folder or a separator) this value is the 
		 * time that the item was last modified. For other nodes, this value is 0.
		 *
		 *  @note When an item is added lastModified is set to the same value as
		 *        dateAdded.
		 */
		PRTime LastModified { get; }

		/**
		 * For uri nodes, this is a sorted list of the tags, delimited with commans,
		 * for the uri represented by this node. Otherwise this is an empty string.
		 */
		void GetTags([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
	}


	/**
	 * When you request RESULT_TYPE_VISIT from query options, you will get this
	 * interface for each item, which includes the session ID so that we can
	 * group items from the same session together.
	 */
	[ComImport, Guid("8e2c5a86-b33d-4fa6-944b-559af7e95fcd"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsINavHistoryVisitResultNode : nsINavHistoryResultNode
	{
		#region nsINavHistoryResultNode Members

		new nsINavHistoryContainerResultNode Parent { get; }
		new nsINavHistoryResult ParentResult { get; }
		new void GetUri([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new UInt32 Type { get; }
		new void GetTitle([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new UInt32 AccessCount { get; }
		new PRTime Time { get; }
		new void GetIcon([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new Int32 IndentLevel { get; }
		new Int32 BookmarkIndex { get; }
		new Int64 ItemId { get; }
		new PRTime DateAdded { get; }
		new PRTime LastModified { get; }
		new void GetTags([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		#endregion

		/**
		 * This indicates the session ID of the * visit. This is used for session
		 * grouping when a tree view is sorted by date.
		 */
		Int64 SessionId { get; }
	}


	/**
	 * This structure will be returned when you request RESULT_TYPE_FULL_VISIT in
	 * the query options. This includes uncommonly used information about each
	 * visit.
	 */
	[ComImport, Guid("c49fd9d5-56e2-43eb-932c-f933f28cba85"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsINavHistoryFullVisitResultNode : nsINavHistoryVisitResultNode
	{
		#region nsINavHistoryResultNode Members

		new nsINavHistoryContainerResultNode Parent { get; }
		new nsINavHistoryResult ParentResult { get; }
		new void GetUri([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new UInt32 Type { get; }
		new void GetTitle([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new UInt32 AccessCount { get; }
		new PRTime Time { get; }
		new void GetIcon([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new Int32 IndentLevel { get; }
		new Int32 BookmarkIndex { get; }
		new Int64 ItemId { get; }
		new PRTime DateAdded { get; }
		new PRTime LastModified { get; }
		new void GetTags([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		#endregion

		#region nsINavHistoryVisitResultNode Members

		new Int64 SessionId { get; }

		#endregion

		/**
		 * This indicates the visit ID of the visit.
		 */
		Int64 VisitId { get; }

		/**
		 * This indicates the referring visit ID of the visit. The referrer should
		 * have the same sessionId.
		 */
		Int64 ReferringVisitId { get; }

		/**
		 * Indicates the transition type of the visit.
		 * One of nsINavHistoryService.TRANSITION_*
		 */
		Int32 TransitionType { get; }
	}


	public static class nsINavHistoryContainerResultNodeConstants
	{
		public const UInt16 STATE_CLOSED = 0;
		public const UInt16 STATE_LOADING = 1;
		public const UInt16 STATE_OPENED = 2;
	}

	/**
	 * Base class for container results. This includes all types of groupings.
	 * Bookmark folders and places queries will be QueryResultNodes which extends
	 * these items.
	 */
	[ComImport, Guid("55829318-0f6c-4503-8739-84231f3a6793"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsINavHistoryContainerResultNode : nsINavHistoryResultNode
	{
		#region nsINavHistoryResultNode Members

		new nsINavHistoryContainerResultNode Parent { get; }
		new nsINavHistoryResult ParentResult { get; }
		new void GetUri([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new UInt32 Type { get; }
		new void GetTitle([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new UInt32 AccessCount { get; }
		new PRTime Time { get; }
		new void GetIcon([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new Int32 IndentLevel { get; }
		new Int32 BookmarkIndex { get; }
		new Int64 ItemId { get; }
		new PRTime DateAdded { get; }
		new PRTime LastModified { get; }
		new void GetTags([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		#endregion

		/**
		 * Set this to allow descent into the container. When closed, attempting
		 * to call getChildren or childCount will result in an error. You should
		 * set this to false when you are done reading.
		 *
		 * For HOST and DAY groupings, doing this is free since the children have
		 * been precomputed. For queries and bookmark folders, being open means they
		 * will keep themselves up-to-date by listening for updates and re-querying
		 * as needed.
		 */
		Boolean ContainerOpen { get; set; }

		/**
		 * Indicates whether the container is closed, loading, or opened.  Loading
		 * implies that the container has been opened asynchronously and has not yet
		 * fully opened.
		 */
		UInt16 State { get; }

		/**
		 * This indicates whether this node "may" have children, and can be used
		 * when the container is open or closed. When the container is closed, it
		 * will give you an exact answer if the node can easily be populated (for
		 * example, a bookmark folder). If not (for example, a complex history query),
		 * it will return true. When the container is open, it will always be
		 * accurate. It is intended to be used to see if we should draw the "+" next
		 * to a tree item.
		 */
		Boolean HasChildren { get; }

		/**
		 * This gives you the children of the nodes. It is preferrable to use this
		 * interface over the array one, since it avoids creating an nsIArray object
		 * and the interface is already the correct type.
		 *
		 * @throws NS_ERROR_NOT_AVAILABLE if containerOpen is false.
		 */
		UInt32 ChildCount { get; }
		nsINavHistoryResultNode GetChild(UInt32 aIndex);

		/**
		 * Get the index of a direct child in this container.
		 *
		 * @param aNode
		 *        a result node.
		 *
		 * @return aNode's index in this container.
		 * @throws NS_ERROR_NOT_AVAILABLE if containerOpen is false.
		 * @throws NS_ERROR_INVALID_ARG if aNode isn't a direct child of this
		 * container.
		 */
		UInt32 GetChildIndex(nsINavHistoryResultNode aNode);

		/**
		 * Look for a node in the container by some of its details.  Does not search
		 * closed containers.
		 *
		 * @param aURI
		 *        the node's uri attribute value
		 * @param aTime
		 *        the node's time attribute value.
		 * @param aItemId
		 *        the node's itemId attribute value.
		 * @param aRecursive
		 *        whether or not to search recursively.
		 *
		 * @throws NS_ERROR_NOT_AVAILABLE if this container is closed.
		 * @return a result node that matches the given details if any, null
		 *         otherwise.
		 */
		nsINavHistoryResultNode FindNodeByDetails([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aURIString,
												  PRTime aTime,
												  Int64 aItemId,
												  Boolean aRecursive);

		/**
		 * Returns false if this node's list of children can be modified
		 * (adding or removing children, or reordering children), or true if
		 * the UI should not allow the list of children to be modified.
		 * This is false for bookmark folder nodes unless setFolderReadOnly() has
		 * been called to override it, and true for non-folder nodes.
		 */
		Boolean ChildrenReadOnly { get; }

		// --------------------------------------------------------------------------
		// Dynamic container

		/**
		 * This is a string representing the dynamic container API service that is
		 * responsible for this container. This throws if if the node is not a dynamic
		 * container.
		 */
		void GetDynamicContainerType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * Appends a full visit node to this container and returns it. For the dynamic
		 * container API. TO BE CALLED FROM nsIDynamicContainer::OnContainerOpening()
		 * ONLY, and only for non-bookmark-folder containers.
		 *
		 * @see nsINavHistoryURIResultNode for parameters.
		 */
		nsINavHistoryResultNode AppendURINode(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aTitle, UInt32 aAccessCount,
			PRTime aTime, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aIconURI);

		/**
		 * Appends a full visit node to this container and returns it. For the dynamic
		 * container API. TO BE CALLED FROM nsIDynamicContainer::OnContainerOpening()
		 * ONLY, and only for non-bookmark-folder containers.
		 *
		 * @see nsINavHistoryVisitResultNode for parameters.
		 *
		 * UNTESTED: Container API functions are commented out until we can test
		 */
		/*nsINavHistoryVisitResultNode appendVisitNode(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aTitle, in PRUint32 aAccessCount,
			in PRTime aTime, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aIconURI, in PRInt64 aSession);*/

		/**
		 * Appends a full visit node to this container and returns it. For the dynamic
		 * container API. TO BE CALLED FROM nsIDynamicContainer::OnContainerOpening()
		 * ONLY, and only for non-bookmark-folder containers.
		 *
		 * @see nsINavHistoryFullVisitResultNode for parameters.
		 *
		 * UNTESTED: Container API functions are commented out until we can test
		 */
		/*nsINavHistoryFullVisitResultNode appendFullVisitNode(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aTitle, in PRUint32 aAccessCount,
			in PRTime aTime, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aIconURI, in PRInt64 aSession,
			in PRInt64 aVisitId, in PRInt64 aReferringVisitId,
			in PRInt32 aTransitionType);*/

		/**
		 * Appends a container node to this container and returns it. For the dynamic
		 * container API. TO BE CALLED FROM nsIDynamicContainer::OnContainerOpening()
		 * ONLY, and only for non-bookmark-folder containers.
		 *
		 * aContainerType should be RESULT_TYPE_DYNAMIC_CONTAINER.
		 * When type is dynamic container you must
		 * specify a dynamic container type, otherwise, the dynamic container type must
		 * be null. Use appendQueryNode and appendFolderNode for the other container
		 * types.
		 *
		 * UNTESTED: Container API functions are commented out until we can test
		 */
		/*nsINavHistoryContainerResultNode appendContainerNode(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aTitle, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aIconURI, in PRUint32 aContainerType,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aDynamicContainerType);*/

		/**
		 * Appends a query node to this container and returns it. For the dynamic
		 * container API. TO BE CALLED FROM nsIDynamicContainer::OnContainerOpening()
		 * ONLY, and only for non-bookmark-folder containers.
		 *
		 * Normally you should supply an empty string for IconURI and it will take
		 * the default query icon for the current theme.
		 *
		 * UNTESTED: Container API functions are commented out until we can test
		 */
		/*nsINavHistoryQueryResultNode appendQueryNode(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aQueryURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aTitle, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aIconURI);*/

		/**
		 * Appends a bookmark folder node to this container and returns it. For the
		 * dynamic container API. TO BE CALLED FROM nsIDynamicContainer::OnContainerOpening()
		 * ONLY, and only for non-bookmark-folder containers.
		 *
		 * All container attributes will come from the boomkarks service for this
		 * folder.
		 */
		nsINavHistoryContainerResultNode AppendFolderNode(Int64 aFolderId);

		/**
		 * Clears all children of this container. For the dynamic container API.
		 * TO BE CALLED FROM nsIDynamicContainer::OnContainerOpening and
		 * nsIDynamicContainer::OnContainerClosed ONLY, and valid only for
		 * non-bookmark-folder containers.
		 *
		 * UNTESTED: Container API functions are commented out until we can test
		 */
		/*void clearContents();*/
	}


	/**
	 * Used for places queries and as a base for bookmark folders.
	 *
	 * Note that if you request places to *not* be expanded in the options that
	 * generated this node, this item will report it has no children and never try
	 * to populate itself.
	 */
	[ComImport, Guid("ea17745a-1852-4155-a98f-d1dd1763b3df"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsINavHistoryQueryResultNode : nsINavHistoryContainerResultNode
	{
		#region nsINavHistoryResultNode Members

		new nsINavHistoryContainerResultNode Parent { get; }
		new nsINavHistoryResult ParentResult { get; }
		new void GetUri([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new UInt32 Type { get; }
		new void GetTitle([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new UInt32 AccessCount { get; }
		new PRTime Time { get; }
		new void GetIcon([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new Int32 IndentLevel { get; }
		new Int32 BookmarkIndex { get; }
		new Int64 ItemId { get; }
		new PRTime DateAdded { get; }
		new PRTime LastModified { get; }
		new void GetTags([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		#endregion

		#region nsINavHistoryContainerResultNode Members

		new Boolean ContainerOpen { get; set; }
		new UInt16 State { get; }
		new Boolean HasChildren { get; }
		new UInt32 ChildCount { get; }
		new nsINavHistoryResultNode GetChild(UInt32 aIndex);
		new UInt32 GetChildIndex(nsINavHistoryResultNode aNode);
		new nsINavHistoryResultNode FindNodeByDetails([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aURIString,
												  PRTime aTime,
												  Int64 aItemId,
												  Boolean aRecursive);
		new Boolean ChildrenReadOnly { get; }
		new void GetDynamicContainerType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new nsINavHistoryResultNode AppendURINode(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aTitle, UInt32 aAccessCount,
			PRTime aTime, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aIconURI);
		new nsINavHistoryContainerResultNode AppendFolderNode(Int64 aFolderId);

		#endregion

		/**
		 * Get the queries which build this node's children.
		 * Only valid for RESULT_TYPE_QUERY nodes.
		 */
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		nsResult GetQueries([Optional] out UInt32 queryCount, [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 0)] out nsINavHistoryQuery[] queries);

		/**
		 * Get the options which group this node's children.
		 * Only valid for RESULT_TYPE_QUERY nodes.
		 */
		nsINavHistoryQueryOptions QueryOptions { get; }

		/**
		 * For both simple folder nodes and simple-folder-query nodes, this is set
		 * to the concrete itemId of the folder. Otherwise, this is set to -1.
		 */
		Int64 FolderItemId { get; }
	}


	/**
	 * Allows clients to observe what is happening to a result as it updates itself
	 * according to history and bookmark system events. Register this observer on a
	 * result using nsINavHistoryResult::addObserver.
	 */
	[ComImport, Guid("9ea86387-6a30-4ee2-b70d-26fbb718902f"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsINavHistoryResultObserver //: nsISupports
	{
		/**
		 * Called when 'aItem' is inserted into 'aParent' at index 'aNewIndex'.
		 * The item previously at index (if any) and everything below it will have
		 * been shifted down by one. The item may be a container or a leaf.
		 */
		void NodeInserted(nsINavHistoryContainerResultNode aParent,
						  nsINavHistoryResultNode aNode,
						  UInt32 aNewIndex);

		/**
		 * Called whan 'aItem' is removed from 'aParent' at 'aOldIndex'. The item
		 * may be a container or a leaf. This function will be called after the item
		 * has been removed from its parent list, but before anything else (including
		 * NULLing out the item's parent) has happened.
		 */
		void NodeRemoved(nsINavHistoryContainerResultNode aParent,
						 nsINavHistoryResultNode aItem,
						 UInt32 aOldIndex);

		/**
		 * Called whan 'aItem' is moved from 'aOldParent' at 'aOldIndex' to
		 * aNewParent at aNewIndex. The item may be a container or a leaf.
		 *
		 * XXX: at the moment, this method is called only when an item is moved
		 * within the same container. When an item is moved between containers,
		 * a new node is created for the item, and the itemRemoved/itemAdded methods
		 * are used.
		 */
		void NodeMoved(nsINavHistoryResultNode aNode,
					   nsINavHistoryContainerResultNode aOldParent,
					   UInt32 aOldIndex,
					   nsINavHistoryContainerResultNode aNewParent,
					   UInt32 aNewIndex);

		/**
		 * Called right after aNode's title has changed.
		 * 
		 * @param aNode
		 *        a result node
		 * @param aNewTitle
		 *        the new title
		 */
		void NodeTitleChanged(nsINavHistoryResultNode aNode,
							  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aNewTitle);

		/**
		 * Called right after aNode's uri property has changed.
		 * 
		 * @param aNode
		 *        a result node
		 * @param aNewURI
		 *        the new uri
		 */
		void NodeURIChanged(nsINavHistoryResultNode aNode,
							[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aNewURI);

		/**
		 * Called right after aNode's icon property has changed.
		 *
		 * @param aNode
		 *        a result node
		 *
		 * @note: The new icon is accessible through aNode.icon.
		 */
		void NodeIconChanged(nsINavHistoryResultNode aNode);

		/**
		 * Called right after aNode's time property or accessCount property, or both,
		 * have changed.
		 *
		 * @param aNode
		 *        a uri result node
		 * @param aNewVisitDate
		 *        the new visit date
		 * @param aNewAccessCount
		 *        the new access-count
		 */
		void NodeHistoryDetailsChanged(nsINavHistoryResultNode aNode,
									   PRTime aNewVisitDate,
									   UInt32 aNewAccessCount);

		/**
		 * Called when the tags set on the uri represented by aNode have changed.
		 *
		 * @param aNode
		 *        a uri result node
		 *
		 * @note: The new tags list is accessible through aNode.tags.
		 */
		void NodeTagsChanged(nsINavHistoryResultNode aNode);

		/**
		 * Called right after the aNode's keyword property has changed.
		 * 
		 * @param aNode
		 *        a uri result node
		 * @param aNewKeyword
		 *        the new keyword
		 */
		void NodeKeywordChanged(nsINavHistoryResultNode aNode,
								[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aNewKeyword);

		/**
		 * Called right after an annotation of aNode's has changed (set, altered, or
		 * unset).
		 * 
		 * @param aNode
		 *        a result node
		 * @param aAnnoName
		 *        the name of the annotation that changed
		 */
		void NodeAnnotationChanged(nsINavHistoryResultNode aNode,
								   [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aAnnoName);

		/**
		 * Called right after aNode's dateAdded property has changed.
		 *
		 * @param aNode
		 *        a result node
		 * @param aNewValue
		 *        the new value of the dateAdded property
		 */
		void NodeDateAddedChanged(nsINavHistoryResultNode aNode,
								  PRTime aNewValue);

		/**
		 * Called right after aNode's dateModified property has changed.
		 *
		 * @param aNode
		 *        a result node
		 * @param aNewValue
		 *        the new value of the dateModified property
		 */
		void NodeLastModifiedChanged(nsINavHistoryResultNode aNode,
									 PRTime aNewValue);

		/**
		 * Called when an item is being replaced with another item at the exact
		 * same position.
		 *
		 * @param aParentNode
		 *        the parent node of the node which is being replaced
		 * @param aOldNode
		 *        the node which is being replaced
		 * @param aNewNode
		 *        the new node
		 * @param aParentNode
		 *        the index in aParentNode, at which a node is being replaced
		 */
		void NodeReplaced(nsINavHistoryContainerResultNode aParentNode,
						  nsINavHistoryResultNode aOldNode,
						  nsINavHistoryResultNode aNewNode,
						  UInt32 aIndex);

		/**
		 * Called after a container node went from closed to opened.
		 *
		 * @note  This method is DEPRECATED.  In the future only containerStateChanged
		 *        will notify when a container is opened.
		 *
		 * @param aContainerNode
		 *        the container node which was opened
		 */
		void ContainerOpened(nsINavHistoryContainerResultNode aContainerNode);

		/**
		 * Called after a container node went from opened to closed. This will be
		 * called for the topmost container that is closing, and implies that any
		 * child containers have closed as well.
		 *
		 * @note  This method is DEPRECATED.  In the future only containerStateChanged
		 *        will notify when a container is closed.
		 *
		 * @param aContainerNode
		 *        the container node which was closed
		 */
		void ContainerClosed(nsINavHistoryContainerResultNode aContainerNode);

		/**
		 * Called after a container changes state.
		 *
		 * @param aContainerNode
		 *        The container that has changed state.
		 * @param aOldState
		 *        The state that aContainerNode has transitioned out of.
		 * @param aNewState
		 *        The state that aContainerNode has transitioned into.
		 */
		void ContainerStateChanged(nsINavHistoryContainerResultNode aContainerNode,
								   UInt32 aOldState,
								   UInt32 aNewState);

		/**
		 * Called when something significant has happened within the container. The
		 * contents of the container should be re-built.
		 *
		 * @param aContainerNode
		 *        the container node to invalidate
		 */
		void InvalidateContainer(nsINavHistoryContainerResultNode aContainerNode);

		/**
		 * This is called to indicate to the UI that the sort has changed to the
		 * given mode. For trees, for example, this would update the column headers
		 * to reflect the sorting. For many other types of views, this won't be
		 * applicable.
		 *
		 * @param sortingMode  One of nsINavHistoryQueryOptions.SORT_BY_* that
		 *                     indicates the new sorting mode.
		 *
		 * This only is expected to update the sorting UI. invalidateAll() will also
		 * get called if the sorting changes to update everything.
		 */
		void SortingChanged(UInt16 sortingMode);

		/**
		 * This is called to indicate that a batch operation is about to start or end.
		 * The observer could want to disable some events or updates during batches,
		 * since multiple operations are packed in a Int16 time.
		 * For example treeviews could temporarily suppress select notifications.
		 *
		 * @param aToggleMode
		 *        true if a batch is starting, false if it's ending.
		 */
		void Batching(Boolean aToggleMode);

		/**
		 * Called by the result when this observer is added.
		 */
		nsINavHistoryResult Result { get; set; }
	}


	public static class nsINavHistoryResultTreeViewerConstants
	{
		public const UInt32 INDEX_INVISIBLE = 0xffffffff;
	}

	/**
	 * TODO: Bug 517719.
	 *
	 * A predefined view adaptor for interfacing results with an nsITree. This
	 * object will remove itself from its associated result when the tree has been
	 * detached. This prevents circular references. Users should be aware of this,
	 * if you want to re-use the same viewer, you will need to keep your own
	 * reference to it and re-initialize it when the tree changes. If you use this
	 * object, attach it to a result, never attach it to a tree, and forget about
	 * it, it will leak!
	 */
	[ComImport, Guid("f8b518c0-1faf-11df-8a39-0800200c9a66"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsINavHistoryResultTreeViewer : nsINavHistoryResultObserver
	{
		#region nsINavHistoryResultObserver Members

		new void NodeInserted(nsINavHistoryContainerResultNode aParent, nsINavHistoryResultNode aNode, UInt32 aNewIndex);
		new void NodeRemoved(nsINavHistoryContainerResultNode aParent, nsINavHistoryResultNode aItem, UInt32 aOldIndex);
		new void NodeMoved(nsINavHistoryResultNode aNode, nsINavHistoryContainerResultNode aOldParent, UInt32 aOldIndex, nsINavHistoryContainerResultNode aNewParent, UInt32 aNewIndex);
		new void NodeTitleChanged(nsINavHistoryResultNode aNode, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aNewTitle);
		new void NodeURIChanged(nsINavHistoryResultNode aNode, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aNewURI);
		new void NodeIconChanged(nsINavHistoryResultNode aNode);
		new void NodeHistoryDetailsChanged(nsINavHistoryResultNode aNode, PRTime aNewVisitDate, UInt32 aNewAccessCount);
		new void NodeTagsChanged(nsINavHistoryResultNode aNode);
		new void NodeKeywordChanged(nsINavHistoryResultNode aNode, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aNewKeyword);
		new void NodeAnnotationChanged(nsINavHistoryResultNode aNode, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aAnnoName);
		new void NodeDateAddedChanged(nsINavHistoryResultNode aNode, PRTime aNewValue);
		new void NodeLastModifiedChanged(nsINavHistoryResultNode aNode, PRTime aNewValue);
		new void NodeReplaced(nsINavHistoryContainerResultNode aParentNode, nsINavHistoryResultNode aOldNode, nsINavHistoryResultNode aNewNode, UInt32 aIndex);
		new void ContainerOpened(nsINavHistoryContainerResultNode aContainerNode);
		new void ContainerClosed(nsINavHistoryContainerResultNode aContainerNode);
		new void ContainerStateChanged(nsINavHistoryContainerResultNode aContainerNode, UInt32 aOldState, UInt32 aNewState);
		new void InvalidateContainer(nsINavHistoryContainerResultNode aContainerNode);
		new void SortingChanged(UInt16 sortingMode);
		new void Batching(Boolean aToggleMode);
		new nsINavHistoryResult Result { get; set; }

		#endregion

		/**
		 * This allows you to get at the real node for a given row index. This is
		 * only valid when a tree is attached.
		 */
		nsINavHistoryResultNode NodeForTreeIndex(UInt32 aIndex);

		/**
		 * Reverse of nodeForFlatIndex, returns the row index for a given result node.
		 * Returns INDEX_INVISIBLE if the item is not visible (for example, its
		 * parent is collapsed). This is only valid when a tree is attached. The
		 * the result will always be INDEX_INVISIBLE if not.
		 * 
		 * Note: This sounds sort of obvious, but it got me: aNode must be a node
		 *       retrieved from the same result that this viewer is for. If you 
		 *       execute another query and get a node from a _different_ result, this 
		 *       function will always return the index of that node in the tree that
		 *       is attached to that result.
		 */
		UInt32 TreeIndexForNode(nsINavHistoryResultNode aNode);
	}


	/**
	 * The result of a history/bookmark query.
	 */
	[ComImport, Guid("c2229ce3-2159-4001-859c-7013c52f7619"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsINavHistoryResult //: nsISupports
	{
		/**
		 * Sorts all nodes recursively by the given parameter, one of
		 * nsINavHistoryQueryOptions.SORT_BY_*  This will update the corresponding
		 * options for this result, so that re-using the current options/queries will
		 * always give you the current view.
		 */
		UInt16 SortingMode { get; set; }

		/**
		 * The annotation to use in SORT_BY_ANNOTATION_* sorting modes, set this
		 * before setting the sortingMode attribute.
		 */
		void GetSortingAnnotation([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void SetSortingAnnotation([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);

		/**
		 * Whether or not notifications on result changes are suppressed.
		 * Initially set to false.
		 *
		 * Use this to avoid flickering and to improve performance when you
		 * do temporary changes to the result structure (e.g. when searching for a
		 * node recursively).
		 */
		Boolean SuppressNotifications { get; set; }

		/**
		 * Adds an observer for changes done in the result.
		 *
		 * @param aObserver
		 *        a result observer.
		 * @param aOwnsWeak
		 *        If false, the result will keep an owning reference to the observer,
		 *        which must be removed using removeObserver.
		 *        If true, the result will keep a weak reference to the observer, which
		 *        must implement nsISupportsWeakReference.
		 *
		 * @see nsINavHistoryResultObserver
		 */
		void AddObserver(nsINavHistoryResultObserver aObserver, Boolean aOwnsWeak);

		/**
		 * Removes an observer that was added by addObserver.
		 *
		 * @param aObserver
		 *        a result observer that was added by addObserver.
		 */
		void RemoveObserver(nsINavHistoryResultObserver aObserver);

		/**
		 * This is the root of the results. Remember that you need to open all
		 * containers for their contents to be valid.
		 *
		 * When a result goes out of scope it will continue to observe changes till
		 * it is cycle collected.  While the result waits to be collected it will stay
		 * in memory, and continue to update itself, potentially causing unwanted
		 * additional work.  When you close the root node the result will stop
		 * observing changes, so it is good practice to close the root node when you
		 * are done with a result, since that will avoid unwanted performance hits.
		 */
		nsINavHistoryContainerResultNode Root { get; }
	}


	public static class nsINavHistoryObserverConstants
	{
		public const UInt32 ATTRIBUTE_FAVICON = 3; // favicon updated, aString = favicon annotation URI
	}

	/**
	 * Similar to nsIRDFObserver for history. Note that we don't pass the data
	 * source since that is always the global history.
	 *
	 * DANGER! If you are in the middle of a batch transaction, there may be a
	 * database transaction active. You can still access the DB, but be careful.
	 */
	[ComImport, Guid("0a5ce210-c803-11de-8a39-0800200c9a66"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsINavHistoryObserver //: nsISupports
	{
		/**
		 * Notifies you that a bunch of things are about to change, don't do any
		 * heavy-duty processing until onEndUpdateBatch is called.
		 */
		void OnBeginUpdateBatch();

		/**
		 * Notifies you that we are done doing a bunch of things and you should go
		 * ahead and update UI, etc.
		 */
		void OnEndUpdateBatch();

		/**
		 * Called when a resource is visited. This is called the first time a
		 * resource (page, image, etc.) is seen as well as every subsequent time.
		 *
		 * Normally, transition types of TRANSITION_EMBED (corresponding to images in
		 * a page, for example) are not displayed in history results (unless
		 * includeHidden is set). Many observers can ignore _EMBED notifications
		 * (which will comprise the majority of visit notifications) to save work.
		 *
		 * @param aVisitID        ID of the visit that was just created.
		 * @param aTime           Time of the visit
		 * @param aSessionID      The ID of one connected sequence of visits.
		 * @param aReferringID    The ID of the visit the user came from. 0 if empty.
		 * @param aTransitionType One of nsINavHistory.TRANSITION_*
		 * @param aAdded          Incremented by query nodes when the visited uri
		 *                        belongs to them. If no such query exists, the 
		 *                        history result creates a new query node dynamically.
		 *                        It is used in places views only and can be ignored.
		 */
		void OnVisit(nsIURI aURI, Int64 aVisitID, PRTime aTime,
					 Int64 aSessionID, Int64 aReferringID,
					 UInt32 aTransitionType, out UInt32 aAdded);

		/**
		 * Called whenever either the "real" title or the custom title of the page
		 * changed. BOTH TITLES ARE ALWAYS INCLUDED in this notification, even though
		 * only one will change at a time. Often, consumers will want to display the
		 * user title if it is available, and fall back to the page title (the one
		 * specified in the <title> tag of the page).
		 *
		 * Note that there is a difference between an empty title and a NULL title.
		 * An empty string means that somebody specifically set the title to be
		 * nothing. NULL means nobody set it. From C++: use IsVoid() and SetIsVoid()
		 * to see whether an empty string is "null" or not (it will always be an
		 * empty string in either case).
		 *
		 */
		void OnTitleChanged(nsIURI aURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aPageTitle);

		/**
		 * This page and all of its visits are about to be deleted.  Note: the page
		 * may not necessarily have actually existed for this function to be called.
		 *
		 * @param aURI
		 *        The URI being deleted.
		 */
		void OnBeforeDeleteURI(nsIURI aURI);

		/**
		 * This page and all of its visits are being deleted. Note: the page may not
		 * necessarily have actually existed for this function to be called.
		 *
		 * Delete notifications are only 99.99% accurate. Batch delete operations
		 * must be done in two steps, so first come notifications, then a bulk
		 * delete. If there is some error in the middle (for example, out of memory)
		 * then you'll get a notification and it won't get deleted. There's no easy
		 * way around this.
		 */
		void OnDeleteURI(nsIURI aURI);

		/**
		 * Notification that all of history is being deleted.
		 */
		void OnClearHistory();

		/**
		 * A page has had some attribute on it changed. Note that for TYPED and
		 * HIDDEN, the page may not necessarily have been added yet.
		 */
		void OnPageChanged(nsIURI aURI, UInt32 aWhat, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue);

		/**
		 * Called when some visits of an history entry are expired.
		 *
		 * @param aURI
		 *        The page whose visits have been expired.
		 * @param aVisitTime
		 *        The largest visit time in microseconds that has been expired.  We
		 *        guarantee that we don't have any visit older than this date.
		 *
		 * @note: when all visits for a page are expired and also the full page entry
		 *        is expired, you will only get an onDeleteURI notification.  If a
		 *        page entry is removed, then you can be sure that we don't have
		 *        anymore visits for it.
		 */
		void OnDeleteVisits(nsIURI aURI, PRTime aVisitTime);
	}


	public static class nsINavHistoryQueryConstants
	{
		/**
		 * Time range for results (INCLUSIVE). The *TimeReference is one of the
		 * constants TIME_RELATIVE_* which indicates how to interpret the
		 * corresponding time value.
		 *   TIME_RELATIVE_EPOCH (default):
		 *     The time is relative to Jan 1 1970 GMT, (this is a normal PRTime)
		 *   TIME_RELATIVE_TODAY:
		 *     The time is relative to this morning at midnight. Normally used for
		 *     queries relative to today. For example, a "past week" query would be
		 *     today-6 days -> today+1 day
		 *   TIME_RELATIVE_NOW:
		 *     The time is relative to right now.
		 *
		 * Note: PRTime is in MICROseconds since 1 Jan 1970. Javascript date objects
		 * are expressed in MILLIseconds since 1 Jan 1970.
		 *
		 * As a special case, a 0 time relative to TIME_RELATIVE_EPOCH indicates that
		 * the time is not part of the query. This is the default, so an empty query
		 * will match any time. The has* functions return whether the corresponding
		 * time is considered.
		 *
		 * You can read absolute*Time to get the time value that the currently loaded
		 * reference points + offset resolve to.
		 */
		public const UInt32 TIME_RELATIVE_EPOCH = 0;
		public const UInt32 TIME_RELATIVE_TODAY = 1;
		public const UInt32 TIME_RELATIVE_NOW = 2;
	}

	/**
	 * This object encapsulates all the query parameters you're likely to need
	 * when building up history UI. All parameters are ANDed together.
	 *
	 * This is not intended to be a super-general query mechanism. This was designed
	 * so that most queries can be done in only one SQL query. This is important
	 * because, if the user has their profile on a networked drive, query latency
	 * can be non-negligible.
	 */
	[ComImport, Guid("dc87ae79-22f1-4dcf-975b-852b01d210cb"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsINavHistoryQuery //: nsISupports
	{
		PRTime BeginTime { get; set; }
		UInt32 BeginTimeReference { get; set; }
		Boolean HasBeginTime { get; }
		PRTime AbsoluteBeginTime { get; }

		PRTime EndTime { get; set; }
		UInt32 EndTimeReference { get; set; }
		Boolean HasEndTime { get; }
		PRTime AbsoluteEndTime { get; }

		/**
		 * Text search terms.
		 */
		void GetSearchTerms([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		void SetSearchTerms([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
		Boolean HasSearchTerms { get; }

		/**
		 * Set lower or upper limits for how many times an item has been
		 * visited.  The default is -1, and in that case all items are
		 * matched regardless of their visit count.
		 */
		Int32 MinVisits { get; set; }
		Int32 MaxVisits { get; set; }

		/**
		 * When the set of transitions is nonempty, results are limited to pages which
		 * have at least one visit for each of the transition types.
		 * @note: For searching on more than one transition this can be very slow.
		 *
		 * Limit results to the specified list of transition types.
		 */
		void SetTransitions([In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4, SizeParamIndex = 1)] UInt32[] transitions, UInt32 count);

		/**
		 * Get the transitions set for this query.
		 */
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		nsResult GetTransitions([Optional] out UInt32 count, [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4, SizeParamIndex = 0)] out UInt32[] transitions);

		/**
		 * Get the count of the set query transitions.
		 */
		UInt32 TransitionCount { get; }

		/**
		 * When set, returns only bookmarked items, when unset, returns anything. Setting this
		 * is equivalent to listing all bookmark folders in the 'folders' parameter.
		 */
		Boolean OnlyBookmarked { get; set; }

		/**
		 * This controls the meaning of 'domain', and whether it is an exact match
		 * 'domainIsHost' = true, or hierarchical (= false).
		 */
		Boolean DomainIsHost { get; set; }

		/**
		 * This is the host or domain name (controlled by domainIsHost). When
		 * domainIsHost, domain only does exact matching on host names. Otherwise,
		 * it will return anything whose host name ends in 'domain'.
		 *
		 * This one is a little different than most. Setting it to an empty string
		 * is a real query and will match any URI that has no host name (local files
		 * and such). Set this to NULL (in C++ use SetIsVoid) if you don't want
		 * domain matching.
		 */
		void GetDomain([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void SetDomain([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);
		Boolean HasDomain { get; }

		/**
		 * Controls the interpretation of 'uri'. When unset (default), the URI will
		 * request an exact match of the specified URI. When set, any history entry
		 * beginning in 'uri' will match. For example "http://bar.com/foo" will match
		 * "http://bar.com/foo" as well as "http://bar.com/foo/baz.gif".
		 */
		Boolean UriIsPrefix { get; set; }

		/**
		 * This is a URI to match, to, for example, find out every time you visited
		 * a given URI. Use uriIsPrefix to control whether this is an exact match.
		 */
		nsIURI Uri { get; set; }
		Boolean HasUri { get; }

		/**
		 * Test for existence or non-existence of a given annotation. We don't
		 * currently support >1 annotation name per query. If 'annotationIsNot' is
		 * true, we test for the non-existence of the specified annotation.
		 *
		 * Testing for not annotation will do the same thing as a normal query and
		 * remove everything that doesn't have that annotation. Asking for things
		 * that DO have a given annotation is a little different. It also includes
		 * things that have never been visited. This allows place queries to be
		 * returned as well as anything else that may have been tagged with an
		 * annotation. This will only work for RESULTS_AS_URI since there will be
		 * no visits for these items.
		 */
		Boolean AnnotationIsNot { get; set; }
		void GetAnnotation([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void SetAnnotation([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);
		Boolean HasAnnotation { get; }

		/**
		 * Limit results to items that are tagged with all of the given tags.  This
		 * attribute must be set to an array of strings.  When called as a getter it
		 * will return an array of strings sorted ascending in lexicographical order.
		 * The array may be empty in either case.  Duplicate tags may be specified
		 * when setting the attribute, but the getter returns only unique tags.
		 *
		 * To search for items that are tagged with any given tags rather than all,
		 * multiple queries may be passed to nsINavHistoryService.executeQueries().
		 */
		nsIVariant Tags { get; set; }

		/**
		 * If 'tagsAreNot' is true, the results are instead limited to items that
		 * are not tagged with any of the given tags.  This attribute is used in
		 * conjunction with the 'tags' attribute.
		 */
		Boolean TagsAreNot { get; set; }

		/**
		 * Limit results to items that are in all of the given folders.
		 */
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		nsResult GetFolders([Optional] out UInt32 count, [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I8, SizeParamIndex = 0)] out Int64[] folders);
		UInt32 FolderCount { get; }

		/**
		 * For the special result type RESULTS_AS_TAG_CONTENTS we can define only
		 * one folder that must be a tag folder. This is not recursive so results
		 * will be returned from the first level of that folder.
		 */
		void SetFolders([In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I8, SizeParamIndex = 1)] Int64[] folders, UInt32 folderCount);

		/**
		 * Creates a new query item with the same parameters of this one.
		 */
		nsINavHistoryQuery Clone();
	}


	public static class nsINavHistoryQueryOptionsConstants
	{
		/**
		 * You can ask for the results to be pre-sorted. Since the DB has indices
		 * of many items, it can produce sorted results almost for free. These should
		 * be self-explanatory.
		 *
		 * Note: re-sorting is slower, as is sorting by title or when you have a
		 * host name.
		 *
		 * For bookmark items, SORT_BY_NONE means sort by the natural bookmark order.
		 */
		public const UInt16 SORT_BY_NONE = 0;
		public const UInt16 SORT_BY_TITLE_ASCENDING = 1;
		public const UInt16 SORT_BY_TITLE_DESCENDING = 2;
		public const UInt16 SORT_BY_DATE_ASCENDING = 3;
		public const UInt16 SORT_BY_DATE_DESCENDING = 4;
		public const UInt16 SORT_BY_URI_ASCENDING = 5;
		public const UInt16 SORT_BY_URI_DESCENDING = 6;
		public const UInt16 SORT_BY_VISITCOUNT_ASCENDING = 7;
		public const UInt16 SORT_BY_VISITCOUNT_DESCENDING = 8;
		public const UInt16 SORT_BY_KEYWORD_ASCENDING = 9;
		public const UInt16 SORT_BY_KEYWORD_DESCENDING = 10;
		public const UInt16 SORT_BY_DATEADDED_ASCENDING = 11;
		public const UInt16 SORT_BY_DATEADDED_DESCENDING = 12;
		public const UInt16 SORT_BY_LASTMODIFIED_ASCENDING = 13;
		public const UInt16 SORT_BY_LASTMODIFIED_DESCENDING = 14;
		public const UInt16 SORT_BY_TAGS_ASCENDING = 17;
		public const UInt16 SORT_BY_TAGS_DESCENDING = 18;
		public const UInt16 SORT_BY_ANNOTATION_ASCENDING = 19;
		public const UInt16 SORT_BY_ANNOTATION_DESCENDING = 20;

		/**
		 * "URI" results, one for each URI visited in the range. Individual result
		 * nodes will be of type "URI".
		 */
		public const UInt16 RESULTS_AS_URI = 0;

		/**
		 * "Visit" results, with one for each time a page was visited (this will
		 * often give you multiple results for one URI). Individual result nodes will
		 * have type "Visit"
		 *
		 * @note This result type is only supported by QUERY_TYPE_HISTORY.
		 */
		public const UInt16 RESULTS_AS_VISIT = 1;

		/**
		 * This is identical to RESULT_TYPE_VISIT except that individual result nodes
		 * will have type "FullVisit".  This is used for the attributes that are not
		 * commonly accessed to save space in the common case (the lists can be very
		 * Int32).
		 *
		 * @note Not yet implemented. See bug 409662.
		 * @note This result type is only supported by QUERY_TYPE_HISTORY.
		 */
		public const UInt16 RESULTS_AS_FULL_VISIT = 2;

		/**
		 * This returns query nodes for each predefined date range where we 
		 * had visits. The node contains information how to load its content:
		 * - visits for the given date range will be loaded.
		 *
		 * @note This result type is only supported by QUERY_TYPE_HISTORY.
		 */
		public const UInt16 RESULTS_AS_DATE_QUERY = 3;

		/**
		 * This returns nsINavHistoryQueryResultNode nodes for each site where we 
		 * have visits. The node contains information how to load its content:
		 * - last visit for each url in the given host will be loaded.
		 *
		 * @note This result type is only supported by QUERY_TYPE_HISTORY.
		 */
		public const UInt16 RESULTS_AS_SITE_QUERY = 4;

		/**
		 * This returns nsINavHistoryQueryResultNode nodes for each day where we 
		 * have visits. The node contains information how to load its content:
		 * - list of hosts visited in the given period will be loaded.
		 *
		 * @note This result type is only supported by QUERY_TYPE_HISTORY.
		 */
		public const UInt16 RESULTS_AS_DATE_SITE_QUERY = 5;

		/**
		 * This returns nsINavHistoryQueryResultNode nodes for each tag.
		 * The node contains information how to load its content:
		 * - list of bookmarks with the given tag will be loaded.
		 *
		 * @note Setting this resultType will force queryType to QUERY_TYPE_BOOKMARKS.
		 */
		public const UInt16 RESULTS_AS_TAG_QUERY = 6;

		/**
		 * This is a container with an URI result type that contains the last
		 * modified bookmarks for the given tag.
		 * Tag folder id must be defined in the query.
		 *
		 * @note Setting this resultType will force queryType to QUERY_TYPE_BOOKMARKS.
		 */
		public const UInt16 RESULTS_AS_TAG_CONTENTS = 7;

		/**
		 * Include both redirected-from and redirected-to pages into results.
		 */
		public const UInt16 REDIRECTS_MODE_ALL = 0;
		/**
		 * Query results will not include redirected-to pages, but will include
		 * redirected-from pages.
		 */
		public const UInt16 REDIRECTS_MODE_SOURCE = 1;
		/**
		 * Query results will not include redirected-from pages but will include
		 * redirected-to pages.
		 */
		public const UInt16 REDIRECTS_MODE_TARGET = 2;

		public const UInt16 QUERY_TYPE_HISTORY = 0;
		public const UInt16 QUERY_TYPE_BOOKMARKS = 1;
		/* Unified queries are not yet implemented. See bug 378798 */
		public const UInt16 QUERY_TYPE_UNIFIED = 2;
	}

	/**
	 * This object represents the global options for executing a query.
	 */
	[ComImport, Guid("2d8ff86b-f8c2-451c-8a1a-1ff0749a074e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsINavHistoryQueryOptions //: nsISupports
	{
		/**
		 * The sorting mode to be used for this query.
		 * mode is one of SORT_BY_*
		 */
		UInt16 SortingMode { get; set; }

		/**
		 * The annotation to use in SORT_BY_ANNOTATION_* sorting modes.
		 */
		void GetSortingAnnotation([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void SetSortingAnnotation([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);

		/**
		 * Sets the result type. One of RESULT_TYPE_* which includes how URIs are
		 * represented.
		 */
		UInt16 ResultType { get; set; }

		/**
		 * This option excludes all URIs and separators from a bookmarks query.
		 * This would be used if you just wanted a list of bookmark folders and
		 * queries (such as the left pane of the places page).
		 * Defaults to false.
		 */
		Boolean ExcludeItems { get; set; }

		/**
		 * Set to true to exclude queries ("place:" URIs) from the query results.
		 * Simple folder queries (bookmark folder symlinks) will still be included.
		 * Defaults to false.
		 */
		Boolean ExcludeQueries { get; set; }

		/**
		 * Set to true to exclude read-only folders from the query results. This is
		 * designed for cases where you want to give the user the option of filing
		 * something into a list of folders. It only affects cases where the actual
		 * folder result node would appear in its parent folder and filters it out.
		 * It doesn't affect the query at all, and doesn't affect more complex
		 * queries (such as "folders with annotation X").
		 */
		Boolean ExcludeReadOnlyFolders { get; set; }

		/**
		 * This option excludes items from a bookmarks query
		 * if the parent of the item has this annotation.
		 * An example is to exclude livemark items
		 * (parent folders have the "livemark/feedURI" annotation)
		 * Ignored for queries over history.
		 */
		void GetExcludeItemIfParentHasAnnotation([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void SetExcludeItemIfParentHasAnnotation([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);

		/**
		 * When set, allows items with "place:" URIs to appear as containers,
		 * with the container's contents filled in from the stored query.
		 * If not set, these will appear as normal items. Doesn't do anything if
		 * excludeQueries is set. Defaults to false.
		 *
		 * Note that this has no effect on folder links, which are place: URIs
		 * returned by nsINavBookmarkService.GetFolderURI. These are always expanded
		 * and will appear as bookmark folders.
		 */
		Boolean ExpandQueries { get; set; }

		/**
		 * Most items in history are marked "hidden." Only toplevel pages that the
		 * user sees in the URL bar are not hidden. Hidden things include the content
		 * of iframes and all images on web pages. Normally, you don't want these
		 * things. If you do, set this flag and you'll get all items, even hidden
		 * ones. Does nothing for bookmark queries. Defaults to false.
		 */
		Boolean IncludeHidden { get; set; }

		/**
		 * Defines how redirects should be handled, see REDIRECTS_MODE_* constants
		 * above.
		 * Defaults to REDIRECTS_MODE_ALL.
		 * Note: this option is effective only on QUERY_TYPE_HISTORY.
		 */
		UInt16 RedirectsMode { get; set; }

		/**
		 * This is the maximum number of results that you want. The query is exeucted,
		 * the results are sorted, and then the top 'maxResults' results are taken
		 * and returned. Set to 0 (the default) to get all results.
		 *
		 * THIS DOES NOT WORK IN CONJUNCTION WITH SORTING BY TITLE. This is because
		 * sorting by title requires us to sort after using locale-sensetive sorting
		 * (as opposed to letting the database do it for us).
		 *
		 * Instead, we get the result ordered by date, pick the maxResult most recent
		 * ones, and THEN sort by title.
		 */
		UInt32 MaxResults { get; set; }

		/**
		 * The type of search to use when querying the DB; This attribute is only
		 * honored by query nodes. It is silently ignored for simple folder queries.
		 */
		UInt16 QueryType { get; set; }

		/**
		 * When this is true, the root container node generated by these options and
		 * its descendant containers will be opened asynchronously if they support it.
		 * This is false by default.
		 *
		 * @note Currently only bookmark folder containers support being opened
		 *       asynchronously.
		 */
		Boolean AsyncEnabled { get; set; }

		/**
		 * Creates a new options item with the same parameters of this one.
		 */
		nsINavHistoryQueryOptions Clone();
	}


	public static class nsINavHistoryServiceConstants
	{
		/**
		 * This transition type means the user followed a link and got a new toplevel
		 * window.
		 */
		public const UInt32 TRANSITION_LINK = 1;

		/**
		 * This transition type means that the user typed the page's URL in the
		 * URL bar or selected it from URL bar autocomplete results, clicked on
		 * it from a history query (from the History sidebar, History menu, 
		 * or history query in the personal toolbar or Places organizer.
		 */
		public const UInt32 TRANSITION_TYPED = 2;

		/**
		 * This transition is set when the user followed a bookmark to get to the
		 * page.
		 */
		public const UInt32 TRANSITION_BOOKMARK = 3;

		/**
		 * This transition type is set when some inner content is loaded. This is
		 * true of all images on a page, and the contents of the iframe. It is also
		 * true of any content in a frame if the user did not explicitly follow
		 * a link to get there.
		 */
		public const UInt32 TRANSITION_EMBED = 4;

		/**
		 * Set when the transition was a permanent redirect.
		 */
		public const UInt32 TRANSITION_REDIRECT_PERMANENT = 5;

		/**
		 * Set when the transition was a temporary redirect.
		 */
		public const UInt32 TRANSITION_REDIRECT_TEMPORARY = 6;

		/**
		 * Set when the transition is a download.
		 */
		public const UInt32 TRANSITION_DOWNLOAD = 7;

		/**
		 * This transition type means the user followed a link and got a visit in
		 * a frame.
		 */
		public const UInt32 TRANSITION_FRAMED_LINK = 8;

		/**
		 * Set when database is coherent
		 */
		public const UInt16 DATABASE_STATUS_OK = 0;

		/**
		 * Set when database did not exist and we created a new one
		 */
		public const UInt16 DATABASE_STATUS_CREATE = 1;

		/**
		 * Set when database was corrupt and we replaced it
		 */
		public const UInt16 DATABASE_STATUS_CORRUPT = 2;

		/**
		 * Set when database schema has been upgraded
		 */
		public const UInt16 DATABASE_STATUS_UPGRADED = 3;
	}

	[ComImport, Guid("437f539b-d541-4a0f-a200-6f9a6d45cce2"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsINavHistoryService //: nsISupports
	{
		/**
		 * System Notifications:
		 *
		 * places-init-complete - Sent once the History service is completely
		 *                        initialized successfully.
		 * places-database-locked - Sent if initialization of the History service
		 *                          failed due to the inability to open the places.sqlite
		 *                          for access reasons.
		 */

		/**
		 * Returns the current database status
		 */
		UInt16 DatabaseStatus { get; }

		/**
		 * True if there is any history. This can be used in UI to determine whether
		 * the "clear history" button should be enabled or not. This is much better
		 * than using BrowserHistory.count since that can be very slow if there is
		 * a lot of history (it must enumerate each item). This is pretty fast.
		 */
		Boolean HasHistoryEntries { get; }

		/**
		 * Gets the original title of the page.
		 */
		void GetPageTitle(nsIURI aURI, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * This is just like markPageAsTyped (in nsIBrowserHistory, also implemented
		 * by the history service), but for bookmarks. It declares that the given URI
		 * is being opened as a result of following a bookmark. If this URI is loaded
		 * soon after this message has been received, that transition will be marked
		 * as following a bookmark.
		 */
		void MarkPageAsFollowedBookmark(nsIURI aURI);

		/**
		 * Gets the stored character-set for an URI.
		 *
		 * @param aURI
		 *        URI to retrieve character-set for
		 * @return character-set, empty string if not found
		 */
		void GetCharsetForURI(nsIURI aURI, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * Sets the character-set for an URI.
		 *
		 * @param aURI
		 *        URI to set the character-set for
		 * @param aCharset
		 *        character-set to be set
		 */
		void SetCharsetForURI(nsIURI aURI, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aCharset);

		/**
		 * Returns true if this URI would be added to the history. You don't have to
		 * worry about calling this, addPageToSession/addURI will always check before
		 * actually adding the page. This function is public because some components
		 * may want to check if this page would go in the history (i.e. for
		 * annotations).
		 */
		Boolean CanAddURI(nsIURI aURI);

		/**
		 * Call to manually add a visit for a specific page. This will probably not
		 * be commonly used other than for backup/restore type operations. If the URI
		 * does not have an entry in the history database already, one will be created
		 * with no visits, no title, hidden, not typed.  Adding a visit will
		 * automatically increment the visit count for the visited page and will unhide
		 * it and/or mark it typed according to the transition type.
		 *
		 * @param aURI             Visited page
		 * @param aTime            Time page was visited (microseconds)
		 * @param aReferringURI    The URI of the visit that generated this one. Use
		 *                         null for no referrer.
		 * @param aTranstitionType Type of transition: one of TRANSITION_* above
		 * @param aIsRedirect      True if the given visit redirects to somewhere else.
		 *                         (ie you will create an visit out of here that is a
		 *                         redirect transition). This causes this page to be
		 *                         hidden in normal history views (unless it has been
		 *                         unhidden by visiting it with a non-redirect).
		 * @param aSessionID       The session ID that this page belongs to. Use 0 for
		 *                         no session.
		 * @return The ID of the created visit. This will be 0 if the URI cannot
		 *         be added to history (canAddURI = false) or the visit is session
		 *         persistent (TRANSITION_EMBED).
		 */
		Int64 AddVisit(nsIURI aURI, PRTime aTime,
						   nsIURI aReferringURI, Int32 aTransitionType,
						   Boolean aIsRedirect, Int64 aSessionID);

		/**
		 * This returns a new query object that you can pass to executeQuer[y/ies].
		 * It will be initialized to all empty (so using it will give you all history).
		 */
		nsINavHistoryQuery GetNewQuery();

		/**
		 * This returns a new options object that you can pass to executeQuer[y/ies]
		 * after setting the desired options.
		 */
		nsINavHistoryQueryOptions GetNewQueryOptions();

		/**
		 * Executes a single query.
		 */
		nsINavHistoryResult ExecuteQuery(nsINavHistoryQuery aQuery,
										 nsINavHistoryQueryOptions options);

		/**
		 * Executes an array of queries. All of the query objects are ORed
		 * together. Within a query, all the terms are ANDed together as in
		 * executeQuery. See executeQuery()
		 */
		nsINavHistoryResult ExecuteQueries(
		  [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 1)] nsINavHistoryQuery[] aQueries,
		  UInt32 aQueryCount,
		  nsINavHistoryQueryOptions options);

		/**
		 * Converts a query URI-like string to an array of actual query objects for
		 * use to executeQueries(). The output query array may be empty if there is
		 * no information. However, there will always be an options structure returned
		 * (if nothing is defined, it will just have the default values).
		 */
		void QueryStringToQueries([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aQueryString,
		  [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 2)] out nsINavHistoryQuery[] aQueries,
		  out UInt32 aResultCount,
		  out nsINavHistoryQueryOptions options);

		/**
		 * Converts a query into an equivalent string that can be persisted. Inverse
		 * of queryStringToQueries()
		 */
		void QueriesToQueryString(
		  [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 1)] nsINavHistoryQuery[] aQueries,
		  UInt32 aQueryCount,
		  nsINavHistoryQueryOptions options,
			[In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * Adds a history observer. If ownsWeak is false, the history service will
		 * keep an owning reference to the observer.  If ownsWeak is true, then
		 * aObserver must implement nsISupportsWeakReference, and the history service
		 * will keep a weak reference to the observer.
		 */
		void AddObserver(nsINavHistoryObserver observer, Boolean ownsWeak);

		/**
		 * Removes a history observer.
		 */
		void RemoveObserver(nsINavHistoryObserver observer);

		/**
		 * Runs the passed callback in batch mode. Use this when a lot of things
		 * are about to change. Calls can be nested, observers will only be
		 * notified when all batches begin/end.
		 *
		 * @param aCallback
		 *        nsINavHistoryBatchCallback interface to call.
		 * @param aUserData
		 *        Opaque parameter passed to nsINavBookmarksBatchCallback
		 */
		void RunInBatchMode(nsINavHistoryBatchCallback aCallback,
							[MarshalAs(UnmanagedType.IUnknown)] nsISupports aClosure);

		/** 
		 * True if history is disabled. currently, 
		 * history is disabled if the places.history.enabled pref is false.
		 */
		Boolean HistoryDisabled { get; }

		/**
		 * Import the given Mork history file.
		 *  @param file     The Mork history file to import
		 */
		void ImportHistory(nsIFile file);
	}

	/**
	 * @see runInBatchMode of nsINavHistoryService/nsINavBookmarksService
	 */
	[ComImport, Guid("5143f2bb-be0a-4faf-9acb-b0ed3f82952c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsINavHistoryBatchCallback //: nsISupports
	{
		void RunBatched([MarshalAs(UnmanagedType.IUnknown)] nsISupports aUserData);
	}
}
