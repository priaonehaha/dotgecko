using System;

namespace Xpidl.Parser
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

		public override String ToString()
		{
			return String.Format("#include \"{0}\"", FileName);
		}

		private readonly String m_FileName;
	}
}
