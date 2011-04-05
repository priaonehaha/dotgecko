using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[StructLayout(LayoutKind.Sequential)]
	public struct gfxRect
	{
		private readonly gfxPoint pos;
		private readonly gfxSize size;
	}
}
