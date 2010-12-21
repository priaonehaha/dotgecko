using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/*
	 * Should this really be scriptable?  Using atoms from script or proxies
	 * could be dangerous since double-wrapping could lead to loss of
	 * pointer identity.
	 */
	[ComImport, Guid("9c1f50b9-f9eb-42d4-a8cb-2c7600aeb241"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIAtomService //: nsISupports
	{
		/**
		 * Version of NS_NewAtom that doesn't require linking against the
		 * XPCOM library.  See nsIAtom.idl.
		 */
		nsIAtom GetAtom([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);

		/**
		 * Version of NS_NewPermanentAtom that doesn't require linking against
		 * the XPCOM library.  See nsIAtom.idl.
		 */
		nsIAtom GetPermanentAtom([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);

		/**
		 * Get an atom with a utf8 string.
		 */
		nsIAtom GetAtomUTF8([MarshalAs(UnmanagedType.LPStr)] String value);
		nsIAtom GetPermanentAtomUTF8([MarshalAs(UnmanagedType.LPStr)] String value);
	}
}
