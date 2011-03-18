using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIWebProgressConstants
	{
		/**
		 * The following flags may be combined to form the aNotifyMask parameter for
		 * the addProgressListener method.  They limit the set of events that are
		 * delivered to an nsIWebProgressListener instance.
		 */

		/**
		 * These flags indicate the state transistions to observe, corresponding to
		 * nsIWebProgressListener::onStateChange.
		 *
		 * NOTIFY_STATE_REQUEST
		 *   Only receive the onStateChange event if the aStateFlags parameter
		 *   includes nsIWebProgressListener::STATE_IS_REQUEST.
		 *   
		 * NOTIFY_STATE_DOCUMENT
		 *   Only receive the onStateChange event if the aStateFlags parameter
		 *   includes nsIWebProgressListener::STATE_IS_DOCUMENT.
		 *
		 * NOTIFY_STATE_NETWORK
		 *   Only receive the onStateChange event if the aStateFlags parameter
		 *   includes nsIWebProgressListener::STATE_IS_NETWORK.
		 *
		 * NOTIFY_STATE_WINDOW
		 *   Only receive the onStateChange event if the aStateFlags parameter
		 *   includes nsIWebProgressListener::STATE_IS_WINDOW.
		 *
		 * NOTIFY_STATE_ALL
		 *   Receive all onStateChange events.
		 */
		public const UInt32 NOTIFY_STATE_REQUEST = 0x00000001;
		public const UInt32 NOTIFY_STATE_DOCUMENT = 0x00000002;
		public const UInt32 NOTIFY_STATE_NETWORK = 0x00000004;
		public const UInt32 NOTIFY_STATE_WINDOW = 0x00000008;
		public const UInt32 NOTIFY_STATE_ALL = 0x0000000f;

		/**
		 * These flags indicate the other events to observe, corresponding to the
		 * other four methods defined on nsIWebProgressListener.
		 *
		 * NOTIFY_PROGRESS
		 *   Receive onProgressChange events.
		 *
		 * NOTIFY_STATUS
		 *   Receive onStatusChange events.
		 *
		 * NOTIFY_SECURITY
		 *   Receive onSecurityChange events.
		 *
		 * NOTIFY_LOCATION
		 *   Receive onLocationChange events.
		 *
		 * NOTIFY_REFRESH
		 *   Receive onRefreshAttempted events.
		 *   This is defined on nsIWebProgressListener2.
		 */
		public const UInt32 NOTIFY_PROGRESS = 0x00000010;
		public const UInt32 NOTIFY_STATUS = 0x00000020;
		public const UInt32 NOTIFY_SECURITY = 0x00000040;
		public const UInt32 NOTIFY_LOCATION = 0x00000080;
		public const UInt32 NOTIFY_REFRESH = 0x00000100;

		/**
		 * This flag enables all notifications.
		 */
		public const UInt32 NOTIFY_ALL = 0x000001ff;
	}

	/**
	 * The nsIWebProgress interface is used to add or remove nsIWebProgressListener
	 * instances to observe the loading of asynchronous requests (usually in the
	 * context of a DOM window).
	 *
	 * nsIWebProgress instances may be arranged in a parent-child configuration,
	 * corresponding to the parent-child configuration of their respective DOM
	 * windows.  However, in some cases a nsIWebProgress instance may not have an
	 * associated DOM window.  The parent-child relationship of nsIWebProgress
	 * instances is not made explicit by this interface, but the relationship may
	 * exist in some implementations.
	 *
	 * A nsIWebProgressListener instance receives notifications for the
	 * nsIWebProgress instance to which it added itself, and it may also receive
	 * notifications from any nsIWebProgress instances that are children of that
	 * nsIWebProgress instance.
	 */
	[ComImport, Guid("570F39D0-EFD0-11d3-B093-00A024FFC08C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWebProgress //: nsISupports
	{
		/**
		 * Registers a listener to receive web progress events.
		 *
		 * @param aListener
		 *        The listener interface to be called when a progress event occurs.
		 *        This object must also implement nsISupportsWeakReference.
		 * @param aNotifyMask
		 *        The types of notifications to receive.
		 *
		 * @throw NS_ERROR_INVALID_ARG
		 *        Indicates that aListener was either null or that it does not
		 *        support weak references.
		 * @throw NS_ERROR_FAILURE
		 *        Indicates that aListener was already registered.
		 */
		void AddProgressListener(nsIWebProgressListener aListener, UInt32 aNotifyMask);

		/**
		 * Removes a previously registered listener of progress events.
		 *
		 * @param aListener
		 *        The listener interface previously registered with a call to
		 *        addProgressListener.
		 *
		 * @throw NS_ERROR_FAILURE
		 *        Indicates that aListener was not registered.
		 */
		void RemoveProgressListener(nsIWebProgressListener aListener);

		/**
		 * The DOM window associated with this nsIWebProgress instance.
		 *
		 * @throw NS_ERROR_FAILURE
		 *        Indicates that there is no associated DOM window.
		 */
		nsIDOMWindow DOMWindow { get; }

		/**
		 * Indicates whether or not a document is currently being loaded
		 * in the context of this nsIWebProgress instance.
		 */
		Boolean IsLoadingDocument { get; }
	}
}
