using System;

namespace XPIDL.Parser
{
	internal sealed class XpidlInlineCHeader : XpidlNode
	{
		public XpidlInlineCHeader(String headerText)
		{
			m_HeaderText = headerText;
		}

		public String HeaderText
		{
			get { return m_HeaderText; }
		}

		private readonly String m_HeaderText;
	}
}
