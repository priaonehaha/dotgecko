using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("e676e1a3-1dc6-4802-ac03-291fa9de7f93"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageBindingParamsArray //: nsISupports
	{
		/**
		 * Creates a new mozIStorageBindingParams object that can be added to this
		 * array.
		 *
		 * @returns a mozIStorageBindingParams object that can be used to specify
		 *          parameters that need to be bound.
		 */
		mozIStorageBindingParams NewBindingParams();

		/**
		 * Adds the parameters to the end of this array.
		 *
		 * @param aParameters
		 *        The parameters to add to this array.
		 */
		void AddParams(mozIStorageBindingParams aParameters);
	}
}
