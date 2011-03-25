using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIStandardURLConstants
	{
		/**
		 * blah:foo/bar    => blah://foo/bar
		 * blah:/foo/bar   => blah:///foo/bar
		 * blah://foo/bar  => blah://foo/bar
		 * blah:///foo/bar => blah:///foo/bar
		 */
		public const UInt32 URLTYPE_STANDARD = 1;

		/**
		 * blah:foo/bar    => blah://foo/bar
		 * blah:/foo/bar   => blah://foo/bar
		 * blah://foo/bar  => blah://foo/bar
		 * blah:///foo/bar => blah://foo/bar
		 */
		public const UInt32 URLTYPE_AUTHORITY = 2;

		/**
		 * blah:foo/bar    => blah:///foo/bar
		 * blah:/foo/bar   => blah:///foo/bar
		 * blah://foo/bar  => blah://foo/bar
		 * blah:///foo/bar => blah:///foo/bar
		 */
		public const UInt32 URLTYPE_NO_AUTHORITY = 3;
	}

	/**
	 * nsIStandardURL defines the interface to an URL with the standard
	 * file path format common to protocols like http, ftp, and file.
	 * It supports initialization from a relative path and provides
	 * some customization on how URLs are normalized.
	 */
	[ComImport, Guid("babd6cca-ebe7-4329-967c-d6b9e33caa81"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIStandardURL : nsIMutable
	{
		#region nsIMutable Members

		new Boolean Mutable { get; set; }

		#endregion

		/**
		 * Initialize a standard URL.
		 *
		 * @param aUrlType       - one of the URLTYPE_ flags listed above.
		 * @param aDefaultPort   - if the port parsed from the URL string matches
		 *                         this port, then the port will be removed from the
		 *                         canonical form of the URL.
		 * @param aSpec          - URL string.
		 * @param aOriginCharset - the charset from which this URI string
		 *                         originated.  this corresponds to the charset
		 *                         that should be used when communicating this
		 *                         URI to an origin server, for example.  if
		 *                         null, then provide aBaseURI implements this
		 *                         interface, the origin charset of aBaseURI will
		 *                         be assumed, otherwise defaulting to UTF-8 (i.e.,
		 *                         no charset transformation from aSpec).
		 * @param aBaseURI       - if null, aSpec must specify an absolute URI.
		 *                         otherwise, aSpec will be resolved relative
		 *                         to aBaseURI.
		 */
		void Init(UInt32 aUrlType, Int32 aDefaultPort,
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aSpec,
			[MarshalAs(UnmanagedType.LPStr)] String aOriginCharset, nsIURI aBaseURI);
	}
}
