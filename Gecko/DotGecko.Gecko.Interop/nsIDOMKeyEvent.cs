using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;
using DOMTimeStamp = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	public static class nsIDOMKeyEventConstants
	{
		public const UInt32 DOM_VK_CANCEL = 0x03;
		public const UInt32 DOM_VK_HELP = 0x06;
		public const UInt32 DOM_VK_BACK_SPACE = 0x08;
		public const UInt32 DOM_VK_TAB = 0x09;
		public const UInt32 DOM_VK_CLEAR = 0x0C;
		public const UInt32 DOM_VK_RETURN = 0x0D;
		public const UInt32 DOM_VK_ENTER = 0x0E;
		public const UInt32 DOM_VK_SHIFT = 0x10;
		public const UInt32 DOM_VK_CONTROL = 0x11;
		public const UInt32 DOM_VK_ALT = 0x12;
		public const UInt32 DOM_VK_PAUSE = 0x13;
		public const UInt32 DOM_VK_CAPS_LOCK = 0x14;
		public const UInt32 DOM_VK_ESCAPE = 0x1B;
		public const UInt32 DOM_VK_SPACE = 0x20;
		public const UInt32 DOM_VK_PAGE_UP = 0x21;
		public const UInt32 DOM_VK_PAGE_DOWN = 0x22;
		public const UInt32 DOM_VK_END = 0x23;
		public const UInt32 DOM_VK_HOME = 0x24;
		public const UInt32 DOM_VK_LEFT = 0x25;
		public const UInt32 DOM_VK_UP = 0x26;
		public const UInt32 DOM_VK_RIGHT = 0x27;
		public const UInt32 DOM_VK_DOWN = 0x28;
		public const UInt32 DOM_VK_PRINTSCREEN = 0x2C;
		public const UInt32 DOM_VK_INSERT = 0x2D;
		public const UInt32 DOM_VK_DELETE = 0x2E;

		// DOM_VK_0 - DOM_VK_9 match their ascii values
		public const UInt32 DOM_VK_0 = 0x30;
		public const UInt32 DOM_VK_1 = 0x31;
		public const UInt32 DOM_VK_2 = 0x32;
		public const UInt32 DOM_VK_3 = 0x33;
		public const UInt32 DOM_VK_4 = 0x34;
		public const UInt32 DOM_VK_5 = 0x35;
		public const UInt32 DOM_VK_6 = 0x36;
		public const UInt32 DOM_VK_7 = 0x37;
		public const UInt32 DOM_VK_8 = 0x38;
		public const UInt32 DOM_VK_9 = 0x39;

		public const UInt32 DOM_VK_SEMICOLON = 0x3B;
		public const UInt32 DOM_VK_EQUALS = 0x3D;

		// DOM_VK_A - DOM_VK_Z match their ascii values
		public const UInt32 DOM_VK_A = 0x41;
		public const UInt32 DOM_VK_B = 0x42;
		public const UInt32 DOM_VK_C = 0x43;
		public const UInt32 DOM_VK_D = 0x44;
		public const UInt32 DOM_VK_E = 0x45;
		public const UInt32 DOM_VK_F = 0x46;
		public const UInt32 DOM_VK_G = 0x47;
		public const UInt32 DOM_VK_H = 0x48;
		public const UInt32 DOM_VK_I = 0x49;
		public const UInt32 DOM_VK_J = 0x4A;
		public const UInt32 DOM_VK_K = 0x4B;
		public const UInt32 DOM_VK_L = 0x4C;
		public const UInt32 DOM_VK_M = 0x4D;
		public const UInt32 DOM_VK_N = 0x4E;
		public const UInt32 DOM_VK_O = 0x4F;
		public const UInt32 DOM_VK_P = 0x50;
		public const UInt32 DOM_VK_Q = 0x51;
		public const UInt32 DOM_VK_R = 0x52;
		public const UInt32 DOM_VK_S = 0x53;
		public const UInt32 DOM_VK_T = 0x54;
		public const UInt32 DOM_VK_U = 0x55;
		public const UInt32 DOM_VK_V = 0x56;
		public const UInt32 DOM_VK_W = 0x57;
		public const UInt32 DOM_VK_X = 0x58;
		public const UInt32 DOM_VK_Y = 0x59;
		public const UInt32 DOM_VK_Z = 0x5A;

		public const UInt32 DOM_VK_CONTEXT_MENU = 0x5D;

		public const UInt32 DOM_VK_NUMPAD0 = 0x60;
		public const UInt32 DOM_VK_NUMPAD1 = 0x61;
		public const UInt32 DOM_VK_NUMPAD2 = 0x62;
		public const UInt32 DOM_VK_NUMPAD3 = 0x63;
		public const UInt32 DOM_VK_NUMPAD4 = 0x64;
		public const UInt32 DOM_VK_NUMPAD5 = 0x65;
		public const UInt32 DOM_VK_NUMPAD6 = 0x66;
		public const UInt32 DOM_VK_NUMPAD7 = 0x67;
		public const UInt32 DOM_VK_NUMPAD8 = 0x68;
		public const UInt32 DOM_VK_NUMPAD9 = 0x69;
		public const UInt32 DOM_VK_MULTIPLY = 0x6A;
		public const UInt32 DOM_VK_ADD = 0x6B;
		public const UInt32 DOM_VK_SEPARATOR = 0x6C;
		public const UInt32 DOM_VK_SUBTRACT = 0x6D;
		public const UInt32 DOM_VK_DECIMAL = 0x6E;
		public const UInt32 DOM_VK_DIVIDE = 0x6F;
		public const UInt32 DOM_VK_F1 = 0x70;
		public const UInt32 DOM_VK_F2 = 0x71;
		public const UInt32 DOM_VK_F3 = 0x72;
		public const UInt32 DOM_VK_F4 = 0x73;
		public const UInt32 DOM_VK_F5 = 0x74;
		public const UInt32 DOM_VK_F6 = 0x75;
		public const UInt32 DOM_VK_F7 = 0x76;
		public const UInt32 DOM_VK_F8 = 0x77;
		public const UInt32 DOM_VK_F9 = 0x78;
		public const UInt32 DOM_VK_F10 = 0x79;
		public const UInt32 DOM_VK_F11 = 0x7A;
		public const UInt32 DOM_VK_F12 = 0x7B;
		public const UInt32 DOM_VK_F13 = 0x7C;
		public const UInt32 DOM_VK_F14 = 0x7D;
		public const UInt32 DOM_VK_F15 = 0x7E;
		public const UInt32 DOM_VK_F16 = 0x7F;
		public const UInt32 DOM_VK_F17 = 0x80;
		public const UInt32 DOM_VK_F18 = 0x81;
		public const UInt32 DOM_VK_F19 = 0x82;
		public const UInt32 DOM_VK_F20 = 0x83;
		public const UInt32 DOM_VK_F21 = 0x84;
		public const UInt32 DOM_VK_F22 = 0x85;
		public const UInt32 DOM_VK_F23 = 0x86;
		public const UInt32 DOM_VK_F24 = 0x87;

		public const UInt32 DOM_VK_NUM_LOCK = 0x90;
		public const UInt32 DOM_VK_SCROLL_LOCK = 0x91;

		public const UInt32 DOM_VK_COMMA = 0xBC;
		public const UInt32 DOM_VK_PERIOD = 0xBE;
		public const UInt32 DOM_VK_SLASH = 0xBF;
		public const UInt32 DOM_VK_BACK_QUOTE = 0xC0;
		public const UInt32 DOM_VK_OPEN_BRACKET = 0xDB;
		public const UInt32 DOM_VK_BACK_SLASH = 0xDC;
		public const UInt32 DOM_VK_CLOSE_BRACKET = 0xDD;
		public const UInt32 DOM_VK_QUOTE = 0xDE;

		public const UInt32 DOM_VK_META = 0xE0;
	}

	[ComImport, Guid("028e0e6e-8b01-11d3-aae7-0010838a3123"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMKeyEvent : nsIDOMUIEvent
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
