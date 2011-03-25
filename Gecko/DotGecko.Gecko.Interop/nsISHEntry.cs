using System;
using System.Runtime.InteropServices;
using System.Text;
using nsISupports = System.Object;
using nsDocShellEditorData = System.IntPtr;
using nsDocShellEditorDataPtr = System.IntPtr;
using nsILayoutHistoryState = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The interface to nsISHentry. Each document or subframe in 
	 * Session History will have a nsISHEntry associated with it which will
	 * hold all information required to recreate the document from history
	 * 
	 */
	[ComImport, Guid("39b73c3a-48eb-4189-8069-247279c3c42d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISHEntry : nsIHistoryEntry
	{
		#region nsIHistoryEntry Members

		new nsIURI URI { get; }
		new String Title { [return: MarshalAs(UnmanagedType.LPWStr)] get; }
		new Boolean IsSubFrame { get; }

		#endregion

		/** URI for the document */
		void SetURI(nsIURI aURI);

		/** Referrer URI */
		nsIURI ReferrerURI { get; set; }

		/** Content viewer, for fast restoration of presentation */
		nsIContentViewer ContentViewer { get; set; }

		/** Whether the content viewer is marked "sticky" */
		Boolean Sticky { get; set; }

		/** Saved state of the global window object */
		nsISupports WindowState { [return: MarshalAs(UnmanagedType.IUnknown)] get; [param: MarshalAs(UnmanagedType.IUnknown)] set; }

		/**
		 * Saved position and dimensions of the content viewer; we must adjust the
		 * root view's widget accordingly if this has changed when the presentation
		 * is restored.
		 */
		void GetViewerBounds(ref nsIntRect bounds);
		void SetViewerBounds([In] ref nsIntRect bounds);

		/**
		 * Saved child docshells corresponding to contentViewer.  The child shells
		 * are restored as children of the parent docshell, in this order, when the
		 * parent docshell restores a saved presentation.
		 */

		/** Append a child shell to the end of our list. */
		void AddChildShell(nsIDocShellTreeItem shell);

		/**
		 * Get the child shell at |index|; returns null if |index| is out of bounds.
		 */
		nsIDocShellTreeItem ChildShellAt(Int32 index);

		/**
		 * Clear the child shell list.
		 */
		void ClearChildShells();

		/** Saved refresh URI list for the content viewer */
		nsISupportsArray RefreshURIList { get; set; }

		/**
		 * Ensure that the cached presentation members are self-consistent.
		 * If either |contentViewer| or |windowState| are null, then all of the
		 * following members are cleared/reset:
		 *  contentViewer, sticky, windowState, viewerBounds, childShells,
		 *  refreshURIList.
		 */
		void SyncPresentationState();

		/** Title for the document */
		void SetTitle([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aTitle);

		/** Post Data for the document */
		nsIInputStream PostData { get; set; }

		/** LayoutHistoryState for scroll position and form values */
		nsILayoutHistoryState LayoutHistoryState { get; set; }

		/** parent of this entry */
		nsISHEntry Parent { get; set; }

		/**
		 * The loadType for this entry. This is typically loadHistory except
		 * when reload is pressed, it has the appropriate reload flag
		 */
		UInt32 LoadType { get; set; }

		/**
		 * An ID to help identify this entry from others during
		 * subframe navigation
		 */
		UInt32 ID { get; set; }

		/**
		 * pageIdentifier is an integer that should be the same for two entries
		 * attached to the same docshell only if the two entries are entries for
		 * the same page in the sense that one could go from the state represented
		 * by one to the state represented by the other simply by scrolling (so the
		 * entries are separated by an anchor traversal or a subframe navigation in
		 * some other frame).
		 */
		UInt32 PageIdentifier { get; set; }

		/**
		 * docIdentifier is an integer that should be the same for two entries
		 * attached to the same docshell if and only if the two entries are entries
		 * for the same document.  In practice, two entries A and B will have the
		 * same docIdentifier if they have the same pageIdentifier or if B was
		 * created by A calling history.pushState().
		 */
		UInt64 DocIdentifier { get; set; }

		/**
		 * Changes this entry's doc identifier to a new value which is unique
		 * among those of all other entries.
		 */
		void SetUniqueDocIdentifier();

		/** attribute to set and get the cache key for the entry */
		nsISupports CacheKey { [return: MarshalAs(UnmanagedType.IUnknown)] get; [param: MarshalAs(UnmanagedType.IUnknown)] set; }

		/** attribute to indicate whether layoutHistoryState should be saved */
		Boolean SaveLayoutStateFlag { get; set; }

		/** attribute to indicate whether the page is already expired in cache */
		Boolean ExpirationStatus { get; set; }

		/**
		 * attribute to indicate the content-type of the document that this
		 * is a session history entry for
		 */
		void GetContentType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);
		void SetContentType([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String value);

		/** Set/Get scrollers' positon in anchored pages */
		void SetScrollPosition(Int32 x, Int32 y);
		void GetScrollPosition(out Int32 x, out Int32 y);

		/** Additional ways to create an entry */
		void Create(
			nsIURI URI,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String title,
			nsIInputStream inputStream,
			nsILayoutHistoryState layoutHistoryState,
			nsISupports cacheKey,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String contentType,
			nsISupports owner,
			UInt64 docshellID,
			Boolean dynamicCreation);

		nsISHEntry Clone();

		/** Attribute that indicates if this entry is for a subframe navigation */
		void SetIsSubFrame(Boolean aFlag);

		/** Return any content viewer present in or below this node in the
			nsSHEntry tree.  This will differ from contentViewer in the case
			where a child nsSHEntry has the content viewer for this tree. */
		nsIContentViewer GetAnyContentViewer(out nsISHEntry ownerEntry);

		/**
		 * Get the owner, if any, that was associated with the channel
		 * that the document that was loaded to create this history entry
		 * came from.
		 */
		nsISupports Owner { [return: MarshalAs(UnmanagedType.IUnknown)] get; [param: MarshalAs(UnmanagedType.IUnknown)] set; }

		/**
		 * Get/set data associated with this history state via a pushState() call,
		 * encoded as JSON.
		 **/
		void GetStateData([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		void SetStateData([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);

		/**
		 * Gets the owning pointer to the editor data assosicated with
		 * this shistory entry. This forgets its pointer, so free it when
		 * you're done.
		 */
		nsDocShellEditorDataPtr ForgetEditorData();

		/**
		 * Sets the owning pointer to the editor data assosicated with
		 * this shistory entry. Unless forgetEditorData() is called, this
		 * shentry will destroy the editor data when it's destroyed.
		 */
		void SetEditorData(nsDocShellEditorDataPtr aData);

		/** Returns true if this shistory entry is storing a detached editor. */
		Boolean HasDetachedEditor();

		/**
		 * Returns true if the related docshell was added because of
		 * dynamic addition of an iframe/frame.
		 */
		Boolean IsDynamicallyAdded();

		/**
		 * Returns true if any of the child entries returns true
		 * when isDynamicallyAdded is called on it.
		 */
		Boolean HasDynamicallyAddedChild();

		/**
		 * The history ID of the docshell.
		 */
		UInt64 DocshellID { get; set; }
	}

	[ComImport, Guid("bb66ac35-253b-471f-a317-3ece940f04c5"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISHEntryInternal //: nsISupports
	{
		void RemoveFromBFCacheAsync();
		void RemoveFromBFCacheSync();

		/**
		 * A number that is assigned by the sHistory when the entry is activated
		 */
		UInt32 LastTouched { get; set; }
	}
}
