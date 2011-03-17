using System.Runtime.InteropServices;
using DOMTimeStamp = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Interface that represents a listener to be passed to
	 * mozRequestAnimationFrame
	 */
	[ComImport, Guid("ba240e38-c15a-4fb2-802a-8a48f09331bd"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIAnimationFrameListener //: nsISupports
	{
		/**
		 * The timestamp is the same as it would be for the a corresponding
		 * MozBeforePaint event.
		 */
		void OnBeforePaint(DOMTimeStamp timeStamp);
	}
}
