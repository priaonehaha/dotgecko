using System;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public class PromptAppLaunchEventArgs : EventArgs
	{
		internal PromptAppLaunchEventArgs(nsIHelperAppLauncher launcher)
		{
			this.m_Launcher = launcher;
		}

		public Uri Source { get { return m_Launcher.Source.ToUri(); } }

		public String SuggestedFileName { get { return XpcomString.Get(m_Launcher.GetSuggestedFileName); } }

		public String TargetFile { get { return XpcomString.Get(m_Launcher.TargetFile.GetPath); } }

		public Boolean TargetFileIsExecutable { get { return m_Launcher.TargetFileIsExecutable; } }

		public void SaveToDisk(String fileName)
		{
			nsILocalFile file = Xpcom.NewNativeLocalFile(fileName);
			m_Launcher.SaveToDisk(file, false);
		}

		public void LaunchWithApplication(String appFileName)
		{
			nsILocalFile appFile = Xpcom.NewNativeLocalFile(appFileName);
			m_Launcher.LaunchWithApplication(appFile, false);
		}

		private readonly nsIHelperAppLauncher m_Launcher;
	}

	public sealed class PromptSaveToDiskEventArgs : PromptAppLaunchEventArgs
	{
		internal PromptSaveToDiskEventArgs(nsIHelperAppLauncher launcher, String defaultFileName, String suggestedFileExtension, Boolean forcePrompt)
			: base(launcher)
		{
			this.m_DefaultFileName = defaultFileName;
			this.m_ForcePrompt = forcePrompt;
			this.m_SuggestedFileExtension = suggestedFileExtension;
		}

		public String DefaultFileName { get { return this.m_DefaultFileName; } }

		public String SuggestedFileExtension { get { return this.m_SuggestedFileExtension; } }

		public Boolean ForcePrompt { get { return this.m_ForcePrompt; } }

		private readonly String m_DefaultFileName;
		private readonly String m_SuggestedFileExtension;
		private readonly Boolean m_ForcePrompt;
	}
}
