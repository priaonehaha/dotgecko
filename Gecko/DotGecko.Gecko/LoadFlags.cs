using System;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	[Flags]
	public enum LoadFlags : uint
	{
		None = nsIWebNavigationConstants.LOAD_FLAGS_NONE,
		IsRefresh = nsIWebNavigationConstants.LOAD_FLAGS_IS_REFRESH,
		IsLink = nsIWebNavigationConstants.LOAD_FLAGS_IS_LINK,
		BypassHistory = nsIWebNavigationConstants.LOAD_FLAGS_BYPASS_HISTORY,
		ReplaceHistory = nsIWebNavigationConstants.LOAD_FLAGS_REPLACE_HISTORY,
		BypassCache = nsIWebNavigationConstants.LOAD_FLAGS_BYPASS_CACHE,
		BypassProxy = nsIWebNavigationConstants.LOAD_FLAGS_BYPASS_PROXY,
		CharsetChange = nsIWebNavigationConstants.LOAD_FLAGS_CHARSET_CHANGE,
		StopContent = nsIWebNavigationConstants.LOAD_FLAGS_STOP_CONTENT,
		FromExternal = nsIWebNavigationConstants.LOAD_FLAGS_FROM_EXTERNAL,
		AllowThirdPartyFixup = nsIWebNavigationConstants.LOAD_FLAGS_ALLOW_THIRD_PARTY_FIXUP,
		FirstLoad = nsIWebNavigationConstants.LOAD_FLAGS_FIRST_LOAD,
		AllowPopups = nsIWebNavigationConstants.LOAD_FLAGS_ALLOW_POPUPS,
		BypassClassifier = nsIWebNavigationConstants.LOAD_FLAGS_BYPASS_CLASSIFIER,
		ForceAllowCookies = nsIWebNavigationConstants.LOAD_FLAGS_FORCE_ALLOW_COOKIES
	}
}
