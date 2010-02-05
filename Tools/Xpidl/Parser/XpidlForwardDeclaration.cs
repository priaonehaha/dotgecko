using System;

namespace XPIDL.Parser
{
	internal sealed class XpidlForwardDeclaration : XpidlNode
	{
		public XpidlForwardDeclaration(String interfaceName)
		{
			m_InterfaceName = interfaceName;
		}

		public String InterfaceName
		{
			get { return m_InterfaceName; }
		}

		private readonly String m_InterfaceName;
	}
}
