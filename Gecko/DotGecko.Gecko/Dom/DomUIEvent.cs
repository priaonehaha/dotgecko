using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public class DomUIEvent : DomEvent
	{
		internal DomUIEvent(nsIDOMUIEvent domUIEvent)
			: base(domUIEvent)
		{
			Debug.Assert(domUIEvent != null);
			m_DomUIEvent = domUIEvent;
		}

		internal static DomUIEvent Create(nsIDOMUIEvent domUIEvent)
		{
			if (domUIEvent == null)
			{
				return null;
			}

			if (domUIEvent is nsIDOMKeyEvent)
			{
				return DomKeyEvent.Create((nsIDOMKeyEvent)domUIEvent);
			}
			if (domUIEvent is nsIDOMMouseEvent)
			{
				return DomMouseEvent.Create((nsIDOMMouseEvent)domUIEvent);
			}

			return new DomUIEvent(domUIEvent);
		}

		public Int32 Detail { get { return m_DomUIEvent.Detail; } }

		private readonly nsIDOMUIEvent m_DomUIEvent;
	}
}
