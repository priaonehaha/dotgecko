using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Functions that implement user interface dialogs to manage certificates.
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("a03ca940-09be-11d5-ac5d-000064657374"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsICertificateDialogs //: nsISupports
	{
		/**
		 *  UI shown when a user is asked to download a new CA cert.
		 *  Provides user with ability to choose trust settings for the cert.
		 *  Asks the user to grant permission to import the certificate.
		 *
		 *  @param ctx A user interface context.
		 *  @param cert The certificate that is about to get installed.
		 *  @param trust a bit mask of trust flags, 
		 *               see nsIX509CertDB for possible values.
		 *
		 *  @return true if the user allows to import the certificate.
		 */
		Boolean ConfirmDownloadCACert(nsIInterfaceRequestor ctx, nsIX509Cert cert, out UInt32 trust);

		/**
		 *  UI shown when a web site has delivered a CA certificate to
		 *  be imported, but the certificate is already contained in the
		 *  user's storage.
		 *
		 *  @param ctx A user interface context.
		 */
		void NotifyCACertExists(nsIInterfaceRequestor ctx);

		/**
		 *  UI shown when a user's personal certificate is going to be
		 *  exported to a backup file.
		 *  The implementation of this dialog should make sure 
		 *  to prompt the user to type the password twice in order to
		 *  confirm correct input.
		 *  The wording in the dialog should also motivate the user 
		 *  to enter a strong password.
		 *
		 *  @param ctx A user interface context.
		 *  @param password The password provided by the user.
		 *
		 *  @return false if the user requests to cancel.
		 */
		Boolean SetPKCS12FilePassword(nsIInterfaceRequestor ctx, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder password);

		/**
		 *  UI shown when a user is about to restore a personal
		 *  certificate from a backup file.
		 *  The user is requested to enter the password
		 *  that was used in the past to protect that backup file.
		 *
		 *  @param ctx A user interface context.
		 *  @param password The password provided by the user.
		 *
		 *  @return false if the user requests to cancel.
		 */
		Boolean GetPKCS12FilePassword(nsIInterfaceRequestor ctx, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder password);

		/**
		 *  UI shown when a certificate needs to be shown to the user.
		 *  The implementation should try to display as many attributes
		 *  as possible.
		 *
		 *  @param ctx A user interface context.
		 *  @param cert The certificate to be shown to the user.
		 */
		void ViewCert(nsIInterfaceRequestor ctx, nsIX509Cert cert);

		/**
		 *  UI shown after a Certificate Revocation List (CRL) has been
		 *  successfully imported.
		 *
		 *  @param ctx A user interface context.
		 *  @param crl Information describing the CRL that was imported.
		 */
		void CrlImportStatusDialog(nsIInterfaceRequestor ctx, nsICRLInfo crl);
	}
}
