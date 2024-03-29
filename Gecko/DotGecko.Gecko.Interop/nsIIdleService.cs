using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * This interface lets you monitor how long the user has been 'idle',
	 * i.e. not used their mouse or keyboard. You can get the idle time directly,
	 * but in most cases you will want to register an observer for a predefined
	 * interval. The observer will get an 'idle' notification when the user is idle
	 * for that interval (or longer), and receive a 'back' notification when the
	 * user starts using their computer again.
	 */
	[ComImport, Guid("cc52f19a-63ae-4a1c-9cc3-e79eace0b471"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIIdleService //: nsISupports
	{
		/**
		 * The amount of time in milliseconds that has passed
		 * since the last user activity.
		 *
		 * If we do not have a valid idle time to report, 0 is returned
		 * (this can happen if the user never interacted with the browser
		 * at all, and if we are also unable to poll for idle time manually).
		 */
		UInt32 IdleTime { get; }

		/**
		 * Add an observer to be notified when the user idles for some period of
		 * time, and when they get back from that.
		 *
		 * @param observer the observer to be notified
		 * @param time the amount of time in seconds the user should be idle before
		 *             the observer should be notified.
		 *
		 * @note
		 * The subject of the notification the observer will get is always the
		 * nsIIdleService itself.
		 * When the user goes idle, the observer topic is "idle" and when they get
		 * back, the observer topic is "back".
		 * The data param for the notification contains the current user idle time.
		 *
		 * @note
		 * You can add the same observer twice.
		 * @note
		 * Most implementations need to poll the OS for idle info themselves,
		 * meaning your notifications could arrive with a delay up to the length
		 * of the polling interval in that implementation.
		 * Current implementations use a delay of 5 seconds.
		 */
		void AddIdleObserver(nsIObserver observer, UInt32 time);

		/**
		 * Remove an observer registered with addIdleObserver.
		 * @param observer the observer that needs to be removed.
		 * @param time the amount of time they were listening for.
		 * @note
		 * Removing an observer will remove it once, for the idle time you specify. 
		 * If you have added an observer multiple times, you will need to remove it
		 * just as many times.
		 */
		void RemoveIdleObserver(nsIObserver observer, UInt32 time);
	}
}
