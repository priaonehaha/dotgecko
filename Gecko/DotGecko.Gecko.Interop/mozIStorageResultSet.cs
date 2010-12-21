using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("18dd7953-076d-4598-8105-3e32ad26ab24"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageResultSet //: nsISupports
	{
		/**
		 * Obtains the next row from the result set from the statement that was
		 * executed.
		 *
		 * @returns the next row from the result set.  This will be null when there
		 *          are no more results.
		 */
		mozIStorageRow GetNextRow();
	}
}
