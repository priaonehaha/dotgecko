using System;
using System.Runtime.InteropServices;
using nsIWidget = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIBaseWindow describes a generic window and basic operations that 
	 * can be performed on it.  This is not to be a complete windowing interface
	 * but rather a common set that nearly all windowed objects support.    
	 */
	[ComImport, Guid("046BC8A0-8015-11d3-AF70-00A024FFC08C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIBaseWindow //: nsISupports
	{
		/*
		Allows a client to initialize an object implementing this interface with
		the usually required window setup information.
		It is possible to pass null for both parentNativeWindow and parentWidget,
		but only docshells support this.

		@param parentNativeWindow - This allows a system to pass in the parenting
			window as a native reference rather than relying on the calling
			application to have created the parent window as an nsIWidget.  This 
			value will be ignored (should be nsnull) if an nsIWidget is passed in to
			the parentWidget parameter.

		@param parentWidget - This allows a system to pass in the parenting widget.
			This allows some objects to optimize themselves and rely on the view
			system for event flow rather than creating numerous native windows.  If
			one of these is not available, nsnull should be passed.

		@param x - This is the x co-ordinate relative to the parent to place the
			window.

		@param y - This is the y co-ordinate relative to the parent to place the 
			window.

		@param cx - This is the width	for the window to be.

		@param cy - This is the height for the window to be.

		@return	NS_OK - Window Init succeeded without a problem.
					NS_ERROR_UNEXPECTED - Call was unexpected at this time.  Most likely
						due to you calling it after create() has been called.
					NS_ERROR_INVALID_ARG - controls that require either a parentNativeWindow
						or a parentWidget may return invalid arg when they do not 
						receive what they are needing.
		*/
		void InitWindow(IntPtr parentNativeWindow, [In, MarshalAs(UnmanagedType.Interface)] nsIWidget parentWidget, Int32 x, Int32 y, Int32 cx, Int32 cy);

		/*
		Tells the window that intialization and setup is complete.  When this is
		called the window can actually create itself based on the setup
		information handed to it.

		@return	NS_OK - Creation was successfull.
					NS_ERROR_UNEXPECTED - This call was unexpected at this time.
						Perhaps create() had already been called or not all
						required initialization had been done.
		*/
		void Create();

		/*
		Tell the window that it should destroy itself.  This call should not be
		necessary as it will happen implictly when final release occurs on the
		object.  If for some reaons you want the window destroyed prior to release
		due to cycle or ordering issues, then this call provides that ability.

		@return	NS_OK - Everything destroyed properly.
					NS_ERROR_UNEXPECTED - This call was unexpected at this time.
						Perhaps create() has not been called yet.
		*/
		void Destroy();

		/*
		Sets the current x and y coordinates of the control.  This is relative to
		the parent window.
		*/
		void SetPosition(Int32 x, Int32 y);

		/*
		Gets the current x and y coordinates of the control.  This is relatie to the
		parent window.
		*/
		void GetPosition(out Int32 x, out Int32 y);

		/*
		Sets the width and height of the control.
		*/
		void SetSize(Int32 cx, Int32 cy, Boolean fRepaint);

		/*
		Gets the width and height of the control.
		*/
		void GetSize(out Int32 cx, out Int32 cy);

		/*
		Convenience function combining the SetPosition and SetSize into one call.
		Also is more efficient than calling both.
		*/
		void SetPositionAndSize(Int32 x, Int32 y, Int32 cx, Int32 cy, Boolean fRepaint);

		/*
		Convenience function combining the GetPosition and GetSize into one call.
		Also is more efficient than calling both.
		*/
		void GetPositionAndSize(out Int32 x, out Int32 y, out Int32 cx, out Int32 cy);

		/** 
		 * Tell the window to repaint itself
		 * @param aForce - if true, repaint immediately
		 *                 if false, the window may defer repainting as it sees fit.
		 */
		void Repaint(Boolean force);

		/*
		This is the parenting widget for the control.  This may be null if the
		native window was handed in for the parent during initialization.
		If this	is returned, it should refer to the same object as
		parentNativeWindow.

		Setting this after Create() has been called may not be supported by some
		implementations.

		On controls that don't support widgets, setting this will return a 
		NS_ERROR_NOT_IMPLEMENTED error.
		*/
		nsIWidget ParentWidget { [return: MarshalAs(UnmanagedType.Interface)] get; [param: MarshalAs(UnmanagedType.Interface)] set; }

		/*
		This is the native window parent of the control.

		Setting this after Create() has been called may not be supported by some
		implementations.

		On controls that don't support setting nativeWindow parents, setting this
		will return a NS_ERROR_NOT_IMPLEMENTED error.
		*/
		IntPtr ParentNativeWindow { get; set; }

		/*
		Attribute controls the visibility of the object behind this interface.
		Setting this attribute to false will hide the control.  Setting it to 
		true will show it.
		*/
		Boolean Visibility { get; set; }

		/*
		a disabled window should accept no user interaction; it's a dead window,
		like the parent of a modal window.
		*/
		Boolean Enabled { get; set; }

		/** set blurSuppression to true to suppress handling of blur events.
		 *  set it false to re-enable them. query it to determine whether
		 *  blur events are suppressed. The implementation should allow
		 *  for blur events to be suppressed multiple times.
		 */
		Boolean BlurSuppression { get; set; }

		/*
		Allows you to find out what the widget is of a given object.  Depending
		on the object, this may return the parent widget in which this object
		lives if it has not had to create its own widget.
		*/
		nsIWidget MainWidget { [return: MarshalAs(UnmanagedType.Interface)] get; }

		/**
		* Give the window focus.
		*/
		void SetFocus();

		/*
		Title of the window.
		*/
		String Title { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }
	}
}
