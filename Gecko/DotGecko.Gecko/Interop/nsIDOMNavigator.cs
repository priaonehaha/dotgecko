using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("777bd8a1-38c1-4b12-ba8f-ff6c2eb8c56b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMNavigator //: nsISupports
	{
		void GetAppCodeName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetAppName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetAppVersion([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetLanguage([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		nsIDOMMimeTypeArray MimeTypes { get; }
		void GetPlatform([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetOscpu([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetVendor([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetVendorSub([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetProduct([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetProductSub([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		nsIDOMPluginArray Plugins { get; }
		void GetSecurityPolicy([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetUserAgent([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		Boolean CookieEnabled { get; }
		Boolean OnLine { get; }
		void GetBuildID([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);

		Boolean JavaEnabled();
		Boolean TaintEnabled();

		// XXX This one's tough, would nsISupports preference(in DOMString
		// pref /*, ... */); work?

		// jsval      preference(/* ... */);
	}

	[ComImport, Guid("4b4f8316-1dd2-11b2-b265-9a857376d159"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDOMJSNavigator //: nsISupports
	{
		// Nothing about this method (except its name :-) is describeable
		// in XPIDL, argument handling and the return value needs to be
		// dealt with in the implementation of this method.
		void Preference();
	}
}
