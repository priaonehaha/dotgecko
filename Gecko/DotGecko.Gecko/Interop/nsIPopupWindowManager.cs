using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	internal static class nsIPopupWindowManagerConstants
	{
		/**
		 * These values are returned by the testPermission method
		 */
		internal const UInt32 ALLOW_POPUP = 1;
		internal const UInt32 DENY_POPUP = 2;
		internal const UInt32 ALLOW_POPUP_WITH_PREJUDICE = 3;
	}

	/**
	 * This is the interface to the Popup Window Manager: an object which
	 * maintains popup window permissions by website.
	 */
	[ComImport, Guid("3210a6aa-b464-4f57-9335-b22815567cf1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIPopupWindowManager //: nsISupports
	{
		/**
		 * Test whether a website has permission to show a popup window.
		 * @param   uri is the URI to be tested
		 * @return  one of the enumerated permission actions defined above
		 */
		UInt32 TestPermission(nsIURI uri);
	}
}
