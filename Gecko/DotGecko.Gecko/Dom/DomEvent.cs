using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public class DomEvent : EventArgs
	{
		public enum Phase : ushort
		{
			Capturing = nsIDOMEventConstants.CAPTURING_PHASE,
			AtTarget = nsIDOMEventConstants.AT_TARGET,
			Bubbling = nsIDOMEventConstants.BUBBLING_PHASE
		}

		internal DomEvent(nsIDOMEvent domEvent)
		{
			Debug.Assert(domEvent != null);
			m_DomEvent = domEvent;
		}

		internal static DomEvent Create(nsIDOMEvent domEvent)
		{
			if (domEvent == null)
			{
				return null;
			}

			if (domEvent is nsIDOMUIEvent)
			{
				return DomUIEvent.Create((nsIDOMUIEvent)domEvent);
			}

			if (domEvent is nsIDOMPopupBlockedEvent)
			{
				return DomPopupBlockedEvent.Create((nsIDOMPopupBlockedEvent)domEvent);
			}

			return new DomEvent(domEvent);
		}

		public String Type { get { return XpcomStringHelper.Get(m_DomEvent.GetType); } }

		public Object Target { get { return m_DomEvent.Target; } }

		public Object CurrentTarget { get { return m_DomEvent.CurrentTarget; } }

		public Phase EventPhase { get { return (Phase)m_DomEvent.EventPhase; } }

		public Boolean Bubbles { get { return m_DomEvent.Bubbles; } }

		public Boolean Cancelable { get { return m_DomEvent.Cancelable; } }

		public DateTime TimeStamp { get { return m_DomEvent.TimeStamp.ToDateTime(); } }

		public void StopPropagation()
		{
			m_DomEvent.StopPropagation();
		}

		public void PreventDefault()
		{
			m_DomEvent.PreventDefault();
		}

		private readonly nsIDOMEvent m_DomEvent;
	}
}
