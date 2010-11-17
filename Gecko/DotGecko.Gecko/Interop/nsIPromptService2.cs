using System;
using System.Runtime.InteropServices;
using System.Text;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * This is an improved version of nsIPromptService that is less prescriptive
	 * about the resulting user interface.
	 *
	 * @status INCOMPLETE do not freeze before fixing bug 228207
	 */
	[ComImport, Guid("cf86d196-dbee-4482-9dfa-3477aa128319"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIPromptService2 : nsIPromptService
	{
		#region nsIPromptService Members

		new void Alert(nsIDOMWindow aParent, [MarshalAs(UnmanagedType.LPWStr)] String aDialogTitle, [MarshalAs(UnmanagedType.LPWStr)] String aText);
		new void AlertCheck(nsIDOMWindow aParent,
							[MarshalAs(UnmanagedType.LPWStr)] String aDialogTitle,
							[MarshalAs(UnmanagedType.LPWStr)] String aText,
							[MarshalAs(UnmanagedType.LPWStr)] String aCheckMsg,
							ref Boolean aCheckState);
		new Boolean Confirm(nsIDOMWindow aParent, [MarshalAs(UnmanagedType.LPWStr)] String aDialogTitle, [MarshalAs(UnmanagedType.LPWStr)] String aText);
		new Boolean ConfirmCheck(nsIDOMWindow aParent,
								 [MarshalAs(UnmanagedType.LPWStr)] String aDialogTitle,
								 [MarshalAs(UnmanagedType.LPWStr)] String aText,
								 [MarshalAs(UnmanagedType.LPWStr)] String aCheckMsg,
								 ref Boolean aCheckState);
		new Int32 ConfirmEx(nsIDOMWindow aParent,
							[MarshalAs(UnmanagedType.LPWStr)] String aDialogTitle,
							[MarshalAs(UnmanagedType.LPWStr)] String aText,
							UInt32 aButtonFlags,
							[MarshalAs(UnmanagedType.LPWStr)] String aButton0Title,
							[MarshalAs(UnmanagedType.LPWStr)] String aButton1Title,
							[MarshalAs(UnmanagedType.LPWStr)] String aButton2Title,
							[MarshalAs(UnmanagedType.LPWStr)] String aCheckMsg,
							ref Boolean aCheckState);
		new Boolean Prompt(nsIDOMWindow aParent,
						   [MarshalAs(UnmanagedType.LPWStr)] String aDialogTitle,
						   [MarshalAs(UnmanagedType.LPWStr)] String aText,
						   [MarshalAs(UnmanagedType.LPWStr)] StringBuilder aValue,
						   [MarshalAs(UnmanagedType.LPWStr)] String aCheckMsg,
						   ref Boolean aCheckState);
		new Boolean PromptUsernameAndPassword(nsIDOMWindow aParent,
											  [MarshalAs(UnmanagedType.LPWStr)] String aDialogTitle,
											  [MarshalAs(UnmanagedType.LPWStr)] String aText,
											  [MarshalAs(UnmanagedType.LPWStr)] StringBuilder aUsername,
											  [MarshalAs(UnmanagedType.LPWStr)] StringBuilder aPassword,
											  [MarshalAs(UnmanagedType.LPWStr)] String aCheckMsg,
											  ref Boolean aCheckState);
		new Boolean PromptPassword(nsIDOMWindow aParent,
								   [MarshalAs(UnmanagedType.LPWStr)] String aDialogTitle,
								   [MarshalAs(UnmanagedType.LPWStr)] String aText,
								   [MarshalAs(UnmanagedType.LPWStr)] StringBuilder aPassword,
								   [MarshalAs(UnmanagedType.LPWStr)] String aCheckMsg,
								   ref Boolean aCheckState);
		new Boolean Select(nsIDOMWindow aParent,
						   [MarshalAs(UnmanagedType.LPWStr)] String aDialogTitle,
						   [MarshalAs(UnmanagedType.LPWStr)] String aText,
						   UInt32 aCount,
						   [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 3)] String[] aSelectList,
						   out Int32 aOutSelection);

		#endregion

		// NOTE: These functions differ from their nsIAuthPrompt counterparts by
		// having additional checkbox parameters
		// checkValue can be null meaning to show no checkbox
		// checkboxLabel is a wstring so that it can be null from both JS and C++ in
		// a convenient way
		//
		// See nsIAuthPrompt2 for documentation on the semantics of the other
		// parameters.
		Boolean PromptAuth(nsIDOMWindow aParent,
						   nsIChannel aChannel,
						   UInt32 level,
						   nsIAuthInformation authInfo,
						   [MarshalAs(UnmanagedType.LPWStr)] String checkboxLabel,
						   ref Boolean checkValue);

		nsICancelable AsyncPromptAuth(nsIDOMWindow aParent,
									  nsIChannel aChannel,
									  nsIAuthPromptCallback aCallback,
									  [MarshalAs(UnmanagedType.IUnknown)] nsISupports aContext,
									  UInt32 level,
									  nsIAuthInformation authInfo,
									  [MarshalAs(UnmanagedType.LPWStr)] String checkboxLabel,
									  ref Boolean checkValue);
	}
}
