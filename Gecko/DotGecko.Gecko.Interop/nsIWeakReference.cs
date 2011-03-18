using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * An instance of |nsIWeakReference| is a proxy object that cooperates with
	 * its referent to give clients a non-owning, non-dangling reference.  Clients
	 * own the proxy, and should generally manage it with an |nsCOMPtr| (see the
	 * type |nsWeakPtr| for a |typedef| name that stands out) as they would any
	 * other XPCOM object.  The |QueryReferent| member function provides a
	 * (hopefully short-lived) owning reference on demand, through which clients
	 * can get useful access to the referent, while it still exists.
	 *
	 * @version 1.0
	 * @see nsISupportsWeakReference
	 * @see nsWeakReference
	 * @see nsWeakPtr
	 */
	[ComImport, Guid("9188bc85-f92e-11d2-81ef-0060083a0bcf"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWeakReference //: nsISupports
	{
		/**
		 * |QueryReferent| queries the referent, if it exists, and like |QueryInterface|, produces
		 * an owning reference to the desired interface.  It is designed to look and act exactly
		 * like (a proxied) |QueryInterface|.  Don't hold on to the produced interface permanently;
		 * that would defeat the purpose of using a non-owning |nsIWeakReference| in the first place.
		 */
		/*[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 0)]*/
		IntPtr QueryReferent([In] ref Guid uuid);
	}

	/**
	 * |nsISupportsWeakReference| is a factory interface which produces appropriate
	 * instances of |nsIWeakReference|.  Weak references in this scheme can only be
	 * produced for objects that implement this interface.
	 *
	 * @version 1.0
	 * @see nsIWeakReference
	 * @see nsSupportsWeakReference
	 */
	[ComImport, Guid("9188bc86-f92e-11d2-81ef-0060083a0bcf"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsWeakReference //: nsISupports
	{
		/**
		 * |GetWeakReference| produces an appropriate instance of |nsIWeakReference|.
		 * As with all good XPCOM `getters', you own the resulting interface and should
		 * manage it with an |nsCOMPtr|.
		 *
		 * @see nsIWeakReference
		 * @see nsWeakPtr
		 * @see nsCOMPtr
		 */
		nsIWeakReference GetWeakReference();
	}
}
