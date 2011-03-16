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

	/**
	 * nsIBlocklistPrompt is used, if available, by the default implementation of 
	 * nsIBlocklistService to display a confirmation UI to the user before blocking
	 * extensions/plugins.
	 */
	[ComImport, Guid("36f97f40-b0c9-11df-94e2-0800200c9a66"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIBlocklistPrompt //: nsISupports
	{
		/**
		 * Prompt the user about newly blocked addons. The prompt is then resposible
		 * for soft-blocking any addons that need to be afterwards
		 *
		 * @param  aAddons
		 *         An array of addons and plugins that are blocked. These are javascript
		 *         objects with properties:
		 *          name    - the plugin or extension name,
		 *          version - the version of the extension or plugin,
		 *          icon    - the plugin or extension icon,
		 *          disable - can be used by the nsIBlocklistPrompt to allows users to decide
		 *                    whether a soft-blocked add-on should be disabled,
		 *          blocked - true if the item is hard-blocked, false otherwise,
		 *          item    - the nsIPluginTag or Addon object
		 * @param  aCount
		 *         The number of addons
		 */
		void Prompt([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 1)] nsIVariant[] aAddons, [Optional] UInt32 aCount);
	}
}
