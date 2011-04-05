using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[StructLayout(LayoutKind.Sequential)]
	public struct gfxPoint
	{
		private readonly Single x;
		private readonly Single y;
	}
}
