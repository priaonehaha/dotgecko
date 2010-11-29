using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal abstract class nsStringContainer_base
	{
		protected IntPtr d1;
		protected UInt32 d2;
		protected UInt32 d3;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal abstract class nsStringContainer : nsStringContainer_base
	{ }

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal abstract class nsCStringContainer : nsStringContainer_base
	{ }
}
