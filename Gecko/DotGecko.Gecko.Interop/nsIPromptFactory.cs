using System;
using System.Runtime.InteropServices;
using nsQIResult = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	/**
	 * This interface allows creating various prompts that have a specific parent.
	 */
	[ComImport, Guid("2532b748-75db-4732-9173-78d3bf34f694"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIPromptFactory //: nsISupports
	{
		/**
		 * Returns an object implementing the specified interface that creates
		 * prompts parented to aParent.
		 */
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		nsResult GetPrompt(nsIDOMWindow aParent, [In] ref Guid iid, /*[iid_is(iid),retval]*/ out nsQIResult result);
	}
}
