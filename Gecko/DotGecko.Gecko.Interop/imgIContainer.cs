using System;
using System.Runtime.InteropServices;
using gfxImageSurface = System.IntPtr;
using gfxASurface = System.IntPtr;
using gfxContext = System.IntPtr;
using nsIFrame = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	public enum gfxGraphicsFilter
	{
		FILTER_FAST,
		FILTER_GOOD,
		FILTER_BEST,
		FILTER_NEAREST,
		FILTER_BILINEAR,
		FILTER_GAUSSIAN
	}

	public static class imgIContainerConstants
	{
		/**
		  * Enumerated values for the 'type' attribute (below).
		  */
		public const UInt16 TYPE_RASTER = 0;
		public const UInt16 TYPE_VECTOR = 1;

		/**
		 * Flags for imgIContainer operations.
		 *
		 * Meanings:
		 *
		 * FLAG_NONE: Lack of flags
		 *
		 * FLAG_SYNC_DECODE: Forces synchronous/non-progressive decode of all
		 * available data before the call returns. It is an error to pass this flag
		 * from a call stack that originates in a decoder (ie, from a decoder
		 * observer event).
		 *
		 * FLAG_DECODE_NO_PREMULTIPLY_ALPHA: Do not premultiply alpha if
		 * it's not already premultiplied in the image data.
		 *
		 * FLAG_DECODE_NO_COLORSPACE_CONVERSION: Do not do any colorspace conversion;
		 * ignore any embedded profiles, and don't convert to any particular destination
		 * space.
		 */

		public const Int32 FLAG_NONE = 0x0;
		public const Int32 FLAG_SYNC_DECODE = 0x1;
		public const Int32 FLAG_DECODE_NO_PREMULTIPLY_ALPHA = 0x2;
		public const Int32 FLAG_DECODE_NO_COLORSPACE_CONVERSION = 0x4;

		/**
		  * Constants for specifying various "special" frames.
		  *
		  * FRAME_FIRST: The first frame
		  * FRAME_CURRENT: The current frame
		  *
		  * FRAME_MAX_VALUE should be set to the value of the maximum constant above,
		  * as it is used for ensuring that a valid value was passed in.
		  */
		public const UInt32 FRAME_FIRST = 0;
		public const UInt32 FRAME_CURRENT = 1;
		public const UInt32 FRAME_MAX_VALUE = 1;

		/**
		 * Animation mode Constants
		 *   0 = normal
		 *   1 = don't animate
		 *   2 = loop once
		 */
		public const Int16 kNormalAnimMode = 0;
		public const Int16 kDontAnimMode = 1;
		public const Int16 kLoopOnceAnimMode = 2;
	}

	/**
	 * imgIContainer is the interface that represents an image. It allows
	 * access to frames as Thebes surfaces, and permits users to extract subregions
	 * as other imgIContainers. It also allows drawing of images on to Thebes
	 * contexts.
	 *
	 * Internally, imgIContainer also manages animation of images.
	 */
	[ComImport, Guid("239dfa70-2285-4d63-99cd-e9b7ff9555c7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface imgIContainer //: nsISupports
	{
		/**
		 * The width of the container rectangle.  In the case of any error,
		 * zero is returned, and an exception will be thrown.
		 */
		Int32 Width { get; }

		/**
		 * The height of the container rectangle.  In the case of any error,
		 * zero is returned, and an exception will be thrown.
		 */
		Int32 Height { get; }

		/**
		 * The type of this image (one of the TYPE_* values above).
		 */
		UInt16 Type { get; }

		/**
		 * Direct C++ accessor for 'type' attribute, for convenience.
		 */
		UInt16 GetType();

		/**
		 * Whether this image is animated. You can only be guaranteed that querying
		 * this will not throw if STATUS_DECODE_COMPLETE is set on the imgIRequest.
		 *
		 * @throws NS_ERROR_NOT_AVAILABLE if the animated state cannot be determined.
		 */
		Boolean Animated { get; }

		/**
		 * Whether the current frame is opaque; that is, needs the background painted
		 * behind it.
		 */
		Boolean CurrentFrameIsOpaque { get; }

		/**
		 * Get a surface for the given frame. This may be a platform-native,
		 * optimized surface, so you cannot inspect its pixel data.
		 *
		 * @param aWhichFrame Frame specifier of the FRAME_* variety.
		 * @param aFlags Flags of the FLAG_* variety
		 */
		gfxASurface GetFrame(UInt32 aWhichFrame, UInt32 aFlags);

		/**
		 * Create and return a new copy of the given frame that you can write to
		 * and otherwise inspect the pixels of.
		 *
		 * @param aWhichFrame Frame specifier of the FRAME_* variety.
		 * @param aFlags Flags of the FLAG_* variety
		 */
		gfxImageSurface CopyFrame(UInt32 aWhichFrame, UInt32 aFlags);

		/**
		 * Create a new imgContainer that contains only a single frame, which itself
		 * contains a subregion of the given frame.
		 *
		 * @param aWhichFrame Frame specifier of the FRAME_* variety.
		 * @param aRect the area of the current frame to be duplicated in the
		 *              returned imgContainer's frame.
		 * @param aFlags Flags of the FLAG_* variety
		 */
		imgIContainer ExtractFrame(UInt32 aWhichFrame, [In] ref nsIntRect aRect, UInt32 aFlags);

		/**
		 * Draw the current frame on to the context specified.
		 *
		 * @param aContext The Thebes context to draw the image to.
		 * @param aFilter The filter to be used if we're scaling the image.
		 * @param aUserSpaceToImageSpace The transformation from user space (e.g.,
		 *                               appunits) to image space.
		 * @param aFill The area in the context to draw pixels to. Image will be
		 *              automatically tiled as necessary.
		 * @param aSubimage The area of the image, in pixels, that we are allowed to
		 *                  sample from.
		 * @param aViewportSize
		 *          The size (in CSS pixels) of the viewport that would be available
		 *          for the full image to occupy, if we were drawing the full image.
		 *          (Note that we might not actually be drawing the full image -- we
		 *          might be restricted by aSubimage -- but we still need the full
		 *          image's viewport-size in order for SVG images with the "viewBox"
		 *          attribute to position their content correctly.)
		 * @param aFlags Flags of the FLAG_* variety
		 */
		void Draw(gfxContext aContext,
				  gfxGraphicsFilter aFilter,
				  [In] ref gfxMatrix aUserSpaceToImageSpace,
				  [In] ref gfxRect aFill,
				  [In] ref nsIntRect aSubimage,
				  [In] ref nsIntSize aViewportSize,
				  UInt32 aFlags);

		/**
		 * If this image is TYPE_VECTOR, i.e. is really an embedded SVG document,
		 * this method returns a pointer to the root nsIFrame of that document. If
		 * not (or if the root nsIFrame isn't available for some reason), this method
		 * returns nsnull.
		 *
		 * "notxpcom" for convenience, since we have no need for nsresult return-val.
		 */
		nsIFrame GetRootLayoutFrame();

		/*
		 * Ensures that an image is decoding. Calling this function guarantees that
		 * the image will at some point fire off decode notifications. Calling draw(),
		 * getFrame(), copyFrame(), or extractCurrentFrame() triggers the same
		 * mechanism internally. Thus, if you want to be sure that the image will be
		 * decoded but don't want to access it until then, you must call
		 * requestDecode().
		 */
		void RequestDecode();

		/**
		  * Increments the lock count on the image. An image will not be discarded
		  * as Int32 as the lock count is nonzero. Note that it is still possible for
		  * the image to be undecoded if decode-on-draw is enabled and the image
		  * was never drawn.
		  *
		  * Upon instantiation images have a lock count of zero.
		  */
		void LockImage();

		/**
		  * Decreases the lock count on the image. If the lock count drops to zero,
		  * the image is allowed to discard its frame data to save memory.
		  *
		  * Upon instantiation images have a lock count of zero. It is an error to
		  * call this method without first having made a matching lockImage() call.
		  * In other words, the lock count is not allowed to be negative.
		  */
		void UnlockImage();

		UInt16 AnimationMode { get; set; }

		/* Methods to control animation */
		void ResetAnimation();
	}
}
