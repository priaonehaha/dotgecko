using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomEvent
	{
		public enum Phase : ushort
		{
			Capturing = nsIDOMEventConstants.CAPTURING_PHASE,
			AtTarget = nsIDOMEventConstants.AT_TARGET,
			Bubbling = nsIDOMEventConstants.BUBBLING_PHASE
		}

		private DomEvent(nsIDOMEvent domEvent)
		{
			Debug.Assert(domEvent != null);
			m_DomEvent = domEvent;
		}

		internal static DomEvent Create(nsIDOMEvent domEvent)
		{
			return domEvent != null ? new DomEvent(domEvent) : null;
		}

		public String Type { get { return XpcomString.Get(m_DomEvent.GetType); } }

		//nsIDOMEventTarget Target { get; }

		//nsIDOMEventTarget CurrentTarget { get; }

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

		public void InitEvent(String eventTypeArg, Boolean canBubbleArg, Boolean cancelableArg)
		{
			m_DomEvent.InitEvent(eventTypeArg, canBubbleArg, cancelableArg);
		}

		private readonly nsIDOMEvent m_DomEvent;
	}
}
