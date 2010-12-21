using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;
using PRFileDescStar = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	public static class nsISocketProviderConstants
	{
		/**
		 * PROXY_RESOLVES_HOST
		 *
		 * This flag is set if the proxy is to perform hostname resolution instead
		 * of the client.  When set, the hostname parameter passed when in this
		 * interface will be used instead of the address structure passed for a
		 * later connect et al. request.
		 */
		public const Int32 PROXY_RESOLVES_HOST = 1 << 0;

		/**
		 * When setting this flag, the socket will not apply any
		 * credentials when establishing a connection. For example,
		 * an SSL connection would not send any client-certificates
		 * if this flag is set.
		 */
		public const Int32 ANONYMOUS_CONNECT = 1 << 1;
	}

	/**
	 * nsISocketProvider
	 */
	[ComImport, Guid("00b3df92-e830-11d8-d48e-0004e22243f8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISocketProvider //: nsISupports
	{
		/**
		 * newSocket
		 *
		 * @param aFamily
		 *        The address family for this socket (PR_AF_INET or PR_AF_INET6).
		 * @param aHost
		 *        The hostname for this connection.
		 * @param aPort
		 *        The port for this connection.
		 * @param aProxyHost
		 *        If non-null, the proxy hostname for this connection.
		 * @param aProxyPort
		 *        The proxy port for this connection.
		 * @param aFlags
		 *        Control flags that govern this connection (see below.)
		 * @param aFileDesc
		 *        The resulting PRFileDesc.
		 * @param aSecurityInfo
		 *        Any security info that should be associated with aFileDesc.  This
		 *        object typically implements nsITransportSecurityInfo.
		 */
		void NewSocket(Int32 aFamily,
					   [MarshalAs(UnmanagedType.LPStr)] String aHost,
					   Int32 aPort,
					   [MarshalAs(UnmanagedType.LPStr)] String aProxyHost,
					   Int32 aProxyPort,
					   UInt32 aFlags,
					   out PRFileDescStar aFileDesc,
					   [MarshalAs(UnmanagedType.IUnknown)] out nsISupports aSecurityInfo);

		/**
		 * addToSocket
		 *
		 * This function is called to allow the socket provider to layer a
		 * PRFileDesc on top of another PRFileDesc.  For example, SSL via a SOCKS
		 * proxy.
		 *
		 * Parameters are the same as newSocket with the exception of aFileDesc,
		 * which is an in-param instead.
		 */
		void AddToSocket(Int32 aFamily,
						 [MarshalAs(UnmanagedType.LPStr)] String aHost,
						 Int32 aPort,
						 [MarshalAs(UnmanagedType.LPStr)] String aProxyHost,
						 Int32 aProxyPort,
						 UInt32 aFlags,
						 PRFileDescStar aFileDesc,
						 [MarshalAs(UnmanagedType.IUnknown)] out nsISupports aSecurityInfo);
	}
}
