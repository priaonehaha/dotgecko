using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/*
	 * nsIWinTaskbar
	 *
	 * This interface represents a service which exposes the APIs provided by the
	 * Windows taskbar to applications.
	 *
	 * Starting in Windows 7, applications gain some control over their appearance
	 * in the taskbar. By default, there is one taskbar preview per top level
	 * window (excluding popups). This preview is represented by an
	 * nsITaskbarWindowPreview object.
	 *
	 * An application can register its own "tab" previews. Such previews will hide
	 * the corresponding nsITaskbarWindowPreview automatically (though this is not
	 * reflected in the visible attribute of the nsITaskbarWindowPreview). These
	 * tab previews do not have to correspond to tabs in the application - they can
	 * vary in size, shape and location. They do not even need to be actual GUI
	 * elements on the window. Unlike window previews, tab previews require most of
	 * the functionality of the nsITaskbarPreviewController to be implemented.
	 *
	 * Applications can also show progress on their taskbar icon. This does not
	 * interact with the taskbar previews except if the nsITaskbarWindowPreview is
	 * made invisible in which case the progress is naturally not shown on that
	 * window.
	 *
	 * When taskbar icons are combined as is the default in Windows 7, the progress
	 * for those windows is also combined as defined here:
	 * http://msdn.microsoft.com/en-us/library/dd391697%28VS.85%29.aspx
	 *
	 * Applications may also define custom taskbar jump lists on application shortcuts.
	 * See nsIJumpListBuilder for more information.
	 */
	[ComImport, Guid("a25ad3ed-1ded-4473-bb6e-bf8b89d88949"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWinTaskbar //: nsISupports
	{
		/**
		 * Returns true if the operating system supports Win7+ taskbar features.
		 * This property acts as a replacement for in-place os version checking.
		 */
		Boolean Available { get; }

		/**
		 * Returns the default application user model identity the application
		 * registers with the system. This id is used by the taskbar in grouping
		 * windows and in associating pinned shortcuts with running instances and
		 * jump lists.
		 */
		void GetDefaultGroupId([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		void SetDefaultGroupId([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);

		/**
		 * Taskbar window and tab preview management
		 */

		/**
		 * Creates a taskbar preview. The docshell should be a toplevel docshell and
		 * is used to find the toplevel window. See the documentation for
		 * nsITaskbarTabPreview for more information.
		 */
		nsITaskbarTabPreview CreateTaskbarTabPreview(nsIDocShell shell, nsITaskbarPreviewController controller);

		/**
		 * Gets the taskbar preview for a window. The docshell is used to find the
		 * toplevel window. See the documentation for nsITaskbarTabPreview for more
		 * information.
		 *
		 * Note: to implement custom drawing or buttons, a controller is required.
		 */
		nsITaskbarWindowPreview GetTaskbarWindowPreview(nsIDocShell shell);

		/**
		 * Taskbar icon progress indicator
		 */

		/**
		 * Gets the taskbar progress for a window. The docshell is used to find the
		 * toplevel window. See the documentation for nsITaskbarProgress for more
		 * information.
		 */
		nsITaskbarProgress GetTaskbarProgress(nsIDocShell shell);

		/**
		 * Taskbar and start menu jump list management
		 */

		/**
		 * Retrieve a taskbar jump list builder
		 *
		 * Fails if a jump list build operation has already been initiated, developers
		 * should make use of a single instance of nsIJumpListBuilder for building lists
		 * within an application.
		 *
		 * @thow NS_ERROR_ALREADY_INITIALIZED if an nsIJumpListBuilder instance is
		 * currently building a list.
		 */
		nsIJumpListBuilder CreateJumpListBuilder();

		/**
		 * Application window taskbar group settings
		 */

		/**
		 * Set the grouping id for a window.
		 *
		 * The runtime sets a default, global grouping id for all windows on startup.
		 * setGroupIdForWindow allows individual windows to be grouped independently
		 * on the taskbar. Ids should be unique to the app and window to insure
		 * conflicts with other pinned applications do no arise.
		 *
		 * The default group id is based on application.ini vendor, application, and
		 * version values, with a format of 'vendor.app.version'. The default can be
		 * retrieved via defaultGroupId.
		 *
		 * Note, when a window changes taskbar window stacks, it is placed at the
		 * bottom of the new stack.
		 *
		 * @thow NS_ERROR_INVALID_ARG if the window is not a valid top level window
		 * associated with a widget.
		 * @thow NS_ERROR_FAILURE if the property on the window could not be set.
		 * @thow NS_ERROR_UNEXPECTED for general failures.
		 */
		void SetGroupIdForWindow(nsIDOMWindow aParent, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aIdentifier);
	}
}
