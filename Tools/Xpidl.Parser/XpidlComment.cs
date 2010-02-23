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

		public String CommentBody
		{
			get { return IsSingleline ? CommentText.Substring(2).Trim() : GetCommentBlockBody(); }
		}

		public Boolean IsSingleline
		{
			get { return CommentText.StartsWith(@"//"); }
		}

		private String GetCommentBlockBody()
		{
			//TODO: Get body of multiline comment
			return CommentText;
		}

		private readonly String m_CommentText;
	}
}
