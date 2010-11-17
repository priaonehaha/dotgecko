using System;
using System.ComponentModel;

namespace DotGecko.Gecko
{
	public sealed class CreateWindowEventArgs : CancelEventArgs
	{
		internal CreateWindowEventArgs(ChromeFlags chromeFlags, Uri uri)
			: base(false)
		{
			m_ChromeFlags = chromeFlags;
			m_Uri = uri;
		}

		public ChromeFlags ChromeFlags { get { return m_ChromeFlags; } }

		public Uri Uri { get { return m_Uri; } }

		public WebBrowser Window { get; set; }

		private readonly ChromeFlags m_ChromeFlags;
		private readonly Uri m_Uri;
	}
}
