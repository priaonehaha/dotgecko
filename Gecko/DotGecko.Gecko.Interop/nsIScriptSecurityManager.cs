using System;
using System.Runtime.InteropServices;
using DotGecko.Gecko.Interop.JavaScript;
using nsISupports = System.Object;
using JSContextPtr = System.IntPtr;
using JSObjectPtr = System.IntPtr;
using JSStackFramePtr = System.IntPtr;
using nsAXPCNativeCallContextPtr = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	public static class nsIScriptSecurityManagerConstants
	{
		/**
		 * Default CheckLoadURI permissions
		 */
		// Default permissions
		public const UInt32 STANDARD = 0;

		// Indicate that the load is a load of a new document that is not
		// user-triggered.  Here "user-triggered" could be broadly interpreted --
		// for example, scripted sets of window.location.href might be treated as
		// "user-triggered" in some circumstances.  A typical example of a load
		// that is not user-triggered is a <meta> refresh load.  If this flag is
		// set, the load will be denied if the originating principal's URI has the
		// nsIProtocolHandler::URI_FORBIDS_AUTOMATIC_DOCUMENT_REPLACEMENT flag set.
		public const UInt32 LOAD_IS_AUTOMATIC_DOCUMENT_REPLACEMENT = 1 << 0;

		// Allow the loading of chrome URLs by non-chrome URLs.  Use with great
		// care!  This will actually allow the loading of any URI which has the
		// nsIProtocolHandler::URI_IS_UI_RESOURCE protocol handler flag set.  Ths
		// probably means at least chrome: and resource:.
		public const UInt32 ALLOW_CHROME = 1 << 1;

		// Don't allow URLs which would inherit the caller's principal (such as
		// javascript: or data:) to load.  See
		// nsIProtocolHandler::URI_INHERITS_SECURITY_CONTEXT.
		public const UInt32 DISALLOW_INHERIT_PRINCIPAL = 1 << 2;

		// Alias for DISALLOW_INHERIT_PRINCIPAL for backwards compat with
		// JS-implemented extensions.
		public const UInt32 DISALLOW_SCRIPT_OR_DATA = DISALLOW_INHERIT_PRINCIPAL;

		// Don't allow javascript: URLs to load
		//   WARNING: Support for this value was added in Mozilla 1.7.8 and
		//   Firefox 1.0.4.  Use in prior versions WILL BE IGNORED.
		// When using this, make sure that you actually want DISALLOW_SCRIPT, not
		// DISALLOW_INHERIT_PRINCIPAL
		public const UInt32 DISALLOW_SCRIPT = 1 << 3;
	}

	[ComImport, Guid("50eda256-4dd2-4c7c-baed-96983910af9f"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIScriptSecurityManager : nsIXPCSecurityManager
	{
		#region nsIXPCSecurityManager Members

		new void CanCreateWrapper(JSContextPtr aJSContext, [In] ref Guid aIID, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aObj, nsIClassInfo aClassInfo, ref IntPtr aPolicy);
		new void CanCreateInstance(JSContextPtr aJSContext, [In] ref Guid aCID);
		new void CanGetService(JSContextPtr aJSContext, [In] ref Guid aCID);
		new void CanAccess(UInt32 aAction, nsAXPCNativeCallContextPtr aCallContext, JSContextPtr aJSContext, JSObjectPtr aJSObject,
			[MarshalAs(UnmanagedType.IUnknown)] nsISupports aObj, nsIClassInfo aClassInfo, JsId aName, ref IntPtr aPolicy);

		#endregion

		///////////////// Security Checks //////////////////
		/**
		 * Checks whether the running script is allowed to access aProperty.
		 */
		void CheckPropertyAccess(JSContextPtr aJSContext,
								 JSObjectPtr aJSObject,
								 [MarshalAs(UnmanagedType.LPStr)] String aClassName,
								 JsId aProperty,
								 UInt32 aAction);

		/**
		 * Check that the script currently running in context "cx" can load "uri".
		 *
		 * Will return error code NS_ERROR_DOM_BAD_URI if the load request 
		 * should be denied.
		 *
		 * @param cx the JSContext of the script causing the load
		 * @param uri the URI that is being loaded
		 */
		void CheckLoadURIFromScript(JSContextPtr cx, nsIURI uri);

		/**
		 * Check that content with principal aPrincipal can load "uri".
		 *
		 * Will return error code NS_ERROR_DOM_BAD_URI if the load request 
		 * should be denied.
		 *
		 * @param aPrincipal the principal identifying the actor causing the load
		 * @param uri the URI that is being loaded
		 * @param flags the permission set, see above
		 */
		void CheckLoadURIWithPrincipal(nsIPrincipal aPrincipal,
									   nsIURI uri,
									   UInt32 flags);

		/**
		 * Check that content from "from" can load "uri".
		 *
		 * Will return error code NS_ERROR_DOM_BAD_URI if the load request 
		 * should be denied.
		 *
		 * @param from the URI causing the load
		 * @param uri the URI that is being loaded
		 * @param flags the permission set, see above
		 *
		 * @deprecated Use checkLoadURIWithPrincipal instead of this function.
		 */
		[Obsolete("Use CheckLoadURIWithPrincipal instead of this method.")]
		void CheckLoadURI(nsIURI from, nsIURI uri, UInt32 flags);

		/**
		 * Similar to checkLoadURIWithPrincipal but there are two differences:
		 *
		 * 1) The URI is a string, not a URI object.
		 * 2) This function assumes that the URI may still be subject to fixup (and
		 * hence will check whether fixed-up versions of the URI are allowed to
		 * load as well); if any of the versions of this URI is not allowed, this
		 * function will return error code NS_ERROR_DOM_BAD_URI.
		 */
		void CheckLoadURIStrWithPrincipal(nsIPrincipal aPrincipal, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String uri, UInt32 flags);

		/**
		 * Same as CheckLoadURI but takes string arguments for ease of use
		 * by scripts
		 *
		 * @deprecated Use checkLoadURIStrWithPrincipal instead of this function.
		 */
		[Obsolete("Use CheckLoadURIStrWithPrincipal instead of this method.")]
		void CheckLoadURIStr([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String from,
							 [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String uri,
							 UInt32 flags);

		/**
		 * Check that the function 'funObj' is allowed to run on 'targetObj'
		 *
		 * Will return error code NS_ERROR_DOM_SECURITY_ERR if the function
		 * should not run
		 *
		 * @param cx The current active JavaScript context.
		 * @param funObj The function trying to run..
		 * @param targetObj The object the function will run on.
		 */
		void CheckFunctionAccess(JSContextPtr cx, IntPtr funObj, IntPtr targetObj);

		/**
		 * Return true if content from the given principal is allowed to
		 * execute scripts.
		 */
		Boolean CanExecuteScripts(JSContextPtr cx, nsIPrincipal principal);

		///////////////// Principals /////////////////////// 
		/**
		 * Return the principal of the innermost frame of the currently 
		 * executing script. Will return null if there is no script 
		 * currently executing.
		 */
		nsIPrincipal GetSubjectPrincipal();

		/**
		 * Return the all-powerful system principal.
		 */
		nsIPrincipal GetSystemPrincipal();

		/**
		 * Return a principal with the specified certificate fingerprint, subject
		 * name (the full name or concatenated set of names of the entity
		 * represented by the certificate), pretty name, certificate, and
		 * codebase URI.  The certificate fingerprint and subject name MUST be
		 * nonempty; otherwise an error will be thrown.  Similarly, aCert must
		 * not be null.
		 */
		nsIPrincipal
			 GetCertificatePrincipal([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aCertFingerprint,
									 [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aSubjectName,
									 [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aPrettyName,
									 [MarshalAs(UnmanagedType.IUnknown)] nsISupports aCert,
									 nsIURI aURI);

		/**
		 * Return a principal that has the same origin as aURI.
		 */
		nsIPrincipal GetCodebasePrincipal(nsIURI aURI);

		///////////////// Capabilities API /////////////////////
		/**
		 * Request that 'capability' can be enabled by scripts or applets
		 * running with 'principal'. Will prompt user if
		 * necessary. Returns nsIPrincipal::ENABLE_GRANTED or
		 * nsIPrincipal::ENABLE_DENIED based on user's choice.
		 */
		Int16 RequestCapability(nsIPrincipal principal, [MarshalAs(UnmanagedType.LPStr)] String capability);

		/**
		 * Return true if the currently executing script has 'capability' enabled.
		 */
		Boolean IsCapabilityEnabled([MarshalAs(UnmanagedType.LPStr)] String capability);

		/**
		 * Enable 'capability' in the innermost frame of the currently executing
		 * script.
		 */
		void EnableCapability([MarshalAs(UnmanagedType.LPStr)] String capability);

		/**
		 * Remove 'capability' from the innermost frame of the currently
		 * executing script. Any setting of 'capability' from enclosing
		 * frames thus comes into effect.
		 */
		void RevertCapability([MarshalAs(UnmanagedType.LPStr)] String capability);

		/**
		 * Disable 'capability' in the innermost frame of the currently executing
		 * script.
		 */
		void DisableCapability([MarshalAs(UnmanagedType.LPStr)] String capability);

		//////////////// Master Certificate Functions ////////////////////
		/**
		 * Allow 'certificateID' to enable 'capability.' Can only be performed
		 * by code signed by the system certificate.
		 */
		// XXXbz Capabilities can't have non-ascii chars?
		// XXXbz ideally we'd pass a subjectName here too, and the nsISupports
		// cert we're enabling for...
		void SetCanEnableCapability([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String certificateFingerprint,
									[MarshalAs(UnmanagedType.LPStr)] String capability,
									Int16 canEnable);

		///////////////////////
		/**
		 * Return the principal of the specified object in the specified context.
		 */
		nsIPrincipal GetObjectPrincipal(JSContextPtr cx, JSObjectPtr obj);

		/**
		 * Returns true if the principal of the currently running script is the
		 * system principal, false otherwise.
		 */
		Boolean SubjectPrincipalIsSystem();

		/**
		 * Returns OK if aJSContext and target have the same "origin"
		 * (scheme, host, and port).
		 */
		void CheckSameOrigin(JSContextPtr aJSContext,
							 nsIURI aTargetURI);

		/**
		 * Returns OK if aSourceURI and target have the same "origin"
		 * (scheme, host, and port).
		 * ReportError flag suppresses error reports for functions that
		 * don't need reporting.
		 */
		void CheckSameOriginURI(nsIURI aSourceURI,
								nsIURI aTargetURI,
								Boolean reportError);

		/**
		 * Returns the principal of the global object of the given context, or null
		 * if no global or no principal.
		 */
		nsIPrincipal GetPrincipalFromContext(JSContextPtr cx);

		/**
		 * Get the principal for the given channel.  This will typically be the
		 * channel owner if there is one, and the codebase principal for the
		 * channel's URI otherwise.  aChannel must not be null.
		 */
		nsIPrincipal GetChannelPrincipal(nsIChannel aChannel);

		/**
		 * Check whether a given principal is a system principal.  This allows us
		 * to avoid handing back the system principal to script while allowing
		 * script to check whether a given principal is system.
		 */
		Boolean IsSystemPrincipal(nsIPrincipal aPrincipal);

		/**
		 * Same as getSubjectPrincipal(), only faster. cx must *never* be
		 * passed null, and it must be the context on the top of the
		 * context stack. Does *not* reference count the returned
		 * principal.
		 */
		nsIPrincipal GetCxSubjectPrincipal(JSContextPtr cx);
		nsIPrincipal GetCxSubjectPrincipalAndFrame(JSContextPtr cx, out JSStackFramePtr fp);

		/**
		 * If no scripted code is running "above" (or called from) fp, then
		 * instead of looking at cx->globalObject, we will return |principal|.
		 * This function only affects |cx|. If someone pushes another context onto
		 * the context stack, then it supersedes this call.
		 * NOTE: If |fp| is non-null popContextPrincipal must be called before fp
		 * has finished executing.
		 *
		 * @param cx The context to clamp.
		 * @param fp The frame pointer to clamp at. May be 'null'.
		 * @param principal The principal to clamp to.
		 */
		void PushContextPrincipal(JSContextPtr cx,
								  JSStackFramePtr fp,
								  nsIPrincipal principal);

		/**
		 * Removes a clamp set by pushContextPrincipal from cx. This must be
		 * called in a stack-like fashion (e.g., given two contexts |a| and |b|,
		 * it is not legal to do: push(a) push(b) pop(a)).
		 */
		void PopContextPrincipal(JSContextPtr cx);
	}
}
