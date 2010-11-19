using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomMouseEvent : DomUIEvent
	{
		private DomMouseEvent(nsIDOMMouseEvent domMouseEvent)
			: base(domMouseEvent)
		{
			Debug.Assert(domMouseEvent != null);
			m_DomMouseEvent = domMouseEvent;
		}

		internal static DomMouseEvent Create(nsIDOMMouseEvent domMouseEvent)
		{
			return domMouseEvent != null ? new DomMouseEvent(domMouseEvent) : null;
		}

		public Int32 ScreenX { get { return m_DomMouseEvent.ScreenX; } }
		public Int32 ScreenY { get { return m_DomMouseEvent.ScreenY; } }

		public Int32 ClientX { get { return m_DomMouseEvent.ClientX; } }
		public Int32 ClientY { get { return m_DomMouseEvent.ClientY; } }

		public Boolean CtrlKey { get { return m_DomMouseEvent.CtrlKey; } }
		public Boolean ShiftKey { get { return m_DomMouseEvent.ShiftKey; } }
		public Boolean AltKey { get { return m_DomMouseEvent.AltKey; } }
		public Boolean MetaKey { get { return m_DomMouseEvent.MetaKey; } }

		public UInt16 Button { get { return m_DomMouseEvent.Button; } }
		public Object RelatedTarget { get { return m_DomMouseEvent.RelatedTarget; } }

		private readonly nsIDOMMouseEvent m_DomMouseEvent;
	}
}
