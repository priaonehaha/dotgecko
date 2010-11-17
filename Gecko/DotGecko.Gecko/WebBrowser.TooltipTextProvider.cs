using System;
using System.Runtime.InteropServices;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser
	{
		public event EventHandler<ProvideTooltipTextEventArgs> ProvideTooltipText
		{
			add { Events.Add(EventKey.ProvideTooltipText, value); }
			remove { Events.Remove(EventKey.ProvideTooltipText, value); }
		}

		[Guid("CE82B6BB-14DD-4E4A-B01C-8BF6162ACD0C")]
		private sealed class TooltipTextProvider : nsITooltipTextProvider
		{
			Boolean nsITooltipTextProvider.GetNodeText(nsIDOMNode aNode, out String aText)
			{
				WebBrowser browser = GetBrowserFromDomDocument(aNode.OwnerDocument);
				if (browser == null)
				{
					aText = null;
					return false;
				}

				var e = new ProvideTooltipTextEventArgs(aNode);
				browser.Events.Raise(EventKey.ProvideTooltipText, e);
				aText = e.TooltipText;
				return String.IsNullOrWhiteSpace(e.TooltipText);
			}
		}

		private static Factory<TooltipTextProvider> ms_TooltipTextProviderFactory;
	}
}
