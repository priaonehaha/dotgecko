using System;
using System.Windows.Forms;
using DotGecko.Gecko;
using WebBrowser = DotGecko.Gecko.WebBrowser;

namespace DotGecko.Controls
{
	public sealed class GeckoWebBrowserControl : Control, IWebBrowserContainer
	{
		public String StatusText
		{
			get { return m_StatusText; }
			set { m_StatusText = value; }
		}

		public ChromeFlags ChromeFlags
		{
			get { return m_ChromeFlags; }
			set { m_ChromeFlags = value; }
		}

		public WebBrowser Browser
		{
			get { return m_WebBrowser; }
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			m_WebBrowser = new WebBrowser(this);
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			m_WebBrowser.Dispose();
			m_WebBrowser = null;

			base.OnHandleDestroyed(e);
		}

		protected override void Dispose(Boolean disposing)
		{
			if (! IsDisposed)
			{
				if (disposing && m_WebBrowser != null)
				{
					m_WebBrowser.Dispose();
				}
			}
			
			base.Dispose(disposing);
		}

		#region Implementation of IWebBrowserContainer

		String IWebBrowserContainer.Title
		{
			get { return Text; }
			set { Text = value; }
		}

		Boolean IWebBrowserContainer.IsVisible
		{
			get { return Visible; }
			set { Visible = value; }
		}

		void IWebBrowserContainer.Focus()
		{
			this.Focus();
		}

		event EventHandler IWebBrowserContainer.GotFocus
		{
			add { this.Enter += value; }
			remove { this.Enter -= value; }
		}

		event EventHandler IWebBrowserContainer.LostFocus
		{
			add { this.Leave += value; }
			remove { this.Leave -= value; }
		}

		void IWebBrowserContainer.EnterModalState()
		{
			//TODO: enter modal loop
		}

		Boolean IWebBrowserContainer.IsInModalState
		{
			get { return false; }
		}

		void IWebBrowserContainer.ExitModalState()
		{
			//TODO: implement me!
		}

		#endregion

		private WebBrowser m_WebBrowser;
		private String m_StatusText;
		private ChromeFlags m_ChromeFlags;
	}
}
