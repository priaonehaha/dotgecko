using System;

namespace DotGecko.Gecko
{
	public sealed class ProvideWindowEventArgs : EventArgs
	{
		internal ProvideWindowEventArgs(ChromeFlags chromeFlags, Boolean positionSpecified, Boolean sizeSpecified, Uri uri, String name, String features)
		{
			m_ChromeFlags = chromeFlags;
			m_PositionSpecified = positionSpecified;
			m_SizeSpecified = sizeSpecified;
			m_Uri = uri;
			m_Name = name;
			m_Features = features;
			WindowIsNew = true;
		}

		public ChromeFlags ChromeFlags { get { return m_ChromeFlags; } }

		public Boolean PositionSpecified { get { return m_PositionSpecified; } }

		public Boolean SizeSpecified { get { return m_SizeSpecified; } }

		public Uri Uri { get { return m_Uri; } }

		public String Name { get { return m_Name; } }

		public String Features { get { return m_Features; } }

		public WebBrowser Window { get; set; }

		public Boolean WindowIsNew { get; set; }

		private readonly ChromeFlags m_ChromeFlags;
		private readonly Boolean m_PositionSpecified;
		private readonly Boolean m_SizeSpecified;
		private readonly Uri m_Uri;
		private readonly String m_Name;
		private readonly String m_Features;
	}
}
