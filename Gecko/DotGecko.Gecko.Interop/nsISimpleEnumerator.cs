using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Used to enumerate over elements defined by its implementor.
	 * Although hasMoreElements() can be called independently of getNext(),
	 * getNext() must be pre-ceeded by a call to hasMoreElements(). There is
	 * no way to "reset" an enumerator, once you obtain one.
	 *
	 * @status FROZEN
	 * @version 1.0
	 */
	[ComImport, Guid("D1899240-F9D2-11D2-BDD6-000064657374"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISimpleEnumerator //: nsISupports
	{
		/**
		 * Called to determine whether or not the enumerator has
		 * any elements that can be returned via getNext(). This method
		 * is generally used to determine whether or not to initiate or
		 * continue iteration over the enumerator, though it can be
		 * called without subsequent getNext() calls. Does not affect
		 * internal state of enumerator.
		 *
		 * @see getNext()
		 * @return PR_TRUE if there are remaining elements in the enumerator.
		 *         PR_FALSE if there are no more elements in the enumerator.
		 */
		Boolean HasMoreElements();

		/**
		 * Called to retrieve the next element in the enumerator. The "next"
		 * element is the first element upon the first call. Must be
		 * pre-ceeded by a call to hasMoreElements() which returns PR_TRUE.
		 * This method is generally called within a loop to iterate over
		 * the elements in the enumerator.
		 *
		 * @see hasMoreElements()
		 * @return NS_OK if the call succeeded in returning a non-null
		 *               value through the out parameter.
		 *         NS_ERROR_FAILURE if there are no more elements
		 *                          to enumerate.
		 * @return the next element in the enumeration.
		 */
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		nsResult GetNext([MarshalAs(UnmanagedType.IUnknown)] out nsISupports retval);
	}
}
