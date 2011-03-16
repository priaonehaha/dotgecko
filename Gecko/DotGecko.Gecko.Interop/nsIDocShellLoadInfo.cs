using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;
using nsDocShellInfoLoadType = System.Int32;

namespace DotGecko.Gecko.Interop
{
	public static class nsIDocShellLoadInfoConstants
	{
		/* these are load type enums... */
		public const Int32 loadNormal = 0;                     // Normal Load
		public const Int32 loadNormalReplace = 1;              // Normal Load but replaces current history slot
		public const Int32 loadHistory = 2;                    // Load from history
		public const Int32 loadReloadNormal = 3;               // Reload
		public const Int32 loadReloadBypassCache = 4;
		public const Int32 loadReloadBypassProxy = 5;
		public const Int32 loadReloadBypassProxyAndCache = 6;
		public const Int32 loadLink = 7;
		public const Int32 loadRefresh = 8;
		public const Int32 loadReloadCharsetChange = 9;
		public const Int32 loadBypassHistory = 10;
		public const Int32 loadStopContent = 11;
		public const Int32 loadStopContentAndReplace = 12;
		public const Int32 loadNormalExternal = 13;
		public const Int32 loadNormalBypassCache = 14;
		public const Int32 loadNormalBypassProxy = 15;
		public const Int32 loadNormalBypassProxyAndCache = 16;
		public const Int32 loadPushState = 17;                 // history.pushState or replaceState
	}

	/**
	 * The nsIDocShellLoadInfo interface defines an interface for specifying
	 * setup information used in a nsIDocShell::loadURI call.
	 */
	[ComImport, Guid("92a0a637-373e-4647-9476-ead11e005c75"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDocShellLoadInfo //: nsISupports
	{
		/** This is the referrer for the load. */
		nsIURI Referrer { get; set; }

		/** The owner of the load, that is, the entity responsible for 
		 *  causing the load to occur. This should be a nsIPrincipal typically.
		 */
		nsISupports Owner { [return: MarshalAs(UnmanagedType.IUnknown)] get; [param: MarshalAs(UnmanagedType.IUnknown)] set; }

		/** If this attribute is true and no owner is specified, copy
		 *  the owner from the referring document.
		 */
		Boolean InheritOwner { get; set; }

		/** If this attribute is true only ever use the owner specify by
		 *  the owner and inheritOwner attributes.
		 *  If there are security reasons for why this is unsafe, such
		 *  as trying to use a systemprincipal owner for a content docshell
		 *  the load fails.
		 */
		Boolean OwnerIsExplicit { get; set; }

		/** Contains a load type as specified by the load* constants */
		nsDocShellInfoLoadType LoadType { get; set; }

		/** SHEntry for this page */
		nsISHEntry SHEntry { get; set; }

		/** Target for load, like _content, _blank etc. */
		String Target { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }

		/** Post data */
		nsIInputStream PostDataStream { get; set; }

		/** Additional headers */
		nsIInputStream HeadersStream { get; set; }

		/** True if the referrer should be sent, false if it shouldn't be
		 *  sent, even if it's available. This attribute defaults to true.
		 */
		Boolean SendReferrer { get; set; }
	}
}
