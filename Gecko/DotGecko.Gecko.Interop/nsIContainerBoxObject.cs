using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("35d4c04b-3bd3-4375-92e2-a818b4b4acb6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIContainerBoxObject //: nsISupports
	{
		nsIDocShell DocShell { get; }
	}
}
