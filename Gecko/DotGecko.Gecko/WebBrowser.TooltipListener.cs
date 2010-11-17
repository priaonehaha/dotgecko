using System;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser : nsITooltipListener
	{
		public event EventHandler<TooltipEventArgs> ShowTooltip
		{
			add { Events.Add(EventKey.ShowTooltip, value); }
			remove { Events.Remove(EventKey.ShowTooltip, value); }
		}

		public event EventHandler<EventArgs> HideTooltip
		{
			add { Events.Add(EventKey.HideTooltip, value); }
			remove { Events.Remove(EventKey.HideTooltip, value); }
		}

		#region Implementation of nsITooltipListener

		void nsITooltipListener.OnShowTooltip(Int32 aXCoords, Int32 aYCoords, String aTipText)
		{
			var e = new TooltipEventArgs(aXCoords, aYCoords, aTipText);
			Events.Raise(EventKey.ShowTooltip, e);
		}

		void nsITooltipListener.OnHideTooltip()
		{
			Events.Raise(EventKey.HideTooltip, EventArgs.Empty);
		}

		#endregion
	}
}
