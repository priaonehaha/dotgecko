using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;
using nsQIResult = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIMutableArray
	 * A separate set of methods that will act on the array. Consumers of
	 * nsIArray should not QueryInterface to nsIMutableArray unless they
	 * own the array.
	 *
	 * As above, it is legal to add null elements to the array. Note also
	 * that null elements can be created as a side effect of
	 * insertElementAt(). Conversely, if insertElementAt() is never used,
	 * and null elements are never explicitly added to the array, then it
	 * is guaranteed that queryElementAt() will never return a null value.
	 *
	 * Any of these methods may throw NS_ERROR_OUT_OF_MEMORY when the
	 * array must grow to complete the call, but the allocation fails.
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("af059da0-c85b-40ec-af07-ae4bfdc192cc"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIMutableArray : nsIArray
	{
		#region nsIArray Members

		new UInt32 Length { get; }
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 1)]
		new nsQIResult QueryElementAt(UInt32 index, [In] ref Guid uuid);
		new UInt32 IndexOf(UInt32 startIndex, [MarshalAs(UnmanagedType.IUnknown)] nsISupports element);
		new nsISimpleEnumerator Enumerate();

		#endregion

		/**
		 * appendElement()
		 * 
		 * Append an element at the end of the array.
		 *
		 * @param element The element to append.
		 * @param weak    Whether or not to store the element using a weak
		 *                reference.
		 * @throws NS_ERROR_FAILURE when a weak reference is requested,
		 *                          but the element does not support
		 *                          nsIWeakReference.
		 */
		void AppendElement([MarshalAs(UnmanagedType.IUnknown)] nsISupports element, Boolean weak);

		/**
		 * removeElementAt()
		 * 
		 * Remove an element at a specific position, moving all elements
		 * stored at a higher position down one.
		 * To remove a specific element, use indexOf() to find the index
		 * first, then call removeElementAt().
		 *
		 * @param index the position of the item
		 *
		 */
		void RemoveElementAt(UInt32 index);

		/**
		 * insertElementAt()
		 *
		 * Insert an element at the given position, moving the element 
		 * currently located in that position, and all elements in higher
		 * position, up by one.
		 *
		 * @param element The element to insert
		 * @param index   The position in the array:
		 *                If the position is lower than the current length
		 *                of the array, the elements at that position and
		 *                onwards are bumped one position up.
		 *                If the position is equal to the current length
		 *                of the array, the new element is appended.
		 *                An index lower than 0 or higher than the current
		 *                length of the array is invalid and will be ignored.
		 *
		 * @throws NS_ERROR_FAILURE when a weak reference is requested,
		 *                          but the element does not support
		 *                          nsIWeakReference.
		 */
		void InsertElementAt([MarshalAs(UnmanagedType.IUnknown)] nsISupports element, UInt32 index, Boolean weak);

		/**
		 * replaceElementAt()
		 *
		 * Replace the element at the given position.
		 *
		 * @param element The new element to insert
		 * @param index   The position in the array
		 *                If the position is lower than the current length
		 *                of the array, an existing element will be replaced.
		 *                If the position is equal to the current length
		 *                of the array, the new element is appended.
		 *                If the position is higher than the current length
		 *                of the array, empty elements are appended followed
		 *                by the new element at the specified position.
		 *                An index lower than 0 is invalid and will be ignored.
		 *
		 * @param weak    Whether or not to store the new element using a weak
		 *                reference.
		 *
		 * @throws NS_ERROR_FAILURE when a weak reference is requested,
		 *                          but the element does not support
		 *                          nsIWeakReference.
		 */
		void ReplaceElementAt([MarshalAs(UnmanagedType.IUnknown)] nsISupports element, UInt32 index, Boolean weak);

		/**
		 * clear()
		 *
		 * clear the entire array, releasing all stored objects
		 */
		void Clear();
	}
}
