using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * An interface to individual entries in session history. Each 
	 * document or frame will have a nsIHistoryEntry associated with 
	 * it. nsIHistoryEntry provides access to information like URI, 
	 * title and frame traversal status for that document.
	 * This interface is accessible from javascript.
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("A41661D4-1417-11D5-9882-00C04FA02F40"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIHistoryEntry //: nsISupports 
	{
		/** 
		 * A readonly property that returns the URI
		 * of the current entry. The object returned is
		 * of type nsIURI
		 */
		nsIURI URI { get; }

		/** 
		 * A readonly property that returns the title
		 * of the current entry.  The object returned
		 * is a encoded string
		 */
		String Title { [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		/** 
		 * A readonly property that returns a boolean
		 * flag which indicates if the entry was created as a 
		 * result of a subframe navigation. This flag will be
		 * 'false' when a frameset page is visited for
		 * the first time. This flag will be 'true' for all
		 * history entries created as a result of a subframe
		 * navigation.
		 */
		Boolean IsSubFrame { get; }
	}
}
