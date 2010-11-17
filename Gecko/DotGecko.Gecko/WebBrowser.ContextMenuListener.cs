using System;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser : nsIContextMenuListener, nsIContextMenuListener2
	{
		public event EventHandler<ContextMenuEventArgs> ShowContextMenu
		{
			add { Events.Add(EventKey.ShowContextMenu, value); }
			remove { Events.Remove(EventKey.ShowContextMenu, value); }
		}

		void nsIContextMenuListener.OnShowContextMenu(UInt32 aContextFlags, nsIDOMEvent aEvent, nsIDOMNode aNode)
		{
			var e = new ContextMenuEventArgs((ContextMenuContext)aContextFlags, aEvent, aNode);
			Events.Raise(EventKey.ShowContextMenu, e);
		}

		void nsIContextMenuListener2.OnShowContextMenu(UInt32 aContextFlags, nsIContextMenuInfo aUtils)
		{
			var e = new ContextMenuEventArgs((ContextMenuContext)aContextFlags, aUtils);
			Events.Raise(EventKey.ShowContextMenu, e);
		}
	}
}
