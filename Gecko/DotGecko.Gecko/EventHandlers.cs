using System;
using System.Collections.Generic;

namespace DotGecko.Gecko
{
	public delegate void EventHandler<in TEventArgs>(Object sender, TEventArgs e) where TEventArgs : EventArgs;

	internal sealed class EventHandlers<TEventKey>
	{
		public EventHandlers(Object owner, IEqualityComparer<TEventKey> keyComparer = null)
		{
			m_Owner = owner;
			m_Events = new Dictionary<TEventKey, Delegate>(keyComparer);
		}

		public Boolean Add<TEventArgs>(TEventKey eventKey, EventHandler<TEventArgs> value) where TEventArgs : EventArgs
		{
			lock (m_Events)
			{
				Delegate handler;
				Boolean exists = m_Events.TryGetValue(eventKey, out handler);
				handler = Delegate.Combine(handler, value);
				if (handler != null)
				{
					m_Events[eventKey] = handler;
				}
				return !exists;
			}
		}

		public Boolean Remove<TEventArgs>(TEventKey eventKey, EventHandler<TEventArgs> value) where TEventArgs : EventArgs
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
						return false;
					}
					m_Events.Remove(eventKey);
				}
				return true;
			}
		}

		public void Raise(TEventKey eventKey, EventArgs e)
		{
			Delegate handler;
			lock (m_Events)
			{
				m_Events.TryGetValue(eventKey, out handler);
			}
			if (handler != null)
			{
				handler.DynamicInvoke(m_Owner, e);
			}
		}

		public void Raise<TEventArgs>(TEventKey eventKey, TEventArgs e) where TEventArgs : EventArgs
		{
			Delegate handler;
			lock (m_Events)
			{
				m_Events.TryGetValue(eventKey, out handler);
			}
			if (handler != null)
			{
				var eventHandler = (EventHandler<TEventArgs>)handler;
				eventHandler(m_Owner, e);
			}
		}

		private readonly Object m_Owner;
		private readonly Dictionary<TEventKey, Delegate> m_Events;
	}
}
