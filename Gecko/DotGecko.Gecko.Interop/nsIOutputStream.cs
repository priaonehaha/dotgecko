using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The signature for the reader function passed to WriteSegments. This 
	 * is the "provider" of data that gets written into the stream's buffer.
	 *
	 * @param aOutStream stream being written to
	 * @param aClosure opaque parameter passed to WriteSegments
	 * @param aToSegment pointer to memory owned by the output stream
	 * @param aFromOffset amount already written (since WriteSegments was called)
	 * @param aCount length of toSegment
	 * @param aReadCount number of bytes written
	 *
	 * Implementers should return the following:
	 *
	 * @return NS_OK and (*aReadCount > 0) if successfully provided some data
	 * @return NS_OK and (*aReadCount = 0) or
	 * @return <any-error> if not interested in providing any data
	 *
	 * Errors are never passed to the caller of WriteSegments.
	 */
	[return: MarshalAs(UnmanagedType.U4)]
	public delegate nsResult nsReadSegmentFun(nsIOutputStream aOutStream, IntPtr aClosure, IntPtr aToSegment, UInt32 aFromOffset, UInt32 aCount, ref UInt32 aReadCount);

	/**
	 * nsIOutputStream
	 *
	 * An interface describing a writable stream of data.  An output stream may be
	 * "blocking" or "non-blocking" (see the IsNonBlocking method).  A blocking
	 * output stream may suspend the calling thread in order to satisfy a call to
	 * Close, Flush, Write, WriteFrom, or WriteSegments.  A non-blocking output
	 * stream, on the other hand, must not block the calling thread of execution.
	 *
	 * NOTE: blocking output streams are often written to on a background thread to
	 * avoid locking up the main application thread.  For this reason, it is
	 * generally the case that a blocking output stream should be implemented using
	 * thread- safe AddRef and Release.
	 */
	[ComImport, Guid("0d0acd2a-61b4-11d4-9877-00c04fa0cf4a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIOutputStream //: nsISupports
	{
		/** 
		 * Close the stream. Forces the output stream to flush any buffered data.
		 *
		 * @throws NS_BASE_STREAM_WOULD_BLOCK if unable to flush without blocking 
		 *   the calling thread (non-blocking mode only)
		 */
		void Close();

		/**
		 * Flush the stream.
		 *
		 * @throws NS_BASE_STREAM_WOULD_BLOCK if unable to flush without blocking 
		 *   the calling thread (non-blocking mode only)
		 */
		void Flush();

		/**
		 * Write data into the stream.
		 *
		 * @param aBuf the buffer containing the data to be written
		 * @param aCount the maximum number of bytes to be written
		 *
		 * @return number of bytes written (may be less than aCount)
		 *
		 * @throws NS_BASE_STREAM_WOULD_BLOCK if writing to the output stream would
		 *   block the calling thread (non-blocking mode only)
		 * @throws <other-error> on failure
		 */
		UInt32 Write([MarshalAs(UnmanagedType.LPStr)] String aBuf, UInt32 aCount);

		/**
		 * Writes data into the stream from an input stream.
		 *
		 * @param aFromStream the stream containing the data to be written
		 * @param aCount the maximum number of bytes to be written
		 *
		 * @return number of bytes written (may be less than aCount)
		 *
		 * @throws NS_BASE_STREAM_WOULD_BLOCK if writing to the output stream would
		 *    block the calling thread (non-blocking mode only)
		 * @throws <other-error> on failure
		 *
		 * NOTE: This method is defined by this interface in order to allow the
		 * output stream to efficiently copy the data from the input stream into
		 * its internal buffer (if any). If this method was provided as an external
		 * facility, a separate char* buffer would need to be used in order to call
		 * the output stream's other Write method.
		 */
		UInt32 WriteFrom(nsIInputStream aFromStream, UInt32 aCount);

		/**
		 * Low-level write method that has access to the stream's underlying buffer.
		 * The reader function may be called multiple times for segmented buffers.
		 * WriteSegments is expected to keep calling the reader until either there
		 * is nothing left to write or the reader returns an error.  WriteSegments
		 * should not call the reader with zero bytes to provide.
		 *
		 * @param aReader the "provider" of the data to be written
		 * @param aClosure opaque parameter passed to reader
		 * @param aCount the maximum number of bytes to be written
		 *
		 * @return number of bytes written (may be less than aCount)
		 *
		 * @throws NS_BASE_STREAM_WOULD_BLOCK if writing to the output stream would
		 *    block the calling thread (non-blocking mode only)
		 * @throws NS_ERROR_NOT_IMPLEMENTED if the stream has no underlying buffer
		 * @throws <other-error> on failure
		 *
		 * NOTE: this function may be unimplemented if a stream has no underlying
		 * buffer (e.g., socket output stream).
		 */
		UInt32 WriteSegments([MarshalAs(UnmanagedType.FunctionPtr)] nsReadSegmentFun aReader, IntPtr aClosure, UInt32 aCount);

		/**
		 * @return true if stream is non-blocking
		 *
		 * NOTE: writing to a blocking output stream will block the calling thread
		 * until all given data can be consumed by the stream.
		 *
		 * NOTE: a non-blocking output stream may implement nsIAsyncOutputStream to
		 * provide consumers with a way to wait for the stream to accept more data
		 * once its write method is unable to accept any data without blocking.
		 */
		Boolean IsNonBlocking();
	}
}
