using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("bbb20a59-524e-4662-981e-5e142814b20c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMCanvasGradient //: nsISupports
	{
		void AddColorStop(Single offset, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String color);
	}

	[ComImport, Guid("21dea65c-5c08-4eb1-ac82-81fe95be77b8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMCanvasPattern //: nsISupports
	{
	}

	[ComImport, Guid("2d01715c-ec7d-424a-ab85-e0fd70c8665c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMTextMetrics //: nsISupports
	{
		Single Width { get; }
	}

	internal static class nsIDOMCanvasRenderingContext2DConstants
	{
		// Show the caret if appropriate when drawing
		internal const UInt32 DRAWWINDOW_DRAW_CARET = 0x01;

		// Don't flush pending layout notifications that could otherwise
		// be batched up
		internal const UInt32 DRAWWINDOW_DO_NOT_FLUSH = 0x02;

		// Draw scrollbars and scroll the viewport if they are present
		internal const UInt32 DRAWWINDOW_DRAW_VIEW = 0x04;
	}

	[ComImport, Guid("3e7d5d06-8846-4cff-8739-44756cbf494f"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMCanvasRenderingContext2D //: nsISupports
	{
		// back-reference to the canvas element for which
		// this context was created
		nsIDOMHTMLCanvasElement Canvas { get; }

		// state
		void Save();
		void Restore();

		// transformations
		void Scale(Single x, Single y);
		void Rotate(Single angle);
		void Translate(Single x, Single y);
		void Transform(Single m11, Single m12, Single m21, Single m22, Single dx, Single dy);
		void SetTransform(Single m11, Single m12, Single m21, Single m22, Single dx, Single dy);

		// compositing
		Single GlobalAlpha { get; set; } /* default 1.0 -- opaque */

		void GetGlobalCompositeOperation([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result); /* default "over" */
		void SetGlobalCompositeOperation([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		// colors and styles
		nsIVariant StrokeStyle { get; set; }
		nsIVariant GetFillStyle { get; set; }

		nsIDOMCanvasGradient CreateLinearGradient(Single x0, Single y0, Single x1, Single y1);

		nsIDOMCanvasGradient CreateRadialGradient(Single x0, Single y0, Single r0, Single x1, Single y1, Single r1);

		//nsIDOMCanvasPattern createPattern(in nsIDOMHTMLImageElement image, in DOMString repetition);
		nsIDOMCanvasPattern CreatePattern(nsIDOMHTMLElement image, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String repetition);

		Single LineWidth { get; set; } /* default 1 */

		void GetLineCap([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result); /* "butt", "round", "square" (default) */
		void SetLineCap([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		void GetLineJoin([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result); /* "round", "bevel", "miter" (default) */
		void SetLineJoin([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		Single MiterLimit { get; set; } /* default 10 */

		// shadows
		Single ShadowOffsetX { get; set; }
		Single ShadowOffsetY { get; set; }
		Single ShadowBlur { get; set; }

		void GetShadowColor([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void SetShadowColor([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		// rects
		void ClearRect(Single x, Single y, Single w, Single h);
		void FillRect(Single x, Single y, Single w, Single h);
		void StrokeRect(Single x, Single y, Single w, Single h);

		// path API
		void BeginPath();
		void ClosePath();

		void MoveTo(Single x, Single y);
		void LineTo(Single x, Single y);
		void QuadraticCurveTo(Single cpx, Single cpy, Single x, Single y);
		void BezierCurveTo(Single cp1x, Single cp1y, Single cp2x, Single cp2y, Single x, Single y);
		void ArcTo(Single x1, Single y1, Single x2, Single y2, Single radius);
		void Arc(Single x, Single y, Single r, Single startAngle, Single endAngle, Boolean clockwise);
		void Rect(Single x, Single y, Single w, Single h);

		void Fill();
		void Stroke();
		void Clip();

		// text api
		void GetFont([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result); /* default "10px sans-serif" */
		void SetFont([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		void GetTextAlign([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result); /* "start" (default), "end", "left", "right", "center" */
		void SetTextAlign([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		void GetTextBaseline([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result); /* "alphabetic" (default), "top", "hanging", "middle", "ideographic", "bottom" */
		void SetTextBaseline([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		void FillText([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String text, Single x, Single y, [Optional] Single maxWidth);
		void StrokeText([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String text, Single x, Single y, [Optional] Single maxWidth);
		nsIDOMTextMetrics MeasureText([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String text);

		void GetMozTextStyle([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void SetMozTextStyle([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		void MozDrawText([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String textToDraw);
		Single MozMeasureText([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String textToMeasure);
		void MozPathText([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String textToPath);
		void MozTextAlongPath([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String textToDraw, Boolean stroke);

		// image api
		void DrawImage();
		/*
		  void drawImage(in HTMLImageElement image, Single dx, Single dy);
		  void drawImage(in HTMLImageElement image, Single dx, Single dy, Single sw, Single sh);
		  void drawImage(in HTMLImageElement image, Single sx, Single sy, Single sw, Single sh, Single dx, Single dy, Single dw, Single dh);
		*/

		// point-membership test
		Boolean IsPointInPath(Single x, Single y);

		// pixel manipulation
		// ImageData getImageData (Single x, Single y, Single width, Single height);
		// void putImageData (in ImageData d, Single x, Single y);
		// ImageData = { width: #, height: #, data: [r, g, b, a, ...] }

		void GetImageData();
		void PutImageData();

		// ImageData createImageData(Single w, Single h);
		void CreateImageData();

		// image smoothing mode -- if disabled, images won't be smoothed
		// if scaled.
		Boolean GetMozImageSmoothingEnabled { get; set; }

		/**
		 * Renders a region of a window into the canvas.  The contents of
		 * the window's viewport are rendered, ignoring viewport clipping
		 * and scrolling.
		 *
		 * @param x
		 * @param y
		 * @param w
		 * @param h specify the area of the window to render, in CSS
		 * pixels.
		 *
		 * @param backgroundColor the canvas is filled with this color
		 * before we render the window into it. This color may be
		 * transparent/translucent. It is given as a CSS color string
		 * (e.g., rgb() or rgba()).
		 *
		 * @param flags Uused to better control the drawWindow call.
		 * Flags can be ORed together.
		 *
		 * Of course, the rendering obeys the current scale, transform and
		 * globalAlpha values.
		 *
		 * Hints:
		 * -- If 'rgba(0,0,0,0)' is used for the background color, the
		 * drawing will be transparent wherever the window is transparent.
		 * -- Top-level browsed documents are usually not transparent
		 * because the user's background-color preference is applied,
		 * but IFRAMEs are transparent if the page doesn't set a background.
		 * -- If an opaque color is used for the background color, rendering
		 * will be faster because we won't have to compute the window's
		 * transparency.
		 *
		 * This API cannot currently be used by Web content. It is chrome
		 * only.
		 */
		void DrawWindow(nsIDOMWindow window, Single x, Single y, Single w, Single h, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String bgColor, [Optional] UInt32 flags);
	}
}
