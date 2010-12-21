using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("00da7d20-3768-4398-bedc-e310c324b3f0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStoragePendingStatement //: nsISupports
	{
		/**
		 * Cancels a pending statement, if possible.  This will only fail if you try
		 * cancel more than once.
		 *
		 * @note For read statements (such as SELECT), you will no longer receive any
		 *       notifications about results once cancel is called.
		 */
		void Cancel();
	}
}
