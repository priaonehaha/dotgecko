using System.CodeDom;

namespace Xpidl.Formatter.CodeDom
{
	internal static class CodeDomExtensions
	{
		public static void AddAttribute(this CodeAttributeDeclarationCollection codeAttributeDeclarationCollection, CodeAttributeDeclaration codeAttributeDeclaration)
		{
			if (codeAttributeDeclaration != null)
			{
				codeAttributeDeclarationCollection.Add(codeAttributeDeclaration);
			}
		}
	}
}
