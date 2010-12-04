using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("e177399e-2e31-4019-aed3-cba63ce9fa99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIAlertsService //: nsISupports
	{
		/**
		 * Displays a sliding notification window.
		 *    
		 * @param imageUrl       A URL identifying the image to put in the alert.
		 * @param title          The title for the alert.
		 * @param text           The contents of the alert.
		 * @param textClickable  If true, causes the alert text to look like a link
		 *                       and notifies the listener when user attempts to 
		 *                       click the alert text.
		 * @param cookie         A blind cookie the alert will pass back to the 
		 *                       consumer during the alert listener callbacks.
		 * @param alertListener  Used for callbacks. May be null if the caller 
		 *                       doesn't care about callbacks.
		 * @param name           The name of the notification.  This is currently
		 *                       only used on OS X with Growl.  On OS X with Growl,
		 *                       users can disable notifications with a given name.
		 *
		 * @throws NS_ERROR_NOT_AVAILABLE If the notification cannot be displayed.
		 *
		 * The following arguments will be passed to the alertListener's observe() 
		 * method:
		 *   subject - null
		 *   topic   - "alertfinished" when the alert goes away
		 *             "alertclickcallback" when the text is clicked
		 *   data    - the value of the cookie parameter passed to showAlertNotification.
		 */
		void ShowAlertNotification([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String imageUrl,
								   [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String title,
								   [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String text,
								   [Optional] Boolean textClickable,
								   [Optional, In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String cookie,
								   [Optional] nsIObserver alertListener,
								   [Optional, In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String name);
	}
}
