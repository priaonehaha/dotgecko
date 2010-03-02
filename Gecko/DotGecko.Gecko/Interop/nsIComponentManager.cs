using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIComponentManager interface.
	 * @status FROZEN
	 */
	[ComImport]
	[Guid("a88e5a60-205a-4bb1-94e1-2628daf51eae")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIComponentManager //: nsISupports
	{
		/**
		 * getClassObject
		 *
		 * Returns the factory object that can be used to create instances of
		 * CID aClass
		 *
		 * @param aClass The classid of the factory that is being requested
		 */
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 1)]
		Object GetClassObject(ref Guid aClass, ref Guid aIID);

		/**
		 * getClassObjectByContractID
		 *
		 * Returns the factory object that can be used to create instances of
		 * CID aClass
		 *
		 * @param aClass The classid of the factory that is being requested
		 */
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 1)]
		Object GetClassObjectByContractID([MarshalAs(UnmanagedType.LPStr)] String aContractID, ref Guid aIID);

		/**
		 * createInstance
		 *
		 * Create an instance of the CID aClass and return the interface aIID.
		 *
		 * @param aClass : ClassID of object instance requested
		 * @param aDelegate : Used for aggregation
		 * @param aIID : IID of interface requested
		 */
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 2)]
		Object CreateInstance(ref Guid aClass, [MarshalAs(UnmanagedType.IUnknown)] Object aDelegate, ref Guid aIID);

		/**
		 * createInstanceByContractID
		 *
		 * Create an instance of the CID that implements aContractID and return the
		 * interface aIID. 
		 *
		 * @param aContractID : aContractID of object instance requested
		 * @param aDelegate : Used for aggregation
		 * @param aIID : IID of interface requested
		 */
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 2)]
		Object CreateInstanceByContractID([MarshalAs(UnmanagedType.LPStr)] String aContractID, [MarshalAs(UnmanagedType.IUnknown)] Object aDelegate, ref Guid aIID);
	}
}
