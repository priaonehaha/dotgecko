using System;
using System.Drawing;

namespace DotGecko.Gecko
{
	public interface IWebBrowserContainer
	{
		IntPtr Handle { get; }

		Int32 Width { get; }

		Int32 Height { get; }

		Boolean IsVisible { get; set; }

		String Title { get; set; }

		ChromeFlags ChromeFlags { get; set; }

		String StatusText { set; }

		Boolean IsInModalState { get; }

		void Focus();

		void EnterModalState();

		void ExitModalState();

		Point PointToScreen(Point point);

		event EventHandler SizeChanged;
		
		event EventHandler GotFocus;

		event EventHandler LostFocus;
	}
}
