using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("4a2abaf0-6886-11d3-9382-00104ba0fd40"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIRunnable //: nsISupports
	{
		void Run();
	}
}
