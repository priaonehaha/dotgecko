using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIContentViewerEdit    
	 */

	/**
	 * The nsIMarkupDocumentViewer
	 * This interface describes the properties of a content viewer 
	 * for a markup document - HTML or XML
	 */
	[ComImport, Guid("40b2282a-a882-4483-a634-dec468d88377"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIMarkupDocumentViewer //: nsISupports
	{
		/*
		Scrolls to a given DOM content node. 
		*/
		void ScrollToNode(nsIDOMNode node);

		/** The amount by which to scale all text. Default is 1.0. */
		Single TextZoom { get; set; }

		/** The amount by which to scale all lengths. Default is 1.0. */
		Single FullZoom { get; set; }

		/** Disable entire author style level (including HTML presentation hints) */
		Boolean AuthorStyleDisabled { get; set; }

		/*
		XXX Comment here!
		*/
		void GetDefaultCharacterSet([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);
		void SetDefaultCharacterSet([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String value);

		/*
		XXX Comment here!
		*/
		void GetForceCharacterSet([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);
		void SetForceCharacterSet([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String value);

		/*
		XXX Comment here!
		*/
		void GetHintCharacterSet([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);
		void SetHintCharacterSet([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String value);

		/*
		XXX Comment here!
		*/
		Int32 HintCharacterSetSource { get; set; }

		/*
		character set from prev document 
		*/
		void GetprevDocCharacterSet([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder retval);
		void SetprevDocCharacterSet([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String value);

		//void GetCharacterSetHint(in wstring hintCharset, in PRInt32 charsetSource);

		/**
		* Tell the container to shrink-to-fit or grow-to-fit its contents
		*/
		void SizeToContent();

		/**
		 * Options for Bidi presentation.
		 *
		 * Use these attributes to access the individual Bidi options.
		 */

		/**
		 * bidiTextDirection: the default direction for the layout of bidirectional text.
		 *                    1 - left to right
		 *                    2 - right to left
		 */
		Byte BidiTextDirection { get; set; }

		/**
		 * bidiTextType: the ordering of bidirectional text. This may be either "logical"
		 * or "visual". Logical text will be reordered for presentation using the Unicode
		 * Bidi Algorithm. Visual text will be displayed without reordering. 
		 *               1 - the default order for the charset
		 *               2 - logical order
		 *               3 - visual order
		 */
		Byte BidiTextType { get; set; }

		/**
		 * bidiControlsTextMode: the order of bidirectional text in form controls.
		 *                       1 - logical
		 *                       2 - visual
		 *                       3 - like the containing document
		 */
		Byte BidiControlsTextMode { get; set; }

		/**
		 * bidiNumeral: the type of numerals to display. 
		 *              1 - depending on context, default is Arabic numerals
		 *              2 - depending on context, default is Hindi numerals
		 *              3 - Arabic numerals
		 *              4 - Hindi numerals
		 */
		Byte BidiNumeral { get; set; }

		/**
		 * bidiSupport: whether to use platform bidi support or Mozilla's bidi support
		 *              1 - Use Mozilla's bidi support
		 *              2 - Use the platform bidi support
		 *              3 - Disable bidi support
		 */
		Byte BidiSupport { get; set; }

		/**
		 * bidiCharacterSet: whether to force the user's character set
		 *                   1 - use the document character set
		 *                   2 - use the character set chosen by the user
		 */
		Byte BidiCharacterSet { get; set; }

		/**
		 * Use this attribute to access all the Bidi options in one operation
		 */
		UInt32 BidiOptions { get; set; }
	}
}
