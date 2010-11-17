using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser
	{
		public event EventHandler<CreateWindowEventArgs> CreateWindow
		{
			add { Events.Add(EventKey.CreateWindow, value); }
			remove { Events.Remove(EventKey.CreateWindow, value); }
		}

		private sealed class WindowCreator : nsIWindowCreator, nsIWindowCreator2
		{
			nsIWebBrowserChrome nsIWindowCreator.CreateChromeWindow(nsIWebBrowserChrome parent, UInt32 chromeFlags)
			{
				Trace.TraceInformation("nsIWindowCreator.CreateChromeWindow");

				var browser = parent as WebBrowser;
				if (browser == null)
				{
					Trace.TraceWarning("Can't get Browser object");
					return null;
				}

				var e = new CreateWindowEventArgs((ChromeFlags)chromeFlags, null);
				browser.Events.Raise(EventKey.CreateWindow, e);
				return !e.Cancel ? e.Window : null;
			}

			#region Redirect

			nsIWebBrowserChrome nsIWindowCreator2.CreateChromeWindow(nsIWebBrowserChrome parent, UInt32 chromeFlags)
			{
				return ((nsIWindowCreator)this).CreateChromeWindow(parent, chromeFlags);
			}

			#endregion

			nsIWebBrowserChrome nsIWindowCreator2.CreateChromeWindow2(nsIWebBrowserChrome parent, UInt32 chromeFlags, UInt32 contextFlags, nsIURI uri, out Boolean cancel)
			{
				Trace.TraceInformation("nsIWindowCreator2.CreateChromeWindow2");

				var browser = parent as WebBrowser;
				if (browser == null)
				{
					Trace.TraceWarning("Can't get Browser object");

					cancel = true;
					return null;
				}

				var e = new CreateWindowEventArgs((ChromeFlags)chromeFlags, uri.ToUri());
				browser.Events.Raise(EventKey.CreateWindow, e);
				cancel = e.Cancel;
				return !e.Cancel ? e.Window : null;
			}
		}

		private static WindowCreator ms_WindowCreator;
	}
}
