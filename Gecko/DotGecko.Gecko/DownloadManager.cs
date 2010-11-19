using System;
using System.Collections.Generic;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public enum DownloadState : short
	{
		NotStarted = nsIDownloadManagerConstants.DOWNLOAD_NOTSTARTED,
		Downloading = nsIDownloadManagerConstants.DOWNLOAD_DOWNLOADING,
		Finished = nsIDownloadManagerConstants.DOWNLOAD_FINISHED,
		Failed = nsIDownloadManagerConstants.DOWNLOAD_FAILED,
		Canceled = nsIDownloadManagerConstants.DOWNLOAD_CANCELED,
		Paused = nsIDownloadManagerConstants.DOWNLOAD_PAUSED,
		Queued = nsIDownloadManagerConstants.DOWNLOAD_QUEUED,
		BlockedParental = nsIDownloadManagerConstants.DOWNLOAD_BLOCKED_PARENTAL,
		Scanning = nsIDownloadManagerConstants.DOWNLOAD_SCANNING,
		Dirty = nsIDownloadManagerConstants.DOWNLOAD_DIRTY,
		BlockedPolicy = nsIDownloadManagerConstants.DOWNLOAD_BLOCKED_POLICY
	}

	public enum DownloadRetention
	{
		RemoveOnComplete = 0,
		RemoveOnQuit = 1,
		DoNotRemove = 2
	}

	public enum DownloadQuitBehavior
	{
		PauseResume = 0,
		Pause = 1,
		Cancel = 2
	}

	public static partial class DownloadManager
	{
		static DownloadManager()
		{
			ms_DownloadManager = Xpcom.GetService<nsIDownloadManager>(Xpcom.NS_DOWNLOADMANAGER_CONTRACTID);
			ms_DownloadProgressListener = new DownloadProgressListener();
			ms_DownloadManager.AddListener(ms_DownloadProgressListener);

			var observerService = Xpcom.GetService<nsIObserverService>(Xpcom.NS_OBSERVERSERVICE_CONTRACTID);
			observerService.AddObserver(ms_DownloadProgressListener, "download-manager-remove-download", false);

			ms_DownloadManagerPreferences = new Lazy<PreferencesBranch>(() => PreferencesService.GetUserBranch("browser.download.manager"));
		}

		public static Boolean CanCleanUp
		{
			get { return ms_DownloadManager.CanCleanUp; }
		}

		public static void CleanUp()
		{
			ms_DownloadManager.CleanUp();
		}

		public static Int32 ActiveDownloadCount
		{
			get { return ms_DownloadManager.ActiveDownloadCount; }
		}

		public static IEnumerable<Download> ActiveDownloads
		{
			get
			{
				nsISimpleEnumerator simpleEnumerator = ms_DownloadManager.ActiveDownloads;
				return simpleEnumerator.ToEnumerable(item => new Download((nsIDownload)item));
			}
		}

		public static DownloadRetention Retention
		{
			get
			{
				return (DownloadRetention)(Int32)Preferences.GetValue("retention", DownloadRetention.RemoveOnComplete);
			}
			set
			{
				Preferences["retention"] = (Int32)value;
			}
		}

		public static DownloadQuitBehavior QuitBehavior
		{
			get
			{
				return (DownloadQuitBehavior)(Int32)Preferences.GetValue("quitBehavior", DownloadQuitBehavior.PauseResume);
			}
			set
			{
				Preferences["quitBehavior"] = (Int32)value;
			}
		}

		private static PreferencesBranch Preferences { get { return ms_DownloadManagerPreferences.Value; } }

		private sealed class DownloadProgressListener : nsIDownloadProgressListener, nsIObserver
		{
			nsIDOMDocument nsIDownloadProgressListener.Document { get; set; }

			void nsIDownloadProgressListener.OnDownloadStateChange(Int16 aState, nsIDownload aDownload)
			{
				//
			}

			void nsIDownloadProgressListener.OnStateChange(nsIWebProgress aWebProgress, nsIRequest aRequest, UInt32 aStateFlags, nsResult aStatus, nsIDownload aDownload)
			{
				//
			}

			void nsIDownloadProgressListener.OnProgressChange(nsIWebProgress aWebProgress, nsIRequest aRequest, Int64 aCurSelfProgress, Int64 aMaxSelfProgress, Int64 aCurTotalProgress, Int64 aMaxTotalProgress, nsIDownload aDownload)
			{
				//
			}

			void nsIDownloadProgressListener.OnSecurityChange(nsIWebProgress aWebProgress, nsIRequest aRequest, UInt32 aState, nsIDownload aDownload)
			{
				//
			}

			void nsIObserver.Observe(Object aSubject, String aTopic, String aData)
			{
				//
			}
		}

		private static readonly nsIDownloadManager ms_DownloadManager;
		private static readonly DownloadProgressListener ms_DownloadProgressListener;
		private static readonly Lazy<PreferencesBranch> ms_DownloadManagerPreferences;
	}
}
