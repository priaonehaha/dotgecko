using System;

namespace Xpidl.Parser
{
	public sealed class XpidlFile : XpidlComplexNode
	{
		internal XpidlFile(String name)
		{
			m_Name = name;
		}

		public String Name
		{
			get { return m_Name; }
		}

		internal void AddNode(XpidlComment comment)
		{
			AddNodeImpl(comment);
		}

		internal void AddNode(XpidlTypeDef typeDef)
		{
			AddNodeImpl(typeDef);
		}

		internal void AddNode(XpidlInlineCHeader inlineCHeader)
		{
			AddNodeImpl(inlineCHeader);
		}

		internal void AddNode(XpidlInclude include)
		{
			AddNodeImpl(include);
		}

		internal void AddNode(XpidlForwardDeclaration forwardDeclaration)
		{
			AddNodeImpl(forwardDeclaration);
		}

		internal void AddNode(XpidlNativeType xpidlNativeType)
		{
			AddNodeImpl(xpidlNativeType);
		}

		internal void AddNode(XpidlInterface xpidlInterface)
		{
			AddNodeImpl(xpidlInterface);
		}

		private readonly String m_Name;
	}
}
