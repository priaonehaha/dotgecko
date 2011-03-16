using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("67eea5c3-4881-41ff-b0fe-09f2356aeadb"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageBindingParamsArray //: nsISupports
	{
		/**
		 * Creates a new mozIStorageBindingParams object that can be added to this
		 * array.
		 *
		 * @return a mozIStorageBindingParams object that can be used to specify
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

		/**
		 * The number of mozIStorageBindingParams this object contains.
		 */
		UInt32 Length { get; }
	}
}
