using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMNodeList interface provides the abstraction of an ordered 
	 * collection of nodes, without defining or constraining how this collection 
	 * is implemented.
	 * The items in the list are accessible via an integral index, starting from 0.
	 *
	 * For more information on this interface please see 
	 * http://www.w3.org/TR/DOM-Level-2-Core/
	 *
	 * @status FROZEN
	 */
	[ComImport]
	[Guid("a6cf907d-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMNodeList //: nsISupports
	{
		nsIDOMNode Item(UInt32 index);
		UInt32 GetLength();
	}
}
