using System;
using System.Drawing;

namespace DotGecko.Gecko
{
	public interface IWebBrowserContainer
	{
		IntPtr GetHandle();

		Point GetOuterPosition();

		void SetOuterPosition(Point outerPosition);

		Size GetOuterSize();

		void SetOuterSize(Size outerSize);

		Size GetInnerSize();

		void SetInnerSize(Size innerSize);

		Boolean GetIsVisible();

		void SetIsVisible(Boolean visible);

		String GetTitle();

		void SetTitle(String title);

		ChromeFlags GetChromeFlags();

		void SetChromeFlags(ChromeFlags chromeFlags);

		void SetStatusText(String statusText);

		void Focus();

		void Blur();

		void EnterModalState();

		void ExitModalState();

		Boolean GetIsInModalState();

		event EventHandler SizeChanged;
		
		event EventHandler GotFocus;

		event EventHandler LostFocus;
	}
}
