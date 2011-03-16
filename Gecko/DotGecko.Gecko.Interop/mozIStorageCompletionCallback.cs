using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("0bfee0c4-2c24-400e-b18e-b5bb41a032c8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageCompletionCallback //: nsISupports
	{
		/**
		 * Indicates that the event this callback was passed in for has completed.
		 */
		void Complete();
	}
}
