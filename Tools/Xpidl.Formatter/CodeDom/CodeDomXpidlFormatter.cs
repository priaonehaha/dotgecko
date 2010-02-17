using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using Xpidl.Parser;

namespace Xpidl.Formatter.CodeDom
{
	public sealed partial class CodeDomXpidlFormatter : IXpidlFormatter
	{
		public CodeDomXpidlFormatter(String language, CodeGeneratorOptions options)
		{
			if (language == null)
			{
				throw new ArgumentNullException("language");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (!CodeDomProvider.IsDefinedLanguage(language))
			{
				throw new ArgumentException("Language is not defined.", "language");
			}
			m_Language = language;
			m_Options = options;
		}

		public void Format(XpidlFile xpidlFile, TextWriter textWriter)
		{
			using (var codeDomProvider = CodeDomProvider.CreateProvider(Language))
			{
				CodeNamespace codeNamespace = CreateCodeNamespace(xpidlFile);
				codeDomProvider.GenerateCodeFromNamespace(codeNamespace, textWriter, Options);
			}
		}

		public String Language
		{
			get { return m_Language; }
		}

		public CodeGeneratorOptions Options
		{
			get { return m_Options; }
		}

		private readonly String m_Language;
		private readonly CodeGeneratorOptions m_Options;
	}
}
