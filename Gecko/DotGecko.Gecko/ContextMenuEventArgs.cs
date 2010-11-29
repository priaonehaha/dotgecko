using System;
using DotGecko.Gecko.Dom;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public enum ContextMenuContext : uint
	{
		None = nsIContextMenuListener2Constants.CONTEXT_NONE,
		Link = nsIContextMenuListener2Constants.CONTEXT_LINK,
		Image = nsIContextMenuListener2Constants.CONTEXT_IMAGE,
		Document = nsIContextMenuListener2Constants.CONTEXT_DOCUMENT,
		Text = nsIContextMenuListener2Constants.CONTEXT_TEXT,
		Input = nsIContextMenuListener2Constants.CONTEXT_INPUT,
		BackgroundImage = nsIContextMenuListener2Constants.CONTEXT_BACKGROUND_IMAGE
	}

	public sealed class ContextMenuEventArgs : EventArgs
	{
		internal ContextMenuEventArgs(ContextMenuContext context, nsIDOMEvent domEvent, nsIDOMNode domNode)
		{
			m_Context = context;
			m_DomEvent = DomEvent.Create(domEvent);
			m_DomNode = DomNode.Create(domNode);
		}

		internal ContextMenuEventArgs(ContextMenuContext context, nsIContextMenuInfo contextMenuInfo)
		{
			m_Context = context;
			m_DomEvent = DomEvent.Create(contextMenuInfo.MouseEvent);
			m_DomNode = DomNode.Create(contextMenuInfo.TargetNode);
			String associaledLink = XpcomStringHelper.Get(contextMenuInfo.GetAssociatedLink);
			Uri.TryCreate(associaledLink, UriKind.RelativeOrAbsolute, out m_AssociatedLink);
		}

		public ContextMenuContext Context
		{
			get { return m_Context; }
		}

		public DomEvent DomEvent
		{
			get { return m_DomEvent; }
		}

		public DomNode DomNode
		{
			get { return m_DomNode; }
		}

		public Uri AssociatedLink
		{
			get { return m_AssociatedLink; }
		}

		private readonly ContextMenuContext m_Context;
		private readonly DomEvent m_DomEvent;
		private readonly DomNode m_DomNode;
		private readonly Uri m_AssociatedLink;
	}
}
