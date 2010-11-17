using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	internal static class nsIAuthInformationConstants
	{
		/** @name Flags */
		/* @{ */
		/**
		 * This dialog belongs to a network host.
		 */
		internal const UInt32 AUTH_HOST = 1;

		/**
		 * This dialog belongs to a proxy.
		 */
		internal const UInt32 AUTH_PROXY = 2;

		/**
		 * This dialog needs domain information. The user interface should show a
		 * domain field, prefilled with the domain attribute's value.
		 */
		internal const UInt32 NEED_DOMAIN = 4;

		/**
		 * This dialog only asks for password information. Authentication prompts
		 * SHOULD NOT show a username field. Attempts to change the username field
		 * will have no effect. nsIAuthPrompt2 implementations should, however, show
		 * its initial value to the user in some form. For example, a paragraph in
		 * the dialog might say "Please enter your password for user jsmith at
		 * server intranet".
		 *
		 * This flag is mutually exclusive with #NEED_DOMAIN.
		 */
		internal const UInt32 ONLY_PASSWORD = 8;
		/* @} */
	}

	/**
	 * A object that hold authentication information. The caller of
	 * nsIAuthPrompt2::promptUsernameAndPassword or
	 * nsIAuthPrompt2::promptPasswordAsync provides an object implementing this
	 * interface; the prompt implementation can then read the values here to prefill
	 * the dialog. After the user entered the authentication information, it should
	 * set the attributes of this object to indicate to the caller what was entered
	 * by the user.
	 */
	[ComImport, Guid("0d73639c-2a92-4518-9f92-28f71fea5f20"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIAuthInformation //: nsISupports
	{
		/**
		 * Flags describing this dialog. A bitwise OR of the flag values
		 * above.
		 *
		 * It is possible that neither #AUTH_HOST nor #AUTH_PROXY are set.
		 *
		 * Auth prompts should ignore flags they don't understand; especially, they
		 * should not throw an exception because of an unsupported flag.
		 */
		UInt32 Flags { get; }

		/**
		 * The server-supplied realm of the authentication as defined in RFC 2617.
		 * Can be the empty string if the protocol does not support realms.
		 * Otherwise, this is a human-readable string like "Secret files".
		 */
		void GetRealm([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);

		/**
		 * The authentication scheme used for this request, if applicable. If the
		 * protocol for this authentication does not support schemes, this will be
		 * the empty string. Otherwise, this will be a string such as "basic" or 
		 * "digest". This string will always be in lowercase.
		 */
		void GetAuthenticationScheme([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);

		/**
		 * The initial value should be used to prefill the dialog or be shown
		 * in some other way to the user.
		 * On return, this parameter should contain the username entered by
		 * the user.
		 * This field can only be changed if the #ONLY_PASSWORD flag is not set.
		 */
		void GetUsername([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		void SetUsername([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);

		/**
		 * The initial value should be used to prefill the dialog or be shown
		 * in some other way to the user.
		 * The password should not be shown in clear.
		 * On return, this parameter should contain the password entered by
		 * the user.
		 */
		void GetPassword([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		void SetPassword([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);

		/**
		 * The initial value should be used to prefill the dialog or be shown
		 * in some other way to the user.
		 * On return, this parameter should contain the domain entered by
		 * the user.
		 * This attribute is only used if flags include #NEED_DOMAIN.
		 */
		void GetDomain([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		void SetDomain([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
	}
}
