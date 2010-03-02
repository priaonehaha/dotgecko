using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMBarProp interface is the interface for controlling and
	 * accessing the visibility of certain UI items (scrollbars, menubars,
	 * toolbars, ...) through the DOM.
	 *
	 * @status FROZEN
	 */
	[ComImport]
	[Guid("9eb2c150-1d56-11d3-8221-0060083a0bcf")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMBarProp //: nsISupports
	{
		Boolean GetVisible();
		void SetVisible(Boolean value);
	}
}
