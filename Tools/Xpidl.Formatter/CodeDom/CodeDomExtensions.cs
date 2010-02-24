using System;
using System.CodeDom;
using System.Collections.Generic;
using Xpidl.Parser;

namespace Xpidl.Formatter.CodeDom
{
	internal static class CodeDomExtensions
	{
		public static String ToUpperCamel(this String s)
		{
			if (String.IsNullOrEmpty(s))
			{
				return s;
			}
			
			if (s.Length == 1)
			{
				return s.ToUpper();
			}

			return Char.ToUpper(s[0]) + s.Substring(1);
		}

		public static void AddComment(this CodeCommentStatementCollection codeCommentStatementCollection, String commentText)
		{
			if (commentText == null)
			{
				return;
			}

			foreach (String comment in commentText.Split('\n'))
			{
				var codeComment = new CodeComment(comment, false);
				codeCommentStatementCollection.Add(new CodeCommentStatement(codeComment));
			}
		}

		public static void AddPrecedingComments(this CodeCommentStatementCollection codeCommentStatementCollection, XpidlComplexNode xpidlComplexNode, Int32 currentIndex)
		{
			foreach (XpidlComment xpidlComment in xpidlComplexNode.CommentsBefore(currentIndex))
			{
				codeCommentStatementCollection.AddComment(xpidlComment.CommentBody);
			}
		}

		public static void AddAttribute(this CodeAttributeDeclarationCollection codeAttributeDeclarationCollection, CodeAttributeDeclaration codeAttributeDeclaration)
		{
			if (codeAttributeDeclaration != null)
			{
				codeAttributeDeclarationCollection.Add(codeAttributeDeclaration);
			}
		}

		private static IEnumerable<XpidlComment> CommentsBefore(this XpidlComplexNode complexNode, Int32 currentIndex)
		{
			Int32 commentIndex = currentIndex;
			while (commentIndex > 0 && complexNode.ChildNodes[commentIndex - 1] is XpidlComment)
			{
				commentIndex--;
			}

			for (Int32 i = commentIndex; i < currentIndex; i++)
			{
				yield return (XpidlComment)complexNode.ChildNodes[i];
			}

			yield break;
		}
	}
}
