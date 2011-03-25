using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIXULSortServiceConstants
	{
		public const UInt32 SORT_COMPARECASE = 0x0001;
		public const UInt32 SORT_INTEGER = 0x0100;
	}

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
		 * @param aSortHints One or more hints as to how to sort:
		 *
		 *   ascending: to sort the contents in ascending order
		 *   descending: to sort the contents in descending order
		 *   comparecase: perform case sensitive comparisons
		 *   integer: treat values as integers, non-integers are compared as strings
		 *   twostate: don't allow the natural (unordered state)
		 */
		void Sort(nsIDOMNode aNode,
				  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aSortKey,
				  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aSortHints);
	}
}
