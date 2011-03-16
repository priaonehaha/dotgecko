using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMDocumentStyle interface is an interface to a document
	 * object that supports style sheets in the Document Object Model.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Style
	 */
	[ComImport, Guid("3d9f4973-dd2e-48f5-b5f7-2634e09eadd9"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMDocumentStyle //: nsISupports
	{
		nsIDOMStyleSheetList StyleSheets { get; }
	}
}
