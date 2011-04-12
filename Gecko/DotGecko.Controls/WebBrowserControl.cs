using System;
using System.Windows.Forms;
using DotGecko.Gecko;
using WebBrowser = DotGecko.Gecko.WebBrowser;

namespace DotGecko.Controls
{
	public sealed partial class WebBrowserControl : Control
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

		protected override void WndProc(ref Message m)
		{
			if (!DesignMode)
			{
				const Int32 WM_MOUSEACTIVATE = 0x0021;

				if (m.Msg == WM_MOUSEACTIVATE)
				{
					this.Focus();
				}
			}

			base.WndProc(ref m);
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

		private WebBrowser m_WebBrowser;
		private String m_StatusText;
		private ChromeFlags m_ChromeFlags;
	}
}
