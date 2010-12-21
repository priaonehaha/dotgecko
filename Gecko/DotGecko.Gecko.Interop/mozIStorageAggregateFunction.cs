using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * mozIStorageAggregateFunction represents aggregate SQL function.
	 * Common examples of aggregate functions are SUM() and COUNT().
	 *
	 * An aggregate function calculates one result for a given set of data, where
	 * a set of data is a group of tuples. There can be one group
	 * per request or many of them, if GROUP BY clause is used or not.
	 */
	[ComImport, Guid("763217b7-3123-11da-918d-000347412e16"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageAggregateFunction //: nsISupports
	{
		/**
		 * onStep is called when next value should be passed to
		 * a custom function.
		 * 
		 * @param aFunctionArguments    The arguments passed in to the function
		 */
		void OnStep(mozIStorageValueArray aFunctionArguments);

		/**
		 * Called when all tuples in a group have been processed and the engine
		 * needs the aggregate function's value.
		 *
		 * @returns aggregate result as Variant.
		 */
		nsIVariant OnFinal();
	}
}
