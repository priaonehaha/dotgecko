using System;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	[Flags]
	public enum ChromeFlags : uint
	{
		Default = nsIWebBrowserChromeConstants.CHROME_DEFAULT,
		WindowBorders = nsIWebBrowserChromeConstants.CHROME_WINDOW_BORDERS,
		WindowClose = nsIWebBrowserChromeConstants.CHROME_WINDOW_CLOSE,
		WindowResize = nsIWebBrowserChromeConstants.CHROME_WINDOW_RESIZE,
		Menubar = nsIWebBrowserChromeConstants.CHROME_MENUBAR,
		Toolbar = nsIWebBrowserChromeConstants.CHROME_TOOLBAR,
		Locationbar = nsIWebBrowserChromeConstants.CHROME_LOCATIONBAR,
		Statusbar = nsIWebBrowserChromeConstants.CHROME_STATUSBAR,
		PersonalToolbar = nsIWebBrowserChromeConstants.CHROME_PERSONAL_TOOLBAR,
		Scrollbars = nsIWebBrowserChromeConstants.CHROME_SCROLLBARS,
		Titlebar = nsIWebBrowserChromeConstants.CHROME_TITLEBAR,
		Extra = nsIWebBrowserChromeConstants.CHROME_EXTRA,
		WithSize = nsIWebBrowserChromeConstants.CHROME_WITH_SIZE,
		WithPosition = nsIWebBrowserChromeConstants.CHROME_WITH_POSITION,
		WindowMin = nsIWebBrowserChromeConstants.CHROME_WINDOW_MIN,
		WindowPopup = nsIWebBrowserChromeConstants.CHROME_WINDOW_POPUP,
		WindowRaised = nsIWebBrowserChromeConstants.CHROME_WINDOW_RAISED,
		WindowLowered = nsIWebBrowserChromeConstants.CHROME_WINDOW_LOWERED,
		CenterScreen = nsIWebBrowserChromeConstants.CHROME_CENTER_SCREEN,
		Dependent = nsIWebBrowserChromeConstants.CHROME_DEPENDENT,
		Modal = nsIWebBrowserChromeConstants.CHROME_MODAL,
		OpenAsDialog = nsIWebBrowserChromeConstants.CHROME_OPENAS_DIALOG,
		OpenAsChrome = nsIWebBrowserChromeConstants.CHROME_OPENAS_CHROME,
		All = nsIWebBrowserChromeConstants.CHROME_ALL
	}
}
