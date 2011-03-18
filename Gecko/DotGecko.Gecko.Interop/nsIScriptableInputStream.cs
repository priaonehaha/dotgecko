using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIScriptableInputStream provides scriptable access to an nsIInputStream
	 * instance.
	 */
	[ComImport, Guid("e546afd6-1248-4deb-8940-4b000b618a58"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIScriptableInputStream //: nsISupports
	{
		/** 
		 * Closes the stream. 
		 */
		void Close();

		/**
		 * Wrap the given nsIInputStream with this nsIScriptableInputStream. 
		 *
		 * @param aInputStream parameter providing the stream to wrap 
		 */
		void Init(nsIInputStream aInputStream);

		/**
		 * Return the number of bytes currently available in the stream 
		 *
		 * @return the number of bytes 
		 *
		 * @throws NS_BASE_STREAM_CLOSED if called after the stream has been closed
		 */
		UInt32 Available();

		/**
		 * Read data from the stream.
		 *
		 * WARNING: If the data contains a null byte, then this method will return
		 * a truncated string.
		 *
		 * @param aCount the maximum number of bytes to read 
		 *
		 * @return the data, which will be an empty string if the stream is at EOF.
		 *
		 * @throws NS_BASE_STREAM_CLOSED if called after the stream has been closed
		 * @throws NS_ERROR_NOT_INITIALIZED if init was not called
		 */
		[return: MarshalAs(UnmanagedType.LPStr)]
		String Read(UInt32 aCount);

		/**
		 * Read data from the stream, including NULL bytes.
		 *
		 * @param aCount the maximum number of bytes to read.
		 *
		 * @return the data from the stream, which will be an empty string if EOF
		 *         has been reached.
		 *
		 * @throws NS_BASE_STREAM_WOULD_BLOCK if reading from the input stream
		 *         would block the calling thread (non-blocking mode only).
		 * @throws NS_ERROR_FAILURE if there are not enough bytes available to read
		 *         aCount amount of data.
		 */
		void ReadBytes(UInt32 aCount, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);
	}
}
