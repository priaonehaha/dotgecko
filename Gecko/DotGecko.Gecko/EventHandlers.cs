using System;
using System.Collections.Generic;

namespace DotGecko.Gecko
{
	internal sealed class EventHandlers<TEventKey>
	{
		public EventHandlers(Object owner)
		{
			m_Owner = owner;
		}

		public void Add<TEventArgs>(TEventKey eventKey, EventHandler<TEventArgs> value) where TEventArgs : EventArgs
		{
			lock (m_Events)
			{
				Delegate handler;
				m_Events.TryGetValue(eventKey, out handler);
				handler = Delegate.Combine(handler, value);
				if (handler != null)
				{
					m_Events[eventKey] = handler;
				}
			}
		}

		public void Remove<TEventArgs>(TEventKey eventKey, EventHandler<TEventArgs> value) where TEventArgs : EventArgs
		{
			lock (m_Events)
			{
				Delegate handler;
				if (m_Events.TryGetValue(eventKey, out handler))
				{
					handler = Delegate.Remove(handler, value);
					if (handler != null)
					{
						m_Events[eventKey] = handler;
					}
					else
					{
						m_Events.Remove(eventKey);
					}
				}
			}
		}

		public void Raise<TEventArgs>(TEventKey eventKey, TEventArgs e) where TEventArgs : EventArgs
		{
			Delegate handler;
			lock (m_Events)
			{
				m_Events.TryGetValue(eventKey, out handler);
			}
			var eventHandler = (EventHandler<TEventArgs>)handler;
			if (eventHandler != null)
			{
				eventHandler(m_Owner, e);
			}
		}

		private readonly Object m_Owner;
		private readonly Dictionary<TEventKey, Delegate> m_Events = new Dictionary<TEventKey, Delegate>();
	}
}
