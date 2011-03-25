using System;
using System.Runtime.InteropServices;
using System.Text;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	public static class nsIContentSecurityPolicyConstants
	{
		public const UInt16 VIOLATION_TYPE_INLINE_SCRIPT = 1;
		public const UInt16 VIOLATION_TYPE_EVAL = 2;
	}

	/**
	 * nsIContentSecurityPolicy  
	 * Describes an XPCOM component used to model an enforce CSPs.
	 */
	[ComImport, Guid("AB36A2BF-CB32-4AA6-AB41-6B4E4444A221"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIContentSecurityPolicy //: nsISupports
	{
		/**
		 * Set to true when the CSP has been read in and parsed and is ready to
		 * enforce.  This is a barrier for the nsDocument so it doesn't load any
		 * sub-content until either it knows that a CSP is ready or will not be used.
		 */
		Boolean IsInitialized { get; set; }

		/**
		 * When set to true, content load-blocking and fail-closed are disabled: CSP
		 * will ONLY send reports, and not modify behavior.
		 */
		Boolean ReportOnlyMode { get; set; }

		/**
		 * A read-only string version of the policy for debugging.
		 */
		void GetPolicy([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * Whether this policy allows in-page script.
		 */
		Boolean AllowsInlineScript { get; }

		/**
		 * whether this policy allows eval and eval-like functions
		 * such as setTimeout("code string", time).
		 */
		Boolean AllowsEval { get; }

		/**
		 * Log policy violation on the Error Console and send a report if a report-uri
		 * is present in the policy
		 *
		 * @param violationType
		 *     one of the VIOLATION_TYPE_* constants, e.g. inline-script or eval
		 * @param sourceFile
		 *     name of the source file containing the violation (if available)
		 * @param contentSample
		 *     sample of the violating content (to aid debugging)
		 * @param lineNum
		 *     source line number of the violation (if available)
		 */
		void LogViolationDetails(UInt16 violationType,
								 [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String sourceFile,
								 [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String scriptSample,
								 Int32 lineNum);

		/**
		 * Manually triggers violation report sending given a URI and reason.
		 * The URI may be null, in which case "self" is sent.
		 * @param blockedURI
		 *     the URI that violated the policy
		 * @param violatedDirective
		 *     the directive that was violated.
		 * @param scriptSample
		 *     a sample of the violating inline script
		 * @param lineNum
		 *     source line number of the violation (if available)
		 * @return 
		 *     nothing.
		 */
		void SendReports([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String blockedURI,
						 [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String violatedDirective,
						 [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String scriptSample,
						 Int32 lineNum);

		/**
		 * Called after the CSP object is created to fill in the appropriate request
		 * and request header information needed in case a report needs to be sent.
		 */
		void ScanRequestData(nsIHttpChannel aChannel);

		/**
		 * Updates the policy currently stored in the CSP to be "refined" or
		 * tightened by the one specified in the string policyString.
		 */
		void RefinePolicy([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String policyString, nsIURI selfURI);

		/**
		 * Verifies ancestry as permitted by the policy.
		 *
		 * Calls to this may trigger violation reports when queried, so
		 * this value should not be cached.
		 *
		 * @param docShell
		 *    containing the protected resource
		 * @return
		 *    true if the frame's ancestors are all permitted by policy
		 */
		Boolean PermitsAncestry(nsIDocShell docShell);

		/**
		 * Delegate method called by the service when sub-elements of the protected
		 * document are being loaded.  Given a bit of information about the request,
		 * decides whether or not the policy is satisfied.
		 *
		 * Calls to this may trigger violation reports when queried, so
		 * this value should not be cached.
		 */
		Int16 ShouldLoad(UInt32 aContentType,
						 nsIURI aContentLocation,
						 nsIURI aRequestOrigin,
						 [MarshalAs(UnmanagedType.IUnknown)] nsISupports aContext,
						 [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String aMimeTypeGuess,
						 [MarshalAs(UnmanagedType.IUnknown)] nsISupports aExtra);

		/**
		 * Delegate method called by the service when sub-elements of the protected
		 * document are being processed.  Given a bit of information about the request,
		 * decides whether or not the policy is satisfied.
		 */
		Int16 ShouldProcess(UInt32 aContentType,
							nsIURI aContentLocation,
							nsIURI aRequestOrigin,
							[MarshalAs(UnmanagedType.IUnknown)] nsISupports aContext,
							[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String aMimeType,
							[MarshalAs(UnmanagedType.IUnknown)] nsISupports aExtra);

	}
}
