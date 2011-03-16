using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMDocumentView interface is a datatype for a document that
	 * supports views in the Document Object Model.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Views
	 */
	[ComImport, Guid("1ACDB2BA-1DD2-11B2-95BC-9542495D2569"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMDocumentView //: nsISupports
	{
		nsIDOMAbstractView DefaultView { get; }
	}
}
