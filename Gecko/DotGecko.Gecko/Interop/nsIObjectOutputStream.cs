using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * @See nsIObjectInputStream
	 * @See nsIBinaryOutputStream
	 */
	[ComImport, Guid("92c898ac-5fde-4b99-87b3-5d486422094b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIObjectOutputStream : nsIBinaryOutputStream
	{
		#region nsIOutputStream Members

		new void Close();
		new void Flush();
		new UInt32 Write([MarshalAs(UnmanagedType.LPStr)] String aBuf, UInt32 aCount);
		new UInt32 WriteFrom(nsIInputStream aFromStream, UInt32 aCount);
		new UInt32 WriteSegments([MarshalAs(UnmanagedType.FunctionPtr)] nsReadSegmentFun aReader, IntPtr aClosure, UInt32 aCount);
		new Boolean IsNonBlocking();

		#endregion

		#region nsIBinaryOutputStream Members

		new void SetOutputStream(nsIOutputStream aOutputStream);
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		new nsResult WriteBoolean(Boolean aBoolean);
		new void Write8(Byte aByte);
		new void Write16(UInt16 a16);
		new void Write32(UInt32 a32);
		new void Write64(UInt64 a64);
		new void WriteFloat(Single aFloat);
		new void WriteDouble(Double aDouble);
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		new nsResult WriteStringZ([MarshalAs(UnmanagedType.LPStr)] String aString);
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		new nsResult WriteWStringZ([MarshalAs(UnmanagedType.LPWStr)] String aString);
		new void WriteUtf8Z(IntPtr aString);
		new void WriteBytes([MarshalAs(UnmanagedType.LPStr, SizeParamIndex = 1)] String aString, UInt32 aLength);
		new void WriteByteArray([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] Byte[] aBytes, UInt32 aLength);

		#endregion

		/**
		 * Write the object whose "root" or XPCOM-identity nsISupports is aObject.
		 * The cause for writing this object is a strong or weak reference, so the
		 * aIsStrongRef argument must tell which kind of pointer is being followed
		 * here during serialization.
		 *
		 * If the object has only one strong reference in the serialization and no
		 * weak refs, use writeSingleRefObject.  This is a valuable optimization:
		 * it saves space in the stream, and cycles on both ends of the process.
		 *
		 * If the reference being serialized is a pointer to an interface not on
		 * the primary inheritance chain ending in the root nsISupports, you must
		 * call writeCompoundObject instead of this method.
		 */
		void WriteObject([MarshalAs(UnmanagedType.IUnknown)] nsISupports aObject, Boolean aIsStrongRef);

		/**
		 * Write an object referenced singly and strongly via its root nsISupports
		 * or a subclass of its root nsISupports.  There must not be other refs to
		 * aObject in memory, or in the serialization.
		 */
		void WriteSingleRefObject([MarshalAs(UnmanagedType.IUnknown)] nsISupports aObject);

		/**
		 * Write the object referenced by an interface pointer at aObject that
		 * inherits from a non-primary nsISupports, i.e., a reference to one of
		 * the multiply inherited interfaces derived from an nsISupports other
		 * than the root or XPCOM-identity nsISupports; or a reference to an
		 * inner object in the case of true XPCOM aggregation.  aIID identifies
		 * this interface.
		 */
		void WriteCompoundObject([MarshalAs(UnmanagedType.IUnknown)] nsISupports aObject, ref Guid aIID, Boolean aIsStrongRef);

		void WriteID(ref Guid aID);

		/**
		 * Optimized serialization support -- see nsIStreamBufferAccess.idl.
		 */
		IntPtr GetBuffer(UInt32 aLength, UInt32 aAlignMask);
		void PutBuffer(IntPtr aBuffer, UInt32 aLength);
	}

	//%{C++

	//inline nsresult
	//NS_WriteOptionalObject(nsIObjectOutputStream* aStream, nsISupports* aObject,
	//                       PRBool aIsStrongRef)
	//{
	//    PRBool nonnull = (aObject != nsnull);
	//    nsresult rv = aStream->WriteBoolean(nonnull);
	//    if (NS_SUCCEEDED(rv) && nonnull)
	//        rv = aStream->WriteObject(aObject, aIsStrongRef);
	//    return rv;
	//}

	//inline nsresult
	//NS_WriteOptionalSingleRefObject(nsIObjectOutputStream* aStream,
	//                                nsISupports* aObject)
	//{
	//    PRBool nonnull = (aObject != nsnull);
	//    nsresult rv = aStream->WriteBoolean(nonnull);
	//    if (NS_SUCCEEDED(rv) && nonnull)
	//        rv = aStream->WriteSingleRefObject(aObject);
	//    return rv;
	//}

	//inline nsresult
	//NS_WriteOptionalCompoundObject(nsIObjectOutputStream* aStream,
	//                               nsISupports* aObject,
	//                               const nsIID& aIID,
	//                               PRBool aIsStrongRef)
	//{
	//    PRBool nonnull = (aObject != nsnull);
	//    nsresult rv = aStream->WriteBoolean(nonnull);
	//    if (NS_SUCCEEDED(rv) && nonnull)
	//        rv = aStream->WriteCompoundObject(aObject, aIID, aIsStrongRef);
	//    return rv;
	//}

	//%}
}
