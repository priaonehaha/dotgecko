using System;
using System.Runtime.InteropServices;
using System.Text;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * @see nsIObjectOutputStream
	 * @see nsIBinaryInputStream
	 */
	[ComImport, Guid("6c248606-4eae-46fa-9df0-ba58502368eb"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIObjectInputStream : nsIBinaryInputStream
	{
		#region nsIInputStream Members

		new void Close();
		new UInt32 Available();
		new UInt32 Read(IntPtr aBuf, UInt32 aCount);
		new UInt32 ReadSegments(nsWriteSegmentFun aWriter, IntPtr aClosure, UInt32 aCount);
		new Boolean IsNonBlocking();

		#endregion

		#region nsIBinaryInputStream Members

		new void SetInputStream(nsIInputStream aInputStream);
		new Boolean ReadBoolean();
		new Byte Read8();
		new UInt16 Read16();
		new UInt32 Read32();
		new UInt64 Read64();
		new Single ReadFloat();
		new Double ReadDouble();
		new void ReadCString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);
		new void ReadString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		[return: MarshalAs(UnmanagedType.LPStr, SizeParamIndex = 0)]
		new String ReadBytes(UInt32 aLength);
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		new nsResult ReadByteArray(UInt32 aLength, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 0)] out Byte[] aBytes);

		#endregion

		/**
		 * Read an object from this stream to satisfy a strong or weak reference
		 * to one of its interfaces.  If the interface was not along the primary
		 * inheritance chain ending in the "root" or XPCOM-identity nsISupports,
		 * readObject will QueryInterface from the deserialized object root to the
		 * correct interface, which was specified when the object was serialized.
		 *
		 * @see nsIObjectOutputStream
		 */
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports ReadObject(Boolean aIsStrongRef);

		[return: MarshalAs(UnmanagedType.U4)]
		nsResult ReadID(out Guid aID);

		/**
		 * Optimized deserialization support -- see nsIStreamBufferAccess.idl.
		 */
		IntPtr GetBuffer(UInt32 aLength, UInt32 aAlignMask);
		void PutBuffer(IntPtr aBuffer, UInt32 aLength);
	}

	//%{C++

	//inline nsresult
	//NS_ReadOptionalObject(nsIObjectInputStream* aStream, PRBool aIsStrongRef,
	//                      nsISupports* *aResult)
	//{
	//    PRBool nonnull;
	//    nsresult rv = aStream->ReadBoolean(&nonnull);
	//    if (NS_SUCCEEDED(rv)) {
	//        if (nonnull)
	//            rv = aStream->ReadObject(aIsStrongRef, aResult);
	//        else
	//            *aResult = nsnull;
	//    }
	//    return rv;
	//}

	//%}
}
