using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/*
	 * Should this really be scriptable?  Using atoms from script or proxies
	 * could be dangerous since double-wrapping could lead to loss of
	 * pointer identity.
	 */
	[ComImport, Guid("3d1b15b0-93b4-11d1-895b-006008911b81"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIAtom //: nsISupports
	{
		/**
		 * Get the Unicode or UTF8 value for the string
		 */
		void ToString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		void ToUTF8String([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);

		/**
		 * Return a pointer to a zero terminated UTF8 string.
		 */
		[return: MarshalAs(UnmanagedType.LPStr)]
		String GetUTF8String();

		/**
		 * Compare the atom to a specific string value
		 * Note that this will NEVER return/throw an error condition.
		 */
		Boolean Equals([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aString);

		Boolean EqualsUTF8([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aString);
	}
}
