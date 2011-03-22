using System.Runtime.InteropServices;
using JSRuntime = System.IntPtr;
using JSGCCallback = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("364bcec3-7034-4a4e-bff5-b3f796ca9771"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIJSRuntimeService //: nsISupports
	{
		JSRuntime Runtime { get; }
		nsIXPCScriptable BackstagePass { get; }

		/**
		 * Register additional GC callback which will run after the
		 * standard XPConnect callback.
		 */
		void RegisterGCCallback(JSGCCallback func);
		void UnregisterGCCallback(JSGCCallback func);
	}
}
