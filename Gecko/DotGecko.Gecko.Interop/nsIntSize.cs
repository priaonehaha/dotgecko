using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[StructLayout(LayoutKind.Sequential)]
	public struct nsIntSize
	{
		private readonly Int32 width;
		private readonly Int32 height;
	}
}
