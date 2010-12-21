using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIBlocklistServiceConstants
	{
		// Indicates that the item does not appear in the blocklist.
		public const UInt32 STATE_NOT_BLOCKED = 0;
		// Indicates that the item is in the blocklist but the problem is not severe
		// enough to warant forcibly blocking.
		public const UInt32 STATE_SOFTBLOCKED = 1;
		// Indicates that the item should be blocked and never used.
		public const UInt32 STATE_BLOCKED = 2;
		// Indicates that the item is considered outdated, and there is a known
		// update available.
		public const UInt32 STATE_OUTDATED = 3;
	}

	[ComImport, Guid("8439f9c0-da03-4260-8b21-dc635eed28fb"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIBlocklistService //: nsISupports
	{
		/**
		 * Determine if an item is blocklisted
		 * @param   id
		 *          The ID of the item.
		 * @param   version
		 *          The item's version.
		 * @param   appVersion
		 *          The version of the application we are checking in the blocklist.
		 *          If this parameter is null, the version of the running application
		 *          is used.
		 * @param   toolkitVersion
		 *          The version of the toolkit we are checking in the blocklist.
		 *          If this parameter is null, the version of the running toolkit
		 *          is used.
		 * @returns true if the item is compatible with this version of the
		 *          application or this version of the toolkit, false, otherwise.
		 */
		Boolean IsAddonBlocklisted([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String id,
								   [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String version,
								   [Optional] [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String appVersion,
								   [Optional] [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String toolkitVersion);

		/**
		 * Determine the blocklist state of an add-on
		 * @param   id
		 *          The ID of the item.
		 * @param   version
		 *          The item's version.
		 * @param   appVersion
		 *          The version of the application we are checking in the blocklist.
		 *          If this parameter is null, the version of the running application
		 *          is used.
		 * @param   toolkitVersion
		 *          The version of the toolkit we are checking in the blocklist.
		 *          If this parameter is null, the version of the running toolkit
		 *          is used.
		 * @returns The STATE constant.
		 */
		UInt32 GetAddonBlocklistState([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String id,
									  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String version,
									  [Optional] [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String appVersion,
									  [Optional] [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String toolkitVersion);

		/**
		 * Determine the blocklist state of a plugin
		 * @param   plugin
		 *          The plugin to get the state for
		 * @param   appVersion
		 *          The version of the application we are checking in the blocklist.
		 *          If this parameter is null, the version of the running application
		 *          is used.
		 * @param   toolkitVersion
		 *          The version of the toolkit we are checking in the blocklist.
		 *          If this parameter is null, the version of the running toolkit
		 *          is used.
		 * @returns The STATE constant.
		 */
		UInt32 GetPluginBlocklistState(nsIPluginTag plugin,
									   [Optional] [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String appVersion,
									   [Optional] [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String toolkitVersion);
	}
}
