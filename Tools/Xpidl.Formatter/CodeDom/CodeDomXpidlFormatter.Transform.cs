using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using Xpidl.Parser;

namespace Xpidl.Formatter.CodeDom
{
	public sealed partial class CodeDomXpidlFormatter
	{
		private static CodeNamespace CreateCodeNamespace(XpidlFile xpidlFile)
		{
			var codeNamespace = new CodeNamespace("Xpcom.Interop");
			codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Runtime.InteropServices"));

			// Add type declarations to namespace
			BuildCodeNamespace(codeNamespace, xpidlFile);

			return codeNamespace;
		}

		private static void BuildCodeNamespace(CodeNamespace codeNamespace, XpidlFile xpidlFile)
		{
			foreach (XpidlNode xpidlNode in xpidlFile.ChildNodes)
			{
				//TODO: collect interface's comments
				if (xpidlNode is XpidlInterface)
				{
					var xpidlInterface = (XpidlInterface) xpidlNode;

					CodeTypeDeclaration codeConstClassDeclaration;
					CodeTypeDeclaration codeInterfaceDeclaration = CraeteCodeInterfaceDeclaration(xpidlInterface, out codeConstClassDeclaration);
					if (codeConstClassDeclaration != null)
					{
						codeNamespace.Types.Add(codeConstClassDeclaration);
					}
					codeNamespace.Types.Add(codeInterfaceDeclaration);
				}
			}
		}

		private static CodeTypeDeclaration CraeteCodeInterfaceDeclaration(XpidlInterface xpidlInterface, out CodeTypeDeclaration codeConstClassDeclaration)
		{
			// Create interface declaration
			var codeInterfaceDeclaration =
				new CodeTypeDeclaration(xpidlInterface.Name)
				{
					IsInterface = true,
					TypeAttributes = TypeAttributes.Interface | TypeAttributes.NotPublic
				};

			// Set base interface (except of nsISupports)
			if (!String.IsNullOrEmpty(xpidlInterface.BaseName) && !String.Equals(xpidlInterface.BaseName, "nsISupports"))
			{
				codeInterfaceDeclaration.BaseTypes.Add(xpidlInterface.BaseName);
			}

			// Add [ComImport] attribute
			var comImportAttributeDeclaration =
				new CodeAttributeDeclaration(
					new CodeTypeReference(typeof(ComImportAttribute)));
			codeInterfaceDeclaration.CustomAttributes.Add(comImportAttributeDeclaration);

			// Add [Guid] attribute
			var guidAttributeDeclaration =
				new CodeAttributeDeclaration(
					new CodeTypeReference(typeof(GuidAttribute)),
					new CodeAttributeArgument(new CodePrimitiveExpression(xpidlInterface.Uuid.ToString())));
			codeInterfaceDeclaration.CustomAttributes.Add(guidAttributeDeclaration);

			// Add [InterfaceType] attribute
			var interfaceTypeAttributeDeclaration =
				new CodeAttributeDeclaration(
					new CodeTypeReference(typeof(InterfaceTypeAttribute)),
					new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(ComInterfaceType)), "InterfaceIsIUnknown")));
			codeInterfaceDeclaration.CustomAttributes.Add(interfaceTypeAttributeDeclaration);

			// Create interface members and get separate class for interface constants
			codeConstClassDeclaration = BuildCodeInterfaceDeclaration(codeInterfaceDeclaration, xpidlInterface);
			return codeInterfaceDeclaration;
		}

		private static CodeTypeDeclaration BuildCodeInterfaceDeclaration(CodeTypeDeclaration codeInterfaceDeclaration, XpidlInterface xpidlInterface)
		{
			CodeTypeDeclaration codeConstClassDeclaration = null;

			foreach (XpidlNode xpidlNode in xpidlInterface.ChildNodes)
			{
				if (xpidlNode is XpidlConstant)
				{
					var xpidlConstant = (XpidlConstant) xpidlNode;

					// Create constants container class
					if (codeConstClassDeclaration == null)
					{
						codeConstClassDeclaration =
							new CodeTypeDeclaration(String.Format("{0}Constants", xpidlInterface.Name))
							{
								IsClass = true,
								TypeAttributes = TypeAttributes.Class | TypeAttributes.NotPublic | TypeAttributes.Sealed
							};
					}

					// Create constant member
					CodeMemberField codeConstantMember = CreateCodeConstantMember(xpidlConstant);
					codeConstClassDeclaration.Members.Add(codeConstantMember);
				}
				else if (xpidlNode is XpidlAttribute)
				{
					var xpidlAttribute = (XpidlAttribute) xpidlNode;

					CodeMemberMethod codeSetterMethod;
					CodeMemberMethod codeGetterMethod = CreateCodeAttributeMember(xpidlAttribute, out codeSetterMethod);
					codeInterfaceDeclaration.Members.Add(codeGetterMethod);
					if (codeSetterMethod != null)
					{
						codeInterfaceDeclaration.Members.Add(codeSetterMethod);
					}
				}
				else if (xpidlNode is XpidlMethod)
				{
					var xpidlMethod = (XpidlMethod) xpidlNode;

					CodeMemberMethod codeMethod = CreateCodeMethodMember(xpidlMethod);
					codeInterfaceDeclaration.Members.Add(codeMethod);
				}
			}

			return codeConstClassDeclaration;
		}

		private static CodeMemberField CreateCodeConstantMember(XpidlConstant xpidlConstant)
		{
			var codeConstDeclaration =
				new CodeMemberField(CreateCodeTypeReference(xpidlConstant.Type), xpidlConstant.Name)
				{
					Attributes = MemberAttributes.Const | MemberAttributes.Public,
					InitExpression = CreateCodeExpression(xpidlConstant.Value)
				};

			// If can't translate Expression to CodeExpression
			if (codeConstDeclaration.InitExpression == null)
			{
				codeConstDeclaration.Comments.Add(new CodeCommentStatement(xpidlConstant.Value.ToString()));
			}

			return codeConstDeclaration;
		}

		private static CodeExpression CreateCodeExpression(Expression expression)
		{
			switch (expression.NodeType)
			{
				case ExpressionType.Or:
					return new CodeBinaryOperatorExpression(
						CreateCodeExpression(((BinaryExpression)expression).Left),
						CodeBinaryOperatorType.BitwiseOr,
						CreateCodeExpression(((BinaryExpression)expression).Right));

				case ExpressionType.And:
					return new CodeBinaryOperatorExpression(
						CreateCodeExpression(((BinaryExpression)expression).Left),
						CodeBinaryOperatorType.BitwiseAnd,
						CreateCodeExpression(((BinaryExpression)expression).Right));

				case ExpressionType.Add:
					return new CodeBinaryOperatorExpression(
						CreateCodeExpression(((BinaryExpression) expression).Left),
						CodeBinaryOperatorType.Add,
						CreateCodeExpression(((BinaryExpression) expression).Right));

				case ExpressionType.Subtract:
					return new CodeBinaryOperatorExpression(
						CreateCodeExpression(((BinaryExpression) expression).Left),
						CodeBinaryOperatorType.Subtract,
						CreateCodeExpression(((BinaryExpression) expression).Right));

				case ExpressionType.Multiply:
					return new CodeBinaryOperatorExpression(
						CreateCodeExpression(((BinaryExpression) expression).Left),
						CodeBinaryOperatorType.Multiply,
						CreateCodeExpression(((BinaryExpression) expression).Right));

				case ExpressionType.Divide:
					return new CodeBinaryOperatorExpression(
						CreateCodeExpression(((BinaryExpression) expression).Left),
						CodeBinaryOperatorType.Divide,
						CreateCodeExpression(((BinaryExpression) expression).Right));

				case ExpressionType.MemberAccess:
					var memberName = (String) ((ConstantExpression) ((MemberExpression) expression).Expression).Value;
					return new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), memberName);

				case ExpressionType.Constant:
					return new CodePrimitiveExpression(((ConstantExpression) expression).Value);

				//TODO: return CodeSnippetExpression for some expressions

				default:
					return null;
			}
		}

		private static CodeMemberMethod CreateCodeAttributeMember(XpidlAttribute xpidlAttribute, out CodeMemberMethod codeSetterMethod)
		{
			// Create CodeTypeReference and custom attributes for XpidlAttribute.Type
			CodeAttributeDeclaration codeMarshalAsAttribute;
			var codeAttributeType = CreateCodeTypeReference(xpidlAttribute.Type, out codeMarshalAsAttribute);

			// Create getter method
			var codeGetterMethod =
				new CodeMemberMethod
				{
					Name = "Get" + Char.ToUpper(xpidlAttribute.Name[0]) + xpidlAttribute.Name.Substring(1)
				};
			// Create special getter for NS Strings
			switch (xpidlAttribute.Type)
			{
				case XpidlType.AString:
				case XpidlType.ACString:
				case XpidlType.AUTF8String:
				case XpidlType.DOMString:
					codeGetterMethod.ReturnType = new CodeTypeReference(typeof(void));
					var codeGetterParameter = new CodeParameterDeclarationExpression(codeAttributeType, "result");
					codeGetterParameter.CustomAttributes.AddAttribute(codeMarshalAsAttribute);
					codeGetterMethod.Parameters.Add(codeGetterParameter);
					break;
				default:
					codeGetterMethod.ReturnType = codeAttributeType;
					codeGetterMethod.ReturnTypeCustomAttributes.AddAttribute(codeMarshalAsAttribute);
					break;
			}

			// Create setter method for read-write attribute
			if (xpidlAttribute.IsReadOnly)
			{
				codeSetterMethod = null;
			}
			else
			{
				codeSetterMethod =
					new CodeMemberMethod
					{
						Name = "Set" + Char.ToUpper(xpidlAttribute.Name[0]) + xpidlAttribute.Name.Substring(1),
						ReturnType = new CodeTypeReference(typeof(void))
					};
				var codeSetterParameter = new CodeParameterDeclarationExpression(codeAttributeType, "value");
				codeSetterParameter.CustomAttributes.AddAttribute(codeMarshalAsAttribute);
				codeSetterMethod.Parameters.Add(codeSetterParameter);
			}

			return codeGetterMethod;
		}

		private static CodeMemberMethod CreateCodeMethodMember(XpidlMethod xpidlMethod)
		{
			var codeMethodMember =
				new CodeMemberMethod { Name = Char.ToUpper(xpidlMethod.Name[0]) + xpidlMethod.Name.Substring(1) };

			CodeParameterDeclarationExpression[] codeParameters = CreateMethodParameters(xpidlMethod);
			codeMethodMember.Parameters.AddRange(codeParameters);

			// Create CodeTypeReference and custom attributes for XpidlMethod return type
			CodeAttributeDeclaration codeMarshalAsAttribute;
			CodeTypeReference codeMethodType = CreateCodeTypeReference(xpidlMethod.Type, out codeMarshalAsAttribute);
			switch (xpidlMethod.Type)
			{
				case XpidlType.AString:
				case XpidlType.ACString:
				case XpidlType.AUTF8String:
				case XpidlType.DOMString:
					codeMethodMember.ReturnType = new CodeTypeReference(typeof(void));
					var resultParameter = new CodeParameterDeclarationExpression(codeMethodType, "result");
					resultParameter.CustomAttributes.AddAttribute(codeMarshalAsAttribute);
					codeMethodMember.Parameters.Add(resultParameter);
					break;
				default:
					codeMethodMember.ReturnType = codeMethodType;
					codeMethodMember.ReturnTypeCustomAttributes.AddAttribute(codeMarshalAsAttribute);
					break;
			}

			if ((xpidlMethod.Parameters.Count > 0) && (xpidlMethod.Parameters[xpidlMethod.Parameters.Count - 1].Modifiers.Contains(XpidlParamModifier.RetVal)))
			{
				var preserveSigAttribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof(PreserveSigAttribute)));
				codeMethodMember.CustomAttributes.Add(preserveSigAttribute);
			}

			return codeMethodMember;
		}

		private static CodeParameterDeclarationExpression[] CreateMethodParameters(XpidlMethod xpidlMethod)
		{
			var codeParameters = new List<CodeParameterDeclarationExpression>(xpidlMethod.Parameters.Count);
			foreach (XpidlMethodParameter xpidlParameter in xpidlMethod.Parameters)
			{
				var codeParameter = new CodeParameterDeclarationExpression();

				switch (xpidlParameter.Direction)
				{
					case XpidlParameterDirection.In:
						switch (xpidlParameter.Type)
						{
							case XpidlType.nsIDRef:
							case XpidlType.nsIIDRef:
							case XpidlType.nsCIDRef:
								codeParameter.Direction = FieldDirection.Ref;
								break;
							default:
								codeParameter.Direction = FieldDirection.In;
								break;
						}
						break;
					case XpidlParameterDirection.Out:
						codeParameter.Direction = FieldDirection.Out;
						break;
					case XpidlParameterDirection.In | XpidlParameterDirection.Out:
						codeParameter.Direction = FieldDirection.Ref;
						break;
				}

				CodeAttributeDeclaration codeMarshalAsAttribute;
				CodeTypeReference parameterType = CreateCodeTypeReference(xpidlParameter.Type, out codeMarshalAsAttribute);

				if (xpidlParameter.Modifiers.Contains(XpidlParamModifier.Array))
				{
					CodeAttributeArgument marshalAsArgument = codeMarshalAsAttribute == null ? null : codeMarshalAsAttribute.Arguments[0];
					codeMarshalAsAttribute =
						new CodeAttributeDeclaration(
							new CodeTypeReference(typeof(MarshalAsAttribute)),
							new CodeAttributeArgument(
								new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(UnmanagedType)), "LPArray")));
					if (marshalAsArgument != null)
					{
						marshalAsArgument.Name = "ArraySubType";
						codeMarshalAsAttribute.Arguments.Add(marshalAsArgument);
					}
					parameterType = new CodeTypeReference(parameterType, 1);
				}

				if (xpidlParameter.Modifiers.Contains(XpidlParamModifier.SizeIs) && (codeMarshalAsAttribute != null))
				{
					var sizeIsArgument =
						new CodeAttributeArgument(
							"SizeParamIndex",
							new CodePrimitiveExpression(xpidlMethod.GetParameterIndex(xpidlParameter.Modifiers[XpidlParamModifier.SizeIs])));
					codeMarshalAsAttribute.Arguments.Add(sizeIsArgument);
				}

				if (xpidlParameter.Modifiers.Contains(XpidlParamModifier.IidIs) && (codeMarshalAsAttribute == null))
				{
					codeMarshalAsAttribute =
						new CodeAttributeDeclaration(
							new CodeTypeReference(typeof(MarshalAsAttribute)),
							new CodeAttributeArgument(
								new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(UnmanagedType)), "Interface")),
							new CodeAttributeArgument(
								"IidParameterIndex",
								new CodePrimitiveExpression(xpidlMethod.GetParameterIndex(xpidlParameter.Modifiers[XpidlParamModifier.IidIs]))));
					parameterType = new CodeTypeReference(typeof(Object));
				}

				codeParameter.Name = xpidlParameter.Name;
				codeParameter.Type = parameterType;
				codeParameter.CustomAttributes.AddAttribute(codeMarshalAsAttribute);

				codeParameters.Add(codeParameter);
			}
			return codeParameters.ToArray();
		}

		private static CodeTypeReference CreateCodeTypeReference(String xpidlType)
		{
			CodeAttributeDeclaration codeMarshalAsAttribute;
			return CreateCodeTypeReference(xpidlType, out codeMarshalAsAttribute);
		}

		private static CodeTypeReference CreateCodeTypeReference(String xpidlType, out CodeAttributeDeclaration codeMarshalAsAttribute)
		{
			Type clrType = null;
			codeMarshalAsAttribute = null;

			switch (xpidlType)
			{
				case XpidlType.Void:
					clrType = typeof(void);
					break;

				case XpidlType.Boolean:
				case XpidlType.PRBool:
					clrType = typeof(Boolean);
					break;

				case XpidlType.Octet:
				case XpidlType.PRUint8:
					clrType = typeof(Byte);
					break;

				case XpidlType.Short:
				case XpidlType.PRInt16:
					clrType = typeof(Int16);
					break;

				case XpidlType.Long:
				case XpidlType.PRInt32:
					clrType = typeof(Int32);
					break;

				case XpidlType.LongLong:
				case XpidlType.PRInt64:
					clrType = typeof(Int64);
					break;

				case XpidlType.UnsignedShort:
				case XpidlType.PRUint16:
				case XpidlType.PRUnichar:
					clrType = typeof(UInt16);
					break;

				case XpidlType.UnsignedLong:
				case XpidlType.PRUint32:
				case XpidlType.nsRefCnt:
				case XpidlType.nsResult:
					clrType = typeof(UInt32);
					break;

				case XpidlType.UnsignedLongLong:
				case XpidlType.PRUint64:
				case XpidlType.PRTime:
					clrType = typeof(UInt64);
					break;

				case XpidlType.Float:
					clrType = typeof(Single);
					break;

				case XpidlType.Double:
					clrType = typeof(Double);
					break;

				case XpidlType.Char:
					clrType = typeof(Byte);
					break;

				case XpidlType.WChar:
					clrType = typeof(Char);
					break;

				case XpidlType.String:
					clrType = typeof(String);
					codeMarshalAsAttribute =
						new CodeAttributeDeclaration(
							new CodeTypeReference(typeof(MarshalAsAttribute)),
							new CodeAttributeArgument(
								new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(UnmanagedType)), "LPStr")));
					break;

				case XpidlType.WString:
					clrType = typeof(String);
					codeMarshalAsAttribute =
						new CodeAttributeDeclaration(
							new CodeTypeReference(typeof(MarshalAsAttribute)),
							new CodeAttributeArgument(
								new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(UnmanagedType)), "LPWStr")));
					break;

				case XpidlType.nsQIResult:
				case XpidlType.VoidPtr:
					clrType = typeof(IntPtr);
					break;

				case XpidlType.nsIDRef:
				case XpidlType.nsIIDRef:
				case XpidlType.nsCIDRef:
				case XpidlType.nsIDPtr:
				case XpidlType.nsIIDPtr:
				case XpidlType.nsCIDPtr:
				case XpidlType.nsID:
				case XpidlType.nsIID:
				case XpidlType.nsCID:
					clrType = typeof(Guid);
					break;
			}

			var codeTypeReference = clrType != null ? new CodeTypeReference(clrType) : new CodeTypeReference(xpidlType);
			return codeTypeReference;
		}
	}
}
