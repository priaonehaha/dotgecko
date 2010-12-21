using System;
using System.Runtime.InteropServices;
using nsIRegion = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("4d179656-a5bd-42a6-a937-c81f820dcf2f"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIScriptableRegion //: nsISupports
	{
		void Init();

		/**
		* copy operator equivalent that takes another region
		*
		* @param      region to copy
		* @return     void
		*
		**/
		void SetToRegion(nsIScriptableRegion aRegion);

		/**
		* copy operator equivalent that takes a rect
		*
		* @param      aX xoffset of rect to set region to
		* @param      aY yoffset of rect to set region to
		* @param      aWidth width of rect to set region to
		* @param      aHeight height of rect to set region to
		* @return     void
		*
		**/
		void SetToRect(Int32 aX, Int32 aY, Int32 aWidth, Int32 aHeight);

		/**
		* destructively intersect another region with this one
		*
		* @param      region to intersect
		* @return     void
		*
		**/
		void IntersectRegion(nsIScriptableRegion aRegion);

		/**
		* destructively intersect a rect with this region
		*
		* @param      aX xoffset of rect to intersect with region
		* @param      aY yoffset of rect to intersect with region
		* @param      aWidth width of rect to intersect with region
		* @param      aHeight height of rect to intersect with region
		* @return     void
		*
		**/
		void IntersectRect(Int32 aX, Int32 aY, Int32 aWidth, Int32 aHeight);

		/**
		* destructively union another region with this one
		*
		* @param      region to union
		* @return     void
		*
		**/
		void UnionRegion(nsIScriptableRegion aRegion);

		/**
		* destructively union a rect with this region
		*
		* @param      aX xoffset of rect to union with region
		* @param      aY yoffset of rect to union with region
		* @param      aWidth width of rect to union with region
		* @param      aHeight height of rect to union with region
		* @return     void
		*
		**/
		void UnionRect(Int32 aX, Int32 aY, Int32 aWidth, Int32 aHeight);

		/**
		* destructively subtract another region with this one
		*
		* @param      region to subtract
		* @return     void
		*
		**/
		void SubtractRegion(nsIScriptableRegion aRegion);

		/**
		* destructively subtract a rect from this region
		*
		* @param      aX xoffset of rect to subtract with region
		* @param      aY yoffset of rect to subtract with region
		* @param      aWidth width of rect to subtract with region
		* @param      aHeight height of rect to subtract with region
		* @return     void
		*
		**/
		void SubtractRect(Int32 aX, Int32 aY, Int32 aWidth, Int32 aHeight);

		/**
		* is this region empty? i.e. does it contain any pixels
		*
		* @param      none
		* @return     returns whether the region is empty
		*
		**/
		Boolean IsEmpty();

		/**
		* == operator equivalent i.e. do the regions contain exactly
		* the same pixels
		*
		* @param      region to compare
		* @return     whether the regions are identical
		*
		**/
		Boolean IsEqualRegion(nsIScriptableRegion aRegion);

		/**
		* returns the bounding box of the region i.e. the smallest
		* rectangle that completely contains the region.        
		*
		* @param      aX out parameter for xoffset of bounding rect for region
		* @param      aY out parameter for yoffset of bounding rect for region
		* @param      aWidth out parameter for width of bounding rect for region
		* @param      aHeight out parameter for height of bounding rect for region
		* @return     void
		*
		**/
		void GetBoundingBox(out Int32 aX, out Int32 aY, out Int32 aWidth, out Int32 aHeight);

		/**
		* offsets the region in x and y
		*
		* @param  xoffset  pixel offset in x
		* @param  yoffset  pixel offset in y
		* @return          void
		*
		**/
		void Offset(Int32 aXOffset, Int32 aYOffset);

		/**
		 * @return null if there are no rects,
		 * @return flat array of rects,ie [x1,y1,width1,height1,x2...].
		 * The result will contain bogus data if values don't fit in 31 bit
		 **/
		void GetRects();

		/**
		* does the region intersect the rectangle?
		*
		* @param      rect to check for containment
		* @return     true if the region intersects the rect
		*
		**/
		Boolean ContainsRect(Int32 aX, Int32 aY, Int32 aWidth, Int32 aHeight);

		nsIRegion Region { get; }
	}
}
