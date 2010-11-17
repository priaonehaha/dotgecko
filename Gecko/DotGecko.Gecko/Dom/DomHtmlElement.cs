using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomHtmlElement : DomElement
	{
		private DomHtmlElement(nsIDOMHTMLElement domHtmlElement)
			: base (domHtmlElement)
		{
			Debug.Assert(domHtmlElement != null);
			m_DomHtmlElement = domHtmlElement;
		}

		internal static DomHtmlElement Create(nsIDOMHTMLElement domHtmlElement)
		{
			return domHtmlElement != null ? new DomHtmlElement(domHtmlElement) : null;
		}

		public String Id
		{
			get { return XpcomString.Get(m_DomHtmlElement.GetId); }
			set { m_DomHtmlElement.SetId(value); }
		}

		public String Title
		{
			get { return XpcomString.Get(m_DomHtmlElement.GetTitle); }
			set { m_DomHtmlElement.SetTitle(value); }
		}

		public String Lang
		{
			get { return XpcomString.Get(m_DomHtmlElement.GetLang); }
			set { m_DomHtmlElement.SetLang(value); }
		}

		public String Dir
		{
			get { return XpcomString.Get(m_DomHtmlElement.GetDir); }
			set { m_DomHtmlElement.SetDir(value); }
		}

		public String ClassName
		{
			get { return XpcomString.Get(m_DomHtmlElement.GetClassName); }
			set { m_DomHtmlElement.SetClassName(value); }
		}

		private readonly nsIDOMHTMLElement m_DomHtmlElement;
	}
}
