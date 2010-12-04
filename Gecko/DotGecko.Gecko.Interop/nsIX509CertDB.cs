using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	public static class nsIX509CertDBConstants
	{
		/**
		 *  Constants that define which usages a certificate
		 *  is trusted for.
		 */
		public const UInt32 UNTRUSTED = 0;
		public const UInt32 TRUSTED_SSL = 1 << 0;
		public const UInt32 TRUSTED_EMAIL = 1 << 1;
		public const UInt32 TRUSTED_OBJSIGN = 1 << 2;
	}

	/**
	 * This represents a service to access and manipulate 
	 * X.509 certificates stored in a database.
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("da48b3c0-1284-11d5-ac67-000064657374"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIX509CertDB //: nsISupports
	{
		/**
		 *  Given a nickname and optionally a token,
		 *  locate the matching certificate.
		 *
		 *  @param aToken Optionally limits the scope of 
		 *                this function to a token device.
		 *                Can be null to mean any token.
		 *  @param aNickname The nickname to be used as the key
		 *                   to find a certificate.
		 *                
		 *  @return The matching certificate if found.
		 */
		nsIX509Cert FindCertByNickname([MarshalAs(UnmanagedType.IUnknown)] nsISupports aToken,
									   [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aNickname);

		/**
		 *  Will find a certificate based on its dbkey
		 *  retrieved by getting the dbKey attribute of
		 *  the certificate.
		 *
		 *  @param aDBkey Database internal key, as obtained using
		 *                attribute dbkey in nsIX509Cert.
		 *  @param aToken Optionally limits the scope of 
		 *                this function to a token device.
		 *                Can be null to mean any token.
		 */
		nsIX509Cert FindCertByDBKey([MarshalAs(UnmanagedType.LPStr)] String aDBkey, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aToken);

		/**
		 *  Obtain a list of certificate nicknames from the database.
		 *  What the name is depends on type:
		 *    user, ca, or server cert - the nickname
		 *    email cert - the email address
		 *
		 *  @param aToken Optionally limits the scope of 
		 *                this function to a token device.
		 *                Can be null to mean any token.
		 *  @param aType Type of certificate to obtain
		 *               See certificate type constants in nsIX509Cert.
		 *  @param count The number of nicknames in the returned array
		 *  @param certNameList The returned array of certificate nicknames.
		 */
		void FindCertNicknames([MarshalAs(UnmanagedType.IUnknown)] nsISupports aToken,
							   UInt32 aType,
							   out UInt32 count,
							   [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 2)] out String certNameList);

		/**
		 *  Find the email encryption certificate by nickname.
		 *
		 *  @param aNickname The nickname to be used as the key
		 *                   to find the certificate.
		 *                
		 *  @return The matching certificate if found.
		 */
		nsIX509Cert FindEmailEncryptionCert([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aNickname);

		/**
		 *  Find the email signing certificate by nickname.
		 *
		 *  @param aNickname The nickname to be used as the key
		 *                   to find the certificate.
		 *                
		 *  @return The matching certificate if found.
		 */
		nsIX509Cert FindEmailSigningCert([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aNickname);

		/**
		 *  Find a certificate by email address.
		 *
		 *  @param aToken Optionally limits the scope of 
		 *                this function to a token device.
		 *                Can be null to mean any token.
		 *  @param aEmailAddress The email address to be used as the key
		 *                       to find the certificate.
		 *                
		 *  @return The matching certificate if found.
		 */
		nsIX509Cert FindCertByEmailAddress([MarshalAs(UnmanagedType.IUnknown)] nsISupports aToken,
										   [MarshalAs(UnmanagedType.LPStr)] String aEmailAddress);

		/**
		 *  Use this to import a stream sent down as a mime type into
		 *  the certificate database on the default token.
		 *  The stream may consist of one or more certificates.
		 *
		 *  @param data The raw data to be imported
		 *  @param length The length of the data to be imported
		 *  @param type The type of the certificate, see constants in nsIX509Cert
		 *  @param ctx A UI context.
		 */
		void ImportCertificates([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] Byte[] data,
								UInt32 length,
								UInt32 type,
								nsIInterfaceRequestor ctx);

		/**
		 *  Import another person's email certificate into the database.
		 *
		 *  @param data The raw data to be imported
		 *  @param length The length of the data to be imported
		 *  @param ctx A UI context.
		 */
		void ImportEmailCertificate([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] Byte[] data,
									UInt32 length,
									nsIInterfaceRequestor ctx);

		/**
		 *  Import a server machine's certificate into the database.
		 *
		 *  @param data The raw data to be imported
		 *  @param length The length of the data to be imported
		 *  @param ctx A UI context.
		 */
		void ImportServerCertificate([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] Byte[] data,
									 UInt32 length,
									 nsIInterfaceRequestor ctx);

		/**
		 *  Import a personal certificate into the database, assuming 
		 *  the database already contains the private key for this certificate.
		 *
		 *  @param data The raw data to be imported
		 *  @param length The length of the data to be imported
		 *  @param ctx A UI context.
		 */
		void ImportUserCertificate([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] Byte[] data,
								   UInt32 length,
								   nsIInterfaceRequestor ctx);

		/**
		 *  Delete a certificate stored in the database.
		 *
		 *  @param aCert Delete this certificate.
		 */
		void DeleteCertificate(nsIX509Cert aCert);

		/**
		 *  Modify the trust that is stored and associated to a certificate within
		 *  a database. Separate trust is stored for 
		 *  One call manipulates the trust for one trust type only.
		 *  See the trust type constants defined within this interface.
		 *
		 *  @param cert Change the stored trust of this certificate.
		 *  @param type The type of the certificate. See nsIX509Cert.
		 *  @param trust A bitmask. The new trust for the possible usages.
		 *               See the trust constants defined within this interface.
		 */
		void SetCertTrust(nsIX509Cert cert,
						  UInt32 type,
						  UInt32 trust);

		/**
		 *  Query whether a certificate is trusted for a particular use.
		 *
		 *  @param cert Obtain the stored trust of this certificate.
		 *  @param certType The type of the certificate. See nsIX509Cert.
		 *  @param trustType A single bit from the usages constants defined 
		 *                   within this interface.
		 *
		 *  @return Returns true if the certificate is trusted for the given use.
		 */
		Boolean IsCertTrusted(nsIX509Cert cert,
							 UInt32 certType,
							 UInt32 trustType);

		/**
		 *  Import certificate(s) from file
		 *
		 *  @param aToken Optionally limits the scope of 
		 *                this function to a token device.
		 *                Can be null to mean any token.
		 *  @param aFile Identifies a file that contains the certificate
		 *               to be imported.
		 *  @param aType Describes the type of certificate that is going to
		 *               be imported. See type constants in nsIX509Cert.
		 */
		void ImportCertsFromFile([MarshalAs(UnmanagedType.IUnknown)] nsISupports aToken,
							   nsILocalFile aFile,
							   UInt32 aType);

		/**
		 *  Import a PKCS#12 file containing cert(s) and key(s) into the database.
		 *
		 *  @param aToken Optionally limits the scope of 
		 *                this function to a token device.
		 *                Can be null to mean any token.
		 *  @param aFile Identifies a file that contains the data
		 *               to be imported.
		 */
		void ImportPKCS12File([MarshalAs(UnmanagedType.IUnknown)] nsISupports aToken,
							  nsILocalFile aFile);

		/**
		 *  Export a set of certs and keys from the database to a PKCS#12 file.
		 *
		 *  @param aToken Optionally limits the scope of 
		 *                this function to a token device.
		 *                Can be null to mean any token.
		 *  @param aFile Identifies a file that will be filled with the data
		 *               to be exported.
		 *  @param count The number of certificates to be exported.
		 *  @param aCerts The array of all certificates to be exported.
		 */
		void ExportPKCS12File([MarshalAs(UnmanagedType.IUnknown)] nsISupports aToken,
							  nsILocalFile aFile,
							  UInt32 count,
							  [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 2)] nsIX509Cert aCerts);

		/**
		 *  An array of all known OCSP responders within the scope of the 
		 *  certificate database.
		 *
		 *  @return Array of OCSP responders, entries are QIable to nsIOCSPResponder.
		 */
		nsIArray GetOCSPResponders();

		/**
		 *  Whether OCSP is enabled in preferences.
		 */
		Boolean IsOcspOn { get; }

		/*
		 *  Decode a raw data presentation and instantiate an object in memory.
		 *
		 *  @param base64 The raw representation of a certificate,
		 *                encoded as Base 64.
		 *  @return The new certificate object.
		 */
		nsIX509Cert ConstructX509FromBase64([MarshalAs(UnmanagedType.LPStr)] String base64);
	}
}
