using System;
using DotGecko.Gecko.Interop;
using AString = System.IntPtr;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser : nsIWebBrowserChrome, nsIWebBrowserChrome2
	{
		public event EventHandler<ChromeSizeChangeEventArgs> ChromeSizeChange
		{
			add { Events.Add(EventKey.ChromeSizeChange, value); }
			remove { Events.Remove(EventKey.ChromeSizeChange, value); }
		}

		public event EventHandler<EventArgs> DestroyBrowserWindow
		{
			add { Events.Add(EventKey.DestroyBrowserWindow, value); }
			remove { Events.Remove(EventKey.DestroyBrowserWindow, value); }
		}

		#region Implementation of nsIWebBrowserChrome

		void nsIWebBrowserChrome.SetStatus(UInt32 statusType, String status)
		{
			Container.StatusText = status;
		}

		nsIWebBrowser nsIWebBrowserChrome.WebBrowser
		{
			get { return this.m_WebBrowser; }
			set { this.AssignWebBrowser(value); }
		}

		UInt32 nsIWebBrowserChrome.ChromeFlags
		{
			get { return (UInt32)Container.ChromeFlags; }
			set { Container.ChromeFlags = (ChromeFlags)value; }
		}

		void nsIWebBrowserChrome.DestroyBrowserWindow()
		{
			Events.Raise(EventKey.DestroyBrowserWindow, EventArgs.Empty);
		}

		void nsIWebBrowserChrome.SizeBrowserTo(Int32 aCX, Int32 aCY)
		{
			var e = new ChromeSizeChangeEventArgs(aCX, aCY);
			Events.Raise(EventKey.ChromeSizeChange, e);
		}

		void nsIWebBrowserChrome.ShowAsModal()
		{
			Container.EnterModalState();
		}

		Boolean nsIWebBrowserChrome.IsWindowModal()
		{
			return Container.IsInModalState;
		}

		void nsIWebBrowserChrome.ExitModalEventLoop(UInt32 aStatus)
		{
			Container.ExitModalState();
		}

		#endregion

		#region Implementation of nsIWebBrowserChrome2

		#region Redirect

		void nsIWebBrowserChrome2.SetStatus(UInt32 statusType, String status)
		{
			((nsIWebBrowserChrome)this).SetStatus(statusType, status);
		}

		nsIWebBrowser nsIWebBrowserChrome2.WebBrowser
		{
			get { return ((nsIWebBrowserChrome)this).WebBrowser; }
			set { ((nsIWebBrowserChrome)this).WebBrowser = value; }
		}

		UInt32 nsIWebBrowserChrome2.ChromeFlags
		{
			get { return ((nsIWebBrowserChrome)this).ChromeFlags; }
			set { ((nsIWebBrowserChrome)this).ChromeFlags = value; }
		}

		void nsIWebBrowserChrome2.DestroyBrowserWindow()
		{
			((nsIWebBrowserChrome)this).DestroyBrowserWindow();
		}

		void nsIWebBrowserChrome2.SizeBrowserTo(Int32 aCX, Int32 aCY)
		{
			((nsIWebBrowserChrome)this).SizeBrowserTo(aCX, aCY);
		}

		void nsIWebBrowserChrome2.ShowAsModal()
		{
			((nsIWebBrowserChrome)this).ShowAsModal();
		}

		Boolean nsIWebBrowserChrome2.IsWindowModal()
		{
			return ((nsIWebBrowserChrome)this).IsWindowModal();
		}

		void nsIWebBrowserChrome2.ExitModalEventLoop(UInt32 aStatus)
		{
			((nsIWebBrowserChrome)this).ExitModalEventLoop(aStatus);
		}

		#endregion

		void nsIWebBrowserChrome2.SetStatusWithContext(UInt32 statusType, AString statusText, Object statusContext)
		{
			Container.StatusText = statusText.GetString();
		}

		#endregion
	}
}
