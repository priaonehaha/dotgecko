using System;
using System.Runtime.InteropServices;
using System.Text;
using JSValPtr = System.IntPtr;
using JSContext = System.IntPtr;
using jsval = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Encode and decode JSON text.
	 */
	[ComImport, Guid("a4d68b4e-0c0b-4c7c-b540-ef2f9834171f"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIJSON //: nsISupports
	{
		void Encode(/* in JSObject value */ [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		void EncodeToStream(nsIOutputStream stream,
							[MarshalAs(UnmanagedType.LPStr)] String charset,
							Boolean writeBOM
			/* in JSObject value */);

		void /* JSObject */ Secode([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String str);

		void /* JSObject */ DecodeFromStream(nsIInputStream stream, Int32 contentLength);

		void EncodeFromJSVal(JSValPtr value, JSContext cx, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		// Make sure you GCroot the result of this function before using it.
		jsval DecodeToJSVal([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String str, JSContext cx);


		/*
		 * Decode a JSON string, but also accept some strings in non-JSON format, as
		 * the decoding methods here did previously before tightening.
		 *
		 * This method is provided only as a temporary transition path for users of
		 * the old code who depended on the ability to decode leniently; new users
		 * should use the non-legacy decoding methods.
		 *
		 * @param str the string to parse
		 */
		void /* JSObject */ LegacyDecode([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String str);

		/* Identical to legacyDecode, but decode the contents of stream. */
		void /* JSObject */ LegacyDecodeFromStream(nsIInputStream stream, Int32 contentLength);

		/* Identical to legacyDecode, but decode into a jsval. */
		// Make sure you GCroot the result of this function before using it.
		jsval LegacyDecodeToJSVal([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String str, JSContext cx);
	}
}
