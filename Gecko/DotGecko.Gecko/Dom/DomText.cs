using System;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public class DomText : DomCharacterData
	{
		internal DomText(nsIDOMText domText)
			: base(domText)
		{
			m_DomText = domText;
		}

		internal static DomText Create(nsIDOMText domText)
		{
			if (domText == null)
			{
				return null;
			}

			if (domText is nsIDOMCDATASection)
			{
				return DomCDataSection.Create((nsIDOMCDATASection)domText);
			}

			return new DomText(domText);
		}

		public DomText SplitText(UInt32 offset)
		{
			nsIDOMText domText = m_DomText.SplitText(offset);
			return DomText.Create(domText);
		}

		private readonly nsIDOMText m_DomText;
	}
}
