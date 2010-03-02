using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * URIs are essentially structured names for things -- anything. This interface
	 * provides accessors to set and query the most basic components of an URI.
	 * Subclasses, including nsIURL, impose greater structure on the URI.
	 *
	 * This interface follows Tim Berners-Lee's URI spec (RFC2396) [1], where the
	 * basic URI components are defined as such:
	 * <pre> 
	 *      ftp://username:password@hostname:portnumber/pathname
	 *      \ /   \               / \      / \        /\       /
	 *       -     ---------------   ------   --------  -------
	 *       |            |             |        |         |
	 *       |            |             |        |        Path
	 *       |            |             |       Port         
	 *       |            |            Host      /
	 *       |         UserPass                 /
	 *     Scheme                              /
	 *       \                                /
	 *        --------------------------------
	 *                       |
	 *                    PrePath
	 * </pre>
	 * The definition of the URI components has been extended to allow for
	 * internationalized domain names [2] and the more generic IRI structure [3].
	 *
	 * [1] http://www.ietf.org/rfc/rfc2396.txt
	 * [2] http://www.ietf.org/internet-drafts/draft-ietf-idn-idna-06.txt
	 * [3] http://www.ietf.org/internet-drafts/draft-masinter-url-i18n-08.txt
	 */

	/**
	 * nsIURI - interface for an uniform resource identifier w/ i18n support.
	 *
	 * nsAUTF8String attributes may contain unescaped UTF-8 characters.
	 * Consumers should be careful to escape the UTF-8 strings as necessary, but
	 * should always try to "display" the UTF-8 version as provided by this
	 * interface.
	 *
	 * nsAUTF8String attributes may also contain escaped characters.
	 * 
	 * Unescaping URI segments is unadvised unless there is intimate
	 * knowledge of the underlying charset or there is no plan to display (or
	 * otherwise enforce a charset on) the resulting URI substring.
	 *
	 * The correct way to create an nsIURI from a string is via
	 * nsIIOService.newURI.
	 * 
	 * @status FROZEN
	 */
	[ComImport]
	[Guid("07a22cc0-0ce5-11d3-9331-00104ba0fd40")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIURI //: nsISupports
	{
		/************************************************************************
		 * The URI is broken down into the following principal components:
		 */

		/**
		 * Returns a string representation of the URI. Setting the spec causes
		 * the new spec to be parsed per the rules for the scheme the URI
		 * currently has.  In particular, setting the spec to a URI string with a
		 * different scheme will generally produce incorrect results; no one
		 * outside of a protocol handler implementation should be doing that.  If
		 * the URI stores information from the nsIIOService.newURI call used to
		 * create it other than just the parsed string, then behavior of this
		 * information on setting the spec attribute is undefined.
		 *
		 * Some characters may be escaped.
		 */
		void GetSpec(nsAUTF8String result);
		void SetSpec(nsAUTF8String value);

		/**
		 * The prePath (eg. scheme://user:password@host:port) returns the string
		 * before the path.  This is useful for authentication or managing sessions.
		 *
		 * Some characters may be escaped.
		 */
		void GetPrePath(nsAUTF8String result);

		/**
		 * The Scheme is the protocol to which this URI refers.  The scheme is
		 * restricted to the US-ASCII charset per RFC2396.  Setting this is
		 * highly discouraged outside of a protocol handler implementation, since
		 * that will generally lead to incorrect results.
		 */
		void GetScheme(nsACString result);
		void SetScheme(nsACString value);

		/**
		 * The username:password (or username only if value doesn't contain a ':')
		 *
		 * Some characters may be escaped.
		 */
		void GetUserPass(nsAUTF8String result);
		void SetUserPass(nsAUTF8String value);

		/**
		 * The optional username and password, assuming the preHost consists of
		 * username:password.
		 *
		 * Some characters may be escaped.
		 */
		void GetUsername(nsAUTF8String result);
		void SetUsername(nsAUTF8String value);

		void GetPassword(nsAUTF8String result);
		void SetPassword(nsAUTF8String value);

		/**
		 * The host:port (or simply the host, if port == -1).
		 *
		 * Characters are NOT escaped.
		 */
		void GetHostPort(nsAUTF8String result);
		void SetHostPort(nsAUTF8String value);

		/**
		 * The host is the internet domain name to which this URI refers.  It could
		 * be an IPv4 (or IPv6) address literal.  If supported, it could be a
		 * non-ASCII internationalized domain name.
		 *
		 * Characters are NOT escaped.
		 */
		void GetHost(nsAUTF8String result);
		void SetHost(nsAUTF8String value);

		/**
		 * A port value of -1 corresponds to the protocol's default port (eg. -1
		 * implies port 80 for http URIs).
		 */
		Int32 GetPort();
		void SetPort(Int32 value);

		/**
		 * The path, typically including at least a leading '/' (but may also be
		 * empty, depending on the protocol).
		 *
		 * Some characters may be escaped.
		 */
		void GetPath(nsAUTF8String result);
		void SetPath(nsAUTF8String value);

		/************************************************************************
		 * An URI supports the following methods:
		 */

		/**
		 * URI equivalence test (not a strict string comparison).
		 *
		 * eg. http://foo.com:80/ == http://foo.com/
		 */
		Boolean Equals(nsIURI other);

		/**
		 * An optimization to do scheme checks without requiring the users of nsIURI
		 * to GetScheme, thereby saving extra allocating and freeing. Returns true if
		 * the schemes match (case ignored).
		 */
		Boolean SchemeIs([MarshalAs(UnmanagedType.LPStr)] String scheme);

		/**
		 * Clones the current URI.  For some protocols, this is more than just an
		 * optimization.  For example, under MacOS, the spec of a file URL does not
		 * necessarily uniquely identify a file since two volumes could share the
		 * same name.
		 */
		nsIURI Clone();

		/**
		 * This method resolves a relative string into an absolute URI string,
		 * using this URI as the base. 
		 *
		 * NOTE: some implementations may have no concept of a relative URI.
		 */
		void Resolve(nsAUTF8String relativePath, nsAUTF8String result);

		/************************************************************************
		 * Additional attributes:
		 */

		/**
		 * The URI spec with an ASCII compatible encoding.  Host portion follows
		 * the IDNA draft spec.  Other parts are URL-escaped per the rules of
		 * RFC2396.  The result is strictly ASCII.
		 */
		void GetAsciiSpec(nsACString result);

		/**
		 * The URI host with an ASCII compatible encoding.  Follows the IDNA
		 * draft spec for converting internationalized domain names (UTF-8) to
		 * ASCII for compatibility with existing internet infrasture.
		 */
		void GetAsciiHost(nsACString result);

		/**
		 * The charset of the document from which this URI originated.  An empty
		 * value implies UTF-8.
		 *
		 * If this value is something other than UTF-8 then the URI components
		 * (e.g., spec, prePath, username, etc.) will all be fully URL-escaped.
		 * Otherwise, the URI components may contain unescaped multibyte UTF-8
		 * characters.
		 */
		void GetOriginCharset(nsACString result);
	}
}
