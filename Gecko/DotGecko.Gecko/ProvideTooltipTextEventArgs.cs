using System;
using DotGecko.Gecko.Dom;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed class ProvideTooltipTextEventArgs : EventArgs
	{
		internal ProvideTooltipTextEventArgs(nsIDOMNode domNode)
		{
			m_DomNode = DomNode.Create(domNode);
		}

		public DomNode DomNode { get { return m_DomNode; } }

		public String TooltipText { get; set; }

		private readonly DomNode m_DomNode;
	}
}
