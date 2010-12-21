using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/**
	 * A cross-platform interface that supports platform-specific 
	 * accessibility APIs like MSAA and ATK. Contains the sum of what's needed
	 * to support IAccessible as well as ATK's generic accessibility objects.
	 * Can also be used by in-process accessibility clients to get information
	 * about objects in the accessible tree. The accessible tree is a subset of 
	 * nodes in the DOM tree -- such as documents, focusable elements and text.
	 * Mozilla creates the implementations of nsIAccessible on demand.
	 * See http://www.mozilla.org/projects/ui/accessibility for more information.
	 *
	 * @status UNDER_REVIEW
	 */
	[ComImport, Guid("c81d8f8c-8585-4094-bc7c-71dd01494906"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIAccessible //: nsISupports
	{
		/**
		 * Parent node in accessible tree.
		 */
		nsIAccessible Parent { get; }

		/**
		 * Next sibling in accessible tree
		 */
		nsIAccessible NextSibling { get; }

		/**
		 * Previous sibling in accessible tree
		 */
		nsIAccessible PreviousSibling { get; }

		/**
		 * First child in accessible tree
		 */
		nsIAccessible FirstChild { get; }

		/**
		 * Last child in accessible tree
		 */
		nsIAccessible LastChild { get; }

		/**
		 * Array of all this element's children.
		 */
		nsIArray Children { get; }

		/**
		 * Number of accessible children
		 */
		Int32 ChildCount { get; }

		/**
		 * The 0-based index of this accessible in its parent's list of children,
		 * or -1 if this accessible does not have a parent.
		 */
		Int32 IndexInParent { get; }

		/**
		 * Accessible name -- the main text equivalent for this node. The name is
		 * specified by ARIA or by native markup. Example of ARIA markup is
		 * aria-labelledby attribute placed on element of this accessible. Example
		 * of native markup is HTML label linked with HTML element of this accessible.
		 *
		 * Value can be string or null. A null value indicates that AT may attempt to
		 * compute the name. Any string value, including the empty string, should be
		 * considered author-intentional, and respected.
		 */
		void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		void SetName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);

		/**
		 * Accessible value -- a number or a secondary text equivalent for this node
		 * Widgets that use role attribute can force a value using the valuenow attribute
		 */
		void GetValue([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * Accessible description -- long text associated with this node
		 */
		void GetDescription([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * Provides localized string of accesskey name, such as Alt+D.
		 * The modifier may be affected by user and platform preferences.
		 * Usually alt+letter, or just the letter alone for menu items. 
		 */
		void GetKeyboardShortcut([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * Provides localized string of global keyboard accelerator for default
		 * action, such as Ctrl+O for Open file
		 */
		void GetDefaultKeyBinding([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * Provides array of localized string of global keyboard accelerator for
		 * the given action index supported by accessible.
		 *
		 * @param aActionIndex - index of the given action
		 */
		nsIDOMDOMStringList GetKeyBindings(Byte aActionIndex);

		/**
		 * Enumerated accessible role (see the constants defined in nsIAccessibleRole).
		 *
		 * @note  The values might depend on platform because of variations. Widgets
		 *        can use ARIA role attribute to force the final role.
		 */
		UInt32 Role { get; }

		/**
		 * Accessible states -- bit fields which describe boolean properties of node.
		 * Many states are only valid given a certain role attribute that supports
		 * them.
		 *
		 * @param aState - the first bit field (see nsIAccessibleStates::STATE_*
		 *                 constants)
		 * @param aExtraState - the second bit field
		 *                      (see nsIAccessibleStates::EXT_STATE_* constants)
		 */
		void GetState(out UInt32 aState, UInt32 aExtraState);

		/**
		 * Help text associated with node
		 */
		void GetHelp([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * Focused accessible child of node
		 */
		nsIAccessible FocusedChild { get; }

		/**
		 * Attributes of accessible
		 */
		nsIPersistentProperties Attributes { get; }

		/**
		 * Returns grouping information. Used for tree items, list items, tab panel
		 * labels, radio buttons, etc. Also used for collectons of non-text objects.
		 *
		 * @param groupLevel - 1-based, similar to ARIA 'level' property
		 * @param similarItemsInGroup - 1-based, similar to ARIA 'setsize' property,
		 *                              inclusive of the current item
		 * @param positionInGroup - 1-based, similar to ARIA 'posinset' property
		 */
		void GroupPosition(out Int32 aGroupLevel, out Int32 aSimilarItemsInGroup, out Int32 aPositionInGroup);

		/**
		 * Accessible child which contains the coordinate at (x, y) in screen pixels.
		 * If the point is in the current accessible but not in a child, the
		 * current accessible will be returned.
		 * If the point is in neither the current accessible or a child, then
		 * null will be returned.
		 *
		 * @param x  screen's x coordinate
		 * @param y  screen's y coordinate
		 * @return   the deepest accessible child containing the given point
		 */
		nsIAccessible GetChildAtPoint(Int32 x, Int32 y);

		/**
		 * Deepest accessible child which contains the coordinate at (x, y) in screen
		 * pixels. If the point is in the current accessible but not in a child, the
		 * current accessible will be returned. If the point is in neither the current
		 * accessible or a child, then null will be returned.
		 *
		 * @param x  screen's x coordinate
		 * @param y  screen's y coordinate
		 * @return   the deepest accessible child containing the given point
		 */
		nsIAccessible GetDeepestChildAtPoint(Int32 x, Int32 y);

		/**
		 * Nth accessible child using zero-based index or last child if index less than zero
		 */
		nsIAccessible GetChildAt(Int32 aChildIndex);

		/**
		 * Accessible node geometrically to the right of this one
		 */
		nsIAccessible GetAccessibleToRight();

		/**
		 * Accessible node geometrically to the left of this one
		 */
		nsIAccessible GetAccessibleToLeft();

		/**
		 * Accessible node geometrically above this one
		 */
		nsIAccessible GetAccessibleAbove();

		/**
		 * Accessible node geometrically below this one
		 */
		nsIAccessible GetAccessibleBelow();

		/**
		 * Return accessible relation by the given relation type (see.
		 * constants defined in nsIAccessibleRelation).
		 */
		nsIAccessibleRelation GetRelationByType(UInt32 aRelationType);

		/**
		 * Returns the number of accessible relations for this object.
		 */
		UInt32 RelationsCount { get; }

		/**
		 * Returns one accessible relation for this object.
		 *
		 * @param index - relation index (0-based)
		 */
		nsIAccessibleRelation GetRelation(UInt32 index);

		/**
		 * Returns multiple accessible relations for this object.
		 */
		nsIArray GetRelations();

		/**
		 * Return accessible's x and y coordinates relative to the screen and
		 * accessible's width and height.
		 */
		void GetBounds(out Int32 x, out Int32 y, out Int32 width, out Int32 height);

		/**
		 * Add or remove this accessible to the current selection
		 */
		void SetSelected(Boolean isSelected);

		/**
		 * Extend the current selection from its current accessible anchor node
		 * to this accessible
		 */
		void ExtendSelection();

		/**
		 * Select this accessible node only
		 */
		void TakeSelection();

		/**
		 * Focus this accessible node,
		 * The state STATE_FOCUSABLE indicates whether this node is normally focusable.
		 * It is the callers responsibility to determine whether this node is focusable.
		 * accTakeFocus on a node that is not normally focusable (such as a table),
		 * will still set focus on that node, although normally that will not be visually 
		 * indicated in most style sheets.
		 */
		void TakeFocus();

		/**
		 * The number of accessible actions associated with this accessible
		 */
		Byte NumActions { get; }

		/**
		 * The name of the accessible action at the given zero-based index
		 */
		void GetActionName(Byte index, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * The description of the accessible action at the given zero-based index
		 */
		void GetActionDescription(Byte aIndex, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * Perform the accessible action at the given zero-based index
		 * Action number 0 is the default action
		 */
		void DoAction(Byte index);

		/**
		 * Get a pointer to accessibility interface for this node, which is specific 
		 * to the OS/accessibility toolkit we're running on.
		 */
		void GetNativeInterface(out IntPtr aOutAccessible);
	}
}
