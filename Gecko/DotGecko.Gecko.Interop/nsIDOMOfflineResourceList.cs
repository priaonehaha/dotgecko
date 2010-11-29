using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	public static class nsIDOMOfflineResourceListConstants
	{
		/**
		 * State of the application cache this object is associated with.
		 */

		/* This object is not associated with an application cache. */
		public const UInt16 UNCACHED = 0;

		/* The application cache is not being updated. */
		public const UInt16 IDLE = 1;

		/* The manifest is being fetched and checked for updates */
		public const UInt16 CHECKING = 2;

		/* Resources are being downloaded to be added to the cache */
		public const UInt16 DOWNLOADING = 3;

		/* There is a new version of the application cache available */
		public const UInt16 UPDATEREADY = 4;

		/* The application cache group is now obsolete. */
		public const UInt16 OBSOLETE = 5;
	}

	[ComImport, Guid("f394a721-66e9-46fc-bb24-b980bb732dd0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMOfflineResourceList //: nsISupports
	{
		/**
		 * Get the list of dynamically-managed entries.
		 */
		nsIDOMDOMStringList MozItems { get; }

		/**
		 * Check that an entry exists in the list of dynamically-managed entries.
		 *
		 * @param uri
		 *        The resource to check.
		 */
		Boolean MozHasItem([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String uri);

		/**
		 * Get the number of dynamically-managed entries.
		 * @status DEPRECATED
		 *         Clients should use the "items" attribute.
		 */
		UInt32 MozLength { get; }

		/**
		 * Get the URI of a dynamically-managed entry.
		 * @status DEPRECATED
		 *         Clients should use the "items" attribute.
		 */
		void MozItem(UInt32 index, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		/**
		 * Add an item to the list of dynamically-managed entries.  The resource
		 * will be fetched into the application cache.
		 *
		 * @param uri
		 *        The resource to add.
		 */
		void MozAdd([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String uri);

		/**
		 * Remove an item from the list of dynamically-managed entries.  If this
		 * was the last reference to a URI in the application cache, the cache
		 * entry will be removed.
		 *
		 * @param uri
		 *        The resource to remove.
		 */
		void MozRemove([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String uri);

		UInt16 Status { get; }

		/**
		 * Begin the application update process on the associated application cache.
		 */
		void Update();

		/**
		 * Swap in the newest version of the application cache, or disassociate
		 * from the cache if the cache group is obsolete.
		 */
		void SwapCache();

		/* Events */
		nsIDOMEventListener OnChecking { get; set; }
		nsIDOMEventListener OnError { get; set; }
		nsIDOMEventListener OnNoUpdate { get; set; }
		nsIDOMEventListener OnDownloading { get; set; }
		nsIDOMEventListener OnProgress { get; set; }
		nsIDOMEventListener OnUpdateReady { get; set; }
		nsIDOMEventListener OnCached { get; set; }
		nsIDOMEventListener OnObsolete { get; set; }
	}
}
