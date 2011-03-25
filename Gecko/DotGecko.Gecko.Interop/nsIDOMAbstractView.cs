using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMAbstractView interface is a datatype for a view in the
	 * Document Object Model.
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-2-Views
	 */
	[ComImport, Guid("F51EBADE-8B1A-11D3-AAE7-0010830123B4"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMAbstractView //: nsISupports
	{
		nsIDOMDocumentView Document { get; }
	}
}
