using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 *
	 * nsIMemory: interface to allocate and deallocate memory. Also provides
	 * for notifications in low-memory situations.
	 *
	 * The frozen exported symbols NS_Alloc, NS_Realloc, and NS_Free
	 * provide a more efficient way to access XPCOM memory allocation. Using
	 * those symbols is preferred to using the methods on this interface.
	 *
	 * A client that wishes to be notified of low memory situations (for
	 * example, because the client maintains a large memory cache that
	 * could be released when memory is tight) should register with the
	 * observer service (see nsIObserverService) using the topic 
	 * "memory-pressure". There are three specific types of notications 
	 * that can occur.  These types will be passed as the |aData| 
	 * parameter of the of the "memory-pressure" notification: 
	 * 
	 * "low-memory"
	 * This will be passed as the extra data when the pressure 
	 * observer is being asked to flush for low-memory conditions.
	 *
	 * "heap-minimize"
	 * This will be passed as the extra data when the pressure 
	 * observer is being asked to flush because of a heap minimize 
	 * call.
	 *
	 * "alloc-failure"
	 * This will be passed as the extra data when the pressure 
	 * observer has been asked to flush because a malloc() or 
	 * realloc() has failed.
	 */
	[ComImport, Guid("59e7e77a-38e4-11d4-8cf5-0060b0fc14a3"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIMemory //: nsISupports
	{
		/**
		 * Allocates a block of memory of a particular size. If the memory 
		 * cannot be allocated (because of an out-of-memory condition), null
		 * is returned.
		 *
		 * @param size - the size of the block to allocate
		 * @result the block of memory
		 */
		IntPtr Alloc(UInt32 size);

		/**
		 * Reallocates a block of memory to a new size.
		 *
		 * @param ptr - the block of memory to reallocate
		 * @param size - the new size
		 * @result the reallocated block of memory
		 *
		 * If ptr is null, this function behaves like malloc.
		 * If s is the size of the block to which ptr points, the first
		 * min(s, size) bytes of ptr's block are copied to the new block.
		 * If the allocation succeeds, ptr is freed and a pointer to the 
		 * new block returned.  If the allocation fails, ptr is not freed
		 * and null is returned. The returned value may be the same as ptr.
		 */
		IntPtr Realloc(IntPtr ptr, UInt32 newSize);

		/**
		 * Frees a block of memory. Null is a permissible value, in which case
		 * nothing happens. 
		 *
		 * @param ptr - the block of memory to free
		 */
		void Free(IntPtr ptr);

		/**
		 * Attempts to shrink the heap.
		 * @param immediate - if true, heap minimization will occur
		 *   immediately if the call was made on the main thread. If
		 *   false, the flush will be scheduled to happen when the app is
		 *   idle.
		 * @return NS_ERROR_FAILURE if 'immediate' is set an the call
		 *   was not on the application's main thread.
		 */
		void HeapMinimize(Boolean immediate);

		/**
		 * This predicate can be used to determine if we're in a low-memory
		 * situation (what constitutes low-memory is platform dependent). This
		 * can be used to trigger the memory pressure observers.
		 *
		 * DEPRECATED - Always returns false.  See bug 592308.
		 */
		[Obsolete]
		Boolean IsLowMemory();
	}
}
