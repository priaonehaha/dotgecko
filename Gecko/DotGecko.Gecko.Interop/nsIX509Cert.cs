using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	public static class nsIX509CertConstants
	{
		/**
		 *  Constants to classify the type of a certificate.
		 */
		public const UInt32 UNKNOWN_CERT = 0;
		public const UInt32 CA_CERT = 1 << 0;
		public const UInt32 USER_CERT = 1 << 1;
		public const UInt32 EMAIL_CERT = 1 << 2;
		public const UInt32 SERVER_CERT = 1 << 3;

		/**
		 *  Constants for certificate verification results.
		 */
		public const UInt32 VERIFIED_OK = 0;
		public const UInt32 NOT_VERIFIED_UNKNOWN = 1 << 0;
		public const UInt32 CERT_REVOKED = 1 << 1;
		public const UInt32 CERT_EXPIRED = 1 << 2;
		public const UInt32 CERT_NOT_TRUSTED = 1 << 3;
		public const UInt32 ISSUER_NOT_TRUSTED = 1 << 4;
		public const UInt32 ISSUER_UNKNOWN = 1 << 5;
		public const UInt32 INVALID_CA = 1 << 6;
		public const UInt32 USAGE_NOT_ALLOWED = 1 << 7;

		/**
		 *  Constants that describe the certified usages of a certificate.
		 */
		public const UInt32 CERT_USAGE_SSLClient = 0;
		public const UInt32 CERT_USAGE_SSLServer = 1;
		public const UInt32 CERT_USAGE_SSLServerWithStepUp = 2;
		public const UInt32 CERT_USAGE_SSLCA = 3;
		public const UInt32 CERT_USAGE_EmailSigner = 4;
		public const UInt32 CERT_USAGE_EmailRecipient = 5;
		public const UInt32 CERT_USAGE_ObjectSigner = 6;
		public const UInt32 CERT_USAGE_UserCertImport = 7;
		public const UInt32 CERT_USAGE_VerifyCA = 8;
		public const UInt32 CERT_USAGE_ProtectedObjectSigner = 9;
		public const UInt32 CERT_USAGE_StatusResponder = 10;
		public const UInt32 CERT_USAGE_AnyCA = 11;
	}

	/**
	 * This represents a X.509 certificate.
	 */
	[ComImport, Guid("f0980f60-ee3d-11d4-998b-00b0d02354a0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIX509Cert //: nsISupports
	{
		/**
		 *  A nickname for the certificate.
		 */
		void GetNickname([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The primary email address of the certificate, if present.
		 */
		void GetEmailAddress([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  Obtain a list of all email addresses
		 *  contained in the certificate.
		 *
		 *  @param length The number of strings in the returned array.
		 *  @return An array of email addresses.
		 */
		[return: MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)]
		String[] GetEmailAddresses(out UInt32 length);

		/**
		 *  Check whether a given address is contained in the certificate.
		 *  The comparison will convert the email address to lowercase.
		 *  The behaviour for non ASCII characters is undefined.
		 *
		 *  @param aEmailAddress The address to search for.
		 *                
		 *  @return True if the address is contained in the certificate.
		 */
		Boolean ContainsEmailAddress([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aEmailAddress);

		/**
		 *  The subject owning the certificate.
		 */
		void GetSubjectName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The subject's common name.
		 */
		void GetCommonName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The subject's organization.
		 */
		void GetOrganization([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The subject's organizational unit.
		 */
		void GetOrganizationalUnit([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The fingerprint of the certificate's public key,
		 *  calculated using the SHA1 algorithm.
		 */
		void GetSha1Fingerprint([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The fingerprint of the certificate's public key,
		 *  calculated using the MD5 algorithm.
		 */
		void GetMd5Fingerprint([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  A human readable name identifying the hardware or
		 *  software token the certificate is stored on.
		 */
		void GetTokenName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The subject identifying the issuer certificate.
		 */
		void GetIssuerName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The serial number the issuer assigned to this certificate.
		 */
		void GetSerialNumber([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The issuer subject's common name.
		 */
		void GetIssuerCommonName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The issuer subject's organization.
		 */
		void GetIssuerOrganization([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The issuer subject's organizational unit.
		 */
		void GetIssuerOrganizationUnit([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The certificate used by the issuer to sign this certificate.
		 */
		nsIX509Cert Issuer { get; }

		/**
		 *  This certificate's validity period.
		 */
		nsIX509CertValidity Validity { get; }

		/**
		 *  A unique identifier of this certificate within the local storage.
		 */
		String DbKey { [return: MarshalAs(UnmanagedType.LPStr)] get; }

		/**
		 *  A human readable identifier to label this certificate.
		 */
		String WindowTitle { [return: MarshalAs(UnmanagedType.LPStr)]get; }

		/**
		 *  Obtain a list of certificates that contains this certificate 
		 *  and the issuing certificates of all involved issuers,
		 *  up to the root issuer.
		 *
		 *  @return The chain of certifficates including the issuers.
		 */
		nsIArray GetChain();

		/**
		 *  Obtain an array of human readable strings describing
		 *  the certificate's certified usages.
		 *
		 *  @param ignoreOcsp Do not use OCSP even if it is currently activated.
		 *  @param verified The certificate verification result, see constants.
		 *  @param count The number of human readable usages returned.
		 *  @param usages The array of human readable usages.
		 */
		void GetUsagesArray(Boolean ignoreOcsp, out UInt32 verified, out UInt32 count, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 2)] out String[] usages);

		/**
		 *  Obtain a single comma separated human readable string describing
		 *  the certificate's certified usages.
		 *
		 *  @param ignoreOcsp Do not use OCSP even if it is currently activated.
		 *  @param verified The certificate verification result, see constants.
		 *  @param purposes The string listing the usages.
		 */
		void GetUsagesString(Boolean ignoreOcsp, out UInt32 verified, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder usages);

		/**
		 *  Verify the certificate for a particular usage.
		 *
		 *  @return The certificate verification result, see constants.
		 */
		UInt32 VerifyForUsage(UInt32 usage);

		/**
		 *  This is the attribute which describes the ASN1 layout
		 *  of the certificate.  This can be used when doing a
		 *  "pretty print" of the certificate's ASN1 structure.
		 */
		nsIASN1Object ASN1Structure { get; }

		/**
		 *  Obtain a raw binary encoding of this certificate
		 *  in DER format.
		 *
		 *  @param length The number of bytes in the binary encoding.
		 *  @param data The bytes representing the DER encoded certificate.
		 */
		[return: MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 0)]
		Byte[] GetRawDER(out UInt32 length);

		/**
		 *  Test whether two certificate instances represent the 
		 *  same certificate.
		 *
		 *  @return Whether the certificates are equal
		 */
		Boolean Equals(nsIX509Cert other);
	}
}
