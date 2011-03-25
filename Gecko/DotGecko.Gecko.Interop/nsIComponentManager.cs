using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIComponentManager interface.
	 */
	[ComImport, Guid("a88e5a60-205a-4bb1-94e1-2628daf51eae"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIComponentManager //: nsISupports
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
		Object GetClassObject([In] ref Guid aClass, [In] ref Guid aIID);

		/**
		 * getClassObjectByContractID
		 *
		 * Returns the factory object that can be used to create instances of
		 * CID aClass
		 *
		 * @param aClass The classid of the factory that is being requested
		 */
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 1)]
		Object GetClassObjectByContractID([MarshalAs(UnmanagedType.LPStr)] String aContractID, [In] ref Guid aIID);

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
		Object CreateInstance([In] ref Guid aClass, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aDelegate, [In] ref Guid aIID);

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
		Object CreateInstanceByContractID([MarshalAs(UnmanagedType.LPStr)] String aContractID, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aDelegate, [In] ref Guid aIID);
	}
}
