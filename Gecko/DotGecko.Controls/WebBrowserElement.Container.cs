using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using DotGecko.Gecko;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace DotGecko.Controls
{
	public sealed partial class WebBrowserElement : IWebBrowserContainer
	{
		IntPtr IWebBrowserContainer.GetHandle()
		{
			return (IntPtr)this.m_ParentHandle;
		}

		Point IWebBrowserContainer.GetOuterPosition()
		{
			PresentationSource presentationSource = PresentationSource.FromVisual(this);
			if ((presentationSource == null) || (presentationSource.CompositionTarget == null))
			{
				return Point.Empty;
			}
			CompositionTarget compositionTarget = presentationSource.CompositionTarget;
			Visual rootVisual = compositionTarget.RootVisual;

			System.Windows.Point position = rootVisual.PointToScreen(new System.Windows.Point(0d, 0d));
			System.Windows.Point outerPosition = compositionTarget.TransformToDevice.Transform(position);
			return new Point((Int32)outerPosition.X, (Int32)outerPosition.Y);
		}

		void IWebBrowserContainer.SetOuterPosition(Point outerPosition)
		{
			PresentationSource presentationSource = PresentationSource.FromVisual(this);
			if ((presentationSource == null) || (presentationSource.CompositionTarget == null))
			{
				return;
			}
			CompositionTarget compositionTarget = presentationSource.CompositionTarget;

			System.Windows.Point position = compositionTarget.TransformFromDevice.Transform(new System.Windows.Point(outerPosition.X, outerPosition.Y));
			Visual rootVisual = compositionTarget.RootVisual;
			if (rootVisual is Window)
			{
				var rootWindow = (Window)rootVisual;
				rootWindow.Left = position.X;
				rootWindow.Top = position.Y;
			}
		}

		Size IWebBrowserContainer.GetOuterSize()
		{
			PresentationSource presentationSource = PresentationSource.FromVisual(this);
			if ((presentationSource == null) || (presentationSource.CompositionTarget == null))
			{
				return Size.Empty;
			}
			CompositionTarget compositionTarget = presentationSource.CompositionTarget;
			var rootElement = compositionTarget.RootVisual as FrameworkElement;
			if (rootElement == null)
			{
				return Size.Empty;
			}

			var size = new System.Windows.Point(rootElement.ActualWidth, rootElement.ActualHeight);
			System.Windows.Point outerSize = compositionTarget.TransformToDevice.Transform(size);
			return new Size((Int32)outerSize.X, (Int32)outerSize.Y);
		}

		void IWebBrowserContainer.SetOuterSize(Size outerSize)
		{
			PresentationSource presentationSource = PresentationSource.FromVisual(this);
			if ((presentationSource == null) || (presentationSource.CompositionTarget == null))
			{
				return;
			}
			CompositionTarget compositionTarget = presentationSource.CompositionTarget;
			var rootElement = compositionTarget.RootVisual as FrameworkElement;
			if (rootElement == null)
			{
				return;
			}

			System.Windows.Point size = compositionTarget.TransformFromDevice.Transform(new System.Windows.Point(outerSize.Width, outerSize.Height));
			rootElement.Width = size.X;
			rootElement.Height = size.Y;
		}

		Size IWebBrowserContainer.GetInnerSize()
		{
			PresentationSource presentationSource = PresentationSource.FromVisual(this);
			if ((presentationSource == null) || (presentationSource.CompositionTarget == null))
			{
				return Size.Empty;
			}
			CompositionTarget compositionTarget = presentationSource.CompositionTarget;

			var size = new System.Windows.Point(this.ActualWidth, this.ActualHeight);
			System.Windows.Point innerSize = compositionTarget.TransformToDevice.Transform(size);
			return new Size((Int32)innerSize.X, (Int32)innerSize.Y);
		}

		void IWebBrowserContainer.SetInnerSize(Size innerSize)
		{
			PresentationSource presentationSource = PresentationSource.FromVisual(this);
			if ((presentationSource == null) || (presentationSource.CompositionTarget == null))
			{
				return;
			}
			CompositionTarget compositionTarget = presentationSource.CompositionTarget;

			System.Windows.Point size = compositionTarget.TransformFromDevice.Transform(new System.Windows.Point(innerSize.Width, innerSize.Height));
			this.Width = size.X;
			this.Height = size.Y;
		}

		Boolean IWebBrowserContainer.GetIsVisible()
		{
			return IsVisible;
		}

		void IWebBrowserContainer.SetIsVisible(Boolean visible)
		{
			this.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
		}

		String IWebBrowserContainer.GetTitle()
		{
			return this.Title;
		}

		void IWebBrowserContainer.SetTitle(String title)
		{
			this.Title = title;
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
			//TODO: Implement me!
			//System.Windows.Input.Keyboard.ClearFocus();
		}

		void IWebBrowserContainer.EnterModalState()
		{
			if (m_ModalFrame == null)
			{
				m_ModalFrame = new DispatcherFrame();
				Dispatcher.PushFrame(m_ModalFrame);
			}
		}

		void IWebBrowserContainer.ExitModalState()
		{
			if (m_ModalFrame != null)
			{
				m_ModalFrame.Continue = false;
				m_ModalFrame = null;
			}
		}

		Boolean IWebBrowserContainer.GetIsInModalState()
		{
			return m_ModalFrame != null;
		}

		event EventHandler IWebBrowserContainer.SizeChanged
		{
			add { m_SizeChanged += value; }
			remove { m_SizeChanged -= value; }
		}

		event EventHandler IWebBrowserContainer.GotFocus
		{
			add { this.m_GotFocus += value; }
			remove { this.m_GotFocus -= value; }
		}

		event EventHandler IWebBrowserContainer.LostFocus
		{
			add { this.m_LostFocus += value; }
			remove { this.m_LostFocus -= value; }
		}
	}
}
