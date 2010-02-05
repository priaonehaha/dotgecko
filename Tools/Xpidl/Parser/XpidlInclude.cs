using System;

namespace XPIDL.Parser
{
	internal sealed class XpidlInclude : XpidlNode
	{
		public XpidlInclude(String fileName)
		{
			m_FileName = fileName;
		}

		public String FileName
		{
			get { return m_FileName; }
		}

		private readonly String m_FileName;
	}
}
