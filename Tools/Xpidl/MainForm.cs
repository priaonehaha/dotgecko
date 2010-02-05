using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using XPIDL.Parser;
using XPIDL.Parser.Gold;

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
					using (var xpidlTextReader = new StreamReader(openDialog.FileName, Encoding.UTF8))
					{
						xpidlParser.Parse(xpidlTextReader);
					}
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
