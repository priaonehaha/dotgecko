using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIModule interface.
	 */
	[ComImport, Guid("7392D032-5371-11d3-994E-00805FD26FEE"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIModule //: nsISupports
	{
		/** 
		 * Object Instance Creation
		 *
		 * Obtains a Class Object from a nsIModule for a given CID and IID pair.  
		 * This class object can either be query to a nsIFactory or a may be 
		 * query to a nsIClassInfo.
		 *
		 * @param aCompMgr  : The global component manager
		 * @param aClass    : ClassID of object instance requested
		 * @param aIID      : IID of interface requested
		 * 
		 */
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 2)]
		Object GetClassObject(nsIComponentManager aCompMgr, ref Guid aClass, ref Guid aIID);

		/**
		 * One time registration callback
		 *
		 * When the nsIModule is discovered, this method will be
		 * called so that any setup registration can be preformed.
		 *
		 * @param aCompMgr  : The global component manager
		 * @param aLocation : The location of the nsIModule on disk
		 * @param aLoaderStr: Opaque loader specific string
		 * @param aType     : Loader Type being used to load this module 
		 */
		void RegisterSelf(nsIComponentManager aCompMgr, nsIFile aLocation, [MarshalAs(UnmanagedType.LPStr)] String aLoaderStr, [MarshalAs(UnmanagedType.LPStr)] String aType);

		/**
		 * One time unregistration callback
		 *
		 * When the nsIModule is being unregistered, this method will be
		 * called so that any unregistration can be preformed
		 *
		 * @param aCompMgr   : The global component manager
		 * @param aLocation  : The location of the nsIModule on disk
		 * @param aLoaderStr : Opaque loader specific string
		 * 
		 */
		void UnregisterSelf(nsIComponentManager aCompMgr, nsIFile aLocation, [MarshalAs(UnmanagedType.LPStr)] String aLoaderStr);

		/** 
		 * Module load management
		 * 
		 * @param aCompMgr  : The global component manager
		 *
		 * @return indicates to the caller if the module can be unloaded.
		 * 		Returning PR_TRUE isn't a guarantee that the module will be
		 *		unloaded. It constitues only willingness of the module to be
		 *		unloaded.  It is very important to ensure that no outstanding 
		 *       references to the module's code/data exist before returning 
		 *       PR_TRUE. 
		 *		Returning PR_FALSE guaratees that the module won't be unloaded.
		 */
		Boolean CanUnload(nsIComponentManager aCompMgr);
	}
}
