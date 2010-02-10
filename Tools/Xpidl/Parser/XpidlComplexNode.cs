using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Xpidl.Parser
{
	internal abstract class XpidlComplexNode : XpidlNode
	{
		protected XpidlComplexNode()
		{
			m_ChildNodes = new List<XpidlNode>();
			m_ReadOnlyChildNodes = new ReadOnlyCollection<XpidlNode>(m_ChildNodes);
		}

		public IList<XpidlNode> ChildNodes
		{
			get { return m_ReadOnlyChildNodes; }
		}

		protected void AddNodeImpl(XpidlNode node)
		{
			m_ChildNodes.Add(node);
		}

		private readonly List<XpidlNode> m_ChildNodes;
		private readonly ReadOnlyCollection<XpidlNode> m_ReadOnlyChildNodes;
	}
}
