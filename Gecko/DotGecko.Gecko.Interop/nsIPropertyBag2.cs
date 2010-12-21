using System;
using System.Runtime.InteropServices;
using System.Text;
using nsQIResult = System.Object;

namespace DotGecko.Gecko.Interop
{
	/* nsIVariant based Property Bag support. */
	[ComImport, Guid("625cfd1e-da1e-4417-9ee9-dbc8e0b3fd79"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIPropertyBag2 : nsIPropertyBag
	{
		#region nsIPropertyBag Members

		new nsISimpleEnumerator Enumerator { get; }
		new nsIVariant GetProperty([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String name);

		#endregion

		// Accessing a property as a different type may attempt conversion to the
		// requested value
		Int32 GetPropertyAsInt32([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);
		UInt32 GetPropertyAsUint32([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);
		Int64 GetPropertyAsInt64([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);
		UInt64 GetPropertyAsUint64([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);
		Double GetPropertyAsDouble([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);
		void GetPropertyAsAString([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop,
								  [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		void GetPropertyAsACString([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop,
								   [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);
		void GetPropertyAsAUTF8String([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop,
									  [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		Boolean GetPropertyAsBool([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);

		/**
		 * This method returns null if the value exists, but is null.
		 */
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 1)]
		nsQIResult GetPropertyAsInterface([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop, [In] ref Guid iid);

		/**
		 * This method returns null if the value does not exist,
		 * or exists but is null.
		 */
		nsIVariant Get([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);

		/**
		 * Check for the existence of a key.
		 */
		Boolean HasKey([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);
	}
}
