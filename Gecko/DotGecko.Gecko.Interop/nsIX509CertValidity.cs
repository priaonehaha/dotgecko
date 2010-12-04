using System.Runtime.InteropServices;
using System.Text;
using PRTime = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Information on the validity period of a X.509 certificate.
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("e701dfd8-1dd1-11b2-a172-ffa6cc6156ad"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIX509CertValidity //: nsISupports
	{
		/**
		 *  The earliest point in time where
		 *  a certificate is valid.
		 */
		PRTime NotBefore { get; }

		/**
		 *  "notBefore" attribute formatted as a time string
		 *  according to the environment locale,
		 *  according to the environment time zone.
		 */
		void GetNotBeforeLocalTime([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The day portion of "notBefore" 
		 *  formatted as a time string
		 *  according to the environment locale,
		 *  according to the environment time zone.
		 */
		void GetNotBeforeLocalDay([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  "notBefore" attribute formatted as a string
		 *  according to the environment locale,
		 *  displayed as GMT / UTC.
		 */
		void GetNotBeforeGMT([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The latest point in time where
		 *  a certificate is valid.
		 */
		PRTime NotAfter { get; }

		/**
		 *  "notAfter" attribute formatted as a time string
		 *  according to the environment locale,
		 *  according to the environment time zone.
		 */
		void GetNotAfterLocalTime([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The day portion of "notAfter" 
		 *  formatted as a time string
		 *  according to the environment locale,
		 *  according to the environment time zone.
		 */
		void GetNotAfterLocalDay([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  "notAfter" attribute formatted as a time string
		 *  according to the environment locale,
		 *  displayed as GMT / UTC.
		 */
		void GetNotAfterGMT([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
	}
}
