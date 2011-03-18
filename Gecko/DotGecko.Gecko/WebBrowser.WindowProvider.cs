using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;
using AString = System.IntPtr;
using AUTF8String = System.IntPtr;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser : nsIWindowProvider
	{
		public event EventHandler<ProvideWindowEventArgs> ProvideWindow
		{
			add { Events.Add(EventKey.ProvideWindow, value); }
			remove { Events.Remove(EventKey.ProvideWindow, value); }
		}

		nsIDOMWindow nsIWindowProvider.ProvideWindow(nsIDOMWindow aParent, UInt32 aChromeFlags, Boolean aCalledFromJS, Boolean aPositionSpecified, Boolean aSizeSpecified, nsIURI aURI, AString aName, AUTF8String aFeatures, out Boolean aWindowIsNew)
		{
			Trace.TraceInformation("nsIWindowProvider.ProvideWindow");

			var e = new ProvideWindowEventArgs((ChromeFlags)aChromeFlags, aCalledFromJS, aPositionSpecified, aSizeSpecified, aURI.ToUri(), aName.GetString(), aFeatures.GetUTF8String());
			Events.Raise(EventKey.ProvideWindow, e);
			aWindowIsNew = e.WindowIsNew;
			return e.Window != null ? e.Window.m_WebBrowser.ContentDOMWindow : null;
		}
	}
}
