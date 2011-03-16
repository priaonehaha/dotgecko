using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	public static class nsIASN1ObjectConstants
	{
		/**
		 *  Identifiers for the possible types of object.
		 */
		public const UInt32 ASN1_END_CONTENTS = 0;
		public const UInt32 ASN1_BOOLEAN = 1;
		public const UInt32 ASN1_INTEGER = 2;
		public const UInt32 ASN1_BIT_STRING = 3;
		public const UInt32 ASN1_OCTET_STRING = 4;
		public const UInt32 ASN1_NULL = 5;
		public const UInt32 ASN1_OBJECT_ID = 6;
		public const UInt32 ASN1_ENUMERATED = 10;
		public const UInt32 ASN1_UTF8_STRING = 12;
		public const UInt32 ASN1_SEQUENCE = 16;
		public const UInt32 ASN1_SET = 17;
		public const UInt32 ASN1_PRINTABLE_STRING = 19;
		public const UInt32 ASN1_T61_STRING = 20;
		public const UInt32 ASN1_IA5_STRING = 22;
		public const UInt32 ASN1_UTC_TIME = 23;
		public const UInt32 ASN1_GEN_TIME = 24;
		public const UInt32 ASN1_VISIBLE_STRING = 26;
		public const UInt32 ASN1_UNIVERSAL_STRING = 28;
		public const UInt32 ASN1_BMP_STRING = 30;
		public const UInt32 ASN1_HIGH_TAG_NUMBER = 31;
		public const UInt32 ASN1_CONTEXT_SPECIFIC = 32;
		public const UInt32 ASN1_APPLICATION = 33;
		public const UInt32 ASN1_PRIVATE = 34;
	}

	/**
	 * This represents an ASN.1 object,
	 * where ASN.1 is "Abstract Syntax Notation number One".
	 *
	 * The additional state information carried in this interface
	 * makes it fit for being used as the data structure
	 * when working with visual reprenstation of ASN.1 objects
	 * in a human user interface, like in a tree widget
	 * where open/close state of nodes must be remembered.
	 */
	[ComImport, Guid("ba8bf582-1dd1-11b2-898c-f40246bc9a63"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIASN1Object //: nsISupports
	{
		/**
		 *  "type" will be equal to one of the defined object identifiers.
		 */
		UInt32 Type { get; set; }

		/**
		 *  This contains a tag as explained in ASN.1 standards documents.
		 */
		UInt32 Tag { get; set; }

		/**
		 *  "displayName" contains a human readable explanatory label.
		 */
		void GetDisplayName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		void SetDisplayName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);

		/**
		 *  "displayValue" contains the human readable value.
		 */
		void GetDisplayValue([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		void SetDisplayValue([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
	}
}
