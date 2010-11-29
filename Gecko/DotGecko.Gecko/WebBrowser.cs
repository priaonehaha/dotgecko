using System;
using System.IO;
using System.Text;
using DotGecko.Gecko.Dom;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser : IDisposable
	{
		public static void InitEmbedding(String binDirectory, AppFileLocation appFileLocation,
			Boolean useWindowCreator = true,
			Boolean usePromptService = true,
			Boolean useFilePicker = true,
			Boolean useHelperAppLauncherDialog = true,
			Boolean useTooltipTextProvider = false)
		{
			XpcomHelper.InitEmbedding(binDirectory, appFileLocation);

			if (useWindowCreator)
			{
				ms_WindowCreator = new WindowCreator();
				var windowWatcher = XpcomHelper.GetService<nsIWindowWatcher>(Xpcom.NS_WINDOWWATCHER_CONTRACTID);
				windowWatcher.SetWindowCreator(ms_WindowCreator);
			}

			if (usePromptService)
			{
				ms_PromptServiceFactory = new Factory<PromptService>(Xpcom.NS_PROMPTSERVICE_CONTRACTID);
				ms_PromptServiceFactory.Register();
			}

			if (useFilePicker)
			{
				ms_FilePickerFactory = new Factory<FilePicker>("@mozilla.org/filepicker;1");
				ms_FilePickerFactory.Register();
			}

			if (useHelperAppLauncherDialog)
			{
				ms_HelperAppLauncherDialogFactory = new Factory<HelperAppLauncherDialog>(Xpcom.NS_IHELPERAPPLAUNCHERDLG_CONTRACTID);
				ms_HelperAppLauncherDialogFactory.Register();
			}

			if (useTooltipTextProvider)
			{
				ms_TooltipTextProviderFactory = new Factory<TooltipTextProvider>(Xpcom.NS_TOOLTIPTEXTPROVIDER_CONTRACTID);
				ms_TooltipTextProviderFactory.Register();
			}
		}

		public static void TermEmbedding()
		{
			if (ms_PromptServiceFactory != null)
			{
				ms_PromptServiceFactory.Dispose();
			}

			if (ms_FilePickerFactory != null)
			{
				ms_FilePickerFactory.Dispose();
			}

			if (ms_HelperAppLauncherDialogFactory != null)
			{
				ms_HelperAppLauncherDialogFactory.Dispose();
			}

			if (ms_TooltipTextProviderFactory != null)
			{
				ms_TooltipTextProviderFactory.Dispose();
			}

			XpcomHelper.TermEmbedding();
		}

		public WebBrowser(IWebBrowserContainer container)
		{
			m_Container = container;
			m_Events = new EventHandlers<EventKey>(this);

			var webBrowser = XpcomHelper.CreateInstance<nsIWebBrowser>(Xpcom.NS_WEBBROWSER_CONTRACTID);
			this.AssignWebBrowser(webBrowser);

			Container.GotFocus += ContainerGotFocus;
			Container.LostFocus += ContainerLostFocus;
			Container.SizeChanged += ContainerSizeChanged;
		}

		public DomWindow Window
		{
			get { return DomWindow.Create(m_WebBrowser.ContentDOMWindow); }
		}

		public Uri CurrentUri
		{
			get
			{
				nsIURI nsUri = WebNavigation.CurrentURI;
				return nsUri.ToUri();
			}
		}

		public Boolean IsLoadingDocument
		{
			get { return WebProgress.IsLoadingDocument; }
		}

		public Boolean CanGoBack
		{
			get { return WebNavigation.CanGoBack; }
		}

		public Boolean CanGoForward
		{
			get { return WebNavigation.CanGoForward; }
		}

		public Single TextZoom
		{
			get
			{
				nsIMarkupDocumentViewer documentViewer = this.GetMarkupDocumentViewer();
				return documentViewer != null ? documentViewer.TextZoom : 1;
			}
			set
			{
				nsIMarkupDocumentViewer documentViewer = this.GetMarkupDocumentViewer();
				if (documentViewer != null)
				{
					documentViewer.TextZoom = value;
				}
			}
		}

		public Single FullZoom
		{
			get
			{
				nsIMarkupDocumentViewer documentViewer = this.GetMarkupDocumentViewer();
				return documentViewer != null ? documentViewer.FullZoom : 1;
			}
			set
			{
				nsIMarkupDocumentViewer documentViewer = this.GetMarkupDocumentViewer();
				if (documentViewer != null)
				{
					documentViewer.FullZoom = value;
				}
			}
		}

		public Boolean AuthorStyleDisabled
		{
			get
			{
				nsIMarkupDocumentViewer documentViewer = this.GetMarkupDocumentViewer();
				return documentViewer != null ? documentViewer.AuthorStyleDisabled : false;
			}
			set
			{
				nsIMarkupDocumentViewer documentViewer = this.GetMarkupDocumentViewer();
				if (documentViewer != null)
				{
					documentViewer.AuthorStyleDisabled = value;
				}
			}
		}

		public Boolean AllowPlugins
		{
			get { return DocShell.AllowPlugins; }
			set { DocShell.AllowPlugins = value; }
		}

		public Boolean AllowJavaScript
		{
			get { return DocShell.AllowJavascript; }
			set { DocShell.AllowJavascript = value; }
		}

		public Boolean AllowMetaRedirects
		{
			get { return DocShell.AllowMetaRedirects; }
			set { DocShell.AllowMetaRedirects = value; }
		}

		public Boolean AllowSubframes
		{
			get { return DocShell.AllowSubframes; }
			set { DocShell.AllowSubframes = value; }
		}

		public Boolean AllowImages
		{
			get { return DocShell.AllowImages; }
			set { DocShell.AllowImages = value; }
		}

		public Boolean AllowDNSPrefetch
		{
			get { return DocShell.AllowDNSPrefetch; }
			set { DocShell.AllowDNSPrefetch = value; }
		}

		public Boolean UseErrorPages
		{
			get { return DocShell.UseErrorPages; }
			set { DocShell.UseErrorPages = value; }
		}

		public ClipboardCommands Clipboard
		{
			get { return m_ClipboardCommands.Value; }
		}

		public IntPtr Handle
		{
			get
			{
				var baseWindow = (nsIBaseWindow)this.DocShell;
				return baseWindow.ParentNativeWindow;
			}
		}

		public Boolean IsDisposed
		{
			get { return m_IsDisposed; }
		}

		public Boolean LoadUri(String url, LoadFlags loadFlags = LoadFlags.None, Uri referrer = null)
		{
			nsIURI referrerURI = referrer.ToNsUri();
			Boolean succeded = WebNavigation.LoadURI(url, (UInt32)loadFlags, referrerURI, null, null) == nsResult.NS_OK;
			return succeded;
		}

		public void LoadString(String data, Uri uri = null, String contentType = null, Encoding encoding = null)
		{
			if (encoding == null)
			{
				encoding = Encoding.UTF8;
			}
			Byte[] encodedData = encoding.GetBytes(data);
			var stream = new MemoryStream(encodedData);
			LoadStream(stream, uri, contentType, encoding.WebName);
		}

		public void LoadStream(Stream stream, Uri uri = null, String contentType = null, String contentCharset = null)
		{
			var inputStream = new InputStream(stream);
			nsIURI nsUri = uri.ToNsUri();
			DocShell.LoadStream(inputStream, nsUri, contentType, contentCharset, null);
		}

		public void Reload(LoadFlags loadFlags = LoadFlags.None)
		{
			WebNavigation.Reload((UInt32)loadFlags);
		}

		public void Stop(StopFlags stopFlags = StopFlags.All)
		{
			WebNavigation.Stop((UInt32)stopFlags);
		}

		public void GoBack()
		{
			if (CanGoBack)
			{
				WebNavigation.GoBack();
			}
		}

		public void GoForward()
		{
			if (CanGoForward)
			{
				WebNavigation.GoForward();
			}
		}

		public void ScrollToNode(DomNode domNode)
		{
			nsIMarkupDocumentViewer documentViewer = this.GetMarkupDocumentViewer();
			if (documentViewer != null)
			{
				documentViewer.ScrollToNode(domNode.DomObj);
			}
		}

		public Byte[] GetSnapshot()
		{
			Byte[] imageData = SnapshotCapture.CaptureWindow(m_WebBrowser.ContentDOMWindow);
			return imageData;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private IWebBrowserContainer Container
		{
			get { return m_Container; }
		}

		private EventHandlers<EventKey> Events
		{
			get { return m_Events; }
		}

		private nsIBaseWindow BaseWindow
		{
			get { return m_BaseWindow; }
		}

		private nsIWebNavigation WebNavigation
		{
			get { return m_WebNavigation.Value; }
		}

		private nsIWebProgress WebProgress
		{
			get { return m_WebProgress.Value; }
		}

		private nsIWebBrowserFocus WebBrowserFocus
		{
			get { return m_WebBrowserFocus.Value; }
		}

		private nsICommandManager CommandManager
		{
			get { return m_CommandManager.Value; }
		}

		private nsIDocShell DocShell
		{
			get { return m_DocShell.Value; }
		}

		private void AssignWebBrowser(nsIWebBrowser webBrowser)
		{
			m_WebBrowser = webBrowser;
			m_BaseWindow = (nsIBaseWindow)m_WebBrowser;
			m_WebNavigation = new Lazy<nsIWebNavigation>(() => (nsIWebNavigation)m_WebBrowser);
			m_WebProgress = new Lazy<nsIWebProgress>(() => XpcomHelper.RequestInterface<nsIWebProgress>(m_WebBrowser));
			m_WebBrowserFocus = new Lazy<nsIWebBrowserFocus>(() => (nsIWebBrowserFocus)m_WebBrowser);
			m_CommandManager = new Lazy<nsICommandManager>(() => XpcomHelper.RequestInterface<nsICommandManager>(m_WebBrowser));
			m_ClipboardCommands = new Lazy<ClipboardCommands>(() => new ClipboardCommands(XpcomHelper.RequestInterface<nsIClipboardCommands>(m_WebBrowser)));
			m_DocShell = new Lazy<nsIDocShell>(() => XpcomHelper.RequestInterface<nsIDocShell>(m_WebBrowser));

			m_WebBrowser.ContainerWindow = this;

			BaseWindow.InitWindow(Container.Handle, null, 0, 0, Container.Width, Container.Height);
			BaseWindow.Create();

			m_WebBrowser.AddWebBrowserListener(this, typeof(nsIWebProgressListener).GUID);
			m_WebBrowser.ParentURIContentListener = this;

			BaseWindow.Visibility = true;
		}

		private nsIMarkupDocumentViewer GetMarkupDocumentViewer()
		{
			nsIContentViewer contentViewer = DocShell.ContentViewer;
			return (nsIMarkupDocumentViewer)contentViewer;
		}

		private void ContainerGotFocus(Object sender, EventArgs eventArgs)
		{
			WebBrowserFocus.Activate();
		}

		private void ContainerLostFocus(Object sender, EventArgs eventArgs)
		{
			WebBrowserFocus.Deactivate();
		}

		private void ContainerSizeChanged(Object sender, EventArgs e)
		{
			if (BaseWindow != null)
			{
				BaseWindow.SetPositionAndSize(0, 0, Container.Width, m_Container.Height, true);
			}
		}

		private void Dispose(Boolean disposing)
		{
			if (m_IsDisposed)
			{
				return;
			}

			if (disposing)
			{
				//TODO:
			}

			//TODO:

			m_IsDisposed = true;
		}

		~WebBrowser()
		{
			Dispose(false);
		}

		private static WebBrowser GetBrowserFromDomDocument(nsIDOMDocument domDocument)
		{
			if (domDocument != null)
			{
				var documentView = (nsIDOMDocumentView)domDocument;
				var domWindow = (nsIDOMWindow)documentView.DefaultView;
				return GetBrowserFromDomWindow(domWindow);
			}
			return null;
		}

		private static WebBrowser GetBrowserFromDomWindow(nsIDOMWindow domWindow)
		{
			if (domWindow != null)
			{
				var windowWatcher = XpcomHelper.GetService<nsIWindowWatcher>(Xpcom.NS_WINDOWWATCHER_CONTRACTID);
				if (windowWatcher != null)
				{
					nsIWebBrowserChrome webBrowserChrome = windowWatcher.GetChromeForWindow(domWindow);
					return webBrowserChrome as WebBrowser;
				}
			}
			return null;
		}

		private enum EventKey
		{
			ChromeSizeChange,
			DestroyBrowserWindow,
			ShowContextMenu,

			StartUriOpen,

			RequestStateChange,
			RequestProgressChange,
			LocationChange,
			RequestStatusChange,
			SecurityChange,
			RefreshAttempted,

			ProvideWindow,
			CreateWindow,

			Alert,
			Confirm,
			Prompt,
			PromptUsernameAndPassword,
			PromptPassword,
			Select,
			PromptAuth,
			AsyncPromptAuth,
			NonBlockingAlert,

			ShowFilePicker,
			PromptAppLaunch,
			PromptSaveToDisk,

			ProvideTooltipText,
			ShowTooltip,
			HideTooltip,
		}

		private readonly IWebBrowserContainer m_Container;
		private readonly EventHandlers<EventKey> m_Events;

		private nsIWebBrowser m_WebBrowser;
		private nsIBaseWindow m_BaseWindow;
		private Lazy<nsIWebNavigation> m_WebNavigation;
		private Lazy<nsIWebProgress> m_WebProgress;
		private Lazy<nsICommandManager> m_CommandManager;
		private Lazy<nsIWebBrowserFocus> m_WebBrowserFocus;
		private Lazy<ClipboardCommands> m_ClipboardCommands;
		private Lazy<nsIDocShell> m_DocShell;

		private Boolean m_IsDisposed;
	}
}
