using System;
using System.Runtime.InteropServices;
using System.Text;
using nsQIResult = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * An nsIRDFResource is an object that has unique identity in the 
	 * RDF data model. The object's identity is determined by its URI.
	 */
	[ComImport, Guid("fb9686a7-719a-49dc-9107-10dea5739341"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIRDFResource : nsIRDFNode
	{
		#region nsIRDFNode Members

		new Boolean EqualsNode(nsIRDFNode aNode);

		#endregion

		/**
		 * The single-byte string value of the resource.
		 * @note THIS IS OBSOLETE. C++ should use GetValueConst and script
		 *       should use .valueUTF8
		 */
		String Value { [return: MarshalAs(UnmanagedType.LPStr)] get; }

		/**
		 * The UTF-8 URI of the resource.
		 */
		void GetValueUTF8([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);

		/**
		 * An unscriptable version used to avoid a string copy. Meant
		 * for use as a performance optimization. The string is encoded
		 * in UTF-8.
		 */
		void GetValueConst([Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder aConstValue);

		/**
		 * This method is called by the nsIRDFService after constructing
		 * a resource object to initialize its URI. You would not normally
		 * call this method directly
		 */
		void Init([MarshalAs(UnmanagedType.LPStr)] String uri);

		/**
		 * Determine if the resource has the given URI.
		 */
		Boolean EqualsString([MarshalAs(UnmanagedType.LPStr)] String aURI);

		/**
		 * Retrieve the "delegate" object for this resource. A resource
		 * may have several delegate objects, each of whose lifetimes is
		 * bound to the life of the resource object.
		 *
		 * This method will return the delegate for the given key after
		 * QueryInterface()-ing it to the requested IID.
		 *
		 * If no delegate exists for the specified key, this method will
		 * attempt to create one using the component manager. Specifically,
		 * it will combine aKey with the resource's URI scheme to produce
		 * a ContractID as follows:
		 *
		 *   component:/rdf/delegate-factory/[key]/[scheme]
		 *
		 * This ContractID will be used to locate a factory using the
		 * FindFactory() method of nsIComponentManager. If the nsIFactory
		 * exists, it will be used to create a "delegate factory"; that
		 * is, an object that supports nsIRDFDelegateFactory. The delegate
		 * factory will be used to construct the delegate object.
		 */
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 1)]
		nsQIResult GetDelegate([MarshalAs(UnmanagedType.LPStr)] String aKey, [In] ref Guid aIID);

		/**
		 * Force a delegate to be "unbound" from the resource.
		 *
		 * Normally, a delegate object's lifetime will be identical to
		 * that of the resource to which it is bound; this method allows a
		 * delegate to unlink itself from an RDF resource prematurely.
		 */
		void ReleaseDelegate([MarshalAs(UnmanagedType.LPStr)] String aKey);
	}
}
