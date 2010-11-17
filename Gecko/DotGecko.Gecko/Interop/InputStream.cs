using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	internal sealed class InputStream : nsIInputStream
	{
		public InputStream(Stream baseStream, UInt32? maxBufferSize = null)
		{
			m_BaseStream = baseStream;
			m_MaxBufferSize = maxBufferSize;
		}

		#region Implementation of nsIInputStream

		void nsIInputStream.Close()
		{
			m_BaseStream = null;
		}

		UInt32 nsIInputStream.Available()
		{
			if (ReferenceEquals(m_BaseStream, null))
			{
				return 0;
			}

			Int64 available = m_BaseStream.Length - m_BaseStream.Position;
			return (UInt32)available;
		}

		UInt32 nsIInputStream.Read(IntPtr aBuf, UInt32 aCount)
		{
			if (ReferenceEquals(m_BaseStream, null) || !m_BaseStream.CanRead)
			{
				return 0;
			}

			aCount = GetBufferSize(aCount);
			var buffer = new Byte[aCount];
			Int32 bytesRead = m_BaseStream.Read(buffer, 0, buffer.Length);
			if (bytesRead > 0)
			{
				Marshal.Copy(buffer, 0, aBuf, bytesRead);
			}
			return (UInt32)bytesRead;
		}

		UInt32 nsIInputStream.ReadSegments(nsWriteSegmentFun aWriter, IntPtr aClosure, UInt32 aCount)
		{
			if (ReferenceEquals(m_BaseStream, null) || !m_BaseStream.CanRead)
			{
				return 0;
			}

			// Get maximum buffer size - the segment size.
			UInt32 segmentSize = GetBufferSize(aCount);
			var segment = new Byte[segmentSize];
			// Read data available for first segment
			Int32 segmentBytesCount = m_BaseStream.Read(segment, 0, segment.Length);
			if (segmentBytesCount > 0)
			{
				// Allocate memory for first and all subsequent segments
				IntPtr segmentPtr = Xpcom.NS_Alloc(segmentSize);
				try
				{
					UInt32 totalBytesConsumed = 0;
					do
					{
						// Write available data to current segment
						Marshal.Copy(segment, 0, segmentPtr, segmentBytesCount);
						UInt32 segmentBytesConsumed = 0;
						UInt32 writeCount = 0;
						// Deliver data from current segment to consumer
						while ((segmentBytesConsumed < segmentBytesCount) && (aWriter(this, aClosure, segmentPtr, totalBytesConsumed, (UInt32)segmentBytesCount, ref writeCount) == nsResult.NS_OK))
						{
							segmentBytesConsumed += writeCount;
							totalBytesConsumed += writeCount;
							writeCount = 0;
						}

						// Get data for next segment
						if (totalBytesConsumed < aCount)
						{
							segmentBytesCount = m_BaseStream.Read(segment, 0, segment.Length);
						}
					}
					while ((totalBytesConsumed < aCount) && (segmentBytesCount > 0));

					return totalBytesConsumed;
				}
				finally
				{
					Xpcom.NS_Free(segmentPtr);
				}
			}

			return 0;
		}

		Boolean nsIInputStream.IsNonBlocking()
		{
			return false;
		}

		#endregion

		private UInt32 GetBufferSize(UInt32 bufferSize)
		{
			const UInt32 defaultMaxBufferSize = 0x1000;
			return Math.Min(bufferSize, m_MaxBufferSize ?? defaultMaxBufferSize);
		}

		private Stream m_BaseStream;
		private readonly UInt32? m_MaxBufferSize;
	}
}
