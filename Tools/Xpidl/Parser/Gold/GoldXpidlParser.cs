using System;
using System.Diagnostics;
using System.IO;
using GoldParser;

namespace Xpidl.Parser.Gold
{
	internal sealed partial class GoldXpidlParser : IXpidlParser
	{
		public GoldXpidlParser(BinaryReader grammarBinaryReader)
		{
			m_Grammar = new Grammar(grammarBinaryReader);
		}

		public XpidlFile Parse(TextReader xpidlTextReader)
		{
			ComplexSyntaxNode rootSyntaxNode = ParseImpl(xpidlTextReader);
			DisplaySyntaxTree(rootSyntaxNode);
			XpidlFile xpidlFile = CreateXpidlFile("", rootSyntaxNode);
			return xpidlFile;
		}

		private ComplexSyntaxNode ParseImpl(TextReader xpidlTextReader)
		{
			var goldParser = new GoldParser.Parser(xpidlTextReader, m_Grammar) { TrimReductions = true };
			var rootSyntaxNode = new ComplexSyntaxNode(null);

			while (true)
			{
				ParseMessage parseMessage = goldParser.Parse();
				switch (parseMessage)
				{
					case ParseMessage.Empty:
						break;

					// Comment or inline C header
					case ParseMessage.CommentLineRead:
					case ParseMessage.CommentBlockRead:
						var commentSyntaxNode = new CommentSyntaxNode(goldParser.CommentText);
						if (goldParser.TokenSyntaxNode == null)
						{
							rootSyntaxNode.AddChildNode(commentSyntaxNode);
						}
						else
						{
							((SyntaxNode)goldParser.TokenSyntaxNode).AttachCommentNode(commentSyntaxNode);
						}
						break;

					// Read valid token
					case ParseMessage.TokenRead:
						var simpleSyntaxNode = new SimpleSyntaxNode(goldParser.TokenSymbol, goldParser.TokenText);
						goldParser.TokenSyntaxNode = simpleSyntaxNode;
						break;

					// Can create new xpidl-node
					case ParseMessage.Reduction:
						var complexSyntaxNode = new ComplexSyntaxNode(goldParser.ReductionRule);
						for (Int32 i = 0; i < goldParser.ReductionCount; ++i)
						{
							var syntaxNode = (SyntaxNode)goldParser.GetReductionSyntaxNode(i);
							complexSyntaxNode.AddChildNode(syntaxNode);
							if (i == (goldParser.ReductionCount - 1))
							{
								complexSyntaxNode.ReattachCommentNodes(syntaxNode);
							}
							else
							{
								syntaxNode.DetachCommentNodes(complexSyntaxNode);
							}
						}
						goldParser.TokenSyntaxNode = complexSyntaxNode;
						break;

					// Parsing successfully completed
					case ParseMessage.Accept:
						var acceptedSyntaxNode = (SyntaxNode)goldParser.TokenSyntaxNode;
						Debug.Assert(acceptedSyntaxNode != null);
						rootSyntaxNode.AddChildNode(acceptedSyntaxNode);
						acceptedSyntaxNode.DetachCommentNodes(rootSyntaxNode);
						return rootSyntaxNode;

					// Grammar table is not loaded
					case ParseMessage.NotLoadedError:
						// TODO: throw an exception
						return null;

					// Unexpected end of input
					case ParseMessage.CommentError:
						// TODO: throw an exception
						return null;

					// Invalid token
					case ParseMessage.LexicalError:
						// TODO: throw an exception
						return null;

					// Unexpected token
					case ParseMessage.SyntaxError:
						// TODO: throw an exception
						return null;

					// Fatal internal error
					case ParseMessage.InternalError:
						// TODO: throw an exception
						return null;
				}
			}
		}

		[Conditional("DEBUG")]
		private static void DisplaySyntaxTree(ComplexSyntaxNode rootSyntaxNode)
		{
			for (Int32 i = 0; i < rootSyntaxNode.Count; ++i)
			{
				SyntaxNode currentSyntaxNode = rootSyntaxNode[i];

				if (currentSyntaxNode is CommentSyntaxNode)
				{
					var commentSyntaxNode = (CommentSyntaxNode)currentSyntaxNode;
					if (commentSyntaxNode.CommentText.StartsWith("%{"))
					{
						Debug.WriteLine("%{ C++ inline c header %}");
					}
					else
					{
						Debug.WriteLine("/* comment */");
					}
				}
				else if (currentSyntaxNode is SimpleSyntaxNode)
				{
					Debug.WriteLine(((SimpleSyntaxNode)currentSyntaxNode).Text);
				}
				else
				{
					var complexSyntaxNode = (ComplexSyntaxNode)currentSyntaxNode;
					Debug.WriteLine(complexSyntaxNode.Rule.Name);
					Debug.Indent();
					DisplaySyntaxTree(complexSyntaxNode);
					Debug.Unindent();
				}
			}
		}

		private readonly Grammar m_Grammar;
	}
}
