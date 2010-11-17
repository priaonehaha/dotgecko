using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomLocation
	{
		private DomLocation(nsIDOMLocation domLocation)
		{
			Debug.Assert(domLocation != null);
			m_DomLocation = domLocation;
		}

		internal static DomLocation Create(nsIDOMLocation domLocation)
		{
			return domLocation != null ? new DomLocation(domLocation) : null;
		}

		public String Hash
		{
			get { return XpcomString.Get(m_DomLocation.GetHash); }
			set { m_DomLocation.SetHash(value); }
		}

		public String Host
		{
			get { return XpcomString.Get(m_DomLocation.GetHost); }
			set { m_DomLocation.SetHost(value); }
		}

		public String Hostname
		{
			get { return XpcomString.Get(m_DomLocation.GetHostname); }
			set { m_DomLocation.SetHostname(value); }
		}

		public String Href
		{
			get { return XpcomString.Get(m_DomLocation.GetHref); }
			set { m_DomLocation.SetHref(value); }
		}

		public String Pathname
		{
			get { return XpcomString.Get(m_DomLocation.GetPathname); }
			set { m_DomLocation.SetPathname(value); }
		}

		public String Port
		{
			get { return XpcomString.Get(m_DomLocation.GetPort); }
			set { m_DomLocation.SetPort(value); }
		}

		public String Protocol
		{
			get { return XpcomString.Get(m_DomLocation.GetProtocol); }
			set { m_DomLocation.SetProtocol(value); }
		}

		public String Search
		{
			get { return XpcomString.Get(m_DomLocation.GetSearch); }
			set { m_DomLocation.SetSearch(value); }
		}

		public void Reload(Boolean forceget)
		{
			m_DomLocation.Reload(forceget);
		}

		public void Replace(String url)
		{
			m_DomLocation.Replace(url);
		}

		public void Assign(String url)
		{
			m_DomLocation.Assign(url);
		}

		public override String ToString()
		{
			return XpcomString.Get(m_DomLocation.ToString);
		}

		private readonly nsIDOMLocation m_DomLocation;
	}
}
