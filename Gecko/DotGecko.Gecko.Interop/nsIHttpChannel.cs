using System;
using System.Runtime.InteropServices;
using System.Text;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIHttpChannel
	 *
	 * This interface allows for the modification of HTTP request parameters and
	 * the inspection of the resulting HTTP response status and headers when they
	 * become available.
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("9277fe09-f0cc-4cd9-bbce-581dd94b0260"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIHttpChannel : nsIChannel
	{
		#region nsIRequest Members

		new void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		new Boolean IsPending();
		new nsResult Status { [return: MarshalAs(UnmanagedType.U4)] get; }
		new void Cancel([MarshalAs(UnmanagedType.U4)] nsResult aStatus);
		new void Suspend();
		new void Resume();
		new nsILoadGroup LoadGroup { get; set; }
		new UInt32 LoadFlags { get; set; }

		#endregion

		#region nsIChannel Members

		new nsIURI OriginalURI { get; set; }
		new nsIURI URI { get; }
		new nsISupports Owner { [return: MarshalAs(UnmanagedType.IUnknown)] get; [param: MarshalAs(UnmanagedType.IUnknown)] set; }
		new nsIInterfaceRequestor NotificationCallbacks { get; set; }
		new nsISupports SecurityInfo { [return: MarshalAs(UnmanagedType.IUnknown)] get; }
		new void GetContentType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);
		new void SetContentType([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String value);
		new void GetContentCharset([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);
		new void SetContentCharset([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String value);
		new Int32 ContentLength { get; set; }
		new nsIInputStream Open();
		new void AsyncOpen(nsIStreamListener aListener, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aContext);

		#endregion

		/**************************************************************************
		 * REQUEST CONFIGURATION
		 *
		 * Modifying request parameters after asyncOpen has been called is an error.
		 */

		/**
		 * Set/get the HTTP request method (default is "GET").  Setter is case
		 * insensitive; getter returns an uppercase string.
		 *
		 * This attribute may only be set before the channel is opened.
		 *
		 * NOTE: The data for a "POST" or "PUT" request can be configured via
		 * nsIUploadChannel; however, after setting the upload data, it may be
		 * necessary to set the request method explicitly.  The documentation
		 * for nsIUploadChannel has further details.
		 *
		 * @throws NS_ERROR_IN_PROGRESS if set after the channel has been opened.
		 */
		void GetRequestMethod([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);
		void SetRequestMethod([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String value);

		/**
		 * Get/set the HTTP referrer URI.  This is the address (URI) of the
		 * resource from which this channel's URI was obtained (see RFC2616 section
		 * 14.36).
		 * 
		 * This attribute may only be set before the channel is opened.
		 *
		 * NOTE: The channel may silently refuse to set the Referer header if the
		 * URI does not pass certain security checks (e.g., a "https://" URL will
		 * never be sent as the referrer for a plaintext HTTP request).  The
		 * implementation is not required to throw an exception when the referrer
		 * URI is rejected.
		 *
		 * @throws NS_ERROR_IN_PROGRESS if set after the channel has been opened.
		 */
		nsIURI Referrer { get; set; }

		/**
		 * Get the value of a particular request header.
		 *
		 * @param aHeader
		 *        The case-insensitive name of the request header to query (e.g.,
		 *        "Cache-Control").
		 *
		 * @return the value of the request header.
		 * @throws NS_ERROR_NOT_AVAILABLE if the header is not set.
		 */
		void GetRequestHeader([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String aHeader,
							  [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);

		/**
		 * Set the value of a particular request header.
		 *
		 * This method allows, for example, the cookies module to add "Cookie"
		 * headers to the outgoing HTTP request.
		 *
		 * This method may only be called before the channel is opened.
		 *
		 * @param aHeader
		 *        The case-insensitive name of the request header to set (e.g.,
		 *        "Cookie").
		 * @param aValue
		 *        The request header value to set (e.g., "X=1").
		 * @param aMerge
		 *        If true, the new header value will be merged with any existing
		 *        values for the specified header.  This flag is ignored if the
		 *        specified header does not support merging (e.g., the "Content-
		 *        Type" header can only have one value).  The list of headers for
		 *        which this flag is ignored is an implementation detail.  If this
		 *        flag is false, then the header value will be replaced with the
		 *        contents of |aValue|.
		 *
		 * If aValue is empty and aMerge is false, the header will be cleared.
		 *
		 * @throws NS_ERROR_IN_PROGRESS if called after the channel has been
		 *         opened.
		 */
		void SetRequestHeader([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String aHeader,
							  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String aValue,
							  Boolean aMerge);

		/**
		 * Call this method to visit all request headers.  Calling setRequestHeader
		 * while visiting request headers has undefined behavior.  Don't do it!
		 *
		 * @param aVisitor
		 *        the header visitor instance.
		 */
		void VisitRequestHeaders(nsIHttpHeaderVisitor aVisitor);

		/**
		 * This attribute is a hint to the channel to indicate whether or not
		 * the underlying HTTP transaction should be allowed to be pipelined
		 * with other transactions.  This should be set to FALSE, for example,
		 * if the application knows that the corresponding document is likely
		 * to be very large.
		 *
		 * This attribute is true by default, though other factors may prevent
		 * pipelining.
		 *
		 * This attribute may only be set before the channel is opened.
		 *
		 * @throws NS_ERROR_FAILURE if set after the channel has been opened.
		 */
		Boolean AllowPipelining { get; set; }

		/**
		 * This attribute specifies the number of redirects this channel is allowed
		 * to make.  If zero, the channel will fail to redirect and will generate
		 * a NS_ERROR_REDIRECT_LOOP failure status.
		 *
		 * NOTE: An HTTP redirect results in a new channel being created.  If the
		 * new channel supports nsIHttpChannel, then it will be assigned a value
		 * to its |redirectionLimit| attribute one less than the value of the
		 * redirected channel's |redirectionLimit| attribute.  The initial value
		 * for this attribute may be a configurable preference (depending on the
		 * implementation).
		 */
		UInt32 RedirectionLimit { get; set; }


		/**************************************************************************
		 * RESPONSE INFO
		 *
		 * Accessing response info before the onStartRequest event is an error.
		 */

		/**
		 * Get the HTTP response code (e.g., 200).
		 *
		 * @throws NS_ERROR_NOT_AVAILABLE if called before the response
		 *         has been received (before onStartRequest).
		 */
		UInt32 ResponseStatus { get; }

		/**
		 * Get the HTTP response status text (e.g., "OK").
		 *
		 * NOTE: This returns the raw (possibly 8-bit) text from the server.  There
		 * are no assumptions made about the charset of the returned text.  You
		 * have been warned!
		 *
		 * @throws NS_ERROR_NOT_AVAILABLE if called before the response
		 *         has been received (before onStartRequest).
		 */
		void GetResponseStatusText([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);

		/**
		 * Returns true if the HTTP response code indicates success.  The value of
		 * nsIRequest::status will be NS_OK even when processing a 404 response
		 * because a 404 response may include a message body that (in some cases)
		 * should be shown to the user.
		 *
		 * Use this attribute to distinguish server error pages from normal pages,
		 * instead of comparing the response status manually against the set of
		 * valid response codes, if that is required by your application.
		 *
		 * @throws NS_ERROR_NOT_AVAILABLE if called before the response
		 *         has been received (before onStartRequest).
		 */
		Boolean RequestSucceeded { get; }

		/**
		 * Get the value of a particular response header.
		 *
		 * @param aHeader
		 *        The case-insensitive name of the response header to query (e.g.,
		 *        "Set-Cookie").
		 *
		 * @return the value of the response header.
		 *
		 * @throws NS_ERROR_NOT_AVAILABLE if called before the response
		 *         has been received (before onStartRequest) or if the header is
		 *         not set in the response.
		 */
		void GetResponseHeader([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String header, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);

		/**
		 * Set the value of a particular response header.
		 *
		 * This method allows, for example, the HTML content sink to inform the HTTP
		 * channel about HTTP-EQUIV headers found in HTML <META> tags.
		 *
		 * @param aHeader
		 *        The case-insensitive name of the response header to set (e.g.,
		 *        "Cache-control").
		 * @param aValue
		 *        The response header value to set (e.g., "no-cache").
		 * @param aMerge
		 *        If true, the new header value will be merged with any existing
		 *        values for the specified header.  This flag is ignored if the
		 *        specified header does not support merging (e.g., the "Content-
		 *        Type" header can only have one value).  The list of headers for
		 *        which this flag is ignored is an implementation detail.  If this
		 *        flag is false, then the header value will be replaced with the
		 *        contents of |aValue|.
		 *
		 * If aValue is empty and aMerge is false, the header will be cleared.
		 * 
		 * @throws NS_ERROR_NOT_AVAILABLE if called before the response
		 *         has been received (before onStartRequest).
		 * @throws NS_ERROR_ILLEGAL_VALUE if changing the value of this response
		 *         header is not allowed.
		 */
		void SetResponseHeader([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String header,
							   [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String value,
							   Boolean merge);

		/**
		 * Call this method to visit all response headers.  Calling
		 * setResponseHeader while visiting response headers has undefined
		 * behavior.  Don't do it!
		 *
		 * @param aVisitor
		 *        the header visitor instance.
		 *
		 * @throws NS_ERROR_NOT_AVAILABLE if called before the response
		 *         has been received (before onStartRequest).
		 */
		void VisitResponseHeaders(nsIHttpHeaderVisitor aVisitor);

		/**
		 * Returns true if the server sent a "Cache-Control: no-store" response
		 * header.
		 *
		 * @throws NS_ERROR_NOT_AVAILABLE if called before the response
		 *         has been received (before onStartRequest).
		 */
		Boolean IsNoStoreResponse();

		/**
		 * Returns true if the server sent the equivalent of a "Cache-control:
		 * no-cache" response header.  Equivalent response headers include:
		 * "Pragma: no-cache", "Expires: 0", and "Expires" with a date value
		 * in the past relative to the value of the "Date" header.
		 *
		 * @throws NS_ERROR_NOT_AVAILABLE if called before the response
		 *         has been received (before onStartRequest).
		 */
		Boolean IsNoCacheResponse();
	}
}
