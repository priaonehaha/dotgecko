using System;
using System.Runtime.InteropServices;
using System.Text;
using PRNetAddr = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIDNSRecord
	 *
	 * this interface represents the result of a DNS lookup.  since a DNS
	 * query may return more than one resolved IP address, the record acts
	 * like an enumerator, allowing the caller to easily step through the
	 * list of IP addresses.
	 */
	[ComImport, Guid("31c9c52e-1100-457d-abac-d2729e43f506"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDNSRecord //: nsISupports
	{
		/**
		 * @return the canonical hostname for this record.  this value is empty if
		 * the record was not fetched with the RESOLVE_CANONICAL_NAME flag.
		 *
		 * e.g., www.mozilla.org --> rheet.mozilla.org
		 */
		void GetCanonicalName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);

		/**
		 * this function copies the value of the next IP address into the
		 * given PRNetAddr struct and increments the internal address iterator.
		 *
		 * @param aPort
		 *        A port number to initialize the PRNetAddr with.
		 *
		 * @throws NS_ERROR_NOT_AVAILABLE if there is not another IP address in
		 * the record.
		 */
		PRNetAddr GetNextAddr(UInt16 aPort);

		/**
		 * this function returns the value of the next IP address as a
		 * string and increments the internal address iterator.
		 *
		 * @throws NS_ERROR_NOT_AVAILABLE if there is not another IP address in
		 * the record.
		 */
		void GetNextAddrAsString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);

		/**
		 * this function returns true if there is another address in the record.
		 */
		Boolean HasMore();

		/**
		 * this function resets the internal address iterator to the first
		 * address in the record.
		 */
		void Rewind();
	}
}
