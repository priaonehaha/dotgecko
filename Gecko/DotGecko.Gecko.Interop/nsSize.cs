using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[StructLayout(LayoutKind.Explicit, Pack = 4)]
	public struct nsSize
	{
		public nsSize(Int32 width, Int32 height)
		{
			m_FloatWidth = 0;
			m_FloatHeight = 0;
			m_IntWidth = width;
			m_IntHeight = height;
		}

		public nsSize(Single width, Single height)
		{
			m_IntWidth = 0;
			m_IntHeight = 0;
			m_FloatWidth = width;
			m_FloatHeight = height;
		}

		public Int32 IntWidth { get { return m_IntWidth; } }

		public Single FloatWidth { get { return m_FloatWidth; } }

		public Int32 IntHeight { get { return m_IntHeight; } }

		public Single FloatHeight { get { return m_FloatHeight; } }

		[FieldOffset(0)]
		private readonly Int32 m_IntWidth;
		[FieldOffset(0)]
		private readonly Single m_FloatWidth;
		[FieldOffset(4)]
		private readonly Int32 m_IntHeight;
		[FieldOffset(4)]
		private readonly Single m_FloatHeight;
	}
}
