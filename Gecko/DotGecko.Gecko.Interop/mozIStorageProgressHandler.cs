using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * mozIProgressHandler is to be implemented by storage consumers that
	 * wish to receive callbacks during the request execution.
	 */
	[ComImport, Guid("a3a6fcd4-bf89-4208-a837-bf2a73afd30c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageProgressHandler //: nsISupports
	{
		/**
		 * onProgress is invoked periodically during long running calls.
		 * 
		 * @param aConnection    connection, for which progress handler is
		 *                       invoked.
		 *
		 * @return true to abort request, false to continue work.
		 */

		Boolean OnProgress(mozIStorageConnection aConnection);
	}
}
