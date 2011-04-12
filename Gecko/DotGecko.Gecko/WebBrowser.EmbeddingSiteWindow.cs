using System;
using System.Diagnostics;
using System.Drawing;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser : nsIEmbeddingSiteWindow, nsIEmbeddingSiteWindow2
	{
		void nsIEmbeddingSiteWindow.SetDimensions(UInt32 flags, Int32 x, Int32 y, Int32 cx, Int32 cy)
		{
			Trace.TraceInformation("nsIEmbeddingSiteWindow.SetDimensions(falgs = 0x{0:X8}, x = {1}, y = {2}, cx = {3}, cy = {4})", flags, x, y, cx, cy);

			if (flags.HasFlag(nsIEmbeddingSiteWindowConstants.DIM_FLAGS_POSITION))
			{
				Container.SetOuterPosition(new Point(x, y));
			}

			// The inner and outer flags are mutually exclusive and it is invalid to combine them.
			if (flags.HasFlag(nsIEmbeddingSiteWindowConstants.DIM_FLAGS_SIZE_INNER))
			{
				Container.SetInnerSize(new Size(cx, cy));
			}
			else if (flags.HasFlag(nsIEmbeddingSiteWindowConstants.DIM_FLAGS_SIZE_OUTER))
			{
				Container.SetOuterSize(new Size(cx, cy));
			}
		}

		void nsIEmbeddingSiteWindow.GetDimensions(UInt32 flags, out Int32 x, out Int32 y, out Int32 cx, out Int32 cy)
		{
			Trace.TraceInformation("nsIEmbeddingSiteWindow.GetDimensions(flags = 0x{0:X8})", flags);

			x = y = cx = cy = 0;

			if (flags.HasFlag(nsIEmbeddingSiteWindowConstants.DIM_FLAGS_POSITION))
			{
				Point outerPosition = Container.GetOuterPosition();
				x = outerPosition.X;
				y = outerPosition.Y;
			}

			if (flags.HasFlag(nsIEmbeddingSiteWindowConstants.DIM_FLAGS_SIZE_INNER))
			{
				Size innerSize = Container.GetInnerSize();
				cx = innerSize.Width;
				cy = innerSize.Height;
			}
			else if (flags.HasFlag(nsIEmbeddingSiteWindowConstants.DIM_FLAGS_SIZE_OUTER))
			{
				Size outerSize = Container.GetOuterSize();
				cx = outerSize.Width;
				cy = outerSize.Height;
			}
		}

		void nsIEmbeddingSiteWindow.SetFocus()
		{
			Container.Focus();
			BaseWindow.SetFocus();
		}

		Boolean nsIEmbeddingSiteWindow.Visibility
		{
			get { return Container.GetIsVisible(); }
			set { Container.SetIsVisible(value); }
		}

		String nsIEmbeddingSiteWindow.Title
		{
			get { return Container.GetTitle(); }
			set { Container.SetTitle(value); }
		}

		IntPtr nsIEmbeddingSiteWindow.SiteWindow
		{
			get { return Container.GetHandle(); }
		}

		#region Redirect

		void nsIEmbeddingSiteWindow2.SetDimensions(UInt32 flags, Int32 x, Int32 y, Int32 cx, Int32 cy)
		{
			((nsIEmbeddingSiteWindow)this).SetDimensions(flags, x, y, cx, cy);
		}

		void nsIEmbeddingSiteWindow2.GetDimensions(UInt32 flags, out Int32 x, out Int32 y, out Int32 cx, out Int32 cy)
		{
			((nsIEmbeddingSiteWindow)this).GetDimensions(flags, out x, out y, out cx, out cy);
		}

		void nsIEmbeddingSiteWindow2.SetFocus()
		{
			((nsIEmbeddingSiteWindow)this).SetFocus();
		}

		Boolean nsIEmbeddingSiteWindow2.Visibility
		{
			get { return ((nsIEmbeddingSiteWindow)this).Visibility; }
			set { ((nsIEmbeddingSiteWindow)this).Visibility = value; }
		}

		String nsIEmbeddingSiteWindow2.Title
		{
			get { return ((nsIEmbeddingSiteWindow)this).Title; }
			set { ((nsIEmbeddingSiteWindow)this).Title = value; }
		}

		IntPtr nsIEmbeddingSiteWindow2.SiteWindow
		{
			get { return ((nsIEmbeddingSiteWindow)this).SiteWindow; }
		}

		#endregion

		void nsIEmbeddingSiteWindow2.Blur()
		{
			Container.Blur();
		}
	}
}
