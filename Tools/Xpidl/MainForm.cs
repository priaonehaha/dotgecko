using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Xpidl.Formatter.CodeDom;
using Xpidl.Parser;
using Xpidl.Parser.Gold;

namespace Xpidl
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			using (var openDialog = new OpenFileDialog { RestoreDirectory = true, Filter = "xpidl files|*.idl" })
			{
				if (openDialog.ShowDialog(this) == DialogResult.OK)
				{
					IXpidlParser xpidlParser;
					using (BinaryReader grammarReader = CreateResourceReader("Xpidl.Properties.xpidl.cgt"))
					{
						xpidlParser = new GoldXpidlParser(grammarReader);
					}

					XpidlFile xpidlFile;
					using (var xpidlTextReader = new StreamReader(openDialog.FileName, Encoding.UTF8))
					{
						xpidlFile = xpidlParser.Parse(xpidlTextReader);
					}

					var codeGeneratorOptions =
						new CodeGeneratorOptions
						{
							BlankLinesBetweenMembers = true,
							BracingStyle = "C",
							ElseOnClosing = false,
							IndentString = "\t",
							VerbatimOrder = true
						};
					var xpidlFormatter = new CodeDomXpidlFormatter("c#", codeGeneratorOptions);
					var codeStringuilder = new StringBuilder();
					using (var textWriter = new StringWriter(codeStringuilder))
					{
						xpidlFormatter.Format(xpidlFile, textWriter);
					}
					textBoxOutput.Text = codeStringuilder.ToString();
				}
			}
		}

		private static BinaryReader CreateResourceReader(String resourceName)
		{
			Assembly assembly = typeof(MainForm).Assembly;
			Stream stream = assembly.GetManifestResourceStream(resourceName);
			return new BinaryReader(stream);
		}
	}
}
