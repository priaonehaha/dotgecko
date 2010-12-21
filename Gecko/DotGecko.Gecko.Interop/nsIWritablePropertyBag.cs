using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/* nsIVariant based writable Property Bag support. */
	[ComImport, Guid("96fc4671-eeb4-4823-9421-e50fb70ad353"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWritablePropertyBag : nsIPropertyBag
	{
		#region nsIPropertyBag Members

		new nsISimpleEnumerator Enumerator { get; }
		new nsIVariant GetProperty([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String name);

		#endregion

		/**
		 * Set a property with the given name to the given value.  If
		 * a property already exists with the given name, it is
		 * overwritten.
		 */
		void SetProperty([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String name, nsIVariant value);

		/**
		 * Delete a property with the given name.
		 * @throws NS_ERROR_FAILURE if a property with that name doesn't
		 * exist.
		 */
		void DeleteProperty([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String name);
	}
}
