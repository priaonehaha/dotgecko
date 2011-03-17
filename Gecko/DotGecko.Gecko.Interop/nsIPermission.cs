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
	[ComImport, Guid("5036f0f6-f77b-4168-9d57-a1c0dd66cf02"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIPermission //: nsISupports
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

		/**
		 * The expiration type of the permission (session, time-based or none).
		 * Constants are EXPIRE_*, defined in nsIPermissionManager.
		 * @see nsIPermissionManager
		 */
		UInt32 ExpireType { get; }

		/**
		 * The expiration time of the permission (milliseconds since Jan 1 1970
		 * 0:00:00).
		 */
		Int64 ExpireTime { get; }
	}
}
