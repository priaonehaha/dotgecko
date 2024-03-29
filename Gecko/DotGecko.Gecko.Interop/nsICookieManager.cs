using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/** 
	 * An optional interface for accessing or removing the cookies
	 * that are in the cookie list
	 */
	[ComImport, Guid("AAAB6710-0F2C-11d5-A53B-0010A401EB10"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsICookieManager //: nsISupports
	{
		/**
		 * Called to remove all cookies from the cookie list
		 */
		void RemoveAll();

		/**
		 * Called to enumerate through each cookie in the cookie list.
		 * The objects enumerated over are of type nsICookie
		 */
		nsISimpleEnumerator Enumerator { get; }

		/**
		 * Called to remove an individual cookie from the cookie list, specified
		 * by host, name, and path. If the cookie cannot be found, no exception
		 * is thrown. Typically, the arguments to this method will be obtained
		 * directly from the desired nsICookie object.
		 *
		 * @param aHost The host or domain for which the cookie was set. @see
		 *              nsICookieManager2::add for a description of acceptable host
		 *              strings. If the target cookie is a domain cookie, a leading
		 *              dot must be present.
		 * @param aName The name specified in the cookie
		 * @param aPath The path for which the cookie was set
		 * @param aBlocked Indicates if cookies from this host should be permanently blocked
		 *
		 */
		void Remove([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aHost,
					[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String aName,
					[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aPath,
					Boolean aBlocked);
	}
}
