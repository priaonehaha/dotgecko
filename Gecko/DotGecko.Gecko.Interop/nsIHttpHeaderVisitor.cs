using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Implement this interface to visit http headers.
	 */
	[ComImport, Guid("0cf40717-d7c1-4a94-8c1e-d6c9734101bb"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIHttpHeaderVisitor //: nsISupports
	{
		/**
		 * Called by the nsIHttpChannel implementation when visiting request and
		 * response headers.
		 *
		 * @param aHeader
		 *        the header being visited.
		 * @param aValue
		 *        the header value (possibly a comma delimited list).
		 *
		 * @throw any exception to terminate enumeration
		 */
		void VisitHeader([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String aHeader,
						 [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String aValue);
	}
}
