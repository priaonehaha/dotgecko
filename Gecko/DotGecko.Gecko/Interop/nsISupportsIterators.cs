using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/*
	  ...
	*/


	/**
	 * ...
	 */
	[ComImport, Guid("7330650e-1dd2-11b2-a0c2-9ff86ee97bed"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIOutputIterator //: nsISupports
	{
		/**
		 * Put |anElementToPut| into the underlying container or sequence at the position currently pointed to by this iterator.
		 * The iterator and the underlying container or sequence cooperate to |Release()|
		 * the replaced element, if any and if necessary, and to |AddRef()| the new element.
		 *
		 * The result is undefined if this iterator currently points outside the
		 * useful range of the underlying container or sequence.
		 *
		 * @param anElementToPut the element to place into the underlying container or sequence
		 */
		void PutElement([MarshalAs(UnmanagedType.IUnknown)] nsISupports anElementToPut);

		/**
		 * Advance this iterator to the next position in the underlying container or sequence.
		 */
		void StepForward();
	}

	/**
	 * ...
	 */
	[ComImport, Guid("85585e12-1dd2-11b2-a930-f6929058269a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIInputIterator //: nsISupports
	{
		/**
		 * Retrieve (and |AddRef()|) the element this iterator currently points to.
		 *
		 * The result is undefined if this iterator currently points outside the
		 * useful range of the underlying container or sequence.
		 *
		 * @result a new reference to the element this iterator currently points to (if any)
		 */
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports GetElement();

		/**
		 * Advance this iterator to the next position in the underlying container or sequence.
		 */
		void StepForward();

		/**
		 * Test if |anotherIterator| points to the same position in the underlying container or sequence.
		 *
		 * The result is undefined if |anotherIterator| was not created by or for the same underlying container or sequence.
		 *
		 * @param anotherIterator another iterator to compare against, created by or for the same underlying container or sequence
		 * @result true if |anotherIterator| points to the same position in the underlying container or sequence
		 */
		Boolean IsEqualTo([MarshalAs(UnmanagedType.IUnknown)] nsISupports anotherIterator);

		/**
		 * Create a new iterator pointing to the same position in the underlying container or sequence to which this iterator currently points.
		 * The returned iterator is suitable for use in a subsequent call to |isEqualTo()| against this iterator.
		 *
		 * @result a new iterator pointing at the same position in the same underlying container or sequence as this iterator
		 */
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports Clone();
	}

	/**
	 * ...
	 */
	[ComImport, Guid("8da01646-1dd2-11b2-98a7-c7009045be7e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIForwardIterator //: nsISupports
	{
		/**
		 * Retrieve (and |AddRef()|) the element this iterator currently points to.
		 *
		 * The result is undefined if this iterator currently points outside the
		 * useful range of the underlying container or sequence.
		 *
		 * @result a new reference to the element this iterator currently points to (if any)
		 */
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports GetElement();

		/**
		 * Put |anElementToPut| into the underlying container or sequence at the position currently pointed to by this iterator.
		 * The iterator and the underlying container or sequence cooperate to |Release()|
		 * the replaced element, if any and if necessary, and to |AddRef()| the new element.
		 *
		 * The result is undefined if this iterator currently points outside the
		 * useful range of the underlying container or sequence.
		 *
		 * @param anElementToPut the element to place into the underlying container or sequence
		 */
		void PutElement([MarshalAs(UnmanagedType.IUnknown)] nsISupports anElementToPut);

		/**
		 * Advance this iterator to the next position in the underlying container or sequence.
		 */
		void StepForward();

		/**
		 * Test if |anotherIterator| points to the same position in the underlying container or sequence.
		 *
		 * The result is undefined if |anotherIterator| was not created by or for the same underlying container or sequence.
		 *
		 * @param anotherIterator another iterator to compare against, created by or for the same underlying container or sequence
		 * @result true if |anotherIterator| points to the same position in the underlying container or sequence
		 */
		Boolean IsEqualTo([MarshalAs(UnmanagedType.IUnknown)] nsISupports anotherIterator);

		/**
		 * Create a new iterator pointing to the same position in the underlying container or sequence to which this iterator currently points.
		 * The returned iterator is suitable for use in a subsequent call to |isEqualTo()| against this iterator.
		 *
		 * @result a new iterator pointing at the same position in the same underlying container or sequence as this iterator
		 */
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports Clone();
	}

	/**
	 * ...
	 */
	[ComImport, Guid("948defaa-1dd1-11b2-89f6-8ce81f5ebda9"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIBidirectionalIterator //: nsISupports
	{
		/**
		 * Retrieve (and |AddRef()|) the element this iterator currently points to.
		 *
		 * The result is undefined if this iterator currently points outside the
		 * useful range of the underlying container or sequence.
		 *
		 * @result a new reference to the element this iterator currently points to (if any)
		 */
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports GetElement();

		/**
		 * Put |anElementToPut| into the underlying container or sequence at the position currently pointed to by this iterator.
		 * The iterator and the underlying container or sequence cooperate to |Release()|
		 * the replaced element, if any and if necessary, and to |AddRef()| the new element.
		 *
		 * The result is undefined if this iterator currently points outside the
		 * useful range of the underlying container or sequence.
		 *
		 * @param anElementToPut the element to place into the underlying container or sequence
		 */
		void PutElement([MarshalAs(UnmanagedType.IUnknown)] nsISupports anElementToPut);

		/**
		 * Advance this iterator to the next position in the underlying container or sequence.
		 */
		void StepForward();

		/**
		 * Move this iterator to the previous position in the underlying container or sequence.
		 */
		void StepBackward();

		/**
		 * Test if |anotherIterator| points to the same position in the underlying container or sequence.
		 *
		 * The result is undefined if |anotherIterator| was not created by or for the same underlying container or sequence.
		 *
		 * @param anotherIterator another iterator to compare against, created by or for the same underlying container or sequence
		 * @result true if |anotherIterator| points to the same position in the underlying container or sequence
		 */
		Boolean IsEqualTo([MarshalAs(UnmanagedType.IUnknown)] nsISupports anotherIterator);

		/**
		 * Create a new iterator pointing to the same position in the underlying container or sequence to which this iterator currently points.
		 * The returned iterator is suitable for use in a subsequent call to |isEqualTo()| against this iterator.
		 *
		 * @result a new iterator pointing at the same position in the same underlying container or sequence as this iterator
		 */
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports Clone();
	}

	/**
	 * ...
	 */
	[ComImport, Guid("9bd6fdb0-1dd1-11b2-9101-d15375968230"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIRandomAccessIterator //: nsISupports
	{
		/**
		 * Retrieve (and |AddRef()|) the element this iterator currently points to.
		 *
		 * The result is undefined if this iterator currently points outside the
		 * useful range of the underlying container or sequence.
		 *
		 * @result a new reference to the element this iterator currently points to (if any)
		 */
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports GetElement();

		/**
		 * Retrieve (and |AddRef()|) an element at some offset from where this iterator currently points.
		 * The offset may be negative.  |getElementAt(0)| is equivalent to |getElement()|.
		 *
		 * The result is undefined if this iterator currently points outside the
		 * useful range of the underlying container or sequence.
		 *
		 * @param anOffset a |0|-based offset from the position to which this iterator currently points
		 * @result a new reference to the indicated element (if any)
		 */
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports GetElementAt(Int32 anOffset);

		/**
		 * Put |anElementToPut| into the underlying container or sequence at the position currently pointed to by this iterator.
		 * The iterator and the underlying container or sequence cooperate to |Release()|
		 * the replaced element, if any and if necessary, and to |AddRef()| the new element.
		 *
		 * The result is undefined if this iterator currently points outside the
		 * useful range of the underlying container or sequence.
		 *
		 * @param anElementToPut the element to place into the underlying container or sequence
		 */
		void PutElement([MarshalAs(UnmanagedType.IUnknown)] nsISupports anElementToPut);

		/**
		 * Put |anElementToPut| into the underlying container or sequence at the position |anOffset| away from that currently pointed to by this iterator.
		 * The iterator and the underlying container or sequence cooperate to |Release()|
		 * the replaced element, if any and if necessary, and to |AddRef()| the new element.
		 * |putElementAt(0, obj)| is equivalent to |putElement(obj)|.
		 *
		 * The result is undefined if this iterator currently points outside the
		 * useful range of the underlying container or sequence.
		 *
		 * @param anOffset a |0|-based offset from the position to which this iterator currently points
		 * @param anElementToPut the element to place into the underlying container or sequence
		 */
		void PutElementAt(Int32 anOffset, [MarshalAs(UnmanagedType.IUnknown)] nsISupports anElementToPut);

		/**
		 * Advance this iterator to the next position in the underlying container or sequence.
		 */
		void StepForward();

		/**
		 * Move this iterator by |anOffset| positions in the underlying container or sequence.
		 * |anOffset| may be negative.  |stepForwardBy(1)| is equivalent to |stepForward()|.
		 * |stepForwardBy(0)| is a no-op.
		 *
		 * @param anOffset a |0|-based offset from the position to which this iterator currently points
		 */
		void StepForwardBy(Int32 anOffset);

		/**
		 * Move this iterator to the previous position in the underlying container or sequence.
		 */
		void StepBackward();

		/**
		 * Move this iterator backwards by |anOffset| positions in the underlying container or sequence.
		 * |anOffset| may be negative.  |stepBackwardBy(1)| is equivalent to |stepBackward()|.
		 * |stepBackwardBy(n)| is equivalent to |stepForwardBy(-n)|.  |stepBackwardBy(0)| is a no-op.
		 *
		 * @param anOffset a |0|-based offset from the position to which this iterator currently points
		 */
		void StepBackwardBy(Int32 anOffset);

		/**
		 * Test if |anotherIterator| points to the same position in the underlying container or sequence.
		 *
		 * The result is undefined if |anotherIterator| was not created by or for the same underlying container or sequence.
		 *
		 * @param anotherIterator another iterator to compare against, created by or for the same underlying container or sequence
		 * @result true if |anotherIterator| points to the same position in the underlying container or sequence
		 */
		Boolean IsEqualTo([MarshalAs(UnmanagedType.IUnknown)] nsISupports anotherIterator);

		/**
		 * Create a new iterator pointing to the same position in the underlying container or sequence to which this iterator currently points.
		 * The returned iterator is suitable for use in a subsequent call to |isEqualTo()| against this iterator.
		 *
		 * @result a new iterator pointing at the same position in the same underlying container or sequence as this iterator
		 */
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports Clone();
	}
}
