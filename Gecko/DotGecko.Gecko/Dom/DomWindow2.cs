using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	//TODO: Make DomWindow2 disposable to remove itself from DomEventTarget
	public class DomWindow2 : DomWindow, nsIDOMEventListener
	{
		internal DomWindow2(nsIDOMWindow2 domWindow2)
			: base(domWindow2)
		{
			Debug.Assert(domWindow2 != null);
			m_DomWindow2 = domWindow2;
			m_Events = new EventHandlers<String>(this, StringComparer.OrdinalIgnoreCase);
		}

		internal static DomWindow2 Create(nsIDOMWindow2 domWindow2)
		{
			if (domWindow2 == null)
			{
				return null;
			}

			if (domWindow2 is nsIDOMWindowInternal)
			{
				return DomWindowInternal.Create((nsIDOMWindowInternal)domWindow2);
			}

			return new DomWindow2(domWindow2);
		}

		#region Mouse Events

		public event EventHandler<DomMouseEvent> Click
		{
			add { AddDomEventHandler("click", value); }
			remove { RemoveDomEventHandler("click", value); }
		}

		public event EventHandler<DomMouseEvent> DblClick
		{
			add { AddDomEventHandler("dblclick", value); }
			remove { RemoveDomEventHandler("dblclick", value); }
		}

		public event EventHandler<DomMouseEvent> MouseDown
		{
			add { AddDomEventHandler("mousedown", value); }
			remove { RemoveDomEventHandler("mousedown", value); }
		}

		public event EventHandler<DomMouseEvent> MouseUp
		{
			add { AddDomEventHandler("mouseup", value); }
			remove { RemoveDomEventHandler("mouseup", value); }
		}

		public event EventHandler<DomMouseEvent> MouseOver
		{
			add { AddDomEventHandler("mouseover", value); }
			remove { RemoveDomEventHandler("mouseover", value); }
		}

		public event EventHandler<DomMouseEvent> MouseMove
		{
			add { AddDomEventHandler("mousemove", value); }
			remove { RemoveDomEventHandler("mousemove", value); }
		}

		public event EventHandler<DomMouseEvent> MouseOut
		{
			add { AddDomEventHandler("mouseout", value); }
			remove { RemoveDomEventHandler("mouseout", value); }
		}

		#endregion

		#region Keyboard Events

		public event EventHandler<DomKeyEvent> KeyDown
		{
			add { AddDomEventHandler("keydown", value); }
			remove { RemoveDomEventHandler("keydown", value); }
		}

		public event EventHandler<DomKeyEvent> KeyPress
		{
			add { AddDomEventHandler("keypress", value); }
			remove { RemoveDomEventHandler("keypress", value); }
		}

		public event EventHandler<DomKeyEvent> KeyUp
		{
			add { AddDomEventHandler("keyup", value); }
			remove { RemoveDomEventHandler("keyup", value); }
		}

		#endregion

		#region HTML Object Events

		public event EventHandler<DomEvent> Load
		{
			add { AddDomEventHandler("load", value); }
			remove { RemoveDomEventHandler("load", value); }
		}

		public event EventHandler<DomEvent> Unload
		{
			add { AddDomEventHandler("unload", value); }
			remove { RemoveDomEventHandler("unload", value); }
		}

		public event EventHandler<DomEvent> Abort
		{
			add { AddDomEventHandler("abort", value); }
			remove { RemoveDomEventHandler("abort", value); }
		}

		public event EventHandler<DomEvent> Error
		{
			add { AddDomEventHandler("error", value); }
			remove { RemoveDomEventHandler("error", value); }
		}

		public event EventHandler<DomEvent> Resize
		{
			add { AddDomEventHandler("resize", value); }
			remove { RemoveDomEventHandler("resize", value); }
		}

		public event EventHandler<DomEvent> Scroll
		{
			add { AddDomEventHandler("scroll", value); }
			remove { RemoveDomEventHandler("scroll", value); }
		}

		#endregion

		#region HTML Form Events

		public event EventHandler<DomEvent> Select
		{
			add { AddDomEventHandler("select", value); }
			remove { RemoveDomEventHandler("select", value); }
		}

		public event EventHandler<DomEvent> Change
		{
			add { AddDomEventHandler("change", value); }
			remove { RemoveDomEventHandler("change", value); }
		}

		public event EventHandler<DomEvent> Submit
		{
			add { AddDomEventHandler("submit", value); }
			remove { RemoveDomEventHandler("submit", value); }
		}

		public event EventHandler<DomEvent> Reset
		{
			add { AddDomEventHandler("reset", value); }
			remove { RemoveDomEventHandler("reset", value); }
		}

		public event EventHandler<DomEvent> Focus
		{
			add { AddDomEventHandler("focus", value); }
			remove { RemoveDomEventHandler("focus", value); }
		}

		public event EventHandler<DomEvent> Blur
		{
			add { AddDomEventHandler("blur", value); }
			remove { RemoveDomEventHandler("blur", value); }
		}

		#endregion

		new internal nsIDOMWindow2 DomObj { get { return m_DomWindow2; } }

		private EventHandlers<String> Events { get { return m_Events; } }

		private void AddDomEventHandler<TEventArgs>(String domEvent, EventHandler<TEventArgs> eventHandler) where TEventArgs : EventArgs
		{
			if (Events.Add(domEvent, eventHandler))
			{
				nsIDOMEventTarget domEventTarget = m_DomWindow2.WindowRoot;
				domEventTarget.AddEventListener(domEvent, this, true);
			}
		}

		private void RemoveDomEventHandler<TEventArgs>(String domEvent, EventHandler<TEventArgs> eventHandler) where TEventArgs : EventArgs
		{
			if (Events.Remove(domEvent, eventHandler))
			{
				nsIDOMEventTarget domEventTarget = m_DomWindow2.WindowRoot;
				domEventTarget.RemoveEventListener(domEvent, this, true);
			}
		}

		#region Implementation of nsIDOMEventListener

		void nsIDOMEventListener.HandleEvent(nsIDOMEvent aEvent)
		{
			String eventType = XpcomString.Get(aEvent.GetType);
			EventArgs e = DomEvent.Create(aEvent);
			Events.Raise(eventType, e);
		}

		#endregion

		private readonly nsIDOMWindow2 m_DomWindow2;
		private readonly EventHandlers<String> m_Events;
	}
}
