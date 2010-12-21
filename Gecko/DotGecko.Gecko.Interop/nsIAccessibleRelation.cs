using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIAccessibleRelationConstants
	{
		public const UInt32 RELATION_NUL = 0x00;

		/**
		 * Some attribute of this object is affected by a target object.
		 */
		public const UInt32 RELATION_CONTROLLED_BY = 0x01;

		// First relation
		public const UInt32 RELATION_FIRST = RELATION_CONTROLLED_BY;

		/**
		 * This object is interactive and controls some attribute of a target object.
		 */
		public const UInt32 RELATION_CONTROLLER_FOR = 0x02;

		/**
		 * This object is label for a target object.
		 */
		public const UInt32 RELATION_LABEL_FOR = 0x03;

		/**
		 * This object is labelled by a target object.
		 */
		public const UInt32 RELATION_LABELLED_BY = 0x04;

		/**
		 * This object is a member of a group of one or more objects. When there is
		 * more than one object in the group each member may have one and the same
		 * target, e.g. a grouping object.  It is also possible that each member has
		 * multiple additional targets, e.g. one for every other member in the group.
		 */
		public const UInt32 RELATION_MEMBER_OF = 0x05;

		/**
		 * This object is a child of a target object.
		 */
		public const UInt32 RELATION_NODE_CHILD_OF = 0x06;

		/**
		 * Content flows from this object to a target object, i.e. has content that
		 * flows logically to another object in a sequential way, e.g. text flow.
		 */
		public const UInt32 RELATION_FLOWS_TO = 0x07;

		/**
		 * Content flows to this object from a target object, i.e. has content that
		 * flows logically from another object in a sequential way, e.g. text flow.
		 */
		public const UInt32 RELATION_FLOWS_FROM = 0x08;

		/**
		 * This object is a sub window of a target object.
		 */
		public const UInt32 RELATION_SUBWINDOW_OF = 0x09;

		/**
		 * This object embeds a target object. This relation can be used on the
		 * OBJID_CLIENT accessible for a top level window to show where the content
		 * areas are.
		 */
		public const UInt32 RELATION_EMBEDS = 0x0a;

		/**
		 * This object is embedded by a target object.
		 */
		public const UInt32 RELATION_EMBEDDED_BY = 0x0b;

		/**
		 * This object is a transient component related to the target object. When
		 * this object is activated the target object doesn't lose focus.
		 */
		public const UInt32 RELATION_POPUP_FOR = 0x0c;

		/**
		 * This object is a parent window of the target object.
		 */
		public const UInt32 RELATION_PARENT_WINDOW_OF = 0x0d;

		/**
		 * This object is described by the target object.
		 */
		public const UInt32 RELATION_DESCRIBED_BY = 0x0e;

		/**
		 * This object is describes the target object.
		 */
		public const UInt32 RELATION_DESCRIPTION_FOR = 0x0f;

		// Last relation that is standard to desktop accessibility APIs
		public const UInt32 RELATION_LAST = RELATION_DESCRIPTION_FOR;

		/**
		 * Part of a form/dialog with a related default button. It is used for
		 * MSAA only, no for IA2 nor ATK.
		 */
		public const UInt32 RELATION_DEFAULT_BUTTON = 0x4000;
	}

	/**
	 * This interface gives access to an accessible's set of relations.
	 * Be carefull, do not change constants until ATK has a structure to map gecko
	 * constants into ATK constants.
	 */
	[ComImport, Guid("f42a1589-70ab-4704-877f-4a9162bbe188"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIAccessibleRelation //: nsISupports
	{
		/**
		 * Returns the type of the relation.
		 */
		UInt32 RelationType { get; }

		/**
		 * Returns the number of targets for this relation.
		 */
		UInt32 TargetsCount { get; }

		/**
		 * Returns one accessible relation target.
		 * @param index - 0 based index of relation target.
		 */
		nsIAccessible GetTarget(UInt32 index);

		/**
		 * Returns multiple accessible relation targets.
		 */
		nsIArray GetTargets();
	}
}
