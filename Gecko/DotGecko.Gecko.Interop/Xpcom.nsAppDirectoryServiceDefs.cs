using System;

namespace DotGecko.Gecko.Interop
{
	public static partial class Xpcom
	{
		//========================================================================================
		//
		// Defines property names for directories available from standard nsIDirectoryServiceProviders.
		// These keys are not guaranteed to exist because the nsIDirectoryServiceProviders which
		// provide them are optional.
		//
		// Keys whose definition ends in "DIR" or "FILE" return a single nsIFile (or subclass).
		// Keys whose definition ends in "LIST" return an nsISimpleEnumerator which enumerates a
		// list of file objects.
		//
		// System and XPCOM level properties are defined in nsDirectoryServiceDefs.h.
		//
		//========================================================================================


		// --------------------------------------------------------------------------------------
		// Files and directories which exist on a per-product basis
		// --------------------------------------------------------------------------------------

		public const String NS_APP_APPLICATION_REGISTRY_FILE = "AppRegF";
		public const String NS_APP_APPLICATION_REGISTRY_DIR = "AppRegD";

		public const String NS_APP_DEFAULTS_50_DIR = "DefRt"; // The root dir of all defaults dirs
		public const String NS_APP_PREF_DEFAULTS_50_DIR = "PrfDef";
		public const String NS_APP_PROFILE_DEFAULTS_50_DIR = "profDef"; // The profile defaults of the "current" locale. Should be first choice.
		public const String NS_APP_PROFILE_DEFAULTS_NLOC_50_DIR = "ProfDefNoLoc"; // The profile defaults of the "default" installed locale. Second choice when above is not available.

		public const String NS_APP_USER_PROFILES_ROOT_DIR = "DefProfRt"; // The dir where user profile dirs live.
		public const String NS_APP_USER_PROFILES_LOCAL_ROOT_DIR = "DefProfLRt"; // The dir where user profile temp dirs live.

		public const String NS_APP_RES_DIR = "ARes";
		public const String NS_APP_CHROME_DIR = "AChrom";
		public const String NS_APP_PLUGINS_DIR = "APlugns"; // Deprecated - use NS_APP_PLUGINS_DIR_LIST
		public const String NS_APP_SEARCH_DIR = "SrchPlugns";

		public const String NS_APP_CHROME_DIR_LIST = "AChromDL";
		public const String NS_APP_PLUGINS_DIR_LIST = "APluginsDL";
		public const String NS_APP_SEARCH_DIR_LIST = "SrchPluginsDL";

		// --------------------------------------------------------------------------------------
		// Files and directories which exist on a per-profile basis
		// These locations are typically provided by the profile mgr
		// --------------------------------------------------------------------------------------

		// In a shared profile environment, prefixing a profile-relative
		// key with NS_SHARED returns a location that is shared by
		// other users of the profile. Without this prefix, the consumer
		// has exclusive access to this location.

		public const String NS_SHARED = "SHARED";

		public const String NS_APP_PREFS_50_DIR = "PrefD"; // Directory which contains user prefs       
		public const String NS_APP_PREFS_50_FILE = "PrefF";
		public const String NS_APP_PREFS_DEFAULTS_DIR_LIST = "PrefDL";
		public const String NS_EXT_PREFS_DEFAULTS_DIR_LIST = "ExtPrefDL";
		public const String NS_APP_PREFS_OVERRIDE_DIR = "PrefDOverride"; // Directory for per-profile defaults

		public const String NS_APP_USER_PROFILE_50_DIR = "ProfD";
		public const String NS_APP_USER_PROFILE_LOCAL_50_DIR = "ProfLD";

		public const String NS_APP_USER_CHROME_DIR = "UChrm";
		public const String NS_APP_USER_SEARCH_DIR = "UsrSrchPlugns";

		public const String NS_APP_LOCALSTORE_50_FILE = "LclSt";
		public const String NS_APP_HISTORY_50_FILE = "UHist";
		public const String NS_APP_USER_PANELS_50_FILE = "UPnls";
		public const String NS_APP_USER_MIMETYPES_50_FILE = "UMimTyp";
		public const String NS_APP_CACHE_PARENT_DIR = "cachePDir";

		public const String NS_APP_BOOKMARKS_50_FILE = "BMarks";

		public const String NS_APP_DOWNLOADS_50_FILE = "DLoads";

		public const String NS_APP_SEARCH_50_FILE = "SrchF";

		public const String NS_APP_INSTALL_CLEANUP_DIR = "XPIClnupD"; //location of xpicleanup.dat xpicleanup.exe 

		public const String NS_APP_STORAGE_50_FILE = "UStor"; // sqlite database used as mozStorage profile db
	}
}
