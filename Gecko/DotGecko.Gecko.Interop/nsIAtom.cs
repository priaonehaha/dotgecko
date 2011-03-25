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
	[ComImport, Guid("1f341018-521a-49de-b806-1bef5c9a00b0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIAtom //: nsISupports
	{
		/**
		 * Get the Unicode or UTF8 value for the string
		 */
		void ToString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		void ToUTF8String([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);

		/**
		 * Compare the atom to a specific string value
		 * Note that this will NEVER return/throw an error condition.
		 */
		Boolean Equals([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aString);

		Boolean EqualsUTF8([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aString);

		/**
		 * Returns true if the atom is static and false otherwise.
		 */
		Boolean IsStaticAtom();
	}
}
