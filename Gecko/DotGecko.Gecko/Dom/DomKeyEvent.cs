using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomKeyEvent : DomUIEvent
	{
		private DomKeyEvent(nsIDOMKeyEvent domKeyEvent)
			: base(domKeyEvent)
		{
			Debug.Assert(domKeyEvent != null);
			m_DomKeyEvent = domKeyEvent;
		}

		internal static DomKeyEvent Create(nsIDOMKeyEvent domKeyEvent)
		{
			return domKeyEvent != null ? new DomKeyEvent(domKeyEvent) : null;
		}

		public UInt32 CharCode { get { return m_DomKeyEvent.CharCode; } }

		public UInt32 KeyCode { get { return m_DomKeyEvent.KeyCode; } }

		public Boolean AltKey { get { return m_DomKeyEvent.AltKey; } }

		public Boolean CtrlKey { get { return m_DomKeyEvent.CtrlKey; } }

		public Boolean ShiftKey { get { return m_DomKeyEvent.ShiftKey; } }

		public Boolean MetaKey { get { return m_DomKeyEvent.MetaKey; } }

		private readonly nsIDOMKeyEvent m_DomKeyEvent;
	}
}
