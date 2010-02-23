using System;
using System.Collections;
using System.Collections.Generic;
using GoldParser;

namespace Xpidl.Parser.Gold
{
	public sealed partial class GoldXpidlParser
	{
		private abstract class SyntaxNode
		{
			protected SyntaxNode()
			{
				m_AttachedCommentNodes = new List<CommentSyntaxNode>();
			}

			internal void AttachCommentNode(CommentSyntaxNode commentNode)
			{
				m_AttachedCommentNodes.Add(commentNode);
			}

			internal void DetachCommentNodes(ComplexSyntaxNode newParentNode)
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

			internal void ReattachCommentNodes(SyntaxNode oldParentNode)
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
			internal CommentSyntaxNode(String commentText)
			{
				m_CommentText = commentText;
				m_IsInlineCHeader = commentText.StartsWith("%{");
			}

			internal String CommentText
			{
				get { return m_CommentText; }
			}

			internal Boolean IsInlineCHeader
			{
				get { return m_IsInlineCHeader; }
			}

			private readonly String m_CommentText;
			private readonly Boolean m_IsInlineCHeader;
		}

		private sealed class SimpleSyntaxNode : SyntaxNode
		{
			internal SimpleSyntaxNode(Symbol symbol, String text)
			{
				m_Symbol = symbol;
				m_Text = text;
			}

			internal Symbol Symbol
			{
				get { return m_Symbol; }
			}

			internal String Text
			{
				get { return m_Text; }
			}

			private readonly Symbol m_Symbol;
			private readonly String m_Text;
		}

		private sealed class ComplexSyntaxNode : SyntaxNode, IEnumerable<SyntaxNode>
		{
			internal ComplexSyntaxNode(Rule rule)
			{
				m_Rule = rule;
				m_ChildNodes = new List<SyntaxNode>();
			}

			internal Rule Rule
			{
				get { return m_Rule; }
			}

			internal Int32 Count
			{
				get { return m_ChildNodes.Count; }
			}

			internal SyntaxNode this[Int32 index]
			{
				get { return this[index, false]; }
			}

			internal SyntaxNode this[Int32 index, Boolean skipComments]
			{
				get
				{
					if (!skipComments)
					{
						return m_ChildNodes[index];
					}

					Int32 i;
					Int32 count = -1;
					for (i = 0; (count < index) && (i < m_ChildNodes.Count); ++i)
					{
						if (!(m_ChildNodes[i] is CommentSyntaxNode))
						{
							++count;
						}
					}
					
					if (count == index)
					{
						return m_ChildNodes[i - 1];
					}

					throw new ArgumentOutOfRangeException("index");
				}
			}

			internal void AddChildNode(SyntaxNode childNode)
			{
				if (childNode == null)
				{
					throw new ArgumentNullException("childNode");
				}

				m_ChildNodes.Add(childNode);
			}

			public IEnumerator<SyntaxNode> GetEnumerator()
			{
				return m_ChildNodes.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			private readonly Rule m_Rule;
			private readonly List<SyntaxNode> m_ChildNodes;
		}
	}
}
