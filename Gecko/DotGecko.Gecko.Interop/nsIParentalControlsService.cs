using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIParentalControlsServiceConstants
	{
		/**
		 * Log entry types. Additional types can be defined and implemented
		 * as needed. Other possible event types might include email events,
		 * media related events, and IM events. 
		 */
		public const Int16 ePCLog_URIVisit = 1;    /* Web content */
		public const Int16 ePCLog_FileDownload = 2;  /* File downloads */
	}

	[ComImport, Guid("871cf229-2b21-4f04-b24d-e08061f14815"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIParentalControlsService //: nsISupports
	{
		/**
		 * @returns true if the current user account has parental controls
		 * restrictions enabled.
		 */
		Boolean ParentalControlsEnabled { get; }

		/**
		 * @returns true if the current user account parental controls
		 * restrictions include the blocking of all file downloads.
		 */
		Boolean BlockFileDownloadsEnabled { get; }

		/**
		 * Request that blocked URI(s) be allowed through parental
		 * control filters. Returns true if the URI was successfully
		 * overriden. Note, may block while native UI is shown.
		 *
		 * @param aTarget(s)          URI to be overridden. In the case of
		 *                            multiple URI, the first URI in the array
		 *                            should be the root URI of the site.
		 * @param window              Window that generates the event.
		 */
		Boolean RequestURIOverride(nsIURI aTarget, [Optional] nsIInterfaceRequestor aWindowContext);
		Boolean RequestURIOverrides(nsIArray aTargets, [Optional] nsIInterfaceRequestor aWindowContext);

		/**
		 * @returns true if the current user account has parental controls
		 * logging enabled. If true, applications should log relevent events
		 * using 'log'.
		 */
		Boolean LoggingEnabled { get; }

		/**
		 * Log an application specific parental controls
		 * event.
		 *
		 * @param aEntryType       Constant defining the type of event.
		 * @param aFlag            A flag indicating if the subject content
		 *                         was blocked.
		 * @param aSource          The URI source of the subject content.
		 * @param aTarget          The location the content was saved to if
		 *                         no blocking occurred.
		 */
		void Log(Int16 aEntryType, Boolean aFlag, nsIURI aSource, [Optional] nsIFile aTarget);
	}
}
