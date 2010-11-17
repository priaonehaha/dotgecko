using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/**
	 * This interface defines a "permission" object,
	 * used to specify allowed/blocked objects from
	 * user-specified sites (cookies, images etc).
	 */
	[ComImport, Guid("28F16D80-157B-11d5-A542-0010A401EB10"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIPermission //: nsISupports
	{
		/**
		 * The name of the host for which the permission is set
		 */
		void GetHost([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * a case-sensitive ASCII string, indicating the type of permission
		 * (e.g., "cookie", "image", etc).
		 * This string is specified by the consumer when adding a permission 
		 * via nsIPermissionManager.
		 * @see nsIPermissionManager
		 */
		void GetType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);

		/**
		 * The permission (see nsIPermissionManager.idl for allowed values)
		 */
		UInt32 Capability { get; }
	}
}
