using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIObserverService
	 * 
	 * Service allows a client listener (nsIObserver) to register and unregister for 
	 * notifications of specific string referenced topic. Service also provides a 
	 * way to notify registered listeners and a way to enumerate registered client 
	 * listeners.
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("D07F5192-E3D1-11d2-8ACD-00105A1B8860"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIObserverService //: nsISupports 
	{
		/**
		 * AddObserver
		 *
		 * Registers a given listener for a notifications regarding the specified
		 * topic.
		 *
		 * @param anObserve : The interface pointer which will receive notifications.
		 * @param aTopic    : The notification topic or subject.
		 * @param ownsWeak  : If set to false, the nsIObserverService will hold a 
		 *                    strong reference to |anObserver|.  If set to true and 
		 *                    |anObserver| supports the nsIWeakReference interface,
		 *                    a weak reference will be held.  Otherwise an error will be
		 *                    returned.
		 */
		void AddObserver(nsIObserver anObserver, [MarshalAs(UnmanagedType.LPStr)] String aTopic, Boolean ownsWeak);

		/**
		 * removeObserver
		 *
		 * Unregisters a given listener from notifications regarding the specified
		 * topic.
		 *
		 * @param anObserver : The interface pointer which will stop recieving
		 *                     notifications.
		 * @param aTopic     : The notification topic or subject.
		 */
		void RemoveObserver(nsIObserver anObserver, [MarshalAs(UnmanagedType.LPStr)] String aTopic);

		/**
		 * notifyObservers
		 *
		 * Notifies all registered listeners of the given topic.
		 *
		 * @param aSubject : Notification specific interface pointer.
		 * @param aTopic   : The notification topic or subject.
		 * @param someData : Notification specific wide string.
		 */
		void NotifyObservers([MarshalAs(UnmanagedType.IUnknown)] nsISupports aSubject, [MarshalAs(UnmanagedType.LPStr)] String aTopic, [MarshalAs(UnmanagedType.LPWStr)] String someData);

		/**
		 * enumerateObservers
		 *
		 * Returns an enumeration of all registered listeners.
		 *
		 * @param aTopic   : The notification topic or subject.
		 */
		nsISimpleEnumerator EnumerateObservers([MarshalAs(UnmanagedType.LPStr)] String aTopic);
	}
}
