using System;
using System.Runtime.InteropServices;
using System.Text;
using nsISupports = System.Object;
using JSContext = System.IntPtr;
using JSPrincipals = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	public static class nsIPrincipalConstants
	{
		/**
		 * Values of capabilities for each principal. Order is
		 * significant: if an operation is performed on a set
		 * of capabilities, the minimum is computed.
		 */
		public const Int16 ENABLE_DENIED = 1;
		public const Int16 ENABLE_UNKNOWN = 2;
		public const Int16 ENABLE_WITH_USER_PERMISSION = 3;
		public const Int16 ENABLE_GRANTED = 4;
	}

	/* Defines the abstract interface for a principal. */
	[ComImport, Guid("799ab95c-0038-4e0f-b705-74c21f185bb5"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIPrincipal : nsISerializable
	{
		#region nsISerializable Members

		new void Read(nsIObjectInputStream aInputStream);
		new void Write(nsIObjectOutputStream aOutputStream);

		#endregion

		/**
		 * Returns the security preferences associated with this principal.
		 * prefBranch will be set to the pref branch to which these preferences
		 * pertain.  id is a pseudo-unique identifier, pertaining to either the
		 * fingerprint or the origin.  subjectName is a name that identifies the
		 * entity this principal represents (may be empty).  grantedList and
		 * deniedList are space-separated lists of capabilities which were
		 * explicitly granted or denied by a pref.  isTrusted is a boolean that
		 * indicates whether this is a codebaseTrusted certificate.
		 */
		void GetPreferences(
			[MarshalAs(UnmanagedType.LPStr)] out String prefBranch,
			[MarshalAs(UnmanagedType.LPStr)] out String id,
			[MarshalAs(UnmanagedType.LPStr)] out String subjectName,
			[MarshalAs(UnmanagedType.LPStr)] out String grantedList,
			[MarshalAs(UnmanagedType.LPStr)] out String deniedList,
			out Boolean isTrusted);

		/**
		 * Returns whether the other principal is equivalent to this principal.
		 * Principals are considered equal if they are the same principal,
		 * they have the same origin, or have the same certificate fingerprint ID
		 */
		Boolean Equals(nsIPrincipal other);

		/**
		 * Returns a hash value for the principal.
		 */
		UInt32 HashValue { get; }

		/**
		 * Returns the JS equivalent of the principal.
		 * @see JSPrincipals.h
		 */
		JSPrincipals GetJSPrincipals(JSContext cx);

		/**
		 * The domain security policy of the principal.
		 */
		// XXXcaa should this be here?  The script security manager is the only
		// thing that should care about this.  Wouldn't storing this data in one
		// of the hashtables in nsScriptSecurityManager be better?
		// XXXbz why is this writable?  Who should have write access to this?  What
		// happens if this principal is in our hashtable and we pass it out of the
		// security manager and someone writes to this field?  Especially if they
		// write garbage?  If we need to give someone other than the security
		// manager a way to set this (which I question, since it can increase the
		// permissions of a page) it should be a |void clearSecurityPolicy()|
		// method.
		IntPtr SecurityPolicy { get; set; }

		// XXXcaa probably should be turned into {get|set}CapabilityFlags
		// XXXbz again, what if this lives in our hashtable and someone
		// messes with it?  Is that OK?
		Int16 CanEnableCapability([MarshalAs(UnmanagedType.LPStr)] String capability);
		void SetCanEnableCapability([MarshalAs(UnmanagedType.LPStr)] String capability, Int16 canEnable);
		Boolean IsCapabilityEnabled([MarshalAs(UnmanagedType.LPStr)] String capability, IntPtr annotation);
		void EnableCapability([MarshalAs(UnmanagedType.LPStr)] String capability, ref IntPtr annotation);
		void RevertCapability([MarshalAs(UnmanagedType.LPStr)] String capability, ref IntPtr annotation);
		void DisableCapability([MarshalAs(UnmanagedType.LPStr)] String capability, ref IntPtr annotation);

		/**
		 * The codebase URI to which this principal pertains.  This is
		 * generally the document URI.
		 */
		nsIURI URI { get; }

		/**
		 * The domain URI to which this principal pertains.
		 * This is congruent with HTMLDocument.domain, and may be null.
		 * Setting this has no effect on the URI.
		 */
		nsIURI Domain { get; set; }

		/**
		 * The origin of this principal's codebase URI.
		 * An origin is defined as: scheme + host + port.
		 */
		// XXXcaa this should probably be turned into an nsIURI.
		// The system principal's origin should be some caps namespace
		// with a chrome URI.  All of chrome should probably be the same.
		String Origin { [return: MarshalAs(UnmanagedType.LPStr)] get; }

		/**
		 * Whether this principal is associated with a certificate.
		 */
		Boolean HasCertificate { get; }

		/**
		 * The fingerprint ID of this principal's certificate.
		 * Throws if there is no certificate associated with this principal.
		 */
		// XXXcaa kaie says this may not be unique.  We should probably
		// consider using something else for this....
		void GetFingerprint([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);

		/**
		 * The pretty name for the certificate.  This sort of (but not really)
		 * identifies the subject of the certificate (the entity that stands behind
		 * the certificate).  Note that this may be empty; prefer to get the
		 * certificate itself and get this information from it, since that may
		 * provide more information.
		 *
		 * Throws if there is no certificate associated with this principal.
		 */
		void GetPrettyName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);

		/**
		 * Returns whether the other principal is equal to or weaker than this
		 * principal.  Principals are equal if they are the same object, they
		 * have the same origin, or they have the same certificate ID.
		 *
		 * Thus a principal always subsumes itself.
		 *
		 * The system principal subsumes itself and all other principals.
		 *
		 * A null principal (corresponding to an unknown, hence assumed minimally
		 * privileged, security context) is not equal to any other principal
		 * (including other null principals), and therefore does not subsume
		 * anything but itself.
		 *
		 * Both codebase and certificate principals are subsumed by the system
		 * principal, but no codebase or certificate principal yet subsumes any
		 * other codebase or certificate principal.  This may change in a future
		 * release; note that nsIPrincipal is unfrozen, not slated to be frozen.
		 *
		 * XXXbz except see bug 147145!
		 *
		 * Note for the future: Perhaps we should consider a certificate principal
		 * for a given URI subsuming a codebase principal for the same URI?  Not
		 * sure what the immediate benefit would be, but I think the setup could
		 * make some code (e.g. MaybeDowngradeToCodebase) clearer.
		 */
		Boolean Subsumes(nsIPrincipal other);

		/**
		 * Checks whether this principal is allowed to load the network resource
		 * located at the given URI under the same-origin policy. This means that
		 * codebase principals are only allowed to load resources from the same
		 * domain, the system principal is allowed to load anything, and null
		 * principals are not allowed to load anything.
		 *
		 * If the load is allowed this function does nothing. If the load is not
		 * allowed the function throws NS_ERROR_DOM_BAD_URI.
		 *
		 * NOTE: Other policies might override this, such as the Access-Control
		 *       specification.
		 * NOTE: The 'domain' attribute has no effect on the behaviour of this
		 *       function.
		 *
		 *
		 * @param uri    The URI about to be loaded.
		 * @param report If true, will report a warning to the console service
		 *               if the load is not allowed.
		 * @throws NS_ERROR_DOM_BAD_URI if the load is not allowed.
		 */
		void CheckMayLoad(nsIURI uri, Boolean report);

		/**
		 * The subject name for the certificate.  This actually identifies the
		 * subject of the certificate.  This may well not be a string that would
		 * mean much to a typical user on its own (e.g. it may have a number of
		 * different names all concatenated together with some information on what
		 * they mean in between).
		 *
		 * Throws if there is no certificate associated with this principal.
		 */
		void GetSubjectName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);

		/**
		 * The certificate associated with this principal, if any.  If there isn't
		 * one, this will return null.  Getting this attribute never throws.
		 */
		nsISupports Certificate { [return: MarshalAs(UnmanagedType.IUnknown)] get; }

		/**
		 * A Content Security Policy associated with this principal.
		 */
		nsIContentSecurityPolicy CSP { get; set; }
	}
}
