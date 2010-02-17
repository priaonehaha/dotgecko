using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Xpidl.Parser.Gold
{
	public sealed partial class GoldXpidlParser
	{
		private static XpidlFile CreateXpidlFile(String xpidlFileName, ComplexSyntaxNode rootSyntaxNode)
		{
			if (rootSyntaxNode == null)
			{
				throw new ArgumentNullException("rootSyntaxNode");
			}
			if (rootSyntaxNode.Rule != null)
			{
				throw new ArgumentException(InvalidSyntaxNode, "rootSyntaxNode");
			}

			var xpidlFile = new XpidlFile(xpidlFileName);
			BuildXpidlFile(xpidlFile, rootSyntaxNode);
			return xpidlFile;
		}

		private static void BuildXpidlFile(XpidlFile xpidlFile, ComplexSyntaxNode rootSyntaxNode)
		{
			foreach (SyntaxNode syntaxNode in rootSyntaxNode)
			{
				if (syntaxNode is CommentSyntaxNode)
				{
					var commentSyntaxNode = (CommentSyntaxNode)syntaxNode;
					if (commentSyntaxNode.IsInlineCHeader)
					{
						XpidlInlineCHeader xpidlInlineCHeader = CreateXpidlInlineCHeader(commentSyntaxNode);
						xpidlFile.AddNode(xpidlInlineCHeader);
					}
					else
					{
						XpidlComment xpidlComment = CreateXpidlComment(commentSyntaxNode);
						xpidlFile.AddNode(xpidlComment);
					}
				}
				else if (syntaxNode is ComplexSyntaxNode)
				{
					var complexSyntaxNode = (ComplexSyntaxNode)syntaxNode;
					switch ((RuleConstants)complexSyntaxNode.Rule.Index)
					{
						case RuleConstants.Xpidl:
						case RuleConstants.XpidlItems1:
						case RuleConstants.XpidlItems2:
						case RuleConstants.XpidlItem1:
						case RuleConstants.XpidlItem2:
						case RuleConstants.XpidlItem3:
						case RuleConstants.XpidlItem4:
						case RuleConstants.XpidlItem5:
							BuildXpidlFile(xpidlFile, complexSyntaxNode);
							break;

						case RuleConstants.XpidlTypeDef:
							XpidlTypeDef xpidlTypeDef = CreateXpidlTypeDef(complexSyntaxNode);
							xpidlFile.AddNode(xpidlTypeDef);
							break;

						case RuleConstants.XpidlInclude:
							XpidlInclude xpidlInclude = CreateXpidlInclude(complexSyntaxNode);
							xpidlFile.AddNode(xpidlInclude);
							break;

						case RuleConstants.XpidlForwardDeclaration:
							XpidlForwardDeclaration xpidlForwardDeclaration = CreateXpidlForwardDeclaration(complexSyntaxNode);
							xpidlFile.AddNode(xpidlForwardDeclaration);
							break;

						case RuleConstants.XpidlNativeType1:
						case RuleConstants.XpidlNativeType2:
							XpidlNativeType xpidlNativeType = CreateXpidlNativeType(complexSyntaxNode);
							xpidlFile.AddNode(xpidlNativeType);
							break;

						case RuleConstants.XpidlInterface1:
						case RuleConstants.XpidlInterface2:
						case RuleConstants.XpidlInterface3:
						case RuleConstants.XpidlInterface4:
							XpidlInterface xpidlInterface = CreateXpidlInterface(complexSyntaxNode);
							xpidlFile.AddNode(xpidlInterface);
							break;

						default:
							throw new ArgumentException(InvalidSyntaxNode, "rootSyntaxNode");
					}
				}
				else // SimpleSyntaxNode
				{
					throw new ArgumentException(InvalidSyntaxNode, "rootSyntaxNode");
				}
			}
		}

		private static XpidlComment CreateXpidlComment(CommentSyntaxNode commentSyntaxNode)
		{
			if (commentSyntaxNode == null)
			{
				throw new ArgumentNullException("commentSyntaxNode");
			}

			var xpidlComment = new XpidlComment(commentSyntaxNode.CommentText);
			return xpidlComment;
		}

		private static XpidlInlineCHeader CreateXpidlInlineCHeader(CommentSyntaxNode inlineCHeaderSyntaxNode)
		{
			if (inlineCHeaderSyntaxNode == null)
			{
				throw new ArgumentNullException("inlineCHeaderSyntaxNode");
			}

			var xpidlInlineCHeader = new XpidlInlineCHeader(inlineCHeaderSyntaxNode.CommentText);
			return xpidlInlineCHeader;
		}

		private static XpidlTypeDef CreateXpidlTypeDef(ComplexSyntaxNode typedefSyntaxNode)
		{
			ValidateSyntaxNode(typedefSyntaxNode, RuleConstants.XpidlTypeDef);

			String typeName = CreateXpidlId((SimpleSyntaxNode)typedefSyntaxNode[2]);
			String typeType = CreateXpidlType((ComplexSyntaxNode)typedefSyntaxNode[1]);

			var xpidlTypeDef = new XpidlTypeDef(typeName, typeType);
			return xpidlTypeDef;
		}

		private static XpidlInclude CreateXpidlInclude(ComplexSyntaxNode includeSyntaxNode)
		{
			ValidateSyntaxNode(includeSyntaxNode, RuleConstants.XpidlInclude);

			String fileName = ((SimpleSyntaxNode)includeSyntaxNode[2]).Text;

			var xpidlInclude = new XpidlInclude(fileName);
			return xpidlInclude;
		}

		private static XpidlForwardDeclaration CreateXpidlForwardDeclaration(ComplexSyntaxNode forwardDeclarationSyntaxNode)
		{
			ValidateSyntaxNode(forwardDeclarationSyntaxNode, RuleConstants.XpidlForwardDeclaration);

			String interfaceName = CreateXpidlId((SimpleSyntaxNode)forwardDeclarationSyntaxNode[1]);

			var xpidlForwardDeclaration = new XpidlForwardDeclaration(interfaceName);
			return xpidlForwardDeclaration;
		}

		private static XpidlNativeType CreateXpidlNativeType(ComplexSyntaxNode nativeTypeSyntaxNode)
		{
			ValidateSyntaxNode(nativeTypeSyntaxNode, RuleConstants.XpidlNativeType1, RuleConstants.XpidlNativeType2);

			//TODO: Create valid XpidlNativeType node.
			var xpidlNativeType = new XpidlNativeType();
			return xpidlNativeType;
		}

		private static XpidlInterface CreateXpidlInterface(ComplexSyntaxNode interfaceSyntaxNode)
		{
			ValidateSyntaxNode(
				interfaceSyntaxNode, 
				RuleConstants.XpidlInterface1,
				RuleConstants.XpidlInterface2,
				RuleConstants.XpidlInterface3,
				RuleConstants.XpidlInterface4);

			String interfaceName = CreateXpidlId((SimpleSyntaxNode)interfaceSyntaxNode[2, true]);
			Guid uuid = CreateInterfaceUuid((ComplexSyntaxNode)interfaceSyntaxNode[0]);
			XpidlInterfaceModifier interfaceModifiers = CreateXpidlInterfaceModifiers((ComplexSyntaxNode)interfaceSyntaxNode[0]);
			String baseInterfaceName = null;

			switch ((RuleConstants)interfaceSyntaxNode.Rule.Index)
			{
				case RuleConstants.XpidlInterface1:
				case RuleConstants.XpidlInterface2:
					baseInterfaceName = CreateXpidlId((SimpleSyntaxNode)interfaceSyntaxNode[4, true]);
					break;
			}

			var xpidlInterface = new XpidlInterface(interfaceName, uuid, interfaceModifiers, baseInterfaceName);
			BuildXpidlInterface(xpidlInterface, interfaceSyntaxNode);
			return xpidlInterface;
		}

		private static void BuildXpidlInterface(XpidlInterface xpidlInterface, ComplexSyntaxNode interfaceSyntaxNode)
		{
			foreach (SyntaxNode syntaxNode in interfaceSyntaxNode)
			{
				if (syntaxNode is CommentSyntaxNode)
				{
					var commentSyntaxNode = (CommentSyntaxNode)syntaxNode;
					if (commentSyntaxNode.IsInlineCHeader)
					{
						XpidlInlineCHeader xpidlInlineCHeader = CreateXpidlInlineCHeader(commentSyntaxNode);
						xpidlInterface.AddNode(xpidlInlineCHeader);
					}
					else
					{
						XpidlComment xpidlComment = CreateXpidlComment(commentSyntaxNode);
						xpidlInterface.AddNode(xpidlComment);
					}
				}
				else if (syntaxNode is ComplexSyntaxNode)
				{
					var complexSyntaxNode = (ComplexSyntaxNode)syntaxNode;
					switch ((RuleConstants)complexSyntaxNode.Rule.Index)
					{
						case RuleConstants.XpidlInterfaceMembers1:
						case RuleConstants.XpidlInterfaceMembers2:
						case RuleConstants.XpidlInterfaceMemberConstant:
						case RuleConstants.XpidlInterfaceMemberAttribute:
						case RuleConstants.XpidlInterfaceMemberMethod:
							BuildXpidlInterface(xpidlInterface, complexSyntaxNode);
							break;
						case RuleConstants.XpidlConstant:
							XpidlConstant xpidlConstant = CreateXpidlConstant(complexSyntaxNode);
							xpidlInterface.AddNode(xpidlConstant);
							break;
						case RuleConstants.XpidlAttribute1:
						case RuleConstants.XpidlAttribute2:
						case RuleConstants.XpidlAttribute3:
						case RuleConstants.XpidlAttribute4:
							XpidlAttribute xpidlAttribute = CreateXpidlAttribute(complexSyntaxNode);
							xpidlInterface.AddNode(xpidlAttribute);
							break;
						case RuleConstants.XpidlMethod1:
						case RuleConstants.XpidlMethod2:
						case RuleConstants.XpidlMethod3:
						case RuleConstants.XpidlMethod4:
						case RuleConstants.XpidlMethod5:
						case RuleConstants.XpidlMethod6:
						case RuleConstants.XpidlMethod7:
						case RuleConstants.XpidlMethod8:
							XpidlMethod xpidlMethod = CreateXpidlMethod(complexSyntaxNode);
							xpidlInterface.AddNode(xpidlMethod);
							break;
						case RuleConstants.XpidlInterfaceModifiersDecl1:
						case RuleConstants.XpidlInterfaceModifiersDecl2:
							break;
						default:
							throw new ArgumentException(InvalidSyntaxNode, "interfaceSyntaxNode");
					}
				}
				else
				{
					var simpleSyntaxNode = (SimpleSyntaxNode)syntaxNode;
					ValidateSyntaxNode(
						simpleSyntaxNode,
						SymbolConstants.Interface,
						SymbolConstants.XpidlId,
						SymbolConstants.Colon,
						SymbolConstants.LBrace,
						SymbolConstants.RBrace,
						SymbolConstants.Semicolon);
				}
			}
		}

		private static Guid CreateInterfaceUuid(ComplexSyntaxNode interfaceModifiersSyntaxNode)
		{
			ValidateSyntaxNode(interfaceModifiersSyntaxNode);

			ComplexSyntaxNode uuidSyntaxNode;
			switch ((RuleConstants)interfaceModifiersSyntaxNode.Rule.Index)
			{
				case RuleConstants.XpidlInterfaceModifiersDecl1:
					uuidSyntaxNode = (ComplexSyntaxNode)interfaceModifiersSyntaxNode[3];
					break;
				case RuleConstants.XpidlInterfaceModifiersDecl2:
					uuidSyntaxNode = (ComplexSyntaxNode)interfaceModifiersSyntaxNode[1];
					break;
				default:
					throw new ArgumentException(InvalidSyntaxNode, "interfaceModifiersSyntaxNode");
			}
			ValidateSyntaxNode(uuidSyntaxNode, RuleConstants.XpidlInterfaceUuid);

			var uuid = new Guid(((SimpleSyntaxNode)uuidSyntaxNode[2]).Text);
			return uuid;
		}

		private static XpidlInterfaceModifier CreateXpidlInterfaceModifiers(ComplexSyntaxNode interfaceModifiersSyntaxNode)
		{
			if (interfaceModifiersSyntaxNode == null)
			{
				throw new ArgumentNullException("interfaceModifiersSyntaxNode");
			}

			var interfaceModifiers = XpidlInterfaceModifier.None;

			for (Int32 i = 0; i < interfaceModifiersSyntaxNode.Count; ++i)
			{
				SyntaxNode syntaxNode = interfaceModifiersSyntaxNode[i];
				if (syntaxNode is ComplexSyntaxNode)
				{
					var complexSyntaxNode = (ComplexSyntaxNode) syntaxNode;
					switch ((RuleConstants)complexSyntaxNode.Rule.Index)
					{
						case RuleConstants.XpidlInterfaceModifiersList1:
						case RuleConstants.XpidlInterfaceModifiersList2:
							interfaceModifiers |= CreateXpidlInterfaceModifiers(complexSyntaxNode);
							break;
						case RuleConstants.XpidlInterfaceModifierScriptable:
							interfaceModifiers |= XpidlInterfaceModifier.Scriptable;
							break;
						case RuleConstants.XpidlInterfaceModifierFunction:
							interfaceModifiers |= XpidlInterfaceModifier.Function;
							break;
						case RuleConstants.XpidlInterfaceModifierObject:
							interfaceModifiers |= XpidlInterfaceModifier.Object;
							break;
						case RuleConstants.XpidlInterfaceModifierNotXpcom:
							interfaceModifiers |= XpidlInterfaceModifier.NotXpcom;
							break;
						case RuleConstants.XpidlInterfaceModifierNoScript:
							interfaceModifiers |= XpidlInterfaceModifier.NoScript;
							break;
						case RuleConstants.XpidlInterfaceUuid:
							break;
						default:
							throw new ArgumentException(InvalidSyntaxNode, "interfaceModifiersSyntaxNode");
					}
				}
				else if (syntaxNode is SimpleSyntaxNode)
				{
					var simpleSyntaxNode = (SimpleSyntaxNode) syntaxNode;
					ValidateSyntaxNode(simpleSyntaxNode, SymbolConstants.LBracket, SymbolConstants.RBracket, SymbolConstants.Comma);
				}
				else
				{
					throw new ArgumentException(InvalidSyntaxNode, "interfaceModifiersSyntaxNode");
				}
			}

			return interfaceModifiers;
		}

		private static XpidlConstant CreateXpidlConstant(ComplexSyntaxNode constantSyntaxNode)
		{
			ValidateSyntaxNode(constantSyntaxNode, RuleConstants.XpidlConstant);

			String constName = CreateXpidlId((SimpleSyntaxNode)constantSyntaxNode[2]);
			String constType = CreateXpidlType((ComplexSyntaxNode)constantSyntaxNode[1]);
			Expression constValue = CreateExpression((ComplexSyntaxNode)constantSyntaxNode[4]);

			var xpidlConstant = new XpidlConstant(constName, constType, constValue);
			return xpidlConstant;
		}

		private static Expression CreateExpression(ComplexSyntaxNode expressionSyntaxNode)
		{
			ValidateSyntaxNode(expressionSyntaxNode);

			switch ((RuleConstants)expressionSyntaxNode.Rule.Index)
			{
				case RuleConstants.XpidlExpression:
				case RuleConstants.XpidlExpressionOr2:
				case RuleConstants.XpidlExpressionXor2:
				case RuleConstants.XpidlExpressionAnd2:
				case RuleConstants.XpidlExpressionShift3:
				case RuleConstants.XpidlExpressionAdd3:
				case RuleConstants.XpidlExpressionMult3:
				case RuleConstants.XpidlExpressionUnary3:
					return CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[0]);

				case RuleConstants.XpidlExpressionOr1:
					return Expression.Or(
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[0]),
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[2]));

				case RuleConstants.XpidlExpressionXor1:
					return Expression.ExclusiveOr(
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[0]),
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[2]));

				case RuleConstants.XpidlExpressionAnd1:
					return Expression.And(
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[0]),
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[2]));

				case RuleConstants.XpidlExpressionShift1:
					return Expression.LeftShift(
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[0]),
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[2]));

				case RuleConstants.XpidlExpressionShift2:
					return Expression.RightShift(
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[0]),
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[2]));

				case RuleConstants.XpidlExpressionAdd1:
					return Expression.Add(
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[0]),
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[2]));

				case RuleConstants.XpidlExpressionAdd2:
					return Expression.Subtract(
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[0]),
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[2]));

				case RuleConstants.XpidlExpressionMult1:
					return Expression.Multiply(
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[0]),
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[2]));

				case RuleConstants.XpidlExpressionMult2:
					return Expression.Divide(
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[0]),
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[2]));

				case RuleConstants.XpidlExpressionUnary1:
					return Expression.Negate(
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[1]));

				case RuleConstants.XpidlExpressionUnary2:
					return Expression.Not(
						CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[1]));

				case RuleConstants.XpidlExpressionOperand1:
					var decSimpleSyntaxNode = (SimpleSyntaxNode)expressionSyntaxNode[0];
					ValidateSyntaxNode(decSimpleSyntaxNode, SymbolConstants.DecLiteral);

					Int32 decValue = Int32.Parse(decSimpleSyntaxNode.Text);
					return Expression.Constant(decValue);

				case RuleConstants.XpidlExpressionOperand2:
					var hexSimpleSyntaxNode = (SimpleSyntaxNode)expressionSyntaxNode[0];
					ValidateSyntaxNode(hexSimpleSyntaxNode, SymbolConstants.HexLiteral);

					UInt32 hexValue = Convert.ToUInt32(hexSimpleSyntaxNode.Text, 16);
					return Expression.Constant(hexValue);

				case RuleConstants.XpidlExpressionOperand3:
					var constSimpleSyntaxNode = (SimpleSyntaxNode)expressionSyntaxNode[0];
					ValidateSyntaxNode(constSimpleSyntaxNode, SymbolConstants.XpidlId);

					String fieldName = constSimpleSyntaxNode.Text;
					return Expression.Property(Expression.Constant(fieldName), "Length");

				case RuleConstants.XpidlExpressionOperand4:
					return CreateExpression((ComplexSyntaxNode)expressionSyntaxNode[1]);

				default:
					throw new ArgumentException(InvalidSyntaxNode, "expressionSyntaxNode");
			}
		}

		private static XpidlAttribute CreateXpidlAttribute(ComplexSyntaxNode attributeSyntaxNode)
		{
			ValidateSyntaxNode(
				attributeSyntaxNode,
				RuleConstants.XpidlAttribute1,
				RuleConstants.XpidlAttribute2,
				RuleConstants.XpidlAttribute3,
				RuleConstants.XpidlAttribute4);

			String attrName = null;
			String attrType = null;
			Boolean attrReadonly = false;
			XpidlModifiers<XpidlMethodModifier> attrModifier = null;

			switch ((RuleConstants)attributeSyntaxNode.Rule.Index)
			{
				case RuleConstants.XpidlAttribute1:
					attrName = CreateAttrName((ComplexSyntaxNode)attributeSyntaxNode[2]);
					attrType = CreateXpidlType((ComplexSyntaxNode)attributeSyntaxNode[1]);
					break;
				case RuleConstants.XpidlAttribute2:
					attrName = CreateAttrName((ComplexSyntaxNode)attributeSyntaxNode[3]);
					attrType = CreateXpidlType((ComplexSyntaxNode)attributeSyntaxNode[2]);
					attrReadonly = true;
					break;
				case RuleConstants.XpidlAttribute3:
					attrName = CreateAttrName((ComplexSyntaxNode)attributeSyntaxNode[3]);
					attrType = CreateXpidlType((ComplexSyntaxNode)attributeSyntaxNode[2]);
					attrModifier = CreateXpidlMethodModifiers((ComplexSyntaxNode)attributeSyntaxNode[0]);
					break;
				case RuleConstants.XpidlAttribute4:
					attrName = CreateAttrName((ComplexSyntaxNode)attributeSyntaxNode[4]);
					attrType = CreateXpidlType((ComplexSyntaxNode)attributeSyntaxNode[3]);
					attrReadonly = true;
					attrModifier = CreateXpidlMethodModifiers((ComplexSyntaxNode)attributeSyntaxNode[0]);
					break;
			}

			var xpidlAttribute = new XpidlAttribute(attrName, attrType, attrReadonly, attrModifier);
			return xpidlAttribute;
		}

		private static String CreateAttrName(ComplexSyntaxNode attrNameSyntaxNode)
		{
			ValidateSyntaxNode(
				attrNameSyntaxNode,
				RuleConstants.XpidlAttributeName1,
				RuleConstants.XpidlAttributeName2,
				RuleConstants.XpidlAttributeName3);

			String attrName = ((SimpleSyntaxNode)attrNameSyntaxNode[0]).Text;
			return attrName;
		}

		private static XpidlMethod CreateXpidlMethod(ComplexSyntaxNode methodSyntaxNode)
		{
			ValidateSyntaxNode(
				methodSyntaxNode,
				RuleConstants.XpidlMethod1,
				RuleConstants.XpidlMethod2,
				RuleConstants.XpidlMethod3,
				RuleConstants.XpidlMethod4,
				RuleConstants.XpidlMethod5,
				RuleConstants.XpidlMethod6,
				RuleConstants.XpidlMethod7,
				RuleConstants.XpidlMethod8);

			String methodName = null;
			String methodType = null;
			XpidlModifiers<XpidlMethodModifier> methodModifiers = null;
			IEnumerable<XpidlMethodParameter> methodParameters = null;

			switch ((RuleConstants)methodSyntaxNode.Rule.Index)
			{
				case RuleConstants.XpidlMethod1:
					methodName = ((SimpleSyntaxNode)methodSyntaxNode[1]).Text;
					methodType = CreateXpidlType((ComplexSyntaxNode)methodSyntaxNode[0]);
					methodParameters = CreateXpidlMethodParameters((ComplexSyntaxNode)methodSyntaxNode[3]);
					break;
				case RuleConstants.XpidlMethod2:
					methodName = ((SimpleSyntaxNode)methodSyntaxNode[1]).Text;
					methodType = CreateXpidlType((ComplexSyntaxNode)methodSyntaxNode[0]);
					break;
				case RuleConstants.XpidlMethod3:
					methodName = ((SimpleSyntaxNode)methodSyntaxNode[2]).Text;
					methodType = CreateXpidlType((ComplexSyntaxNode)methodSyntaxNode[1]);
					methodModifiers = CreateXpidlMethodModifiers((ComplexSyntaxNode)methodSyntaxNode[0]);
					methodParameters = CreateXpidlMethodParameters((ComplexSyntaxNode)methodSyntaxNode[4]);
					break;
				case RuleConstants.XpidlMethod4:
					methodName = ((SimpleSyntaxNode)methodSyntaxNode[2]).Text;
					methodType = CreateXpidlType((ComplexSyntaxNode)methodSyntaxNode[1]);
					methodModifiers = CreateXpidlMethodModifiers((ComplexSyntaxNode)methodSyntaxNode[0]);
					break;
				case RuleConstants.XpidlMethod5:
					methodName = ((SimpleSyntaxNode)methodSyntaxNode[1]).Text;
					methodType = CreateXpidlType((ComplexSyntaxNode)methodSyntaxNode[0]);
					methodParameters = CreateXpidlMethodParameters((ComplexSyntaxNode)methodSyntaxNode[3]);
					break;
				case RuleConstants.XpidlMethod6:
					methodName = ((SimpleSyntaxNode)methodSyntaxNode[1]).Text;
					methodType = CreateXpidlType((ComplexSyntaxNode)methodSyntaxNode[0]);
					break;
				case RuleConstants.XpidlMethod7:
					methodName = ((SimpleSyntaxNode)methodSyntaxNode[2]).Text;
					methodType = CreateXpidlType((ComplexSyntaxNode)methodSyntaxNode[1]);
					methodModifiers = CreateXpidlMethodModifiers((ComplexSyntaxNode)methodSyntaxNode[0]);
					methodParameters = CreateXpidlMethodParameters((ComplexSyntaxNode)methodSyntaxNode[4]);
					break;
				case RuleConstants.XpidlMethod8:
					methodName = ((SimpleSyntaxNode)methodSyntaxNode[2]).Text;
					methodType = CreateXpidlType((ComplexSyntaxNode)methodSyntaxNode[1]);
					methodModifiers = CreateXpidlMethodModifiers((ComplexSyntaxNode)methodSyntaxNode[0]);
					break;
			}

			var xpidlMethod = new XpidlMethod(methodName, methodType, methodModifiers, methodParameters);
			return xpidlMethod;
		}

		private static XpidlModifiers<XpidlMethodModifier> CreateXpidlMethodModifiers(ComplexSyntaxNode methodModifiersSyntaxNode)
		{
			ValidateSyntaxNode(methodModifiersSyntaxNode, RuleConstants.XpidlMethodModifiersDecl);

			var methodModifiers = new XpidlModifiers<XpidlMethodModifier>();
			BuildXpidlMethodModifiers(methodModifiers, methodModifiersSyntaxNode);
			return methodModifiers;
		}

		private static void BuildXpidlMethodModifiers(XpidlModifiers<XpidlMethodModifier> modifiers, ComplexSyntaxNode methodModifiersSyntaxNode)
		{
			for (Int32 i = 0; i < methodModifiersSyntaxNode.Count; ++i)
			{
				SyntaxNode syntaxNode = methodModifiersSyntaxNode[i];
				if (syntaxNode is ComplexSyntaxNode)
				{
					var complexSyntaxNode = (ComplexSyntaxNode)syntaxNode;
					switch ((RuleConstants)complexSyntaxNode.Rule.Index)
					{
						case RuleConstants.XpidlMethodModifiersDecl:
						case RuleConstants.XpidlMethodModifiersList1:
						case RuleConstants.XpidlMethodModifiersList2:
							BuildXpidlMethodModifiers(modifiers, complexSyntaxNode);
							break;
						case RuleConstants.XpidlMethodModifier1:
							modifiers.Add(XpidlMethodModifier.NoScript);
							break;
						case RuleConstants.XpidlMethodModifier2:
							modifiers.Add(XpidlMethodModifier.NotXpcom);
							break;
						case RuleConstants.XpidlMethodModifier3:
							var simpleSyntaxNode = (SimpleSyntaxNode)complexSyntaxNode[2];
							modifiers.Add(XpidlMethodModifier.BinaryName, simpleSyntaxNode.Text);
							break;
						default:
							throw new ArgumentException(InvalidSyntaxNode, "methodModifiersSyntaxNode");
					}
				}
				else if (syntaxNode is SimpleSyntaxNode)
				{
					var simpleSyntaxNode = (SimpleSyntaxNode)syntaxNode;
					ValidateSyntaxNode(
						simpleSyntaxNode,
						SymbolConstants.LBracket,
						SymbolConstants.RBracket,
						SymbolConstants.Comma);
				}
				else
				{
					throw new ArgumentException(InvalidSyntaxNode, "methodModifiersSyntaxNode");
				}
			}
		}

		private static List<XpidlMethodParameter> CreateXpidlMethodParameters(ComplexSyntaxNode methodParametersSyntaxNode)
		{
			var methodParams = new List<XpidlMethodParameter>(0);

			switch ((RuleConstants)methodParametersSyntaxNode.Rule.Index)
			{
				case RuleConstants.XpidlMethodParamsList1:
				case RuleConstants.XpidlMethodParamsList2:
					for (Int32 i = 0; i < methodParametersSyntaxNode.Count; ++i)
					{
						SyntaxNode syntaxNode = methodParametersSyntaxNode[i];
						if (syntaxNode is ComplexSyntaxNode)
						{
							methodParams.AddRange(CreateXpidlMethodParameters((ComplexSyntaxNode)syntaxNode));
						}
					}
					break;
				case RuleConstants.XpidlMethodParam1:
				case RuleConstants.XpidlMethodParam2:
					XpidlMethodParameter methodParameter = CreateXpidlMethodParameter(methodParametersSyntaxNode);
					methodParams.Add(methodParameter);
					break;
			}

			return methodParams;
		}

		private static XpidlMethodParameter CreateXpidlMethodParameter(ComplexSyntaxNode parameterSyntaxNode)
		{
			String paramName;
			String paramType;
			XpidlParameterDirection paramDirection;
			XpidlModifiers<XpidlParamModifier> paramModifiers = null;

			switch ((RuleConstants)parameterSyntaxNode.Rule.Index)
			{
				case RuleConstants.XpidlMethodParam1:
					paramName = CreateMethodParamName((ComplexSyntaxNode)parameterSyntaxNode[2]);
					paramType = CreateXpidlType((ComplexSyntaxNode)parameterSyntaxNode[1]);
					paramDirection = CreateMethodParamDirection((ComplexSyntaxNode)parameterSyntaxNode[0]);
					break;
				case RuleConstants.XpidlMethodParam2:
					paramName = CreateMethodParamName((ComplexSyntaxNode)parameterSyntaxNode[3]);
					paramType = CreateXpidlType((ComplexSyntaxNode)parameterSyntaxNode[2]);
					paramDirection = CreateMethodParamDirection((ComplexSyntaxNode)parameterSyntaxNode[1]);
					paramModifiers = CreateMethodParamModifiers((ComplexSyntaxNode)parameterSyntaxNode[0]);
					break;
				default:
					throw new ArgumentException(InvalidSyntaxNode, "parameterSyntaxNode");
			}

			var methodParam = new XpidlMethodParameter(paramName, paramType, paramDirection, paramModifiers);
			return methodParam;
		}

		private static String CreateMethodParamName(ComplexSyntaxNode paramNameSyntaxNode)
		{
			ValidateSyntaxNode(
				paramNameSyntaxNode,
				RuleConstants.XpidlParamName1,
				RuleConstants.XpidlParamName2,
				RuleConstants.XpidlParamName3,
				RuleConstants.XpidlParamName4,
				RuleConstants.XpidlParamName5,
				RuleConstants.XpidlParamName6);

			String paramName = ((SimpleSyntaxNode)paramNameSyntaxNode[0]).Text;
			return paramName;
		}

		private static XpidlParameterDirection CreateMethodParamDirection(ComplexSyntaxNode paramDirectionSyntaxNode)
		{
			ValidateSyntaxNode(paramDirectionSyntaxNode);

			switch ((RuleConstants)paramDirectionSyntaxNode.Rule.Index)
			{
				case RuleConstants.XpidlParamDirectionIn:
					return XpidlParameterDirection.In;
				case RuleConstants.XpidlParamDirectionOut:
					return XpidlParameterDirection.Out;
				case RuleConstants.XpidlParamDirectionInOut:
					return XpidlParameterDirection.In | XpidlParameterDirection.Out;
				default:
					throw new ArgumentException(InvalidSyntaxNode, "paramDirectionSyntaxNode");
			}
		}

		private static XpidlModifiers<XpidlParamModifier> CreateMethodParamModifiers(ComplexSyntaxNode paramModifiersSyntaxNode)
		{
			ValidateSyntaxNode(paramModifiersSyntaxNode, RuleConstants.XpidlMethodParamModifiersDecl);

			var paramModifiers = new XpidlModifiers<XpidlParamModifier>();
			BuildMethodParamModifiers(paramModifiers, paramModifiersSyntaxNode);
			return paramModifiers;
		}

		private static void BuildMethodParamModifiers(XpidlModifiers<XpidlParamModifier> modifiers, ComplexSyntaxNode paramModifiersSyntaxNode)
		{
			for (Int32 i = 0; i < paramModifiersSyntaxNode.Count; ++i)
			{
				SyntaxNode syntaxNode = paramModifiersSyntaxNode[i];
				if (syntaxNode is ComplexSyntaxNode)
				{
					var complexSyntaxNode = (ComplexSyntaxNode)syntaxNode;
					switch ((RuleConstants)complexSyntaxNode.Rule.Index)
					{
						case RuleConstants.XpidlMethodParamModifiersDecl:
						case RuleConstants.XpidlMethodParamModifiersList1:
						case RuleConstants.XpidlMethodParamModifiersList2:
							BuildMethodParamModifiers(modifiers, complexSyntaxNode);
							break;
						case RuleConstants.XpidlParamModifierArray:
							modifiers.Add(XpidlParamModifier.Array);
							break;
						case RuleConstants.XpidlParamModifierSizeIs:
							var paramNameSyntaxNode = (ComplexSyntaxNode)complexSyntaxNode[2];
							modifiers.Add(XpidlParamModifier.SizeIs, ((SimpleSyntaxNode)paramNameSyntaxNode[0]).Text);
							break;
						case RuleConstants.XpidlParamModifierIidIs:
							paramNameSyntaxNode = (ComplexSyntaxNode)complexSyntaxNode[2];
							modifiers.Add(XpidlParamModifier.IidIs, ((SimpleSyntaxNode)paramNameSyntaxNode[0]).Text);
							break;
						case RuleConstants.XpidlParamModifierRetVal:
							modifiers.Add(XpidlParamModifier.RetVal);
							break;
						case RuleConstants.XpidlParamModifierConst:
							modifiers.Add(XpidlParamModifier.Const);
							break;
						case RuleConstants.XpidlParamModifierShared:
							modifiers.Add(XpidlParamModifier.Shared);
							break;
						case RuleConstants.XpidlParamModifierOptional:
							modifiers.Add(XpidlParamModifier.Optional);
							break;
						default:
							throw new ArgumentException(InvalidSyntaxNode, "paramModifiersSyntaxNode");
					}
				}
				else if (syntaxNode is SimpleSyntaxNode)
				{
					var simpleSyntaxNode = (SimpleSyntaxNode)syntaxNode;
					ValidateSyntaxNode(
						simpleSyntaxNode,
						SymbolConstants.LBracket,
						SymbolConstants.RBracket,
						SymbolConstants.Comma);
				}
				else
				{
					throw new ArgumentException(InvalidSyntaxNode, "paramModifiersSyntaxNode");
				}
			}
		}

		private static String CreateXpidlId(SimpleSyntaxNode idSyntaxNode)
		{
			ValidateSyntaxNode(idSyntaxNode, SymbolConstants.XpidlId);

			String xpidlId = idSyntaxNode.Text;
			return xpidlId;
		}

		private static String CreateXpidlType(ComplexSyntaxNode typeSyntaxNode)
		{
			ValidateSyntaxNode(
				typeSyntaxNode,
				RuleConstants.XpidlTypeBoolean,
				RuleConstants.XpidlTypeVoid,
				RuleConstants.XpidlTypeString,
				RuleConstants.XpidlTypeOctet,
				RuleConstants.XpidlTypeShort,
				RuleConstants.XpidlTypeLong,
				RuleConstants.XpidlTypeLongLong,
				RuleConstants.XpidlTypeUShort,
				RuleConstants.XpidlTypeULong,
				RuleConstants.XpidlTypeULongLong,
				RuleConstants.XpidlTypeFloat,
				RuleConstants.XpidlTypeDouble,
				RuleConstants.XpidlTypeChar,
				RuleConstants.XpidlTypeWChar,
				RuleConstants.XpidlTypeWString,
				RuleConstants.XpidlTypeAString,
				RuleConstants.XpidlTypeACString,
				RuleConstants.XpidlTypeAUTF8String,
				RuleConstants.XpidlTypeDOMString,
				RuleConstants.XpidlTypeId);

			String xpidlType = ((SimpleSyntaxNode)typeSyntaxNode[0]).Text;
			return xpidlType;
		}

		private static void ValidateSyntaxNode(SimpleSyntaxNode simpleSyntaxNode, params SymbolConstants[] symbolConstants)
		{
			if (simpleSyntaxNode == null)
			{
				throw new ArgumentNullException("simpleSyntaxNode");
			}

			if ((simpleSyntaxNode.Symbol == null) ||
				((symbolConstants != null) &&
				 (symbolConstants.Length > 0) &&
				 !symbolConstants.Contains((SymbolConstants)simpleSyntaxNode.Symbol.Index)))
			{
				throw new ArgumentException(InvalidSyntaxNode, "simpleSyntaxNode");
			}
		}

		private static void ValidateSyntaxNode(ComplexSyntaxNode complexSyntaxNode, params RuleConstants[] ruleConstants)
		{
			if (complexSyntaxNode == null)
			{
				throw new ArgumentNullException("complexSyntaxNode");
			}

			if ((complexSyntaxNode.Rule == null) ||
				((ruleConstants != null) &&
				 (ruleConstants.Length > 0) &&
				 !ruleConstants.Contains((RuleConstants)complexSyntaxNode.Rule.Index)))
			{
				throw new ArgumentException(InvalidSyntaxNode, "complexSyntaxNode");
			}
		}

		private const String InvalidSyntaxNode = "Invalid syntax node.";
	}
}
