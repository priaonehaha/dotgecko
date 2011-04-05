using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[StructLayout(LayoutKind.Sequential)]
	public struct gfxSize
	{
		private readonly Single width;
		private readonly Single height;
	}
}
