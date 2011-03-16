using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("34042440-60A8-4992-AE5C-798E69148955"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMDataTransfer //: nsISupports
	{
		/**
		 * The actual effect that will be used, and should always be one of the
		 * possible values of effectAllowed.
		 *
		 * For dragstart, drag and dragleave events, the dropEffect is initialized
		 * to none. Any value assigned to the dropEffect will be set, but the value
		 * isn't used for anything.
		 *
		 * For the dragenter and dragover events, the dropEffect will be initialized
		 * based on what action the user is requesting. How this is determined is
		 * platform specific, but typically the user can press modifier keys to
		 * adjust which action is desired. Within an event handler for the dragenter
		 * and dragover events, the dropEffect should be modified if the action the
		 * user is requesting is not the one that is desired.
		 *
		 * For the drop and dragend events, the dropEffect will be initialized to
		 * the action that was desired, which will be the value that the dropEffect
		 * had after the last dragenter or dragover event.
		 *
		 * Possible values:
		 *  copy - a copy of the source item is made at the new location
		 *  move - an item is moved to a new location
		 *  link - a link is established to the source at the new location
		 *  none - the item may not be dropped
		 *
		 * Assigning any other value has no effect and retains the old value.
		 */
		void GetDropEffect([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetDropEffect([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		/*
		 * Specifies the effects that are allowed for this drag. You may set this in
		 * the dragstart event to set the desired effects for the source, and within
		 * the dragenter and dragover events to set the desired effects for the
		 * target. The value is not used for other events.
		 *
		 * Possible values:
		 *  copy - a copy of the source item is made at the new location
		 *  move - an item is moved to a new location
		 *  link - a link is established to the source at the new location
		 *  copyLink, copyMove, linkMove, all - combinations of the above
		 *  none - the item may not be dropped
		 *  uninitialized - the default value when the effect has not been set,
		 *                  equivalent to all.
		 *
		 * Assigning any other value has no effect and retains the old value.
		 */
		void GetEffectAllowed([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetEffectAllowed([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		/**
		 * Holds a list of all the local files available on this data transfer.
		 * A dataTransfer containing no files will return an empty list, and an
		 * invalid index access on the resulting file list will return null. 
		 */
		nsIDOMFileList Files { get; }

		/**
		 * Holds a list of the format types of the data that is stored for the first
		 * item, in the same order the data was added. An empty list will be
		 * returned if no data was added.
		 */
		nsIDOMDOMStringList Types { get; }

		/**
		 * Remove the data associated with a given format. If format is empty or not
		 * specified, the data associated with all formats is removed. If data for
		 * the specified format does not exist, or the data transfer contains no
		 * data, this method will have no effect.
		 */
		void ClearData([Optional] [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String format);

		/**
		 * Set the data for a given format. If data for the format does not exist,
		 * it is added at the end, such that the last item in the types list will be
		 * the new format. If data for the format already exists, the existing data
		 * is replaced in the same position. That is, the order of the types list is
		 * not changed.
		 *
		 * @throws NS_ERROR_NULL_POINTER if the data is null
		 */
		void SetData([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String format, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String data);

		/**
		 * Retrieves the data for a given format, or an empty string if data for
		 * that format does not exist or the data transfer contains no data.
		 */
		void GetData([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String format,
					 [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);

		/**
		 * Set the image to be used for dragging if a custom one is desired. Most of
		 * the time, this would not be set, as a default image is created from the
		 * node that was dragged.
		 *
		 * If the node is an HTML img element, an HTML canvas element or a XUL image
		 * element, the image data is used. Otherwise, image should be a visible
		 * node and the drag image will be created from this. If image is null, any
		 * custom drag image is cleared and the default is used instead.
		 *
		 * The coordinates specify the offset into the image where the mouse cursor
		 * should be. To center the image for instance, use values that are half the
		 * width and height.
		 *
		 * @param image a node to use 
		 * @param x the horizontal offset
		 * @param y the vertical offset
		 * @throws NO_MODIFICATION_ALLOWED_ERR if the item cannot be modified
		 */
		void SetDragImage(nsIDOMElement image, Int32 x, Int32 y);

		/*
		 * Set the drag source. Usually you would not change this, but it will
		 * affect which node the drag and dragend events are fired at. The
		 * default target is the node that was dragged.
		 *
		 * @param element drag source to use
		 * @throws NO_MODIFICATION_ALLOWED_ERR if the item cannot be modified
		 */
		void AddElement(nsIDOMElement element);
	}

	[ComImport, Guid("AE6DF4E2-FA37-4701-A33E-A5678F826EED"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMNSDataTransfer //: nsISupports
	{
		/*
		 * Integer version of dropEffect, set to one of the constants in nsIDragService.
		 */
		UInt32 DropEffectInt { get; set; }

		/*
		 * Integer version of effectAllowed, set to one or a combination of the
		 * constants in nsIDragService.
		 */
		UInt32 EffectAllowedInt { get; set; }

		/**
		 * Creates a copy of the data transfer object, for the given event type and
		 * user cancelled flag.
		 */
		nsIDOMDataTransfer Clone(UInt32 aEventType, Boolean aUserCancelled);

		/**
		 * The number of items being dragged.
		 */
		UInt32 MozItemCount { get; }

		/**
		 * Sets the drag cursor state. Primarily used to control the cursor during
		 * tab drags, but could be expanded to other uses. XXX Currently implemented
		 * on Win32 only.
		 *
		 * Possible values:
		 *  auto - use default system behavior.
		 *  default - set the cursor to an arrow during the drag operation.
		 *
		 * Values other than 'default' are indentical to setting mozCursor to
		 * 'auto'.
		 */
		void GetMozCursor([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void SetMozCursor([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String value);

		/**
		 * Holds a list of the format types of the data that is stored for an item
		 * at the specified index. If the index is not in the range from 0 to
		 * itemCount - 1, an empty string list is returned.
		 */
		nsIDOMDOMStringList MozTypesAt(UInt32 index);

		/**
		 * Remove the data associated with the given format for an item at the
		 * specified index. The index is in the range from zero to itemCount - 1.
		 *
		 * If the last format for the item is removed, the entire item is removed,
		 * reducing the itemCount by one.
		 *
		 * If format is empty, then the data associated with all formats is removed.
		 * If the format is not found, then this method has no effect.
		 *
		 * @param format the format to remove
		 * @throws NS_ERROR_DOM_INDEX_SIZE_ERR if index is greater or equal than itemCount
		 * @throws NO_MODIFICATION_ALLOWED_ERR if the item cannot be modified
		 */
		void MozClearDataAt([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String format, UInt32 index);

		/*
		 * A data transfer may store multiple items, each at a given zero-based
		 * index. setDataAt may only be called with an index argument less than
		 * itemCount in which case an existing item is modified, or equal to
		 * itemCount in which case a new item is added, and the itemCount is
		 * incremented by one.
		 *
		 * Data should be added in order of preference, with the most specific
		 * format added first and the least specific format added last. If data of
		 * the given format already exists, it is replaced in the same position as
		 * the old data.
		 *
		 * The data should be either a string, a primitive boolean or number type
		 * (which will be converted into a string) or an nsISupports.
		 *
		 * @param format the format to add
		 * @param data the data to add
		 * @throws NS_ERROR_NULL_POINTER if the data is null
		 * @throws NS_ERROR_DOM_INDEX_SIZE_ERR if index is greater than itemCount
		 * @throws NO_MODIFICATION_ALLOWED_ERR if the item cannot be modified
		 */
		void MozSetDataAt([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String format, nsIVariant data, UInt32 index);

		/**
		 * Retrieve the data associated with the given format for an item at the
		 * specified index, or null if it does not exist. The index should be in the
		 * range from zero to itemCount - 1.
		 *
		 * @param format the format of the data to look up
		 * @returns the data of the given format, or null if it doesn't exist.
		 * @throws NS_ERROR_DOM_INDEX_SIZE_ERR if index is greater or equal than itemCount
		 */
		nsIVariant MozGetDataAt([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String format, UInt32 index);

		/**
		 * Will be true when the user has cancelled the drag (typically by pressing
		 * Escape) and when the drag has been cancelled unexpectedly.  This will be
		 * false otherwise, including when the drop has been rejected by its target.
		 * This property is only relevant for the dragend event.
		 */
		Boolean MozUserCancelled { get; }

		/**
		 * The node that the mouse was pressed over to begin the drag. For external
		 * drags, or if the caller cannot access this node, this will be null.
		 */
		nsIDOMNode MozSourceNode { get; }
	}
}
