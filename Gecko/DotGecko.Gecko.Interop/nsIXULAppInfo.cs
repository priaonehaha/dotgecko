using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/**
	 * A scriptable interface to the nsXULAppAPI structure. See nsXULAppAPI.h for
	 * a detailed description of each attribute.
	 *
	 * @status FROZEN - This interface is frozen for use by embedders and will
	 *                  not change in the future.
	 */
	[ComImport, Guid("a61ede2a-ef09-11d9-a5ce-001124787b2e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXULAppInfo //: nsISupports
	{
		/**
		 * @see nsXREAppData.vendor
		 * @returns an empty string if nsXREAppData.vendor is not set.
		 */
		void GetVendor([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);

		/**
		 * @see nsXREAppData.name
		 */
		void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);

		/**
		 * @see nsXREAppData.ID
		 * @returns an empty string if nsXREAppData.ID is not set.
		 */
		void GetID([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);

		/**
		 * The version of the XUL application. It is different than the
		 * version of the XULRunner platform. Be careful about which one you want.
		 *
		 * @see nsXREAppData.version
		 * @returns an empty string if nsXREAppData.version is not set.
		 */
		void GetVersion([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);

		/**
		 * The build ID/date of the application. For xulrunner applications,
		 * this will be different than the build ID of the platform. Be careful
		 * about which one you want.
		 */
		void GetAppBuildID([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);

		/**
		 * The version of the XULRunner platform.
		 */
		void GetPlatformVersion([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);

		/**
		 * The build ID/date of gecko and the XULRunner platform.
		 */
		void GetPlatformBuildID([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);
	}
}
