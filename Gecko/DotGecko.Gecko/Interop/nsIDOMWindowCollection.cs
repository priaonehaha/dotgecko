using System;
using System.Runtime.InteropServices;
using DOMString = DotGecko.Gecko.Interop.nsAString;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMWindowCollection interface is an interface for a
	 * collection of DOM window objects.
	 *
	 * @status FROZEN
	 */
	[ComImport]
	[Guid("a6cf906f-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMWindowCollection //: nsISupports
	{
		/**
		 * Accessor for the number of windows in this collection.
		 */
		UInt32 GetLength();

		/**
		 * Method for accessing an item in this collection by index.
		 */
		nsIDOMWindow Item(UInt32 index);

		/**
		 * Method for accessing an item in this collection by window name.
		 */
		nsIDOMWindow NamedItem(DOMString name);
	}
}
