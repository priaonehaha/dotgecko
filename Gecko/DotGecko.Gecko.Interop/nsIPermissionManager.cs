using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIPermissionManagerConstants
	{
		/**
		   * Predefined return values for the testPermission method and for
		   * the permission param of the add method
		   * NOTE: UNKNOWN_ACTION (0) is reserved to represent the
		   * default permission when no entry is found for a host, and
		   * should not be used by consumers to indicate otherwise.
		   */
		public const UInt32 UNKNOWN_ACTION = 0;
		public const UInt32 ALLOW_ACTION = 1;
		public const UInt32 DENY_ACTION = 2;

		/**
		 * Predefined expiration types for permissions.  Permissions can be permanent
		 * (never expire), expire at the end of the session, or expire at a specified
		 * time.
		 */
		public const UInt32 EXPIRE_NEVER = 0;
		public const UInt32 EXPIRE_SESSION = 1;
		public const UInt32 EXPIRE_TIME = 2;
	}

	/**
	 * This file contains an interface to the Permission Manager,
	 * used to persistenly store permissions for different object types (cookies, 
	 * images etc) on a site-by-site basis.
	 *
	 * This service broadcasts the following notification when the permission list
	 * is changed:
	 *
	 * topic  : "perm-changed" (PERM_CHANGE_NOTIFICATION)
	 *          broadcast whenever the permission list changes in some way. there
	 *          are four possible data strings for this notification; one
	 *          notification will be broadcast for each change, and will involve
	 *          a single permission.
	 * subject: an nsIPermission interface pointer representing the permission object
	 *          that changed.
	 * data   : "deleted"
	 *          a permission was deleted. the subject is the deleted permission.
	 *          "added"
	 *          a permission was added. the subject is the added permission.
	 *          "changed"
	 *          a permission was changed. the subject is the new permission.
	 *          "cleared"
	 *          the entire permission list was cleared. the subject is null.
	 */
	[ComImport, Guid("0b83f9d5-3f96-41b6-91aa-ff3a7e4880d7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIPermissionManager //: nsISupports
	{
		/**
		 * Add permission information for a given URI and permission type. This
		 * operation will cause the type string to be registered if it does not
		 * currently exist. If a permission already exists for a given type, it
		 * will be modified.
		 *
		 * @param uri         the uri to add the permission for
		 * @param type        a case-sensitive ASCII string, identifying the consumer.
		 *                    Consumers should choose this string to be unique, with
		 *                    respect to other consumers.
		 * @param permission  an integer representing the desired action (e.g. allow
		 *                    or deny). The interpretation of this number is up to the
		 *                    consumer, and may represent different actions for different
		 *                    types. Consumers may use one of the enumerated permission
		 *                    actions defined above, for convenience.
		 *                    NOTE: UNKNOWN_ACTION (0) is reserved to represent the
		 *                    default permission when no entry is found for a host, and
		 *                    should not be used by consumers to indicate otherwise.
		 * @param expiretype  a constant defining whether this permission should
		 *                    never expire (EXPIRE_NEVER), expire at the end of the
		 *                    session (EXPIRE_SESSION), or expire at a specified time
		 *                    (EXPIRE_TIME).
		 * @param expiretime  an integer representation of when this permission
		 *                    should be forgotten (milliseconds since Jan 1 1970 0:00:00). 
		 */
		void Add(nsIURI uri, [MarshalAs(UnmanagedType.LPStr)] String type, UInt32 permission, [Optional] UInt32 expireType, [Optional] Int64 expireTime);

		/**
		 * Remove permission information for a given host string and permission type.
		 * The host string represents the exact entry in the permission list (such as
		 * obtained from the enumerator), not a URI which that permission might apply
		 * to.
		 *
		 * @param host   the host to remove the permission for
		 * @param type   a case-sensitive ASCII string, identifying the consumer. 
		 *               The type must have been previously registered using the
		 *               add() method.
		 */
		void Remove([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String host, [MarshalAs(UnmanagedType.LPStr)] String type);

		/**
		 * Clear permission information for all websites.
		 */
		void RemoveAll();

		/**
		 * Test whether a website has permission to perform the given action.
		 * @param uri     the uri to be tested
		 * @param type    a case-sensitive ASCII string, identifying the consumer
		 * @param return  see add(), param permission. returns UNKNOWN_ACTION when
		 *                there is no stored permission for this uri and / or type.
		 */
		UInt32 TestPermission(nsIURI uri, [MarshalAs(UnmanagedType.LPStr)] String type);

		/**
		 * Test whether a website has permission to perform the given action.
		 * This requires an exact hostname match, subdomains are not a match.
		 * @param uri     the uri to be tested
		 * @param type    a case-sensitive ASCII string, identifying the consumer
		 * @param return  see add(), param permission. returns UNKNOWN_ACTION when
		 *                there is no stored permission for this uri and / or type.
		 */
		UInt32 TestExactPermission(nsIURI uri, [MarshalAs(UnmanagedType.LPStr)] String type);

		/**
		 * Allows enumeration of all stored permissions
		 * @return an nsISimpleEnumerator interface that allows access to
		 *         nsIPermission objects
		 */
		nsISimpleEnumerator Enumerator { get; }
	}
}
