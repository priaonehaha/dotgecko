using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	public static class nsIAuthPrompt2Constants
	{
		/** @name Security Levels */
		/* @{ */
		/**
		 * The password will be sent unencrypted. No security provided.
		 */
		public const UInt32 LEVEL_NONE = 0;
		/**
		 * Password will be sent encrypted, but the connection is otherwise
		 * insecure.
		 */
		public const UInt32 LEVEL_PW_ENCRYPTED = 1;
		/**
		 * The connection, both for password and data, is secure.
		 */
		public const UInt32 LEVEL_SECURE = 2;
		/* @} */
	}

	/**
	 * An interface allowing to prompt for a username and password. This interface
	 * is usually acquired using getInterface on notification callbacks or similar.
	 * It can be used to prompt users for authentication information, either
	 * synchronously or asynchronously.
	 */
	[ComImport, Guid("651395EB-8612-4876-8AC0-A88D4DCE9E1E"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIAuthPrompt2 //: nsISupports
	{
		/**
		 * Requests a username and a password. Implementations will commonly show a
		 * dialog with a username and password field, depending on flags also a
		 * domain field.
		 *
		 * @param aChannel
		 *        The channel that requires authentication.
		 * @param level
		 *        One of the level constants from above. See there for descriptions
		 *        of the levels.
		 * @param authInfo
		 *        Authentication information object. The implementation should fill in
		 *        this object with the information entered by the user before
		 *        returning.
		 *
		 * @retval true
		 *         Authentication can proceed using the values in the authInfo
		 *         object.
		 * @retval false
		 *         Authentication should be cancelled, usually because the user did
		 *         not provide username/password.
		 *
		 * @note   Exceptions thrown from this function will be treated like a
		 *         return value of false.
		 */
		Boolean PromptAuth(nsIChannel aChannel, UInt32 level, nsIAuthInformation authInfo);

		/**
		 * Asynchronously prompt the user for a username and password.
		 * This has largely the same semantics as promptUsernameAndPassword(),
		 * but must return immediately after calling and return the entered
		 * data in a callback.
		 *
		 * If the user closes the dialog using a cancel button or similar,
		 * the callback's nsIAuthPromptCallback::onAuthCancelled method must be
		 * called.
		 * Calling nsICancelable::cancel on the returned object SHOULD close the
		 * dialog and MUST call nsIAuthPromptCallback::onAuthCancelled on the provided
		 * callback.
		 *
		 * This implementation may:
		 *
		 *  1) Coalesce identical prompts.  This means prompts that are guaranteed to
		 *     want the same auth information from the user.  A single prompt will be
		 *     shown; then the callbacks for all the coalesced prompts will be notified
		 *     with the resulting auth information.
		 *  2) Serialize prompts that are all in the same "context" (this might mean
		 *     application-wide, for a given window, or something else depending on
		 *     the user interface) so that the user is not deluged with prompts.
		 *
		 * @throw
		 *     This method may throw any exception when the prompt fails to queue e.g
		 *     because of out-of-memory error. It must not throw when the prompt
		 *     could already be potentially shown to the user. In that case information
		 *     about the failure has to come through the callback. This way we
		 *     prevent multiple dialogs shown to the user because consumer may fall
		 *     back to synchronous prompt on synchronous failure of this method.
		 */
		nsICancelable AsyncPromptAuth(nsIChannel aChannel,
									  nsIAuthPromptCallback aCallback,
									  [MarshalAs(UnmanagedType.IUnknown)] nsISupports aContext,
									  UInt32 level,
									  nsIAuthInformation authInfo);
	}
}
