using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser : nsIInterfaceRequestor
	{
		nsResult nsIInterfaceRequestor.GetInterface(ref Guid uuid, out IntPtr result)
		{
			Trace.TraceInformation("nsIInterfaceRequestor.GetInterface: {0}", uuid);

			Object obj = this;

			if (m_WebBrowser != null)
			{
				if (uuid == typeof(nsIDOMWindow).GUID)
				{
					obj = m_WebBrowser.ContentDOMWindow;
				}
				else if (uuid == typeof(nsIDOMDocument).GUID)
				{
					obj = m_WebBrowser.ContentDOMWindow.Document;
				}
			}

			IntPtr pUnk = Marshal.GetIUnknownForObject(obj);
			try
			{
				IntPtr ppv;
				Marshal.QueryInterface(pUnk, ref uuid, out ppv);

				result = ppv;
				return result != IntPtr.Zero ? nsResult.NS_OK : nsResult.NS_NOINTERFACE;
			}
			finally
			{
				Marshal.Release(pUnk);
			}
		}
	}
}
