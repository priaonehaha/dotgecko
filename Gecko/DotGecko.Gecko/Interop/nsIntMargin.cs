using System;

namespace DotGecko.Gecko.Interop
{
	internal struct nsIntMargin
	{
		public nsIntMargin(Int32 top, Int32 right, Int32 bottom, Int32 left)
			: this()
		{
			this.top = top;
			this.right = right;
			this.bottom = bottom;
			this.left = left;
		}

		public Int32 Top { get { return top; } }

		public Int32 Right { get { return right; } }

		public Int32 Bottom { get { return bottom; } }

		public Int32 Left { get { return left; } }

		private readonly Int32 top;
		private readonly Int32 right;
		private readonly Int32 bottom;
		private readonly Int32 left;
	}
}
