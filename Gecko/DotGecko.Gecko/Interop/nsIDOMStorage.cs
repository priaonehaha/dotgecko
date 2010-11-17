using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Interface for client side storage. See
	 * http://www.whatwg.org/specs/web-apps/current-work/multipage/structured.html#storage0
	 * for more information.
	 *
	 * A storage object stores an arbitrary set of key-value pairs, which
	 * may be retrieved, modified and removed as needed. A key may only
	 * exist once within a storage object, and only one value may be
	 * associated with a particular key. Keys are stored in a particular
	 * order with the condition that this order not change by merely changing
	 * the value associated with a key, but the order may change when a
	 * key is added or removed.
	 */
	[ComImport, Guid("43E5EDAD-1E02-42c4-9D99-C3D9DEE22A20"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMStorage //: nsISupports
	{
		/**
		 * The number of keys stored.
		 */
		UInt32 Length { get; }

		/**
		 * Retrieve the name of the key at a particular index.
		 *
		 * @param index index of the item to retrieve
		 * @returns the key at index
		 * @throws INDEX_SIZE_ERR if there is no key at that index
		 */
		void GetKey(UInt32 index, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		/**
		 * Retrieve an item with a given key
		 *
		 * @param key key to retrieve
		 * @returns found data or empty string if the key was not found
		 */
		void GetItem([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String key, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		/**
		 * Assign a value with a key. If the key does not exist already, a new
		 * key is added associated with that value. If the key already exists,
		 * then the existing value is replaced with a new value.
		 *
		 * @param key key to set
		 * @param data data to associate with the key
		 */
		void SetItem([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String key, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String data);

		/**
		 * Remove a key and its corresponding value.
		 *
		 * @param key key to remove
		 */
		void RemoveItem([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String key);

		/**
		 * Clear the content of this storage bound to a domain
		 * or an origin.
		 */
		void Clear();
	}
}
