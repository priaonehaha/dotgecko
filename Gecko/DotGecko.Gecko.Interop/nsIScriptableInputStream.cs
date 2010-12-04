using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIScriptableInputStream provides scriptable access to an nsIInputStream
	 * instance.
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("a2a32f90-9b90-11d3-a189-0050041caf44"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
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
	}
}
