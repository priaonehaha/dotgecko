using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * A load group maintains a collection of nsIRequest objects. 
	 *
	 * @status FROZEN
	 */
	[ComImport]
	[Guid("3de0a31c-feaf-400f-9f1e-4ef71f8b20cc")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsILoadGroup : nsIRequest
	{
		#region nsIRequest Members

		new void GetName(nsAUTF8String result);
		new Boolean IsPending();
		new UInt32 GetStatus();
		new void Cancel(UInt32 aStatus);
		new void Suspend();
		new void Resume();
		new nsILoadGroup GetLoadGroup();
		new void SetLoadGroup(nsILoadGroup value);
		new UInt32 GetLoadFlags();
		new void SetLoadFlags(UInt32 value);

		#endregion

		/**
		 * The group observer is notified when requests are added to and removed
		 * from this load group.  The groupObserver is weak referenced.
		 */
		nsIRequestObserver GetGroupObserver();
		void SetGroupObserver(nsIRequestObserver value);

		/**
		 * Accesses the default load request for the group.  Each time a number
		 * of requests are added to a group, the defaultLoadRequest may be set
		 * to indicate that all of the requests are related to a base request.
		 *
		 * The load group inherits its load flags from the default load request.
		 * If the default load request is NULL, then the group's load flags are
		 * not changed.
		 */
		nsIRequest GetDefaultLoadRequest();
		void SetDefaultLoadRequest(nsIRequest value);

		/**
		 * Adds a new request to the group.  This will cause the default load
		 * flags to be applied to the request.  If this is a foreground
		 * request then the groupObserver's onStartRequest will be called.
		 *
		 * If the request is the default load request or if the default load
		 * request is null, then the load group will inherit its load flags from
		 * the request.
		 */
		void AddRequest(nsIRequest aRequest, [MarshalAs(UnmanagedType.IUnknown)] Object aContext);

		/**
		 * Removes a request from the group.  If this is a foreground request
		 * then the groupObserver's onStopRequest will be called.
		 *
		 * By the time this call ends, aRequest will have been removed from the
		 * loadgroup, even if this function throws an exception.
		 */
		void RemoveRequest(nsIRequest aRequest, [MarshalAs(UnmanagedType.IUnknown)] Object aContext, UInt32 aStatus);

		/**
		 * Returns the requests contained directly in this group.
		 * Enumerator element type: nsIRequest.
		 */
		nsISimpleEnumerator GetRequests();

		/**
		 * Returns the count of "active" requests (ie. requests without the
		 * LOAD_BACKGROUND bit set).
		 */
		UInt32 GetActiveCount();

		/**
		 * Notification callbacks for the load group.
		 */
		nsIInterfaceRequestor GetNotificationCallbacks();
		void SetNotificationCallbacks(nsIInterfaceRequestor value);
	}
}
