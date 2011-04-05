using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[StructLayout(LayoutKind.Sequential)]
	public struct gfxMatrix
	{
		private readonly Double xx;
		private readonly Double yx;
		private readonly Double xy;
		private readonly Double yy;
		private readonly Double x0;
		private readonly Double y0;
	}
}
