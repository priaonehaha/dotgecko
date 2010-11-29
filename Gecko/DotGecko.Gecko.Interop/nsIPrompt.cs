using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	public static class nsIPromptConstants
	{
		public const UInt32 BUTTON_POS_0 = 1;
		public const UInt32 BUTTON_POS_1 = 1 << 8;
		public const UInt32 BUTTON_POS_2 = 1 << 16;

		public const UInt32 BUTTON_TITLE_OK = 1;
		public const UInt32 BUTTON_TITLE_CANCEL = 2;
		public const UInt32 BUTTON_TITLE_YES = 3;
		public const UInt32 BUTTON_TITLE_NO = 4;
		public const UInt32 BUTTON_TITLE_SAVE = 5;
		public const UInt32 BUTTON_TITLE_DONT_SAVE = 6;
		public const UInt32 BUTTON_TITLE_REVERT = 7;

		public const UInt32 BUTTON_TITLE_IS_STRING = 127;

		public const UInt32 BUTTON_POS_0_DEFAULT = 0 << 24;
		public const UInt32 BUTTON_POS_1_DEFAULT = 1 << 24;
		public const UInt32 BUTTON_POS_2_DEFAULT = 2 << 24;

		/* used for security dialogs, buttons are initially disabled */
		public const UInt32 BUTTON_DELAY_ENABLE = 1 << 26;

		public const UInt32 STD_OK_CANCEL_BUTTONS = (BUTTON_TITLE_OK * BUTTON_POS_0) + (BUTTON_TITLE_CANCEL * BUTTON_POS_1);
		public const UInt32 STD_YES_NO_BUTTONS = (BUTTON_TITLE_YES * BUTTON_POS_0) + (BUTTON_TITLE_NO * BUTTON_POS_1);
	}

	/**
	 * @status UNDER_REVIEW
	 */

	/**
	 * This is the prompt interface which can be used without knowlege of a
	 * parent window. The parentage is hidden by the GetInterface though
	 * which it is gotten. This interface is identical to nsIPromptService
	 * but without the parent nsIDOMWindow parameter. See nsIPromptService
	 * for all documentation.
	 *
	 * Accesskeys can be attached to buttons and checkboxes by inserting
	 * an & before the accesskey character. For a real &, use && instead.
	 */
	[ComImport, Guid("a63f70c0-148b-11d3-9333-00104ba0fd40"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIPrompt //: nsISupports
	{
		void Alert([MarshalAs(UnmanagedType.LPWStr)] String dialogTitle, [MarshalAs(UnmanagedType.LPWStr)] String text);

		void AlertCheck([MarshalAs(UnmanagedType.LPWStr)] String dialogTitle, [MarshalAs(UnmanagedType.LPWStr)] String text, [MarshalAs(UnmanagedType.LPWStr)] String checkMsg, ref Boolean checkValue);

		Boolean Confirm([MarshalAs(UnmanagedType.LPWStr)] String dialogTitle, [MarshalAs(UnmanagedType.LPWStr)] String text);

		Boolean ConfirmCheck([MarshalAs(UnmanagedType.LPWStr)] String dialogTitle, [MarshalAs(UnmanagedType.LPWStr)] String text, [MarshalAs(UnmanagedType.LPWStr)] String checkMsg, ref Boolean checkValue);

		Int32 ConfirmEx([MarshalAs(UnmanagedType.LPWStr)] String dialogTitle,
						[MarshalAs(UnmanagedType.LPWStr)] String text,
						UInt32 buttonFlags,
						[MarshalAs(UnmanagedType.LPWStr)] String button0Title,
						[MarshalAs(UnmanagedType.LPWStr)] String button1Title,
						[MarshalAs(UnmanagedType.LPWStr)] String button2Title,
						[MarshalAs(UnmanagedType.LPWStr)] String checkMsg,
						ref Boolean checkValue);

		Boolean Prompt([MarshalAs(UnmanagedType.LPWStr)] String dialogTitle,
					   [MarshalAs(UnmanagedType.LPWStr)] String text,
					   [MarshalAs(UnmanagedType.LPWStr)] StringBuilder value,
					   [MarshalAs(UnmanagedType.LPWStr)] String checkMsg,
					   ref Boolean checkValue);

		Boolean PromptPassword([MarshalAs(UnmanagedType.LPWStr)] String dialogTitle,
							   [MarshalAs(UnmanagedType.LPWStr)] String text,
							   [MarshalAs(UnmanagedType.LPWStr)] StringBuilder password,
							   [MarshalAs(UnmanagedType.LPWStr)] String checkMsg,
							   ref Boolean checkValue);

		Boolean PromptUsernameAndPassword([MarshalAs(UnmanagedType.LPWStr)] String dialogTitle,
										  [MarshalAs(UnmanagedType.LPWStr)] String text,
										  [MarshalAs(UnmanagedType.LPWStr)] StringBuilder username,
										  [MarshalAs(UnmanagedType.LPWStr)] StringBuilder password,
										  [MarshalAs(UnmanagedType.LPWStr)] String checkMsg,
										  ref Boolean checkValue);

		Boolean Select([MarshalAs(UnmanagedType.LPWStr)] String dialogTitle,
					   [MarshalAs(UnmanagedType.LPWStr)] String text,
					   UInt32 count,
					   [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 2)] String[] selectList,
					   out Int32 outSelection);
	}
}
