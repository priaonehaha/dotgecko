using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIURL interface provides convenience methods that further
	 * break down the path portion of nsIURI:
	 *
	 * http://host/directory/fileBaseName.fileExtension?query
	 * http://host/directory/fileBaseName.fileExtension#ref
	 * http://host/directory/fileBaseName.fileExtension;param
	 *            \          \                       /
	 *             \          -----------------------
	 *              \                   |          /
	 *               \               fileName     /
	 *                ----------------------------
	 *                            |
	 *                        filePath
	 */
	[ComImport, Guid("d6116970-8034-11d3-9399-00104ba0fd40"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIURL : nsIURI
	{
		#region nsIURI Members

		new void GetSpec([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		new void SetSpec([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);
		new void GetPrePath([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		new void GetScheme([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);
		new void SetScheme([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String value);
		new void GetUserPass([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		new void SetUserPass([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);
		new void GetUsername([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		new void SetUsername([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);
		new void GetPassword([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		new void SetPassword([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);
		new void GetHostPort([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		new void SetHostPort([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);
		new void GetHost([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		new void SetHost([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);
		new Int32 Port { get; set; }
		new void GetPath([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		new void SetPath([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);
		new Boolean Equals(nsIURI other);
		new Boolean SchemeIs([MarshalAs(UnmanagedType.LPStr)] String scheme);
		new nsIURI Clone();
		new void Resolve(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String relativePath,
			[In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		new void GetAsciiSpec([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);
		new void GetAsciiHost([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);
		new void GetOriginCharset([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);

		#endregion

		/*************************************************************************
		 * The URL path is broken down into the following principal components:
		 */

		/**
		 * Returns a path including the directory and file portions of a
		 * URL.  For example, the filePath of "http://host/foo/bar.html#baz"
		 * is "/foo/bar.html".
		 *
		 * Some characters may be escaped.
		 */
		void GetFilePath([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void SetFilePath([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);

		/**
		 * Returns the parameters specified after the ; in the URL. 
		 *
		 * Some characters may be escaped.
		 */
		void GetParam([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void SetParam([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);

		/**
		 * Returns the query portion (the part after the "?") of the URL.
		 * If there isn't one, an empty string is returned.
		 *
		 * Some characters may be escaped.
		 */
		void GetQuery([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void SetQuery([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);

		/**
		 * Returns the reference portion (the part after the "#") of the URL.
		 * If there isn't one, an empty string is returned.
		 *
		 * Some characters may be escaped.
		 */
		void GetRef([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void SetRef([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);


		/*************************************************************************
		 * The URL filepath is broken down into the following sub-components:
		 */

		/**
		 * Returns the directory portion of a URL.  If the URL denotes a path to a
		 * directory and not a file, e.g. http://host/foo/bar/, then the Directory
		 * attribute accesses the complete /foo/bar/ portion, and the FileName is
		 * the empty string. If the trailing slash is omitted, then the Directory
		 * is /foo/ and the file is bar (i.e. this is a syntactic, not a semantic
		 * breakdown of the Path).  And hence don't rely on this for something to
		 * be a definitely be a file. But you can get just the leading directory
		 * portion for sure.
		 *
		 * Some characters may be escaped.
		 */
		void GetDirectory([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void SetDirectory([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);

		/**
		 * Returns the file name portion of a URL.  If the URL denotes a path to a
		 * directory and not a file, e.g. http://host/foo/bar/, then the Directory
		 * attribute accesses the complete /foo/bar/ portion, and the FileName is
		 * the empty string. Note that this is purely based on searching for the
		 * last trailing slash. And hence don't rely on this to be a definite file. 
		 *
		 * Some characters may be escaped.
		 */
		void GetFileName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void SetFileName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);


		/*************************************************************************
		 * The URL filename is broken down even further:
		 */

		/**
		 * Returns the file basename portion of a filename in a url.
		 *
		 * Some characters may be escaped.
		 */
		void GetFileBaseName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void SetFileBaseName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);

		/**
		 * Returns the file extension portion of a filename in a url.  If a file
		 * extension does not exist, the empty string is returned.
		 *
		 * Some characters may be escaped.
		 */
		void GetFileExtension([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void SetFileExtension([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);

		/**
		 * This method takes a uri and compares the two.  The common uri portion
		 * is returned as a string.  The minimum common uri portion is the 
		 * protocol, and any of these if present:  login, password, host and port
		 * If no commonality is found, "" is returned.  If they are identical, the
		 * whole path with file/ref/etc. is returned.  For file uris, it is
		 * expected that the common spec would be at least "file:///" since '/' is
		 * a shared common root.
		 *
		 * Examples:
		 *    this.spec               aURIToCompare.spec        result
		 * 1) http://mozilla.org/     http://www.mozilla.org/   ""
		 * 2) http://foo.com/bar/     ftp://foo.com/bar/        ""
		 * 3) http://foo.com:8080/    http://foo.com/bar/       ""
		 * 4) ftp://user@foo.com/     ftp://user:pw@foo.com/    ""
		 * 5) ftp://foo.com/bar/      ftp://foo.com/bar         ftp://foo.com/
		 * 6) ftp://foo.com/bar/      ftp://foo.com/bar/b.html  ftp://foo.com/bar/
		 * 7) http://foo.com/a.htm#i  http://foo.com/b.htm      http://foo.com/
		 * 8) ftp://foo.com/c.htm#i   ftp://foo.com/c.htm       ftp://foo.com/c.htm
		 * 9) file:///a/b/c.html      file:///d/e/c.html        file:///
		 */
		void GetCommonBaseSpec(nsIURI aURIToCompare, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * This method tries to create a string which specifies the location of the
		 * argument relative to |this|.  If the argument and |this| are equal, the
		 * method returns "".  If any of the URIs' scheme, host, userpass, or port
		 * don't match, the method returns the full spec of the argument.
		 *
		 * Examples:
		 *    this.spec               aURIToCompare.spec        result
		 * 1) http://mozilla.org/     http://www.mozilla.org/   http://www.mozilla.org/
		 * 2) http://mozilla.org/     http://www.mozilla.org    http://www.mozilla.org/
		 * 3) http://foo.com/bar/     http://foo.com:80/bar/    ""
		 * 4) http://foo.com/         http://foo.com/a.htm#b    a.html#b
		 * 5) http://foo.com/a/b/     http://foo.com/c          ../../c
		 */
		void GetRelativeSpec(nsIURI aURIToCompare, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
	}
}
