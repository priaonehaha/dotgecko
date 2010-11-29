using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;
using DOMTimeStamp = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMPopupBlockedEvent interface is the datatype for events
	 * posted when a popup window is blocked.
	 */
	[ComImport, Guid("05be571f-c3ea-4959-a340-c57b1591ae4b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMPopupBlockedEvent : nsIDOMEvent
	{
		#region nsIDOMEvent Members

		new void GetType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new nsIDOMEventTarget Target { get; }
		new nsIDOMEventTarget CurrentTarget { get; }
		new UInt16 EventPhase { get; }
		new Boolean Bubbles { get; }
		new Boolean Cancelable { get; }
		new DOMTimeStamp TimeStamp { get; }
		new void StopPropagation();
		new void PreventDefault();
		new void InitEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String eventTypeArg, Boolean canBubbleArg, Boolean cancelableArg);

		#endregion

		/**
		 * The window object that attempted to open the blocked popup
		 * (i.e. the window object on which open() was called).
		 */
		nsIDOMWindow RequestingWindow { get; }

		/**
		 * The URI of the window that was blocked.
		 */
		nsIURI PopupWindowURI { get; }

		/**
		 * The string of features passed to the window.open() call
		 * (as the third argument)
		 */
		void GetPopupWindowFeatures([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);

		/**
		 * The window name passed to the window.open() call
		 * (as the second argument)
		 */
		void GetPopupWindowName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);

		void InitPopupBlockedEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String typeArg,
								   Boolean canBubbleArg,
								   Boolean cancelableArg,
								   nsIDOMWindow requestingWindow,
								   nsIURI popupWindowURI,
								   [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String popupWindowName,
								   [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String popupWindowFeatures);
	}
}
