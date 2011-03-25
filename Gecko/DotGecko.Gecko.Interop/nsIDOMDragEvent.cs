using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;
using DOMTimeStamp = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("98351627-62d7-4b07-bbf3-78009b20764b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMDragEvent : nsIDOMMouseEvent
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

		#region nsIDOMMouseEvent Members

		new Int32 ScreenX { get; }
		new Int32 ScreenY { get; }
		new Int32 ClientX { get; }
		new Int32 ClientY { get; }
		new Boolean CtrlKey { get; }
		new Boolean ShiftKey { get; }
		new Boolean AltKey { get; }
		new Boolean MetaKey { get; }
		new UInt16 Button { get; }
		new nsIDOMEventTarget RelatedTarget { get; }
		new void InitMouseEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String typeArg,
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

		#endregion

		nsIDOMDataTransfer DataTransfer { get; }

		void InitDragEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String typeArg,
						   Boolean canBubbleArg,
						   Boolean cancelableArg,
						   nsIDOMAbstractView aView,
						   Int32 aDetail,
						   Int32 aScreenX,
						   Int32 aScreenY,
						   Int32 aClientX,
						   Int32 aClientY,
						   Boolean aCtrlKey,
						   Boolean aAltKey,
						   Boolean aShiftKey,
						   Boolean aMetaKey,
						   UInt16 aButton,
						   nsIDOMEventTarget aRelatedTarget,
						   nsIDOMDataTransfer aDataTransfer);
	}
}
