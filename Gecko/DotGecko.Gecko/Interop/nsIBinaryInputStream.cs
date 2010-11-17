using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	internal static class nsIBinaryInputStreamExtensions
	{
		public static nsResult NS_ReadOptionalCString(this nsIBinaryInputStream aStream, StringBuilder aResult)
		{
			//Boolean nonnull;
			//nsResult rv = aStream.ReadBoolean(&nonnull);
			//if (rv.NS_SUCCEEDED())
			//{
			//    if (nonnull)
			//    {
			//        rv = aStream.ReadCString(aResult);
			//    }
			//    else
			//    {
			//        aResult.Truncate();
			//    }
			//}
			//return rv;
			return nsResult.NS_ERROR_NOT_IMPLEMENTED;
		}

		public static nsResult NS_ReadOptionalString(this nsIBinaryInputStream aStream, StringBuilder aResult)
		{
			//Boolean nonnull;
			//nsResult rv = aStream.ReadBoolean(&nonnull);
			//if (rv.NS_SUCCEEDED())
			//{
			//    if (nonnull)
			//    {
			//        rv = aStream.ReadString(aResult);
			//    }
			//    else
			//    {
			//        aResult.Truncate();
			//    }
			//}
			//return rv;
			return nsResult.NS_ERROR_NOT_IMPLEMENTED;
		}
	}

	/**
	 * This interface allows consumption of primitive data types from a "binary
	 * stream" containing untagged, big-endian binary data, i.e. as produced by an
	 * implementation of nsIBinaryOutputStream.  This might be used, for example,
	 * to implement network protocols or to read from architecture-neutral disk
	 * files, i.e. ones that can be read and written by both big-endian and
	 * little-endian platforms.
	 *
	 * @See nsIBinaryOutputStream
	 */
	[ComImport, Guid("7b456cb0-8772-11d3-90cf-0040056a906e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIBinaryInputStream : nsIInputStream
	{
		#region nsIInputStream Members

		new void Close();
		new UInt32 Available();
		new UInt32 Read(IntPtr aBuf, UInt32 aCount);
		new UInt32 ReadSegments([MarshalAs(UnmanagedType.FunctionPtr)] nsWriteSegmentFun aWriter, IntPtr aClosure, UInt32 aCount);
		new Boolean IsNonBlocking();

		#endregion

		void SetInputStream(nsIInputStream aInputStream);

		/**
		 * Read 8-bits from the stream.
		 *
		 * @return that byte to be treated as a boolean.
		 */
		Boolean ReadBoolean();

		Byte Read8();
		UInt16 Read16();
		UInt32 Read32();
		UInt64 Read64();

		Single ReadFloat();
		Double ReadDouble();

		/**
		 * Read an 8-bit pascal style string from the stream.
		 * 32-bit length field, followed by length 8-bit chars.
		 */
		void ReadCString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);

		/**
		 * Read an 16-bit pascal style string from the stream.
		 * 32-bit length field, followed by length PRUnichars.
		 */
		void ReadString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);

		/**
		 * Read an opaque byte array from the stream.
		 *
		 * @param aLength the number of bytes that must be read.
		 *
		 * @throws NS_ERROR_FAILURE if it can't read aLength bytes
		 */
		[return: MarshalAs(UnmanagedType.LPStr, SizeParamIndex = 0)]
		String ReadBytes(UInt32 aLength);

		/**
		 * Read an opaque byte array from the stream, storing the results
		 * as an array of PRUint8s.
		 *
		 * @param aLength the number of bytes that must be read.
		 *
		 * @throws NS_ERROR_FAILURE if it can't read aLength bytes
		 */
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		nsResult ReadByteArray(UInt32 aLength, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 0)] out Byte[] aBytes);
	}
}
