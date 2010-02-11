using System;

namespace Xpidl.Parser
{
	public sealed class XpidlInlineCHeader : XpidlNode
	{
		internal XpidlInlineCHeader(String headerText)
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
