using System;
using System.IO;
using System.Windows.Forms;
using DotGecko.Gecko;
using DotGecko.SampleWinForms.Properties;
using WebBrowser = DotGecko.Gecko.WebBrowser;

namespace DotGecko.SampleWinForms
{
	static class Program
	{
		private sealed class BrowserFileLocation : AppFileLocation
		{
			static BrowserFileLocation()
			{
				Settings settings = Settings.Default;
				ms_ProfileDir = Path.GetFullPath(settings.ProfileDir);
			}

			public override String ProfileDirectory
			{
				get { return ms_ProfileDir; }
			}

			private static readonly String ms_ProfileDir;
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			InitGeckoEmbedding();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());

			TermGeckoEmbedding();
		}

		private static void InitGeckoEmbedding()
		{
			Settings settings = Settings.Default;

			String xulrunnerDir = Path.GetFullPath(settings.XulRunnerDir);
			var browserFileLocation = new BrowserFileLocation();

			WebBrowser.InitEmbedding(
				xulrunnerDir, browserFileLocation,
				settings.UseWindowCreator,
				settings.UsePromptService,
				settings.UseFilePicker,
				settings.UseHelperAppLauncherDialog,
				settings.UseTooltipTextProvider);
		}

		private static void TermGeckoEmbedding()
		{
			WebBrowser.TermEmbedding();
		}
	}
}
