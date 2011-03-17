using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Represents an object that can be stored in a hashtable.
	 */
	[ComImport, Guid("17e595fa-b57a-4933-bd0f-b1812e8ab188"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIHashable //: nsISupports
	{
		/**
		 * Is this object the equivalent of the other object?
		 */
		Boolean Equals(nsIHashable aOther);

		/**
		 * A generated hashcode for this object. Objects that are equivalent
		 * must have the same hash code. Getting this property should never
		 * throw an exception!
		 */
		UInt32 HashCode { get; }
	}
}
