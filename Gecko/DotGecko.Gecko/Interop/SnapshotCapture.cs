//#define USE_IOSERVICE

using System;
#if USE_IOSERVICE
using System.Runtime.InteropServices;
#endif

namespace DotGecko.Gecko.Interop
{
	internal static class SnapshotCapture
	{
		internal static Byte[] CaptureWindow(nsIDOMWindow domWindow)
		{
			var domWindowInternal = (nsIDOMWindowInternal)domWindow;

			nsIDOMHTMLCanvasElement canvas = GetCanvas(domWindowInternal.Document);
			canvas.Width = domWindowInternal.InnerWidth;// + domWindowInternal.ScrollMaxX;
			canvas.Height = domWindowInternal.InnerHeight;// + domWindowInternal.ScrollMaxY;

			DrawWindow(canvas, domWindow);

			Byte[] imageData = GetImageData(canvas);
			return imageData;
		}

		private static nsIDOMHTMLCanvasElement GetCanvas(nsIDOMDocument domDocument)
		{
			var canvas = (nsIDOMHTMLCanvasElement)domDocument.CreateElementNS("http://www.w3.org/1999/xhtml", "html:canvas");
			return canvas;
		}

		private static void DrawWindow(nsIDOMHTMLCanvasElement canvasElement, nsIDOMWindow domWindow)
		{
			var context = (nsIDOMCanvasRenderingContext2D)canvasElement.GetContext("2d");
			context.DrawWindow(domWindow, 0, 0, canvasElement.Width, canvasElement.Height, "rgba(0,0,0,0)", 0);
		}

		private static Byte[] GetImageData(nsIDOMHTMLCanvasElement canvasElement)
		{
			String data = XpcomString.Get(canvasElement.ToDataURLAs, "image/png", "");
			if (String.IsNullOrWhiteSpace(data))
			{
				return null;
			}

#if USE_IOSERVICE

			var ioService = Xpcom.GetService<nsIIOService>(Xpcom.NS_IOSERVICE_CONTRACTID);
			nsIChannel channel = ioService.NewChannel(data, null, null);
			nsIInputStream stream = channel.Open();
			try
			{
				Byte[] imageData = null;
				stream.ReadSegments(
					delegate(nsIInputStream inStream, IntPtr closure, IntPtr segment, UInt32 offset, UInt32 count, ref UInt32 writeCount)
					{
						var buffer = new Byte[count];
						Marshal.Copy(segment, buffer, 0, buffer.Length);
						writeCount = count;

						Array.Resize(ref imageData, (imageData != null ? imageData.Length : 0) + buffer.Length);
						Array.Copy(buffer, 0, imageData, imageData.Length - buffer.Length, buffer.Length);
						return nsResult.NS_OK;
					},
					IntPtr.Zero, UInt32.MaxValue);

				return imageData;
			}
			finally
			{
				stream.Close();
				Marshal.ReleaseComObject(stream);
				Marshal.ReleaseComObject(channel);
				Marshal.ReleaseComObject(ioService);
			}

#else

			const String dataPrefix = "data:image/png;base64,";
			if (!data.StartsWith(dataPrefix))
			{
				return null;
			}

			String base64Data = data.Substring(dataPrefix.Length);
			Byte[] imageData = Convert.FromBase64String(base64Data);
			return imageData;

#endif
		}
	}
}
