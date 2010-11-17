using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIDirectoryServiceProvider
	 *
	 * Used by Directory Service to get file locations.
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("bbf8cab0-d43a-11d3-8cc2-00609792278c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDirectoryServiceProvider //: nsISupports
	{
		/**
		 * getFile
		 *
		 * Directory Service calls this when it gets the first request for
		 * a prop or on every request if the prop is not persistent.
		 *
		 * @param prop         The symbolic name of the file.
		 * @param persistent   TRUE - The returned file will be cached by Directory
		 *                     Service. Subsequent requests for this prop will
		 *                     bypass the provider and use the cache.
		 *                     FALSE - The provider will be asked for this prop
		 *                     each time it is requested.
		 *
		 * @return             The file represented by the property.
		 *
		 */
		nsIFile GetFile([MarshalAs(UnmanagedType.LPStr)] String prop, out Boolean persistent);
	}

	/**
	 * nsIDirectoryServiceProvider2
	 *
	 * An extension of nsIDirectoryServiceProvider which allows
	 * multiple files to be returned for the given key.
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("2f977d4b-5485-11d4-87e2-0010a4e75ef2"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDirectoryServiceProvider2 : nsIDirectoryServiceProvider
	{
		#region nsIDirectoryServiceProvider Members

		new nsIFile GetFile([MarshalAs(UnmanagedType.LPStr)] String prop, out Boolean persistent);

		#endregion

		/**
		 * getFiles
		 *
		 * Directory Service calls this when it gets a request for
		 * a prop and the requested type is nsISimpleEnumerator.
		 *
		 * @param prop         The symbolic name of the file list.
		 *
		 * @return             An enumerator for a list of file locations.
		 *                     The elements in the enumeration are nsIFile
		 * @returnCode         NS_SUCCESS_AGGREGATE_RESULT if this result should be
		 *                     aggregated with other "lower" providers.
		 */
		nsISimpleEnumerator GetFiles([MarshalAs(UnmanagedType.LPStr)] String prop);
	}

	/**
	 * nsIDirectoryService
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("57a66a60-d43a-11d3-8cc2-00609792278c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDirectoryService //: nsISupports
	{
		/**
		 * init
		 *
		 * Must be called. Used internally by XPCOM initialization.
		 *
		 */
		void Init();

		/**
		 * registerProvider
		 *
		 * Register a provider with the service.
		 *
		 * @param prov            The service will keep a strong reference
		 *                        to this object. It will be released when
		 *                        the service is released.
		 *
		 */
		void RegisterProvider(nsIDirectoryServiceProvider prov);

		/**
		 * unregisterProvider
		 *
		 * Unregister a provider with the service.
		 *
		 * @param prov            
		 *
		 */
		void UnregisterProvider(nsIDirectoryServiceProvider prov);
	}
}
