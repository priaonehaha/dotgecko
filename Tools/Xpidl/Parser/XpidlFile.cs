using System;

namespace Xpidl.Parser
{
	internal sealed class XpidlFile : XpidlComplexNode
	{
		public XpidlFile(String name)
		{
			m_Name = name;
		}

		public String Name
		{
			get { return m_Name; }
		}

		public void AddNode(XpidlComment comment)
		{
			AddNodeImpl(comment);
		}

		public void AddNode(XpidlTypeDef typeDef)
		{
			AddNodeImpl(typeDef);
		}

		public void AddNode(XpidlInlineCHeader inlineCHeader)
		{
			AddNodeImpl(inlineCHeader);
		}

		public void AddNode(XpidlInclude include)
		{
			AddNodeImpl(include);
		}

		public void AddNode(XpidlForwardDeclaration forwardDeclaration)
		{
			AddNodeImpl(forwardDeclaration);
		}

		public void AddNode(XpidlNativeType xpidlNativeType)
		{
			AddNodeImpl(xpidlNativeType);
		}

		public void AddNode(XpidlInterface xpidlInterface)
		{
			AddNodeImpl(xpidlInterface);
		}

		private readonly String m_Name;
	}
}
