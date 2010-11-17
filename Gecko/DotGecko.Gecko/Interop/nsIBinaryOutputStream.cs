using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	internal static class nsIBinaryOutputStreamExtensions
	{
		public static nsResult NS_WriteOptionalStringZ(this nsIBinaryOutputStream aStream, [MarshalAs(UnmanagedType.LPStr)] String aString)
		{
			Boolean nonnull = (aString != null);
			nsResult rv = aStream.WriteBoolean(nonnull);
			if (rv.NS_SUCCEEDED() && nonnull)
			{
				rv = aStream.WriteStringZ(aString);
			}
			return rv;
		}

		public static nsResult NS_WriteOptionalWStringZ(this nsIBinaryOutputStream aStream, [MarshalAs(UnmanagedType.LPWStr)] String aString)
		{
			Boolean nonnull = (aString != null);
			nsResult rv = aStream.WriteBoolean(nonnull);
			if (rv.NS_SUCCEEDED() && nonnull)
			{
				rv = aStream.WriteWStringZ(aString);
			}
			return rv;
		}
	}

	/**
	 * This interface allows writing of primitive data types (integers,
	 * floating-point values, booleans, etc.) to a stream in a binary, untagged,
	 * fixed-endianness format.  This might be used, for example, to implement
	 * network protocols or to produce architecture-neutral binary disk files,
	 * i.e. ones that can be read and written by both big-endian and little-endian
	 * platforms.  Output is written in big-endian order (high-order byte first),
	 * as this is traditional network order.
	 *
	 * @See nsIBinaryInputStream
	 */
	[ComImport, Guid("204ee610-8765-11d3-90cf-0040056a906e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIBinaryOutputStream : nsIOutputStream
	{
		#region nsIOutputStream Members

		new void Close();
		new void Flush();
		new UInt32 Write([MarshalAs(UnmanagedType.LPStr)] String aBuf, UInt32 aCount);
		new UInt32 WriteFrom(nsIInputStream aFromStream, UInt32 aCount);
		new UInt32 WriteSegments([MarshalAs(UnmanagedType.FunctionPtr)] nsReadSegmentFun aReader, IntPtr aClosure, UInt32 aCount);
		new Boolean IsNonBlocking();

		#endregion

		void SetOutputStream(nsIOutputStream aOutputStream);

		/**
		 * Write a boolean as an 8-bit char to the stream.
		 */
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		nsResult WriteBoolean(Boolean aBoolean);

		void Write8(Byte aByte);
		void Write16(UInt16 a16);
		void Write32(UInt32 a32);
		void Write64(UInt64 a64);

		void WriteFloat(Single aFloat);
		void WriteDouble(Double aDouble);

		/**
		 * Write an 8-bit pascal style string to the stream.
		 * 32-bit length field, followed by length 8-bit chars.
		 */
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		nsResult WriteStringZ([MarshalAs(UnmanagedType.LPStr)] String aString);

		/**
		 * Write a 16-bit pascal style string to the stream.
		 * 32-bit length field, followed by length PRUnichars.
		 */
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		nsResult WriteWStringZ([MarshalAs(UnmanagedType.LPWStr)] String aString);

		/**
		 * Write an 8-bit pascal style string (UTF8-encoded) to the stream.
		 * 32-bit length field, followed by length 8-bit chars.
		 */
		void WriteUtf8Z(IntPtr aString);

		/**
		 * Write an opaque byte array to the stream.
		 */
		void WriteBytes([MarshalAs(UnmanagedType.LPStr, SizeParamIndex = 1)] String aString, UInt32 aLength);

		/**
		 * Write an opaque byte array to the stream.
		 */
		void WriteByteArray([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] Byte[] aBytes, UInt32 aLength);
	}
}
