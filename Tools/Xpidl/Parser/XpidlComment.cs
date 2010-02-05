using System;

namespace XPIDL.Parser
{
	internal sealed class XpidlComment : XpidlNode
	{
		public XpidlComment(String commentText)
		{
			m_CommentText = commentText;
		}

		public String CommentText
		{
			get { return m_CommentText; }
		}

		private readonly String m_CommentText;
	}
}
