using System;
using System.Runtime.InteropServices;
using System.Text;
using nsISupports = System.Object;
using nsQIResult = System.Object;

namespace DotGecko.Gecko.Interop
{
	/* nsIVariant based Property Bag support. */
	[ComImport, Guid("9cfd1587-360e-4957-a58f-4c2b1c5e7ed9"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWritablePropertyBag2 : nsIPropertyBag2
	{
		#region nsIPropertyBag Members

		new nsISimpleEnumerator Enumerator { get; }
		new nsIVariant GetProperty([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String name);

		#endregion

		#region nsIPropertyBag2 Members

		new Int32 GetPropertyAsInt32([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);
		new UInt32 GetPropertyAsUint32([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);
		new Int64 GetPropertyAsInt64([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);
		new UInt64 GetPropertyAsUint64([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);
		new Double GetPropertyAsDouble([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);
		new void GetPropertyAsAString([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop,
								  [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		new void GetPropertyAsACString([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop,
								   [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);
		new void GetPropertyAsAUTF8String([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop,
									  [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		new Boolean GetPropertyAsBool([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 1)]
		new nsQIResult GetPropertyAsInterface([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop, [In] ref Guid iid);
		new nsIVariant Get([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);
		new Boolean HasKey([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop);

		#endregion

		void SetPropertyAsInt32([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop, Int32 value);
		void SetPropertyAsUint32([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop, UInt32 value);
		void SetPropertyAsInt64([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop, Int64 value);
		void SetPropertyAsUint64([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop, UInt64 value);
		void SetPropertyAsDouble([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop, Double value);
		void SetPropertyAsAString([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
		void SetPropertyAsACString([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String value);
		void SetPropertyAsAUTF8String([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);
		void SetPropertyAsBool([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop, Boolean value);
		void SetPropertyAsInterface([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String prop, [MarshalAs(UnmanagedType.IUnknown)] nsISupports value);
	}
}
