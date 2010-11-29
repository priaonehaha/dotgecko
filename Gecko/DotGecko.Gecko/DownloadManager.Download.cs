using System;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public static partial class DownloadManager
	{
		public sealed class Download
		{
			internal Download(nsIDownload download)
			{
				m_Download = download;
			}

			public Int32 PercentComplete
			{
				get { return m_Download.PercentComplete; }
			}

			public Int64 AmountTransferred
			{
				get { return m_Download.AmountTransferred; }
			}

			public Int64 Size
			{
				get { return m_Download.Size; }
			}

			public Uri Source
			{
				get
				{
					nsIURI nsUri = m_Download.Source;
					return nsUri.ToUri();
				}
			}

			public Uri Target
			{
				get
				{
					nsIURI nsUri = m_Download.Target;
					return nsUri.ToUri();
				}
			}

			public String DisplayName
			{
				get { return XpcomStringHelper.Get(m_Download.GetDisplayName); }
			}

			public DateTime StartTime
			{
				get { return m_Download.StartTime.ToDateTime(); }
			}

			public Double Speed
			{
				get { return m_Download.Speed; }
			}

			public String MimeType
			{
				get
				{
					nsIMIMEInfo mimeInfo = m_Download.MIMEInfo;
					return XpcomStringHelper.Get(mimeInfo.GetMIMEType);
				}
			}

			public UInt32 Id
			{
				get { return m_Download.Id; }
			}

			public DownloadState State
			{
				get { return (DownloadState)m_Download.State; }
			}

			public Uri Referrer
			{
				get
				{
					nsIURI nsUri = m_Download.Referrer;
					return nsUri.ToUri();
				}
			}

			public Boolean Resumable
			{
				get { return m_Download.Resumable; }
			}

			private readonly nsIDownload m_Download;
		}
	}
}
