using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomBarProp
	{
		private DomBarProp(nsIDOMBarProp domBarProp)
		{
			Debug.Assert(domBarProp != null);
			m_DomBarProp = domBarProp;
		}

		internal static DomBarProp Create(nsIDOMBarProp domBarProp)
		{
			return domBarProp != null ? new DomBarProp(domBarProp) : null;
		}

		public Boolean Visible
		{
			get { return m_DomBarProp.Visible; }
			set { m_DomBarProp.Visible = value; }
		}

		private readonly nsIDOMBarProp m_DomBarProp;
	}
}
