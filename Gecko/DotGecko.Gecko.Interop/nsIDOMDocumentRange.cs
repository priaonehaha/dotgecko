using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMDocumentRange interface is an interface to a document
	 * object that supports ranges in the Document Object Model.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Traversal-Range/
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("7b9badc6-c9bc-447a-8670-dbd195aed24b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMDocumentRange //: nsISupports
	{
		nsIDOMRange CreateRange();
	}
}
