using System;
using System.Runtime.InteropServices;
using JSContext = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("c67d8270-3189-11d3-9885-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIJSContextStack //: nsISupports
	{
		Int32 Count { get; }
		JSContext Peek();
		JSContext Pop();
		void Push(JSContext cx);
	}

	[ComImport, Guid("c7e6b7aa-fc12-4ca7-b140-98c38b698961"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIJSContextStackIterator //: nsISupports
	{
		/**
		 * Resets this iterator to the beginning of this thread's stack.
		 */
		void Reset(nsIJSContextStack stack);

		/**
		 * Returns true if this iterator is at the end of its stack's contexts.
		 * @throws NS_ERROR_NOT_INITIALIZED If there has not been a previous call
		 *         to reset.
		 */
		Boolean Done();

		/**
		 * Returns the prev JSContext off of stack. Note that because we're 
		 * iterating over a stack, this value would be the next popped value.
		 *
		 * @throws NS_ERROR_NOT_INITIALIZED If there has not been a previous call
		 *         to reset.
		 * @throws NS_ERROR_NOT_AVAILABLE if already at the end.
		 */
		JSContext Prev();
	}

	[ComImport, Guid("a1339ae0-05c1-11d4-8f92-0010a4e73d9a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIThreadJSContextStack : nsIJSContextStack
	{
		#region nsIJSContextStack Members

		new Int32 Count { get; }
		new JSContext Peek();
		new JSContext Pop();
		new void Push(JSContext cx);

		#endregion

		/* inherits methods of nsIJSContextStack */
		JSContext SafeJSContext { get; set; }
	}
}
