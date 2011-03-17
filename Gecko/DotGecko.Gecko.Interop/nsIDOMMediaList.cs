using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMMediaList interface is a datatype for a list of media
	 * types in the Document Object Model.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Style
	 */
	[ComImport, Guid("9b0c2ed7-111c-4824-adf9-ef0da6dad371"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMMediaList //: nsISupports
	{
		void GetMediaText([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetMediaText([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value); // raises(DOMException) on setting

		UInt32 Length { get; }
		void Item(UInt32 index, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void DeleteMedium([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String oldMedium); // raises(DOMException);
		void AppendMedium([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String newMedium); // raises(DOMException);
	}
}
