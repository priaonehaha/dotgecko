using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DotGecko.Xpidl.Formatter.CodeDom;
using DotGecko.Xpidl.Parser;
using DotGecko.Xpidl.Parser.Gold;

namespace DotGecko.Xpidl
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			clbInputFiles.DisplayMember = "Name";
		}

		private static BinaryReader CreateResourceReader(String resourceName)
		{
			Assembly assembly = typeof(MainForm).Assembly;
			Stream stream = assembly.GetManifestResourceStream(resourceName);
			return new BinaryReader(stream);
		}

		private void ShowErrorBalloon(IWin32Window control, String message)
		{
			toolTip.ToolTipIcon = ToolTipIcon.Error;
			toolTip.ToolTipTitle = "Error";
			toolTip.Show(message, control, 5000);
		}

		private void btnInputFolder_Click(Object sender, EventArgs e)
		{
			using (var folderDialog =
				new FolderBrowserDialog
				{
                    RootFolder = Environment.SpecialFolder.MyComputer,
					ShowNewFolderButton = false
				})
			{
				if (folderDialog.ShowDialog(this) == DialogResult.OK)
				{
					txtInputFolder.Text = folderDialog.SelectedPath;

					clbInputFiles.BeginUpdate();
					try
					{
						var inputDirectory = new DirectoryInfo(folderDialog.SelectedPath);
						foreach (FileInfo fileInfo in inputDirectory.GetFiles("*.idl", SearchOption.TopDirectoryOnly))
						{
							clbInputFiles.Items.Add(fileInfo, true);
						}
					}
					finally
					{
						clbInputFiles.EndUpdate();
					}
				}
			}
		}

		private void btnOutputFolder_Click(Object sender, EventArgs e)
		{
			using (var folderDialog =
				new FolderBrowserDialog
				{
					RootFolder = Environment.SpecialFolder.MyComputer,
					ShowNewFolderButton = true
				})
			{
				if (folderDialog.ShowDialog(this) == DialogResult.OK)
				{
					txtOutputFolder.Text = folderDialog.SelectedPath;
				}
			}
		}

		private void lnkSelectAll_LinkClicked(Object sender, LinkLabelLinkClickedEventArgs e)
		{
			for (Int32 i = 0; i < clbInputFiles.Items.Count; i++)
			{
				clbInputFiles.SetItemChecked(i, true);
			}
		}

		private void lnkSelectNone_LinkClicked(Object sender, LinkLabelLinkClickedEventArgs e)
		{
			for (Int32 i = 0; i < clbInputFiles.Items.Count; i++)
			{
				clbInputFiles.SetItemChecked(i, false);
			}
		}

		private void lnkSelectInvert_LinkClicked(Object sender, LinkLabelLinkClickedEventArgs e)
		{
			for (Int32 i = 0; i < clbInputFiles.Items.Count; i++)
			{
				clbInputFiles.SetItemChecked(i, !clbInputFiles.GetItemChecked(i));
			}
		}

		private void btnConvert_Click(Object sender, EventArgs e)
		{
			if (!backgroundWorker.IsBusy)
			{
				if (String.IsNullOrEmpty(txtInputFolder.Text))
				{
					ShowErrorBalloon(btnInputFolder, "Input folder is not specified.");
					return;
				}

				if (clbInputFiles.Items.Count == 0)
				{
					ShowErrorBalloon(btnInputFolder, "There is no *.idl files in selected folder.");
					return;
				}

				if (String.IsNullOrEmpty(txtOutputFolder.Text))
				{
					ShowErrorBalloon(btnOutputFolder, "Output folder is not specified.");
					return;
				}

				if (clbInputFiles.CheckedItems.Count == 0)
				{
					ShowErrorBalloon(clbInputFiles, "Select at least one file to convert.");
					return;
				}

				btnInputFolder.Enabled = false;
				btnOutputFolder.Enabled = false;
				clbInputFiles.Enabled = false;
				lnkSelectAll.Enabled = false;
				lnkSelectNone.Enabled = false;
				lnkSelectInvert.Enabled = false;
				btnConvert.Text = "Stop";
				backgroundWorker.RunWorkerAsync();
			}
			else
			{
				btnConvert.Enabled = false;
				backgroundWorker.CancelAsync();
			}
		}

		private void backgroundWorker_DoWork(Object sender, DoWorkEventArgs e)
		{
			if (String.IsNullOrEmpty(txtOutputFolder.Text))
			{
				throw new ApplicationException("Output folder is not specified.");
			}

			var outputDirectory = new DirectoryInfo(txtOutputFolder.Text);
			if (!outputDirectory.Exists)
			{
				outputDirectory.Create();
			}

			// Create parser
			IXpidlParser xpidlParser;
			using (BinaryReader grammarReader = CreateResourceReader("DotGecko.Xpidl.Properties.xpidl.cgt"))
			{
				xpidlParser = new GoldXpidlParser(grammarReader);
			}

			// Create formatter
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

			if (backgroundWorker.CancellationPending)
			{
				return;
			}

			for (Int32 i = 0; i < clbInputFiles.CheckedItems.Count; ++i)
			{
				var fileInfo = (FileInfo)clbInputFiles.CheckedItems[i];
				backgroundWorker.ReportProgress(i * 100 / clbInputFiles.CheckedItems.Count, fileInfo.Name);
				try
				{
					if (backgroundWorker.CancellationPending)
					{
						return;
					}

					XpidlFile xpidlFile;
					try
					{
						using (var xpidlTextReader = fileInfo.OpenText())
						{
							xpidlFile = xpidlParser.Parse(xpidlTextReader);
						}
					}
					catch
					{
						//TODO: Register failed item
						continue;
					}

					if (backgroundWorker.CancellationPending)
					{
						return;
					}

					try
					{
						String outputPath = Path.Combine(outputDirectory.FullName, Path.ChangeExtension(fileInfo.Name, ".cs"));
						using (var textWriter = File.CreateText(outputPath))
						{
							xpidlFormatter.Format(xpidlFile, textWriter);
						}
					}
					catch
					{
						//TODO: Register failed item
						continue;
					}
				}
				finally
				{
					backgroundWorker.ReportProgress((i + 1) * 100 / clbInputFiles.CheckedItems.Count, fileInfo.Name);
				}
			}
		}

		private void backgroundWorker_ProgressChanged(Object sender, ProgressChangedEventArgs e)
		{
			prbProgress.Value = e.ProgressPercentage;
			lblCurrentFile.Text = (String) e.UserState;
		}

		private void backgroundWorker_RunWorkerCompleted(Object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			lblCurrentFile.Text = String.Empty;
			prbProgress.Value = 0;
			btnInputFolder.Enabled = true;
			btnOutputFolder.Enabled = true;
			clbInputFiles.Enabled = true;
			lnkSelectAll.Enabled = true;
			lnkSelectNone.Enabled = true;
			lnkSelectInvert.Enabled = true;
			btnConvert.Text = "Convert";
			btnConvert.Enabled = true;
		}
	}
}
