using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;
using DOMTimeStamp = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	internal static class nsIDOMKeyEventConstants
	{
		internal const UInt32 DOM_VK_CANCEL = 0x03;
		internal const UInt32 DOM_VK_HELP = 0x06;
		internal const UInt32 DOM_VK_BACK_SPACE = 0x08;
		internal const UInt32 DOM_VK_TAB = 0x09;
		internal const UInt32 DOM_VK_CLEAR = 0x0C;
		internal const UInt32 DOM_VK_RETURN = 0x0D;
		internal const UInt32 DOM_VK_ENTER = 0x0E;
		internal const UInt32 DOM_VK_SHIFT = 0x10;
		internal const UInt32 DOM_VK_CONTROL = 0x11;
		internal const UInt32 DOM_VK_ALT = 0x12;
		internal const UInt32 DOM_VK_PAUSE = 0x13;
		internal const UInt32 DOM_VK_CAPS_LOCK = 0x14;
		internal const UInt32 DOM_VK_ESCAPE = 0x1B;
		internal const UInt32 DOM_VK_SPACE = 0x20;
		internal const UInt32 DOM_VK_PAGE_UP = 0x21;
		internal const UInt32 DOM_VK_PAGE_DOWN = 0x22;
		internal const UInt32 DOM_VK_END = 0x23;
		internal const UInt32 DOM_VK_HOME = 0x24;
		internal const UInt32 DOM_VK_LEFT = 0x25;
		internal const UInt32 DOM_VK_UP = 0x26;
		internal const UInt32 DOM_VK_RIGHT = 0x27;
		internal const UInt32 DOM_VK_DOWN = 0x28;
		internal const UInt32 DOM_VK_PRINTSCREEN = 0x2C;
		internal const UInt32 DOM_VK_INSERT = 0x2D;
		internal const UInt32 DOM_VK_DELETE = 0x2E;

		// DOM_VK_0 - DOM_VK_9 match their ascii values
		internal const UInt32 DOM_VK_0 = 0x30;
		internal const UInt32 DOM_VK_1 = 0x31;
		internal const UInt32 DOM_VK_2 = 0x32;
		internal const UInt32 DOM_VK_3 = 0x33;
		internal const UInt32 DOM_VK_4 = 0x34;
		internal const UInt32 DOM_VK_5 = 0x35;
		internal const UInt32 DOM_VK_6 = 0x36;
		internal const UInt32 DOM_VK_7 = 0x37;
		internal const UInt32 DOM_VK_8 = 0x38;
		internal const UInt32 DOM_VK_9 = 0x39;

		internal const UInt32 DOM_VK_SEMICOLON = 0x3B;
		internal const UInt32 DOM_VK_EQUALS = 0x3D;

		// DOM_VK_A - DOM_VK_Z match their ascii values
		internal const UInt32 DOM_VK_A = 0x41;
		internal const UInt32 DOM_VK_B = 0x42;
		internal const UInt32 DOM_VK_C = 0x43;
		internal const UInt32 DOM_VK_D = 0x44;
		internal const UInt32 DOM_VK_E = 0x45;
		internal const UInt32 DOM_VK_F = 0x46;
		internal const UInt32 DOM_VK_G = 0x47;
		internal const UInt32 DOM_VK_H = 0x48;
		internal const UInt32 DOM_VK_I = 0x49;
		internal const UInt32 DOM_VK_J = 0x4A;
		internal const UInt32 DOM_VK_K = 0x4B;
		internal const UInt32 DOM_VK_L = 0x4C;
		internal const UInt32 DOM_VK_M = 0x4D;
		internal const UInt32 DOM_VK_N = 0x4E;
		internal const UInt32 DOM_VK_O = 0x4F;
		internal const UInt32 DOM_VK_P = 0x50;
		internal const UInt32 DOM_VK_Q = 0x51;
		internal const UInt32 DOM_VK_R = 0x52;
		internal const UInt32 DOM_VK_S = 0x53;
		internal const UInt32 DOM_VK_T = 0x54;
		internal const UInt32 DOM_VK_U = 0x55;
		internal const UInt32 DOM_VK_V = 0x56;
		internal const UInt32 DOM_VK_W = 0x57;
		internal const UInt32 DOM_VK_X = 0x58;
		internal const UInt32 DOM_VK_Y = 0x59;
		internal const UInt32 DOM_VK_Z = 0x5A;

		internal const UInt32 DOM_VK_CONTEXT_MENU = 0x5D;

		internal const UInt32 DOM_VK_NUMPAD0 = 0x60;
		internal const UInt32 DOM_VK_NUMPAD1 = 0x61;
		internal const UInt32 DOM_VK_NUMPAD2 = 0x62;
		internal const UInt32 DOM_VK_NUMPAD3 = 0x63;
		internal const UInt32 DOM_VK_NUMPAD4 = 0x64;
		internal const UInt32 DOM_VK_NUMPAD5 = 0x65;
		internal const UInt32 DOM_VK_NUMPAD6 = 0x66;
		internal const UInt32 DOM_VK_NUMPAD7 = 0x67;
		internal const UInt32 DOM_VK_NUMPAD8 = 0x68;
		internal const UInt32 DOM_VK_NUMPAD9 = 0x69;
		internal const UInt32 DOM_VK_MULTIPLY = 0x6A;
		internal const UInt32 DOM_VK_ADD = 0x6B;
		internal const UInt32 DOM_VK_SEPARATOR = 0x6C;
		internal const UInt32 DOM_VK_SUBTRACT = 0x6D;
		internal const UInt32 DOM_VK_DECIMAL = 0x6E;
		internal const UInt32 DOM_VK_DIVIDE = 0x6F;
		internal const UInt32 DOM_VK_F1 = 0x70;
		internal const UInt32 DOM_VK_F2 = 0x71;
		internal const UInt32 DOM_VK_F3 = 0x72;
		internal const UInt32 DOM_VK_F4 = 0x73;
		internal const UInt32 DOM_VK_F5 = 0x74;
		internal const UInt32 DOM_VK_F6 = 0x75;
		internal const UInt32 DOM_VK_F7 = 0x76;
		internal const UInt32 DOM_VK_F8 = 0x77;
		internal const UInt32 DOM_VK_F9 = 0x78;
		internal const UInt32 DOM_VK_F10 = 0x79;
		internal const UInt32 DOM_VK_F11 = 0x7A;
		internal const UInt32 DOM_VK_F12 = 0x7B;
		internal const UInt32 DOM_VK_F13 = 0x7C;
		internal const UInt32 DOM_VK_F14 = 0x7D;
		internal const UInt32 DOM_VK_F15 = 0x7E;
		internal const UInt32 DOM_VK_F16 = 0x7F;
		internal const UInt32 DOM_VK_F17 = 0x80;
		internal const UInt32 DOM_VK_F18 = 0x81;
		internal const UInt32 DOM_VK_F19 = 0x82;
		internal const UInt32 DOM_VK_F20 = 0x83;
		internal const UInt32 DOM_VK_F21 = 0x84;
		internal const UInt32 DOM_VK_F22 = 0x85;
		internal const UInt32 DOM_VK_F23 = 0x86;
		internal const UInt32 DOM_VK_F24 = 0x87;

		internal const UInt32 DOM_VK_NUM_LOCK = 0x90;
		internal const UInt32 DOM_VK_SCROLL_LOCK = 0x91;

		internal const UInt32 DOM_VK_COMMA = 0xBC;
		internal const UInt32 DOM_VK_PERIOD = 0xBE;
		internal const UInt32 DOM_VK_SLASH = 0xBF;
		internal const UInt32 DOM_VK_BACK_QUOTE = 0xC0;
		internal const UInt32 DOM_VK_OPEN_BRACKET = 0xDB;
		internal const UInt32 DOM_VK_BACK_SLASH = 0xDC;
		internal const UInt32 DOM_VK_CLOSE_BRACKET = 0xDD;
		internal const UInt32 DOM_VK_QUOTE = 0xDE;

		internal const UInt32 DOM_VK_META = 0xE0;
	}

	[ComImport, Guid("028e0e6e-8b01-11d3-aae7-0010838a3123"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMKeyEvent : nsIDOMUIEvent
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

		UInt32 CharCode { get; }
		UInt32 KeyCode { get; }

		Boolean AltKey { get; }
		Boolean CtrlKey { get; }
		Boolean ShiftKey { get; }
		Boolean MetaKey { get; }

		void InitKeyEvent([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String typeArg,
						  Boolean canBubbleArg,
						  Boolean cancelableArg,
						  nsIDOMAbstractView viewArg,
						  Boolean ctrlKeyArg,
						  Boolean altKeyArg,
						  Boolean shiftKeyArg,
						  Boolean metaKeyArg,
						  UInt32 keyCodeArg,
						  UInt32 charCodeArg);
	}
}
