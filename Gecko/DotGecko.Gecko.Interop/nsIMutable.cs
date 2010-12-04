using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIMutable defines an interface to be implemented by objects which
	 * can be made immutable.
	 */
	[ComImport, Guid("321578d0-03c1-4d95-8821-021ac612d18d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIMutable //: nsISupports
	{
		/**
		 * Control whether or not this object can be modified.  If the flag is
		 * false, no modification is allowed.  Once the flag has been set to false,
		 * it cannot be reset back to true -- attempts to do so throw
		 * NS_ERROR_INVALID_ARG.
		 */
		Boolean Mutable { get; set; }
	}
}
