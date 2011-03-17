using System;
using System.Runtime.InteropServices;
using System.Text;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * A load group maintains a collection of nsIRequest objects. 
	 */
	[ComImport, Guid("3de0a31c-feaf-400f-9f1e-4ef71f8b20cc"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsILoadGroup : nsIRequest
	{
		#region nsIRequest Members

		new void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		new Boolean IsPending();
		new nsResult Status { [return: MarshalAs(UnmanagedType.U4)] get; }
		new void Cancel([MarshalAs(UnmanagedType.U4)] nsResult aStatus);
		new void Suspend();
		new void Resume();
		new nsILoadGroup LoadGroup { get; set; }
		new UInt32 LoadFlags { get; set; }

		#endregion

		/**
		 * The group observer is notified when requests are added to and removed
		 * from this load group.  The groupObserver is weak referenced.
		 */
		nsIRequestObserver GroupObserver { get; set; }

		/**
		 * Accesses the default load request for the group.  Each time a number
		 * of requests are added to a group, the defaultLoadRequest may be set
		 * to indicate that all of the requests are related to a base request.
		 *
		 * The load group inherits its load flags from the default load request.
		 * If the default load request is NULL, then the group's load flags are
		 * not changed.
		 */
		nsIRequest DefaultLoadRequest { get; set; }

		/**
		 * Adds a new request to the group.  This will cause the default load
		 * flags to be applied to the request.  If this is a foreground
		 * request then the groupObserver's onStartRequest will be called.
		 *
		 * If the request is the default load request or if the default load
		 * request is null, then the load group will inherit its load flags from
		 * the request.
		 */
		void AddRequest(nsIRequest aRequest, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aContext);

		/**
		 * Removes a request from the group.  If this is a foreground request
		 * then the groupObserver's onStopRequest will be called.
		 *
		 * By the time this call ends, aRequest will have been removed from the
		 * loadgroup, even if this function throws an exception.
		 */
		void RemoveRequest(nsIRequest aRequest, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aContext, [MarshalAs(UnmanagedType.U4)] nsResult aStatus);

		/**
		 * Returns the requests contained directly in this group.
		 * Enumerator element type: nsIRequest.
		 */
		nsISimpleEnumerator Requests { get; }

		/**
		 * Returns the count of "active" requests (ie. requests without the
		 * LOAD_BACKGROUND bit set).
		 */
		UInt32 ActiveCount { get; }

		/**
		 * Notification callbacks for the load group.
		 */
		nsIInterfaceRequestor NotificationCallbacks { get; set; }
	}
}
