using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomScreen
	{
		private DomScreen(nsIDOMScreen domScreen)
		{
			Debug.Assert(domScreen != null);
			m_DomScreen = domScreen;
		}

		internal static DomScreen Create(nsIDOMScreen domScreen)
		{
			return domScreen != null ? new DomScreen(domScreen) : null;
		}

		public Int32 Top { get { return m_DomScreen.Top; } }

		public Int32 Left { get { return m_DomScreen.Left; } }

		public Int32 Width { get { return m_DomScreen.Width; } }

		public Int32 Height { get { return m_DomScreen.Height; } }

		public Int32 PixelDepth { get { return m_DomScreen.PixelDepth; } }

		public Int32 ColorDepth { get { return m_DomScreen.ColorDepth; } }

		public Int32 AvailWidth { get { return m_DomScreen.AvailWidth; } }

		public Int32 AvailHeight { get { return m_DomScreen.AvailHeight; } }

		public Int32 AvailLeft { get { return m_DomScreen.AvailLeft; } }

		public Int32 AvailTop { get { return m_DomScreen.AvailTop; } }

		private readonly nsIDOMScreen m_DomScreen;
	}
}
