using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;
using DOMTimeStamp = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMUIEvent interface is the datatype for all UI events in the
	 * Document Object Model.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Events/
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("a6cf90c3-15b3-11d2-932e-00805f8add32"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMUIEvent : nsIDOMEvent
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

		nsIDOMAbstractView View { get; }
		Int32 Detail { get; }
		void InitUIEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String typeArg,
						 Boolean canBubbleArg,
						 Boolean cancelableArg,
						 nsIDOMAbstractView viewArg,
						 Int32 detailArg);
	}
}
