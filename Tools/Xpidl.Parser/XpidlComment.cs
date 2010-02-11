using System;

namespace Xpidl.Parser
{
	public sealed class XpidlComment : XpidlNode
	{
		internal XpidlComment(String commentText)
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
