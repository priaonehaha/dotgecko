using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser : IDisposable
	{
		public WebBrowser(IWebBrowserContainer container)
		{
			m_Container = container;
			m_Events = new EventHandlers<EventKey>(this);
		}

		public Boolean IsDisposed
		{
			get { return m_IsDisposed; }
		}

		public void Dispose()
		{
			Dispose(true);
		}

		private IWebBrowserContainer Container
		{
			get { return m_Container; }
		}

		private void Dispose(Boolean disposing)
		{
			if (m_IsDisposed)
			{
				return;
			}

			if (disposing)
			{
				//TODO:
			}

			//TODO:

			GC.SuppressFinalize(this);
			m_IsDisposed = true;
		}

		~WebBrowser()
		{
			Dispose(false);
		}

		private readonly IWebBrowserContainer m_Container;
		private Boolean m_IsDisposed;
	}
}
