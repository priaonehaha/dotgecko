using System;
using System.Drawing;

namespace DotGecko.Gecko
{
	public sealed class TooltipEventArgs : EventArgs
	{
		internal TooltipEventArgs(Int32 x, Int32 y, String text)
			: this(new Point(x, y), text)
		{ }

		internal TooltipEventArgs(Point position, String text)
		{
			m_Position = position;
			m_Text = text;
		}

		public Point Position
		{
			get { return m_Position; }
		}

		public String Text
		{
			get { return m_Text; }
		}

		private readonly Point m_Position;
		private readonly String m_Text;
	}
}
