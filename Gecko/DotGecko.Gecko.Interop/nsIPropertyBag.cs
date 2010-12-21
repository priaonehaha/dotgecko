using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/* nsIVariant based Property Bag support. */
	[ComImport, Guid("bfcd37b0-a49f-11d5-910d-0010a4e73d9a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIPropertyBag //: nsISupports
	{
		/**
		 * Get a nsISimpleEnumerator whose elements are nsIProperty objects.
		 */
		nsISimpleEnumerator Enumerator { get; }

		/**
		 * Get a property value for the given name.
		 * @throws NS_ERROR_FAILURE if a property with that name doesn't
		 * exist.
		 */
		nsIVariant GetProperty([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String name);
	}
}
