using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomPopupBlockedEvent : DomEvent
	{
		private DomPopupBlockedEvent(nsIDOMPopupBlockedEvent domPopupBlockedEvent)
			: base(domPopupBlockedEvent)
		{
			Debug.Assert(domPopupBlockedEvent != null);
			m_DomPopupBlockedEvent = domPopupBlockedEvent;
		}

		internal static DomPopupBlockedEvent Create(nsIDOMPopupBlockedEvent domPopupBlockedEvent)
		{
			return domPopupBlockedEvent != null ? new DomPopupBlockedEvent(domPopupBlockedEvent) : null;
		}

		public Uri PopupWindowUri { get { return m_DomPopupBlockedEvent.PopupWindowURI.ToUri(); } }

		public String PopupWindowFeatures { get { return XpcomString.Get(m_DomPopupBlockedEvent.GetPopupWindowFeatures); } }

		public String PopupWindowName { get { return XpcomString.Get(m_DomPopupBlockedEvent.GetPopupWindowName); } }

		private readonly nsIDOMPopupBlockedEvent m_DomPopupBlockedEvent;
	}
}
