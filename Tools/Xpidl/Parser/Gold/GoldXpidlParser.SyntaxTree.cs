using System;
using System.Collections.Generic;
using GoldParser;

namespace XPIDL.Parser.Gold
{
	internal sealed partial class GoldXpidlParser
	{
		private abstract class SyntaxNode
		{
			protected SyntaxNode()
			{
				m_AttachedCommentNodes = new List<CommentSyntaxNode>();
			}

			public void AttachCommentNode(CommentSyntaxNode commentNode)
			{
				m_AttachedCommentNodes.Add(commentNode);
			}

			public void DetachCommentNodes(ComplexSyntaxNode newParentNode)
			{
				if (newParentNode == null)
				{
					throw new ArgumentNullException("newParentNode");
				}

				foreach (CommentSyntaxNode commentNode in m_AttachedCommentNodes)
				{
					newParentNode.AddChildNode(commentNode);
				}
				m_AttachedCommentNodes.Clear();
			}

			public void ReattachCommentNodes(SyntaxNode oldParentNode)
			{
				if (oldParentNode == null)
				{
					throw new ArgumentNullException("oldParentNode");
				}

				foreach (CommentSyntaxNode commentNode in oldParentNode.m_AttachedCommentNodes)
				{
					AttachCommentNode(commentNode);
				}
				oldParentNode.m_AttachedCommentNodes.Clear();
			}

			private readonly List<CommentSyntaxNode> m_AttachedCommentNodes;
		}

		private sealed class CommentSyntaxNode : SyntaxNode
		{
			public CommentSyntaxNode(String commentText)
			{
				m_CommentText = commentText;
			}

			public String CommentText
			{
				get { return m_CommentText; }
			}

			private readonly String m_CommentText;
		}

		private sealed class SimpleSyntaxNode : SyntaxNode
		{
			public SimpleSyntaxNode(Symbol symbol, String text)
			{
				m_Symbol = symbol;
				m_Text = text;
			}

			public Symbol Symbol
			{
				get { return m_Symbol; }
			}

			public String Text
			{
				get { return m_Text; }
			}

			private readonly Symbol m_Symbol;
			private readonly String m_Text;
		}

		private sealed class ComplexSyntaxNode : SyntaxNode
		{
			public ComplexSyntaxNode(Rule rule)
			{
				m_Rule = rule;
			}

			public Rule Rule
			{
				get { return m_Rule; }
			}

			public Int32 Count
			{
				get { return m_ChildNodes.Count; }
			}

			public SyntaxNode this[Int32 index]
			{
				get { return m_ChildNodes[index]; }
			}

			public void AddChildNode(SyntaxNode childNode)
			{
				if (childNode == null)
				{
					throw new ArgumentNullException("childNode");
				}

				m_ChildNodes.Add(childNode);
			}

			private readonly Rule m_Rule;
			private readonly List<SyntaxNode> m_ChildNodes = new List<SyntaxNode>();
		}
	}
}
