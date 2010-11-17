using System;
using System.Runtime.InteropServices;
using AString = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	internal static class nsIFilePickerConstants
	{
		internal const Int16 modeOpen = 0;              // Load a file or directory
		internal const Int16 modeSave = 1;              // Save a file or directory
		internal const Int16 modeGetFolder = 2;         // Select a folder/directory
		internal const Int16 modeOpenMultiple = 3;      // Load multiple files

		internal const Int16 returnOK = 0;              // User hit Ok, process selection
		internal const Int16 returnCancel = 1;          // User hit cancel, ignore selection
		internal const Int16 returnReplace = 2;         // User acknowledged file already exists so ok to replace, process selection

		internal const Int32 filterAll = 0x01;          // *.*
		internal const Int32 filterHTML = 0x02;         // *.html; *.htm
		internal const Int32 filterText = 0x04;         // *.txt
		internal const Int32 filterImages = 0x08;       // *.png; *.gif; *.jpg; *.jpeg
		internal const Int32 filterXML = 0x10;          // *.xml
		internal const Int32 filterXUL = 0x20;          // *.xul
		internal const Int32 filterApps = 0x40;         // Applications (per-platform implementation)
		internal const Int32 filterAllowURLs = 0x80;    // Allow URLs
	}

	[ComImport, Guid("d24ef0aa-d555-4117-84af-9cbbb7406909"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIFilePicker //: nsISupports
	{
		/**
		 * Initialize the file picker widget.  The file picker is not valid until this
		 * method is called.
		 *
		 * @param      parent   nsIDOMWindow parent.  This dialog will be dependent
		 *                      on this parent. parent must be non-null.
		 * @param      title    The title for the file widget
		 * @param      mode     load, save, or get folder
		 *
		 */
		void Init(nsIDOMWindow parent, [In] AString title, Int16 mode);

		/**
		 * Append to the  filter list with things from the predefined list
		 *
		 * @param      filters  mask of filters i.e. (filterAll | filterHTML)
		 *
		 */
		void AppendFilters(Int32 filterMask);

		/**
		 * Add a filter
		 *
		 * @param      title    name of the filter
		 * @param      filter   extensions to filter -- semicolon and space separated
		 *
		 */
		void AppendFilter([In] AString title, [In] AString filter);

		/**
		 * The filename that should be suggested to the user as a default.
		 *
		 * @throws NS_ERROR_FAILURE on attempts to get
		 */
		void GetDefaultString(AString retval);
		void SetDefaultString(AString value);

		/**
		 * The extension that should be associated with files of the type we
		 * want to work with.  On some platforms, this extension will be
		 * automatically appended to filenames the user enters, if needed.  
		 */
		void GetDefaultExtension(AString retval);
		void SetDefaultExtension(AString value);

		/**
		 * The filter which is currently selected in the File Picker dialog
		 *
		 * @return Returns the index (0 based) of the selected filter in the filter list. 
		 */
		Int32 FilterIndex { get; set; }

		/**
		 * Set the directory that the file open/save dialog initially displays
		 *
		 * @param      displayDirectory  the name of the directory
		 *
		 */
		nsILocalFile DisplayDirectory { get; set; }

		/**
		 * Get the nsILocalFile for the file or directory.
		 *
		 * @return Returns the file currently selected
		 */
		nsILocalFile File { get; }

		/**
		 * Get the nsIURI for the file or directory.
		 *
		 * @return Returns the file currently selected
		 */
		nsIURI FileURL { get; }

		/**
		 * Get the enumerator for the selected files
		 * only works in the modeOpenMultiple mode
		 *
		 * @return Returns the files currently selected
		 */
		nsISimpleEnumerator Files { get; }

		/**
		 * Show File Dialog. The dialog is displayed modally.
		 *
		 * @return returnOK if the user selects OK, returnCancel if the user selects cancel
		 *
		 */
		Int16 Show();
	}
}
