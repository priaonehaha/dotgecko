using System.Runtime.InteropServices;
using System.Text;
using PRTime = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Information on a Certificate Revocation List (CRL)
	 * issued by a Aertificate Authority (CA).
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("c185d920-4a3e-11d5-ba27-00108303b117"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsICRLInfo //: nsISupports
	{
		/**
		 *  The issuing CA's organization.
		 */
		void GetOrganization([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The issuing CA's organizational unit.
		 */
		void GetOrganizationalUnit([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The time this CRL was created at.
		 */
		PRTime LastUpdate { get; }

		/**
		 *  The time the suggested next update for this CRL.
		 */
		PRTime NextUpdate { get; }

		/**
		 *  lastUpdate formatted as a human readable string
		 *  formatted according to the environment locale.
		 */
		void GetLastUpdateLocale([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  nextUpdate formatted as a human readable string
		 *  formatted according to the environment locale.
		 */
		void GetNextUpdateLocale([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The key identifying the CRL in the database.
		 */
		void GetNameInDb([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 *  The URL this CRL was last fetched from.
		 */
		void GetLastFetchURL([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
	}
}
