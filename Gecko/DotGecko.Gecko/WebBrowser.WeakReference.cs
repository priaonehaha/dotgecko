using System;
using System.Runtime.InteropServices;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser : nsISupportsWeakReference, nsIWeakReference
	{
		nsIWeakReference nsISupportsWeakReference.GetWeakReference()
		{
			return this;
		}

		IntPtr nsIWeakReference.QueryReferent(ref Guid uuid)
		{
			IntPtr pUnk = Marshal.GetIUnknownForObject(this);

			IntPtr ppv;
			Marshal.QueryInterface(pUnk, ref uuid, out ppv);

			Marshal.Release(pUnk);

			if (ppv != IntPtr.Zero)
			{
				Marshal.Release(ppv);
			}

			return ppv;
		}
	}
}
