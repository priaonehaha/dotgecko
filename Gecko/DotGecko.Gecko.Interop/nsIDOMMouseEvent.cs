using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;
using DOMTimeStamp = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMMouseEvent interface is the datatype for all mouse events
	 * in the Document Object Model.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Events/
	 */
	[ComImport, Guid("ff751edc-8b02-aae7-0010-8301838a3123"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMMouseEvent : nsIDOMUIEvent
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

		#region nsIDOMUIEvent Members

		new nsIDOMAbstractView View { get; }
		new Int32 Detail { get; }
		new void InitUIEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String typeArg, Boolean canBubbleArg, Boolean cancelableArg, nsIDOMAbstractView viewArg, Int32 detailArg);

		#endregion

		Int32 ScreenX { get; }
		Int32 ScreenY { get; }

		Int32 ClientX { get; }
		Int32 ClientY { get; }

		Boolean CtrlKey { get; }
		Boolean ShiftKey { get; }
		Boolean AltKey { get; }
		Boolean MetaKey { get; }

		UInt16 Button { get; }
		nsIDOMEventTarget RelatedTarget { get; }

		void InitMouseEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String typeArg,
							Boolean canBubbleArg,
							Boolean cancelableArg,
							nsIDOMAbstractView viewArg,
							Int32 detailArg,
							Int32 screenXArg,
							Int32 screenYArg,
							Int32 clientXArg,
							Int32 clientYArg,
							Boolean ctrlKeyArg,
							Boolean altKeyArg,
							Boolean shiftKeyArg,
							Boolean metaKeyArg,
							UInt16 buttonArg,
							nsIDOMEventTarget relatedTargetArg);
	}
}
