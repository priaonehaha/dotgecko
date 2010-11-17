using System;

namespace DotGecko.Gecko
{
	public sealed class ChromeSizeChangeEventArgs : EventArgs
	{
		internal ChromeSizeChangeEventArgs(Int32 width, Int32 height)
		{
			m_Width = width;
			m_Height = height;
		}

		public Int32 Width { get { return m_Width; } }

		public Int32 Height { get { return m_Height; } }

		private readonly Int32 m_Width;
		private readonly Int32 m_Height;
	}
}
