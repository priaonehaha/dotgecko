using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIPrefLocalizedString interface is simply a wrapper interface for
	 * nsISupportsString so the preferences service can have a unique identifier
	 * to distinguish between requests for normal wide strings (nsISupportsString)
	 * and "localized" wide strings, which get their default values from properites
	 * files.
	 *
	 * @see nsIPrefBranch
	 * @see nsISupportsString
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("ae419e24-1dd1-11b2-b39a-d3e5e7073802"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIPrefLocalizedString //: nsISupports
	{
		/**
		 * Provides access to string data stored in this property.
		 *
		 * @return NS_OK The operation succeeded.
		 * @return Other An error occured.
		 */
		String Data { [return: MarshalAs(UnmanagedType.LPWStr)]get; [param: MarshalAs(UnmanagedType.LPWStr)]set; }

		/**
		 * Used to retrieve the contents of this object into a wide string.
		 *
		 * @return wstring The string containing the data stored within this object.
		 */
		[return: MarshalAs(UnmanagedType.LPWStr)]
		String ToString();

		/**
		 * Used to set the contents of this object.
		 *
		 * @param length The length of the string. This value should not include
		 *               space for the null terminator, nor should it account for the
		 *               size of a character. It should  only be the number of
		 *               characters for which there is space in the string.
		 * @param data   The string data to be stored.
		 *
		 * @note
		 * This makes a copy of the string argument passed in.
		 *
		 * @return NS_OK The data was successfully stored.
		 */
		void SetDataWithLength(UInt32 length, [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 0)] String data);
	}
}
