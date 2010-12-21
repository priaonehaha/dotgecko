using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * A service used to sort the contents of a XUL widget.
	 */
	[ComImport, Guid("F29270C8-3BE5-4046-9B57-945A84DFF132"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXULSortService //: nsISupports
	{
		/**
		 * Sort the contents of the widget containing <code>aNode</code>
		 * using <code>aSortKey</code> as the comparison key, and
		 * <code>aSortDirection</code> as the direction.
		 *
		 * @param aNode A node in the XUL widget whose children are to be sorted.
		 * @param aSortKey The value to be used as the comparison key.
		 * @param aSortDirection May be either <b>natural</b> to return
		 * the contents to their natural (unsorted) order,
		 * <b>ascending</b> to sort the contents in ascending order, or
		 * <b>descending</b> to sort the contents in descending order.
		 */
		void Sort(nsIDOMNode aNode,
				  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aSortKey,
				  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aSortDirection);
	}
}
