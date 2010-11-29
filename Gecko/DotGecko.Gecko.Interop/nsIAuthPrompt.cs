using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	public static class nsIAuthPromptConstants
	{
		public const UInt32 SAVE_PASSWORD_NEVER = 0;
		public const UInt32 SAVE_PASSWORD_FOR_SESSION = 1;
		public const UInt32 SAVE_PASSWORD_PERMANENTLY = 2;
	}

	/**
	 * @status UNDER_REVIEW
	 */
	[ComImport, Guid("358089f9-ee4b-4711-82fd-bcd07fc62061"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIAuthPrompt //: nsISupports
	{
		/**
		 * Puts up a text input dialog with OK and Cancel buttons.
		 * Note: prompt uses separate args for the "in" and "out" values of the
		 *       input field, whereas the other functions use a single inout arg.
		 * @param  dialogText    The title for the dialog.
		 * @param  text          The text to display in the dialog.
		 * @param  passwordRealm The "realm" the password belongs to: e.g.
		 *                       ldap://localhost/dc=test
		 * @param  savePassword  One of the SAVE_PASSWORD_* options above.
		 * @param  defaultText   The default text to display in the text input box.
		 * @param  result        The value entered by the user if OK was
		 *                       selected.
		 * @return true for OK, false for Cancel
		 */
		Boolean Prompt([MarshalAs(UnmanagedType.LPWStr)] String dialogTitle,
					   [MarshalAs(UnmanagedType.LPWStr)] String text,
					   [MarshalAs(UnmanagedType.LPWStr)] String passwordRealm,
					   UInt32 savePassword,
					   [MarshalAs(UnmanagedType.LPWStr)] String defaultText,
					   [MarshalAs(UnmanagedType.LPWStr)] out String result);

		/**
		 * Puts up a username/password dialog with OK and Cancel buttons.
		 * Puts up a password dialog with OK and Cancel buttons.
		 * @param  dialogText    The title for the dialog.
		 * @param  text          The text to display in the dialog.
		 * @param  passwordRealm The "realm" the password belongs to: e.g.
		 *                       ldap://localhost/dc=test
		 * @param  savePassword  One of the SAVE_PASSWORD_* options above.
		 * @param  user          The username entered in the dialog.
		 * @param  pwd           The password entered by the user if OK was
		 *                       selected.
		 * @return true for OK, false for Cancel
		 */
		Boolean PromptUsernameAndPassword([MarshalAs(UnmanagedType.LPWStr)] String dialogTitle,
										  [MarshalAs(UnmanagedType.LPWStr)] String text,
										  [MarshalAs(UnmanagedType.LPWStr)] String passwordRealm,
										  UInt32 savePassword,
										  [MarshalAs(UnmanagedType.LPWStr)] StringBuilder user,
										  [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwd);

		/**
		 * Puts up a password dialog with OK and Cancel buttons.
		 * @param  dialogText    The title for the dialog.
		 * @param  text          The text to display in the dialog.
		 * @param  passwordRealm The "realm" the password belongs to: e.g.
		 *                       ldap://localhost/dc=test. If a username is
		 *                       specified (http://user@site.com) it will be used
		 *                       when matching existing logins or saving new ones.
		 *                       If no username is specified, only password-only
		 *                       logins will be matched or saved.
		 *                       Note: if a username is specified, the username
		 *                       should be escaped.
		 * @param  savePassword  One of the SAVE_PASSWORD_* options above.
		 * @param  pwd           The password entered by the user if OK was
		 *                       selected.
		 * @return true for OK, false for Cancel
		 */
		Boolean PromptPassword([MarshalAs(UnmanagedType.LPWStr)] String dialogTitle,
							   [MarshalAs(UnmanagedType.LPWStr)] String text,
							   [MarshalAs(UnmanagedType.LPWStr)] String passwordRealm,
							   UInt32 savePassword,
							   [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwd);
	}
}
