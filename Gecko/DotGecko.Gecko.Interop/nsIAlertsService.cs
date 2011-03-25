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
		 * @param name           The name of the notification. This is currently
		 *                       only used on OS X with Growl and Android.
		 *                       On OS X with Growl, users can disable notifications
		 *                       with a given name. On Android the name is hashed
		 *                       and used as a notification ID.
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

	[ComImport, Guid("df1bd4b0-3a8c-40e6-806a-203f38b0bd9f"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIAlertsProgressListener //: nsISupports
	{
		/**
		 * Called to notify the alert service that progress has occurred for the
		 * given notification previously displayed with showAlertNotification().
		 *
		 * @param name         The name of the notification displaying the
		 *                     progress. On Android the name is hashed and used
		 *                     as a notification ID.
		 * @param progress     Numeric value in the range 0 to progressMax
		 *                     indicating the current progress.
		 * @param progressMax  Numeric value indicating the maximum progress.
		 * @param text         The contents of the alert. If not provided,
		 *                     the percentage will be displayed.
		 */
		void OnProgress([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String name,
						Int64 progress,
						Int64 progressMax,
						[In, Optional, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String text);

		/**
		 * Called to cancel and hide the given notification previously displayed
		 * with showAlertNotification().
		 *
		 * @param name         The name of the notification.
		 */
		void OnCancel([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String name);
	}

}
