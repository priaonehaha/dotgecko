using System;

namespace DotGecko.Gecko.Interop
{
	internal static partial class Xpcom
	{
		internal const String NS_SUPPORTSARRAY_CLASSNAME = @"Supports Array";
		internal const String NS_SUPPORTSARRAY_CONTRACTID = @"@mozilla.org/supports-array;1";
		internal static readonly Guid NS_SUPPORTSARRAY_CID = new Guid(0xbda17d50, 0x0d6b, 0x11d3, 0x93, 0x31, 0x00, 0x10, 0x4b, 0xa0, 0xfd, 0x40);

		// The contractID for the generic implementation built in to xpcom.
		internal const String NS_VARIANT_CONTRACTID = @"@mozilla.org/variant;1";

		internal const String NS_WINDOWMEDIATOR_CONTRACTID = @"@mozilla.org/appshell/window-mediator;1";
		internal static readonly Guid NS_WINDOWMEDIATOR_CID = new Guid(0x0659cb83, 0xfaad, 0x11d2, 0x8e, 0x19, 0xb2, 0x06, 0x62, 0x0a, 0x65, 0x7c);

		/**
		 * Protocol handlers are registered with XPCOM under the following CONTRACTID prefix:
		 *
		 * For example, "@mozilla.org/network/protocol;1?name=http"
		 */
		internal const String NS_NETWORK_PROTOCOL_CONTRACTID_PREFIX = @"@mozilla.org/network/protocol;1?name=";

		internal const String NS_POPUPWINDOWMANAGER_CONTRACTID = "@mozilla.org/PopupWindowManager;1";

		internal const String NS_PERMISSIONMANAGER_CONTRACTID = "@mozilla.org/permissionmanager;1";

		internal const String PERM_CHANGE_NOTIFICATION = "perm-changed";

		/**
		 * nsISocketProvider implementations should be registered with XPCOM under a
		 * contract ID of the form: "@mozilla.org/network/socket;2?type=foo"
		 */
		internal const String NS_NETWORK_SOCKET_CONTRACTID_PREFIX = @"@mozilla.org/network/socket;2?type=";

		internal const String NS_SHISTORY_CONTRACTID = @"@mozilla.org/browser/shistory;1";

		internal const String NS_SECURE_BROWSER_UI_CONTRACTID = @"@mozilla.org/secure_browser_ui;1";
		internal const String NS_SECURE_BROWSER_UI_CLASSNAME = @"Mozilla Secure Browser UI Handler";

		internal const String NS_HISTORYENTRY_CONTRACTID = @"@mozilla.org/browser/history-entry;1";

		internal static readonly Guid NS_SHENTRY_CID = new Guid(0xbfd1a791, 0xad9f, 0x11d3, 0xbd, 0xc7, 0x0, 0x50, 0x4, 0xa, 0x9b, 0x44);
		internal const String NS_SHENTRY_CONTRACTID = @"@mozilla.org/browser/session-history-entry;1";

		internal static readonly Guid NS_COMMAND_MANAGER_CID = new Guid(0x64edb481, 0x0c04, 0x11d5, 0xa7, 0x3c, 0xe9, 0x64, 0xb9, 0x68, 0xb0, 0xbc);
		internal const String NS_COMMAND_MANAGER_CONTRACTID = @"@mozilla.org/embedcomp/command-manager;1";

		internal static readonly Guid NS_COMMAND_PARAMS_CID = new Guid(0xf7fa4581, 0x238e, 0x11d5, 0xa7, 0x3c, 0xab, 0x64, 0xfb, 0x68, 0xf2, 0xbc);
		internal const String NS_COMMAND_PARAMS_CONTRACTID = @"@mozilla.org/embedcomp/command-params;1";

		// {002286a8-494b-43b3-8ddd-49e3fc50622b}
		internal static readonly Guid NS_WINDOWWATCHER_IID = new Guid(0x002286a8, 0x494b, 0x43b3, 0x8d, 0xdd, 0x49, 0xe3, 0xfc, 0x50, 0x62, 0x2b);

		internal const String NS_WINDOWWATCHER_CONTRACTID = @"@mozilla.org/embedcomp/window-watcher;1";

		/* {1cd91b88-1dd2-11b2-92e1-ed22ed298000} */
		internal static readonly Guid NS_PREFSERVICE_CID = new Guid(0x1cd91b88, 0x1dd2, 0x11b2, 0x92, 0xe1, 0xed, 0x22, 0xed, 0x29, 0x80, 0x00);

		internal const String NS_PREFSERVICE_CONTRACTID = @"@mozilla.org/preferences-service;1";
		internal const String NS_PREFSERVICE_CLASSNAME = @"Preferences Server";

		/**
		 * Notification sent before reading the default user preferences files.
		 */
		internal const String NS_PREFSERVICE_READ_TOPIC_ID = @"prefservice:before-read-userprefs";

		/**
		 * Notification sent when resetPrefs has been called, but before the actual
		 * reset process occurs.
		 */
		internal const String NS_PREFSERVICE_RESET_TOPIC_ID = @"prefservice:before-reset";

		/**
		 * Notification sent when after reading app-provided default
		 * preferences, but before user profile override defaults or extension
		 * defaults are loaded.
		 */
		internal const String NS_PREFSERVICE_APPDEFAULTS_TOPIC_ID = @"prefservice:after-app-defaults";

		internal const String NS_PREFBRANCH_CONTRACTID = @"@mozilla.org/preferencesbranch;1";
		internal const String NS_PREFBRANCH_CLASSNAME = @"Preferences Branch";
		/**
		 * Notification sent when a preference changes.
		 */
		internal const String NS_PREFBRANCH_PREFCHANGE_TOPIC_ID = "nsPref:changed";

		// {E3FA9D0A-1DD1-11B2-BDEF-8C720B597445}
		internal static readonly Guid NS_DOWNLOAD_CID = new Guid(0xe3fa9d0a, 0x1dd1, 0x11b2, 0xbd, 0xef, 0x8c, 0x72, 0x0b, 0x59, 0x74, 0x45);

		/**
		 * A component with this contract ID will be created each time a download is
		 * started, and nsITransfer::Init will be called on it and an observer will be set.
		 *
		 * Notifications of the download progress will happen via
		 * nsIWebProgressListener/nsIWebProgressListener2.
		 *
		 * INTERFACES THAT MUST BE IMPLEMENTED:
		 *   nsITransfer
		 *   nsIWebProgressListener
		 *   nsIWebProgressListener2
		 *
		 * XXX move this to nsEmbedCID.h once the interfaces (and the contract ID) are
		 * frozen.
		 */
		internal const String NS_TRANSFER_CONTRACTID = @"@mozilla.org/transfer;1";

		internal const String NS_IHELPERAPPLAUNCHERDLG_CONTRACTID = @"@mozilla.org/helperapplauncherdialog;1";
		internal const String NS_IHELPERAPPLAUNCHERDLG_CLASSNAME = "Mozilla Helper App Launcher Confirmation Dialog";

		internal const String NS_TOOLTIPTEXTPROVIDER_CONTRACTID = "@mozilla.org/embedcomp/tooltiptextprovider;1";

		internal const String CMD_UNDO = "cmd_undo";
		internal const String CMD_REDO = "cmd_redo";
		internal const String CMD_CUT = "cmd_cut";
		internal const String CMD_COPY = "cmd_copy";
		internal const String CMD_PASTE = "cmd_paste";
		internal const String CMD_SELECTTOP = "cmd_selectTop";
		internal const String CMD_SELECTBOTTOM = "cmd_selectBottom";
		internal const String CMD_SELECTNEXTLINE = "cmd_selectLineNext";
		internal const String CMD_SELECTLINEPREVIOUS = "cmd_selectLinePrevious";
		internal const String CMD_SELECTALL = "cmd_selectAll";
		internal const String CMD_DELETE = "cmd_delete";
		internal const String CMD_CUTORDELETE = "cmd_cutOrDelete";
		internal const String CMD_DELETECHARBACKWARD = "cmd_deleteCharBackward";
		internal const String CMD_DELETECHARFORWARD = "cmd_deleteCharForward";
		internal const String CMD_DELETEWORDBACKWARD = "cmd_deleteWordBackward";
		internal const String CMD_DELETEWORDFORWARD = "cmd_deleteWordForward";
		internal const String CMD_DELETETOBEGINNINGOFLINE = "cmd_deleteToBeginningOfLine";
		internal const String CMD_DELETETOENDOFLINE = "cmd_deleteToEndOfLine";
		internal const String CMD_SCROLLTOP = "cmd_scrollTop";
		internal const String CMD_SCROLLBOTTOM = "cmd_scrollBottom";
		internal const String CMD_SCROLLPAGEUP = "cmd_scrollPageUp";
		internal const String CMD_SCROLLPAGEDOWN = "cmd_scrollPageDown";
		internal const String CMD_SWITCHTEXTDIRECTION = "cmd_switchTextDirection";

		/**
		 * We send notifications through nsIObserverService with topic
		 * NS_IOSERVICE_GOING_OFFLINE_TOPIC and data NS_IOSERVICE_OFFLINE
		 * when 'offline' has changed from false to true, and we are about
		 * to shut down network services such as DNS. When those
		 * services have been shut down, we send a notification with
		 * topic NS_IOSERVICE_OFFLINE_STATUS_TOPIC and data
		 * NS_IOSERVICE_OFFLINE.
		 *
		 * When 'offline' changes from true to false, then after
		 * network services have been restarted, we send a notification
		 * with topic NS_IOSERVICE_OFFLINE_STATUS_TOPIC and data
		 * NS_IOSERVICE_ONLINE.
		 */
		internal const String NS_IOSERVICE_GOING_OFFLINE_TOPIC = @"network:offline-about-to-go-offline";
		internal const String NS_IOSERVICE_OFFLINE_STATUS_TOPIC = @"network:offline-status-changed";
		internal const String NS_IOSERVICE_OFFLINE = @"offline";
		internal const String NS_IOSERVICE_ONLINE = @"online";
	}
}
