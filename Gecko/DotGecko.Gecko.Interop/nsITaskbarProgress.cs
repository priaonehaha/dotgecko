using System;
using System.Runtime.InteropServices;
using nsTaskbarProgressState = System.Int32;

namespace DotGecko.Gecko.Interop
{
	public static class nsITaskbarProgressConstants
	{
		/**
		 * Stop displaying progress on the taskbar button. This should be used when
		 * the operation is complete or cancelled.
		 */
		public const nsTaskbarProgressState STATE_NO_PROGRESS = 0;

		/**
		 * Display a cycling, indeterminate progress bar.
		 */
		public const nsTaskbarProgressState STATE_INDETERMINATE = 1;

		/**
		 * Display a determinate, normal progress bar.
		 */
		public const nsTaskbarProgressState STATE_NORMAL = 2;

		/**
		 * Display a determinate, error progress bar.
		 */
		public const nsTaskbarProgressState STATE_ERROR = 3;

		/**
		 * Display a determinate progress bar indicating that the operation has
		 * paused.
		 */
		public const nsTaskbarProgressState STATE_PAUSED = 4;
	}

	/**
	 * Starting in Windows 7, applications can display a progress notification in
	 * the taskbar. This class wraps around the native functionality to do this.
	 */
	[ComImport, Guid("23ac257d-ef3c-4033-b424-be7fef91a86c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsITaskbarProgress //: nsISupports
	{
		/**
		 * Sets the taskbar progress state and value for this window. The currentValue
		 * and maxValue parameters are optional and should be supplied when |state|
		 * is one of STATE_NORMAL, STATE_ERROR or STATE_PAUSED.
		 *
		 * @throws NS_ERROR_INVALID_ARG if state is STATE_NO_PROGRESS or
		 *         STATE_INDETERMINATE, and either currentValue or maxValue is not 0.
		 * @throws NS_ERROR_ILLEGAL_VALUE if currentValue is greater than maxValue.
		 */
		void SetProgressState(nsTaskbarProgressState state, [Optional] UInt64 currentValue, [Optional] UInt64 maxValue);
	}
}
