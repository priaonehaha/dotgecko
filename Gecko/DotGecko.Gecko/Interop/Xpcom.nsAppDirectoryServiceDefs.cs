using System;

namespace DotGecko.Gecko.Interop
{
	internal static partial class Xpcom
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

		internal const String NS_APP_APPLICATION_REGISTRY_FILE = "AppRegF";
		internal const String NS_APP_APPLICATION_REGISTRY_DIR = "AppRegD";

		internal const String NS_APP_DEFAULTS_50_DIR = "DefRt"; // The root dir of all defaults dirs
		internal const String NS_APP_PREF_DEFAULTS_50_DIR = "PrfDef";
		internal const String NS_APP_PROFILE_DEFAULTS_50_DIR = "profDef"; // The profile defaults of the "current" locale. Should be first choice.
		internal const String NS_APP_PROFILE_DEFAULTS_NLOC_50_DIR = "ProfDefNoLoc"; // The profile defaults of the "default" installed locale. Second choice when above is not available.

		internal const String NS_APP_USER_PROFILES_ROOT_DIR = "DefProfRt"; // The dir where user profile dirs live.
		internal const String NS_APP_USER_PROFILES_LOCAL_ROOT_DIR = "DefProfLRt"; // The dir where user profile temp dirs live.

		internal const String NS_APP_RES_DIR = "ARes";
		internal const String NS_APP_CHROME_DIR = "AChrom";
		internal const String NS_APP_PLUGINS_DIR = "APlugns"; // Deprecated - use NS_APP_PLUGINS_DIR_LIST
		internal const String NS_APP_SEARCH_DIR = "SrchPlugns";

		internal const String NS_APP_CHROME_DIR_LIST = "AChromDL";
		internal const String NS_APP_PLUGINS_DIR_LIST = "APluginsDL";
		internal const String NS_APP_SEARCH_DIR_LIST = "SrchPluginsDL";

		// --------------------------------------------------------------------------------------
		// Files and directories which exist on a per-profile basis
		// These locations are typically provided by the profile mgr
		// --------------------------------------------------------------------------------------

		// In a shared profile environment, prefixing a profile-relative
		// key with NS_SHARED returns a location that is shared by
		// other users of the profile. Without this prefix, the consumer
		// has exclusive access to this location.

		internal const String NS_SHARED = "SHARED";

		internal const String NS_APP_PREFS_50_DIR = "PrefD"; // Directory which contains user prefs       
		internal const String NS_APP_PREFS_50_FILE = "PrefF";
		internal const String NS_APP_PREFS_DEFAULTS_DIR_LIST = "PrefDL";
		internal const String NS_EXT_PREFS_DEFAULTS_DIR_LIST = "ExtPrefDL";
		internal const String NS_APP_PREFS_OVERRIDE_DIR = "PrefDOverride"; // Directory for per-profile defaults

		internal const String NS_APP_USER_PROFILE_50_DIR = "ProfD";
		internal const String NS_APP_USER_PROFILE_LOCAL_50_DIR = "ProfLD";

		internal const String NS_APP_USER_CHROME_DIR = "UChrm";
		internal const String NS_APP_USER_SEARCH_DIR = "UsrSrchPlugns";

		internal const String NS_APP_LOCALSTORE_50_FILE = "LclSt";
		internal const String NS_APP_HISTORY_50_FILE = "UHist";
		internal const String NS_APP_USER_PANELS_50_FILE = "UPnls";
		internal const String NS_APP_USER_MIMETYPES_50_FILE = "UMimTyp";
		internal const String NS_APP_CACHE_PARENT_DIR = "cachePDir";

		internal const String NS_APP_BOOKMARKS_50_FILE = "BMarks";

		internal const String NS_APP_DOWNLOADS_50_FILE = "DLoads";

		internal const String NS_APP_SEARCH_50_FILE = "SrchF";

		internal const String NS_APP_INSTALL_CLEANUP_DIR = "XPIClnupD"; //location of xpicleanup.dat xpicleanup.exe 

		internal const String NS_APP_STORAGE_50_FILE = "UStor"; // sqlite database used as mozStorage profile db
	}
}
