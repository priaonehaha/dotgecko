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
			//TODO: Implement me!
		}

		void nsIEmbeddingSiteWindow.GetDimensions(UInt32 flags, out Int32 x, out Int32 y, out Int32 cx, out Int32 cy)
		{
			Trace.TraceInformation("nsIEmbeddingSiteWindow.GetDimensions(flags = 0x{0:X8})", flags);

			//TODO: Implement me!
			x = 0;
			y = 0;
			cx = Container.Width;
			cy = Container.Height;

			if ((flags & nsIEmbeddingSiteWindowConstants.DIM_FLAGS_POSITION) != 0)
			{
				Point pt = Container.PointToScreen(Point.Empty);
				x = pt.X;
				y = pt.Y;
			}
		}

		void nsIEmbeddingSiteWindow.SetFocus()
		{
			Container.Focus();
			BaseWindow.SetFocus();
		}

		Boolean nsIEmbeddingSiteWindow.Visibility
		{
			get { return Container.IsVisible; }
			set { Container.IsVisible = value; }
		}

		String nsIEmbeddingSiteWindow.Title
		{
			get { return Container.Title; }
			set { Container.Title = value; }
		}

		IntPtr nsIEmbeddingSiteWindow.SiteWindow
		{
			get { return Container.Handle; }
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
			//TODO: Implement me!
		}
	}
}
