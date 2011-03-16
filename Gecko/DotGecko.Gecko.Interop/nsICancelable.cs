using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * This interface provides a means to cancel an operation that is in progress.
	 */
	[ComImport, Guid("d94ac0a0-bb18-46b8-844e-84159064b0bd"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsICancelable //: nsISupports
	{
		/**
		 * Call this method to request that this object abort whatever operation it
		 * may be performing.
		 *
		 * @param aReason
		 *        Pass a failure code to indicate the reason why this operation is
		 *        being canceled.  It is an error to pass a success code.
		 */
		void Cancel([MarshalAs(UnmanagedType.U4)] nsResult aReason);
	}
}
