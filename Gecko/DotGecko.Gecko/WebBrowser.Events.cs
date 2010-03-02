using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser
	{
		private EventHandlers<EventKey> Events
		{
			get { return m_Events; }
		}

		private enum EventKey
		{
			//
		}

		private readonly EventHandlers<EventKey> m_Events;
	}
}
