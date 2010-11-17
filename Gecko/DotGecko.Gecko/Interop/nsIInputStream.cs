using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The signature of the writer function passed to ReadSegments. This
	 * is the "consumer" of data that gets read from the stream's buffer.
	 *
	 * @param aInStream stream being read
	 * @param aClosure opaque parameter passed to ReadSegments
	 * @param aFromSegment pointer to memory owned by the input stream.  This is
	 *                     where the writer function should start consuming data.
	 * @param aToOffset amount of data already consumed by this writer during this
	 *                  ReadSegments call.  This is also the sum of the aWriteCount
	 *                  returns from this writer over the previous invocations of
	 *                  the writer by this ReadSegments call.
	 * @param aCount Number of bytes available to be read starting at aFromSegment
	 * @param [out] aWriteCount number of bytes read by this writer function call
	 *
	 * Implementers should return the following:
	 *
	 * @return NS_OK and (*aWriteCount > 0) if consumed some data
	 * @return <any-error> if not interested in consuming any data
	 *
	 * Errors are never passed to the caller of ReadSegments.
	 *
	 * NOTE: returning NS_OK and (*aWriteCount = 0) has undefined behavior.
	 *
	 * @status FROZEN
	 */
	[UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = false)]
	[return: MarshalAs(UnmanagedType.U4)]
	internal delegate nsResult nsWriteSegmentFun(nsIInputStream aInStream, IntPtr aClosure, IntPtr aFromSegment, UInt32 aToOffset, UInt32 aCount, ref UInt32 aWriteCount);

	/**
	 * nsIInputStream
	 *
	 * An interface describing a readable stream of data.  An input stream may be
	 * "blocking" or "non-blocking" (see the IsNonBlocking method).  A blocking
	 * input stream may suspend the calling thread in order to satisfy a call to
	 * Close, Available, Read, or ReadSegments.  A non-blocking input stream, on
	 * the other hand, must not block the calling thread of execution.
	 *
	 * NOTE: blocking input streams are often read on a background thread to avoid
	 * locking up the main application thread.  For this reason, it is generally
	 * the case that a blocking input stream should be implemented using thread-
	 * safe AddRef and Release.
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("fa9c7f6c-61b3-11d4-9877-00c04fa0cf4a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIInputStream //: nsISupports
	{
		/** 
		 * Close the stream.  This method causes subsequent calls to Read and
		 * ReadSegments to return 0 bytes read to indicate end-of-file.  Any
		 * subsequent calls to Available should throw NS_BASE_STREAM_CLOSED.
		 */
		void Close();

		/**
		 * Determine number of bytes available in the stream.  A non-blocking
		 * stream that does not yet have any data to read should return 0 bytes
		 * from this method (i.e., it must not throw the NS_BASE_STREAM_WOULD_BLOCK
		 * exception).
		 * 
		 * In addition to the number of bytes available in the stream, this method
		 * also informs the caller of the current status of the stream.  A stream
		 * that is closed will throw an exception when this method is called.  That
		 * enables the caller to know the condition of the stream before attempting
		 * to read from it.  If a stream is at end-of-file, but not closed, then
		 * this method should return 0 bytes available.
		 *
		 * @return number of bytes currently available in the stream, or
		 *   PR_UINT32_MAX if the size of the stream exceeds PR_UINT32_MAX.
		 *
		 * @throws NS_BASE_STREAM_CLOSED if the stream is closed normally or at
		 *   end-of-file
		 * @throws <other-error> if the stream is closed due to some error
		 *   condition
		 */
		UInt32 Available();

		/** 
		 * Read data from the stream.
		 *
		 * @param aBuf the buffer into which the data is to be read
		 * @param aCount the maximum number of bytes to be read
		 *
		 * @return number of bytes read (may be less than aCount).
		 * @return 0 if reached end-of-file
		 *
		 * @throws NS_BASE_STREAM_WOULD_BLOCK if reading from the input stream would
		 *   block the calling thread (non-blocking mode only)
		 * @throws <other-error> on failure
		 *
		 * NOTE: this method should not throw NS_BASE_STREAM_CLOSED.
		 */
		UInt32 Read(IntPtr aBuf, UInt32 aCount);

		/**
		 * Low-level read method that provides access to the stream's underlying
		 * buffer.  The writer function may be called multiple times for segmented
		 * buffers.  ReadSegments is expected to keep calling the writer until
		 * either there is nothing left to read or the writer returns an error.
		 * ReadSegments should not call the writer with zero bytes to consume.
		 *
		 * @param aWriter the "consumer" of the data to be read
		 * @param aClosure opaque parameter passed to writer 
		 * @param aCount the maximum number of bytes to be read
		 *
		 * @return number of bytes read (may be less than aCount)
		 * @return 0 if reached end-of-file (or if aWriter refused to consume data)
		 *
		 * @throws NS_BASE_STREAM_WOULD_BLOCK if reading from the input stream would
		 *   block the calling thread (non-blocking mode only)
		 * @throws NS_ERROR_NOT_IMPLEMENTED if the stream has no underlying buffer
		 * @throws <other-error> on failure
		 *
		 * NOTE: this function may be unimplemented if a stream has no underlying
		 * buffer (e.g., socket input stream).
		 *
		 * NOTE: this method should not throw NS_BASE_STREAM_CLOSED.
		 */
		UInt32 ReadSegments([MarshalAs(UnmanagedType.FunctionPtr)] nsWriteSegmentFun aWriter, IntPtr aClosure, UInt32 aCount);

		/**
		 * @return true if stream is non-blocking
		 *
		 * NOTE: reading from a blocking input stream will block the calling thread
		 * until at least one byte of data can be extracted from the stream.
		 *
		 * NOTE: a non-blocking input stream may implement nsIAsyncInputStream to
		 * provide consumers with a way to wait for the stream to have more data
		 * once its read method is unable to return any data without blocking.
		 */
		Boolean IsNonBlocking();
	}
}
