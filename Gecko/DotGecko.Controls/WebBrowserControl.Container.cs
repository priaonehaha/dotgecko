using System;
using System.Drawing;
using System.Windows.Forms;
using DotGecko.Gecko;

namespace DotGecko.Controls
{
	public sealed partial class WebBrowserControl : IWebBrowserContainer
	{
		IntPtr IWebBrowserContainer.GetHandle()
		{
			return Handle;
		}

		Point IWebBrowserContainer.GetOuterPosition()
		{
			Control topLevel = this.TopLevelControl ?? this;
			return new Point(topLevel.Left, topLevel.Top);
		}

		void IWebBrowserContainer.SetOuterPosition(Point outerPosition)
		{
			Control topLevel = this.TopLevelControl ?? this;
			topLevel.SetBounds(outerPosition.X, outerPosition.Y, 0, 0, BoundsSpecified.Location);
		}

		Size IWebBrowserContainer.GetOuterSize()
		{
			Control topLevel = this.TopLevelControl ?? this;
			return new Size(topLevel.Width, topLevel.Height);
		}

		void IWebBrowserContainer.SetOuterSize(Size outerSize)
		{
			Control topLevel = this.TopLevelControl ?? this;
			topLevel.SetBounds(0, 0, outerSize.Width, outerSize.Height, BoundsSpecified.Size);
		}

		Size IWebBrowserContainer.GetInnerSize()
		{
			return new Size(this.Width, this.Height);
		}

		void IWebBrowserContainer.SetInnerSize(Size innerSize)
		{
			this.SetBounds(0, 0, innerSize.Width, innerSize.Height, BoundsSpecified.Size);
		}

		Boolean IWebBrowserContainer.GetIsVisible()
		{
			return this.Visible;
		}

		void IWebBrowserContainer.SetIsVisible(Boolean visible)
		{
			this.Visible = visible;
		}

		String IWebBrowserContainer.GetTitle()
		{
			return this.Text;
		}

		void IWebBrowserContainer.SetTitle(String title)
		{
			this.Text = title;
		}

		ChromeFlags IWebBrowserContainer.GetChromeFlags()
		{
			return this.ChromeFlags;
		}

		void IWebBrowserContainer.SetChromeFlags(ChromeFlags chromeFlags)
		{
			this.ChromeFlags = chromeFlags;
		}

		void IWebBrowserContainer.SetStatusText(String statusText)
		{
			this.StatusText = statusText;
		}

		void IWebBrowserContainer.Focus()
		{
			this.Focus();
		}

		void IWebBrowserContainer.Blur()
		{
			//TODO: implement me!
		}

		void IWebBrowserContainer.EnterModalState()
		{
			//TODO: implement me!
		}

		void IWebBrowserContainer.ExitModalState()
		{
			//TODO: implement me!
		}

		Boolean IWebBrowserContainer.GetIsInModalState()
		{
			//TODO: implement me!
			return false;
		}

		event EventHandler IWebBrowserContainer.SizeChanged
		{
			add { this.SizeChanged += value; }
			remove { this.SizeChanged -= value; }
		}

		event EventHandler IWebBrowserContainer.GotFocus
		{
			add { this.Enter += value; }
			remove { this.Enter -= value; }
		}

		event EventHandler IWebBrowserContainer.LostFocus
		{
			add { this.Leave += value; }
			remove { this.Leave -= value; }
		}
	}
}
