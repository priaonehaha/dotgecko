using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/**
	 * This represents a sequence of ASN.1 objects,
	 * where ASN.1 is "Abstract Syntax Notation number One".
	 *
	 * Overview of how this ASN1 interface is intended to
	 * work.
	 *
	 * First off, the nsIASN1Sequence is any type in ASN1
	 * that consists of sub-elements (ie SEQUENCE, SET)
	 * nsIASN1Printable Items are all the other types that
	 * can be viewed by themselves without interpreting further.
	 * Examples would include INTEGER, UTF-8 STRING, OID.
	 * These are not intended to directly reflect the numberous
	 * types that exist in ASN1, but merely an interface to ease
	 * producing a tree display the ASN1 structure of any DER
	 * object.
	 *
	 * The additional state information carried in this interface
	 * makes it fit for being used as the data structure
	 * when working with visual reprenstation of ASN.1 objects
	 * in a human user interface, like in a tree widget
	 * where open/close state of nodes must be remembered.
	 */
	[ComImport, Guid("b6b957e6-1dd1-11b2-89d7-e30624f50b00"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIASN1Sequence : nsIASN1Object
	{
		#region nsIASN1Object Members

		new UInt32 Type { get; set; }
		new UInt32 Tag { get; set; }
		new void GetDisplayName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		new void SetDisplayName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
		new void GetDisplayValue([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		new void SetDisplayValue([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);

		#endregion

		/**
		 *  The array of objects stored in the sequence.
		 */
		nsIMutableArray ASN1Objects { get; set; }

		/**
		 *  Whether the node at this position in the ASN.1 data structure
		 *  sequence contains sub elements understood by the
		 *  application.
		 */
		Boolean IsValidContainer { get; set; }

		/**
		 *  Whether the contained objects should be shown or hidden.
		 *  A UI implementation can use this flag to store the current
		 *  expansion state when shown in a tree widget.
		 */
		Boolean IsExpanded { get; set; }
	}
}
