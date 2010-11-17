using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser
	{
		public event EventHandler<PromptAppLaunchEventArgs> PromptAppLaunch
		{
			add { Events.Add(EventKey.PromptAppLaunch, value); }
			remove { Events.Remove(EventKey.PromptAppLaunch, value); }
		}

		public event EventHandler<PromptSaveToDiskEventArgs> PromptSaveToDisk
		{
			add { Events.Add(EventKey.PromptSaveToDisk, value); }
			remove { Events.Remove(EventKey.PromptSaveToDisk, value); }
		}

		[Guid("402C1476-CD8C-48DF-B8F5-7224B0EC864B")]
		private sealed class HelperAppLauncherDialog : nsIHelperAppLauncherDialog
		{
			void nsIHelperAppLauncherDialog.Show(nsIHelperAppLauncher aLauncher, Object aWindowContext, UInt32 aReason)
			{
				Trace.TraceInformation("nsIHelperAppLauncherDialog.Show");

				WebBrowser browser = GetBrowserFromWindowContext(aWindowContext);
				if (browser == null)
				{
					Trace.TraceWarning("Can't get Browser object from window context");
					return;
				}

				var e = new PromptAppLaunchEventArgs(aLauncher);
				browser.Events.Raise(EventKey.PromptAppLaunch, e);
			}

			nsILocalFile nsIHelperAppLauncherDialog.PromptForSaveToFile(nsIHelperAppLauncher aLauncher, Object aWindowContext, String aDefaultFileName, String aSuggestedFileExtension, Boolean aForcePrompt)
			{
				Trace.TraceInformation("nsIHelperAppLauncherDialog.PromptForSaveToFile");

				WebBrowser browser = GetBrowserFromWindowContext(aWindowContext);
				if (browser == null)
				{
					Trace.TraceWarning("Can't get Browser object from window context");
					return null;
				}

				var e = new PromptSaveToDiskEventArgs(aLauncher, aDefaultFileName, aSuggestedFileExtension, aForcePrompt);
				browser.Events.Raise(EventKey.PromptSaveToDisk, e);

				//TODO: Return selected file or call aLauncher.SaveToDisk() ?
				return null;
			}

			private static WebBrowser GetBrowserFromWindowContext(Object windowContext)
			{
				if (windowContext == null)
				{
					return null;
				}

				nsIDocShellTreeOwner docShellTreeOwner = ((nsIDocShellTreeItem)windowContext).TreeOwner;
				var browser = Xpcom.RequestInterface<nsIEmbeddingSiteWindow>(docShellTreeOwner) as WebBrowser;
				return browser;
			}
		}

		private static Factory<HelperAppLauncherDialog> ms_HelperAppLauncherDialogFactory;
	}
}
