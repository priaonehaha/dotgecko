using System;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public enum CookieBehavior
	{
		AllowAll = 0,
		AllowFromOriginSite = 1,
		BlockAll = 2
	}

	public enum ProxyType
	{
		NoProxy = 0,
		ManualProxy = 1,
		AutoConfigProxy = 2,
		AutoDetectProxy = 4,
		SystemProxy = 5
	}

	public static class PreferencesService
	{
		static PreferencesService()
		{
			ms_PrefService = new Lazy<nsIPrefService>(() => XpcomHelper.GetService<nsIPrefService>(Xpcom.NS_PREFSERVICE_CONTRACTID));
			ms_UserRoot = new Lazy<PreferencesBranch>(() => GetUserBranch(String.Empty));
			ms_DefaultRoot = new Lazy<PreferencesBranch>(() => GetDefaultBranch(String.Empty));
		}

		#region Preferences

		public static Boolean AlwaysAcceptSessionCookies
		{
			get { return (Boolean)UserRoot["network.cookie.alwaysAcceptSessionCookies"]; }
			set { UserRoot["network.cookie.alwaysAcceptSessionCookies"] = value; }
		}

		public static CookieBehavior CookieBehavior
		{
			get { return (CookieBehavior)UserRoot["network.cookie.cookieBehavior"]; }
			set { UserRoot["network.cookie.cookieBehavior"] = (Int32)value; }
		}

		public static ProxyType ProxyType
		{
			get { return (ProxyType)UserRoot["network.proxy.type"]; }
			set { UserRoot["network.proxy.type"] = (Int32)value; }
		}

		public static String BypassProxy
		{
			get { return (String)UserRoot["network.proxy.no_proxies_on"]; }
			set { UserRoot["network.proxy.no_proxies_on"] = value; }
		}

		/// <summary>
		/// This preference controls whether the HTTP proxy defined should be used as the proxy for SSL, FTP, SOCKS, and Gopher protocols.
		/// </summary>
		public static Boolean ShareProxySettings
		{
			get { return (Boolean)UserRoot["network.proxy.share_proxy_settings"]; }
			set { UserRoot["network.proxy.share_proxy_settings"] = value; }
		}

		public static String HttpProxy
		{
			get { return (String)UserRoot["network.proxy.http"]; }
			set { UserRoot["network.proxy.http"] = value; }
		}

		public static Int32 HttpProxyPort
		{
			get { return (Int32)UserRoot["network.proxy.http_port"]; }
			set { UserRoot["network.proxy.http_port"] = value; }
		}

		#endregion

		public static PreferencesBranch UserRoot
		{
			get { return ms_UserRoot.Value; }
		}

		public static PreferencesBranch DefaultRoot
		{
			get { return ms_DefaultRoot.Value; }
		}

		public static PreferencesBranch GetUserBranch(String root)
		{
			nsIPrefBranch prefBranch = PrefService.GetBranch(root);
			return new PreferencesBranch(prefBranch);
		}

		public static PreferencesBranch GetDefaultBranch(String root)
		{
			nsIPrefBranch prefBranch = PrefService.GetDefaultBranch(root);
			return new PreferencesBranch(prefBranch);
		}

		private static nsIPrefService PrefService
		{
			get { return ms_PrefService.Value; }
		}

		private static readonly Lazy<nsIPrefService> ms_PrefService;
		private static readonly Lazy<PreferencesBranch> ms_UserRoot;
		private static readonly Lazy<PreferencesBranch> ms_DefaultRoot;
	}
}
