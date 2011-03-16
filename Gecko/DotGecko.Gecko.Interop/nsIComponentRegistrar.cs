using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIComponentRegistrar interface.
	 */
	[ComImport, Guid("2417cbfe-65ad-48a6-b4b6-eb84db174392"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIComponentRegistrar //: nsISupports
	{
		/**
		 * autoRegister
		 *
		 * Register a .manifest file, or an entire directory containing
		 * these files. Registration lasts for this run only, and is not cached.
		 *
		 * @note Formerly this method would register component files directly. This
		 *       is no longer supported.
		 */
		void AutoRegister(nsIFile aSpec);

		/**
		 * autoUnregister
		 * @status OBSOLETE: This method is no longer implemented, but preserved
		 *                   in this interface for binary compatibility with
		 *                   Mozilla 1.9.2.
		 */
		[Obsolete]
		void AutoUnregister(nsIFile aSpec);

		/**
		 * registerFactory
		 *
		 * Register a factory with a given ContractID, CID and Class Name.
		 *
		 * @param aClass      : CID of object
		 * @param aClassName  : Class Name of CID (unused)
		 * @param aContractID : ContractID associated with CID aClass. May be null
		 *                      if no contract ID is needed.
		 * @param aFactory    : Factory that will be registered for CID aClass.
		 *                      If aFactory is null, the contract will be associated
		 *                      with a previously registered CID.
		 */
		void RegisterFactory([In] ref Guid aClass, [MarshalAs(UnmanagedType.LPStr)] String aClassName, [MarshalAs(UnmanagedType.LPStr)] String aContractID, nsIFactory aFactory);

		/**
		 * unregisterFactory
		 *
		 * Unregister a factory associated with CID aClass.
		 *
		 * @param aClass   : CID being unregistered
		 * @param aFactory : Factory previously registered to create instances of
		 *                   CID aClass.
		 *
		 * @return NS_OK     Unregistration was successful.
		 *         NS_ERROR* Method failure.
		 */
		void UnregisterFactory([In] ref Guid aClass, nsIFactory aFactory);

		/**
		 * registerFactoryLocation
		 * @status OBSOLETE: This method is no longer implemented, but preserved
		 *                   in this interface for binary compatibility with
		 *                   Mozilla 1.9.2.
		 */
		[Obsolete]
		void RegisterFactoryLocation([In] ref Guid aClass, [MarshalAs(UnmanagedType.LPStr)] String aClassName, [MarshalAs(UnmanagedType.LPStr)] String aContractID, nsIFile aFile, [MarshalAs(UnmanagedType.LPStr)] String aLoaderStr, [MarshalAs(UnmanagedType.LPStr)] String aType);

		/**
		 * unregisterFactoryLocation
		 * @status OBSOLETE: This method is no longer implemented, but preserved
		 *                   in this interface for binary compatibility with
		 *                   Mozilla 1.9.2.
		 */
		[Obsolete]
		void UnregisterFactoryLocation([In] ref Guid aClass, nsIFile aFile);

		/**
		 * isCIDRegistered
		 *
		 * Returns true if a factory is registered for the CID.
		 *
		 * @param aClass : CID queried for registeration
		 * @return       : true if a factory is registered for CID 
		 *                 false otherwise.
		 */
		Boolean IsCIDRegistered([In] ref Guid aClass);

		/**
		 * isContractIDRegistered
		 *
		 * Returns true if a factory is registered for the contract id.
		 *
		 * @param aClass : contract id queried for registeration
		 * @return       : true if a factory is registered for contract id 
		 *                 false otherwise.
		 */
		Boolean IsContractIDRegistered([MarshalAs(UnmanagedType.LPStr)] String aContractID);

		/**
		 * enumerateCIDs
		 *
		 * Enumerate the list of all registered CIDs.
		 *
		 * @return : enumerator for CIDs.  Elements of the enumeration can be QI'ed
		 *           for the nsISupportsID interface.  From the nsISupportsID, you 
		 *           can obtain the actual CID.
		 */
		nsISimpleEnumerator EnumerateCIDs();

		/**
		 * enumerateContractIDs
		 *
		 * Enumerate the list of all registered ContractIDs.
		 *
		 * @return : enumerator for ContractIDs. Elements of the enumeration can be 
		 *           QI'ed for the nsISupportsCString interface.  From  the
		 *           nsISupportsCString interface, you can obtain the actual 
		 *           Contract ID string.
		 */
		nsISimpleEnumerator EnumerateContractIDs();

		/**
		 * CIDToContractID
		 * @status OBSOLETE: This method is no longer implemented, but preserved
		 *                   in this interface for binary compatibility with
		 *                   Mozilla 1.9.2.
		 */
		[Obsolete]
		[return: MarshalAs(UnmanagedType.LPStr)]
		String CIDToContractID([In] ref Guid aClass);

		/**
		 * contractIDToCID
		 *
		 * Returns the CID for a given Contract ID, if one exists and is registered.
		 *
		 * @return : Contract ID.
		 */
		[return: MarshalAs(UnmanagedType.LPStruct)]
		Guid ContractIDToCID([MarshalAs(UnmanagedType.LPStr)] String aContractID);
	}
}
