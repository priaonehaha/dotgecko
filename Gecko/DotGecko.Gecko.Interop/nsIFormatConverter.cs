using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("948A0023-E3A7-11d2-96CF-0060B0FB9956"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIFormatConverter //: nsISupports
	{
		/**
		  * Get the list of the "input" data flavors (mime types as nsISupportsCString),
		  * in otherwords, the flavors that this converter can convert "from" (the 
		  * incoming data to the converter).
		  */
		nsISupportsArray GetInputDataFlavors();

		/**
		  * Get the list of the "output" data flavors (mime types as nsISupportsCString),
		  * in otherwords, the flavors that this converter can convert "to" (the 
		  * outgoing data to the converter).
		  *
		  * @param  aDataFlavorList fills list with supported flavors
		  */
		nsISupportsArray GetOutputDataFlavors();

		/**
		  * Determines whether a converion from one flavor to another is supported
		  *
		  * @param  aFromFormatConverter flavor to convert from
		  * @param  aFromFormatConverter flavor to convert to
		  */
		Boolean CanConvert([MarshalAs(UnmanagedType.LPStr)] String aFromDataFlavor, [MarshalAs(UnmanagedType.LPStr)] String aToDataFlavor);

		/**
		  * Converts from one flavor to another.
		  *
		  * @param  aFromFormatConverter flavor to convert from
		  * @param  aFromFormatConverter flavor to convert to (destination own the memory)
		  * @returns returns NS_OK if it was converted
		  */
		void Convert([MarshalAs(UnmanagedType.LPStr)] String aFromDataFlavor, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aFromData, UInt32 aDataLen,
					 [MarshalAs(UnmanagedType.LPStr)] String aToDataFlavor, [MarshalAs(UnmanagedType.IUnknown)] out nsISupports aToData, out UInt32 aDataToLen);
	}
}
