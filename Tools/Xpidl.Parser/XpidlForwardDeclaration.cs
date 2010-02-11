using System;

namespace Xpidl.Parser
{
	public sealed class XpidlForwardDeclaration : XpidlNode
	{
		internal XpidlForwardDeclaration(String interfaceName)
		{
			m_InterfaceName = interfaceName;
		}

		public String InterfaceName
		{
			get { return m_InterfaceName; }
		}

		public override String ToString()
		{
			return String.Format("interface {0};", InterfaceName);
		}

		private readonly String m_InterfaceName;
	}
}
