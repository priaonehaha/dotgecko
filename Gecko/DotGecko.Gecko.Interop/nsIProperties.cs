using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;
using nsQIResult = System.Object;

namespace DotGecko.Gecko.Interop
{
	/*
	 * Simple mapping service interface.
	 */
	[ComImport, Guid("78650582-4e93-4b60-8e85-26ebd3eb14ca"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIProperties //: nsISupports
	{
		/**
		 * Gets a property with a given name. 
		 *
		 * @return NS_ERROR_FAILURE if a property with that name doesn't exist.
		 * @return NS_ERROR_NO_INTERFACE if the found property fails to QI to the 
		 * given iid.
		 */
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		nsResult Get([MarshalAs(UnmanagedType.LPStr)] String prop, [In] ref Guid iid, [MarshalAs(UnmanagedType.Interface, IidParameterIndex = 1)] out nsQIResult result);

		/**
		 * Sets a property with a given name to a given value. 
		 */
		void Set([MarshalAs(UnmanagedType.LPStr)] String prop, [MarshalAs(UnmanagedType.IUnknown)] nsISupports value);

		/**
		 * Returns true if the property with the given name exists.
		 */
		Boolean Has([MarshalAs(UnmanagedType.LPStr)] String prop);

		/**
		 * Undefines a property.
		 * @return NS_ERROR_FAILURE if a property with that name doesn't
		 * already exist.
		 */
		void Undefine([MarshalAs(UnmanagedType.LPStr)] String prop);

		/**
		 *  Returns an array of the keys.
		 */
		[return: MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 0)]
		String[] GetKeys(out UInt32 count);
	}
}
