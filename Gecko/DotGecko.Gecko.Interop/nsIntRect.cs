using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[StructLayout(LayoutKind.Sequential)]
	public struct nsIntRect
	{
		public nsIntRect(Int32 x, Int32 y, Int32 width, Int32 height)
			: this()
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public Int32 X { get { return x; } }

		public Int32 Y { get { return y; } }

		public Int32 Width { get { return width; } }

		public Int32 Height { get { return height; } }

		private readonly Int32 x;
		private readonly Int32 y;
		private readonly Int32 width;
		private readonly Int32 height; 
	}
}
