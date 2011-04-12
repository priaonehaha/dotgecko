using System;
using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using DotGecko.Gecko;
using WebBrowser = DotGecko.Gecko.WebBrowser;

namespace DotGecko.Controls
{
	public sealed partial class WebBrowserElement : HwndHost
	{
		private static readonly DependencyPropertyKey TitlePropertyKey = DependencyProperty.RegisterReadOnly(
			"Title", typeof(String), typeof(WebBrowserElement),
			new PropertyMetadata());
		public static readonly DependencyProperty TitleProperty = TitlePropertyKey.DependencyProperty;

		private static readonly DependencyPropertyKey StatusTextPropertyKey = DependencyProperty.RegisterReadOnly(
			"StatusText", typeof(String), typeof(WebBrowserElement),
			new PropertyMetadata());
		public static readonly DependencyProperty StatusTextProperty = StatusTextPropertyKey.DependencyProperty;

		private static readonly DependencyPropertyKey ChromeFlagsPropertyKey = DependencyProperty.RegisterReadOnly(
			"ChromeFlags", typeof(ChromeFlags), typeof(WebBrowserElement),
			new PropertyMetadata());
		public static readonly DependencyProperty ChromeFlagsProperty = ChromeFlagsPropertyKey.DependencyProperty;

		static WebBrowserElement()
		{
			UIElement.FocusableProperty.OverrideMetadata(typeof(WebBrowserElement), new FrameworkPropertyMetadata(true));
			Control.IsTabStopProperty.OverrideMetadata(typeof(WebBrowserElement), new FrameworkPropertyMetadata(true));
		}

		public String Title
		{
			get { return (String)GetValue(TitleProperty); }
			private set { SetValue(TitlePropertyKey, value); }
		}

		public String StatusText
		{
			get { return (String)GetValue(StatusTextProperty); }
			private set { SetValue(StatusTextPropertyKey, value); }
		}

		public ChromeFlags ChromeFlags
		{
			get { return (ChromeFlags)GetValue(ChromeFlagsProperty); }
			private set { SetValue(ChromeFlagsPropertyKey, value); }
		}

		public WebBrowser Browser
		{
			get { return m_WebBrowser; }
		}

		protected override HandleRef BuildWindowCore(HandleRef hwndParent)
		{
			m_ParentHandle = hwndParent;
			m_WebBrowser = new WebBrowser(this);
			return new HandleRef(m_WebBrowser, m_WebBrowser.Handle);
		}

		protected override void DestroyWindowCore(HandleRef hwnd)
		{
			m_WebBrowser.Dispose();
			m_WebBrowser = null;
		}

		protected override IntPtr WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, ref Boolean handled)
		{
			const Int32 WM_MOUSEACTIVATE = 0x0021;

			if (msg == WM_MOUSEACTIVATE)
			{
				this.Focus();
			}

			return base.WndProc(hwnd, msg, wParam, lParam, ref handled);
		}

		protected override Boolean TabIntoCore(TraversalRequest request)
		{
			if (this.m_GotFocus != null)
			{
				this.m_GotFocus(this, EventArgs.Empty);
			}
			return true;
		}

		protected override void OnGotFocus(RoutedEventArgs e)
		{
			base.OnGotFocus(e);
			if (this.m_GotFocus != null)
			{
				this.m_GotFocus(this, EventArgs.Empty);
			}
		}

		protected override void OnLostFocus(RoutedEventArgs e)
		{
			base.OnLostFocus(e);
			if (this.m_LostFocus != null)
			{
				this.m_LostFocus(this, EventArgs.Empty);
			}
		}

		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
		{
			base.OnRenderSizeChanged(sizeInfo);
			if (m_SizeChanged != null)
			{
				m_SizeChanged(this, EventArgs.Empty);
			}
		}

		private HandleRef m_ParentHandle;
		private WebBrowser m_WebBrowser;
		private DispatcherFrame m_ModalFrame;
		private EventHandler m_GotFocus;
		private EventHandler m_LostFocus;
		private EventHandler m_SizeChanged;
	}
}
