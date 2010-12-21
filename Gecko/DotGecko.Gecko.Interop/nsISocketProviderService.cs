using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsISocketProviderService
	 *
	 * Provides a mapping between a socket type and its associated socket provider
	 * instance.  One could also use the service manager directly.
	 */
	[ComImport, Guid("8f8a23d0-5472-11d3-bbc8-0000861d1237"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISocketProviderService //: nsISupports
	{
		nsISocketProvider GetSocketProvider([MarshalAs(UnmanagedType.LPStr)] String socketType);
	}
}
