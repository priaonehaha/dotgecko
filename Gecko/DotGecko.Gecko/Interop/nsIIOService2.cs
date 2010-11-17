using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIIOService2 extends nsIIOService with support for automatic
	 * online/offline management.
	 */
	[ComImport, Guid("d44fe6d4-ee35-4789-886a-eb8f0554d04e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIIOService2 : nsIIOService
	{
		#region nsIIOService Members

		new nsIProtocolHandler GetProtocolHandler([MarshalAs(UnmanagedType.LPStr)] String aScheme);
		new UInt32 GetProtocolFlags([MarshalAs(UnmanagedType.LPStr)] String aScheme);
		new nsIURI NewURI([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aSpec, [MarshalAs(UnmanagedType.LPStr)] String aOriginCharset, nsIURI aBaseURI);
		new nsIURI NewFileURI(nsIFile aFile);
		new nsIChannel NewChannelFromURI(nsIURI aURI);
		new nsIChannel NewChannel([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aSpec, [MarshalAs(UnmanagedType.LPStr)] String aOriginCharset, nsIURI aBaseURI);
		new Boolean Offline { get; set; }
		new Boolean AllowPort(Int32 aPort, [MarshalAs(UnmanagedType.LPStr)] String aScheme);
		new void ExtractScheme([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String urlString, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);

		#endregion

		/**
		 * While this is set, IOService will monitor an nsINetworkLinkService
		 * (if available) and set its offline status to "true" whenever
		 * isLinkUp is false.
		 *
		 * Applications that want to control changes to the IOService's offline
		 * status should set this to false, watch for network:link-status-changed
		 * broadcasts, and change nsIIOService::offline as they see fit. Note
		 * that this means during application startup, IOService may be offline
		 * if there is no link, until application code runs and can turn off
		 * this management.
		 */
		Boolean ManageOfflineStatus { get; set; }
	}
}
