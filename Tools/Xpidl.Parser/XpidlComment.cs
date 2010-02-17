using System;

namespace Xpidl.Parser
{
	public sealed class XpidlComment : XpidlNode
	{
		internal XpidlComment(String commentText)
		{
			m_CommentText = commentText;
			m_IsSingleline = commentText.StartsWith(@"//");
		}

		public String CommentText
		{
			get { return m_CommentText; }
		}

		public Boolean IsSingleline
		{
			get { return m_IsSingleline; }
		}

		private readonly String m_CommentText;
		private readonly Boolean m_IsSingleline;
	}
}
