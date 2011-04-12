using System;
using System.IO;
using System.Windows;
using DotGecko.Gecko;
using DotGecko.SampleWPF.Properties;

namespace DotGecko.SampleWPF
{
	public partial class App : Application
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

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

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

		protected override void OnExit(ExitEventArgs e)
		{
			WebBrowser.TermEmbedding();

			base.OnExit(e);
		}
	}
}
