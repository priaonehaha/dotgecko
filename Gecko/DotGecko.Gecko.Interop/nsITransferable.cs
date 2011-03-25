using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	  * nsIFlavorDataProvider allows a flavor to 'promise' data later,
	  * supplying the data lazily.
	  * 
	  * To use it, call setTransferData, passing the flavor string,
	  * a nsIFlavorDataProvider QI'd to nsISupports, and a data size of 0.
	  *
	  * When someone calls getTransferData later, if the data size is
	  * stored as 0, the nsISupports will be QI'd to nsIFlavorDataProvider,
	  * and its getFlavorData called.
	  *
	  */
	[ComImport, Guid("7E225E5F-711C-11D7-9FAE-000393636592"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIFlavorDataProvider //: nsISupports
	{
		/**
		  * Retrieve the data from this data provider.
		  *
		  * @param  aTransferable (in parameter) the transferable we're being called for.
		  * @param  aFlavor (in parameter) the flavor of data to retrieve
		  * @param  aData the data. Some variant of class in nsISupportsPrimitives.idl
		  * @param  aDataLen the length of the data
		  */
		void GetFlavorData(nsITransferable aTransferable, [MarshalAs(UnmanagedType.LPStr)] String aFlavor, [MarshalAs(UnmanagedType.IUnknown)] out nsISupports aData, out UInt32 aDataLen);
	}

	public static class nsITransferableConstants
	{
		public const Int32 kFlavorHasDataProvider = 0;
	}

	[ComImport, Guid("8B5314BC-DB01-11d2-96CE-0060B0FB9956"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsITransferable //: nsISupports
	{
		/**
		  * Computes a list of flavors (mime types as nsISupportsCString) that the transferable 
		  * can export, either through intrinsic knowledge or output data converters.
		  *
		  * @param  aDataFlavorList fills list with supported flavors. This is a copy of
		  *          the internal list, so it may be edited w/out affecting the transferable.
		  */
		nsISupportsArray FlavorsTransferableCanExport();

		/**
		  * Given a flavor retrieve the data. 
		  *
		  * @param  aFlavor (in parameter) the flavor of data to retrieve
		  * @param  aData the data. Some variant of class in nsISupportsPrimitives.idl
		  * @param  aDataLen the length of the data
		  */
		void GetTransferData([MarshalAs(UnmanagedType.LPStr)] String aFlavor, [MarshalAs(UnmanagedType.IUnknown)] out nsISupports aData, out UInt32 aDataLen);

		/**
		  * Returns the best flavor in the transferable, given those that have
		  * been added to it with |AddFlavor()|
		  *
		  * @param  aFlavor (out parameter) the flavor of data that was retrieved
		  * @param  aData the data. Some variant of class in nsISupportsPrimitives.idl
		  * @param  aDataLen the length of the data
		  */
		void GetAnyTransferData([MarshalAs(UnmanagedType.LPStr)] out String aFlavor, [MarshalAs(UnmanagedType.IUnknown)] out nsISupports aData, out UInt32 aDataLen);

		/**
		  * Returns true if the data is large.
		  */
		Boolean IsLargeDataSet();

		///////////////////////////////
		// Setter part of interface 
		///////////////////////////////

		/**
		  * Computes a list of flavors (mime types as nsISupportsCString) that the transferable can
		  * accept into it, either through intrinsic knowledge or input data converters.
		  *
		  * @param  outFlavorList fills list with supported flavors. This is a copy of
		  *          the internal list, so it may be edited w/out affecting the transferable.
		  */
		nsISupportsArray FlavorsTransferableCanImport();

		/**
		  * Sets the data in the transferable with the specified flavor. The transferable
		  * will maintain its own copy the data, so it is not necessary to do that beforehand.
		  *
		  * @param  aFlavor the flavor of data that is being set
		  * @param  aData the data, either some variant of class in nsISupportsPrimitives.idl,
		  *         an nsIFile, or an nsIFlavorDataProvider (see above)
		  * @param  aDataLen the length of the data, or 0 if passing a nsIFlavorDataProvider
		  */
		void SetTransferData([MarshalAs(UnmanagedType.LPStr)] String aFlavor, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aData, UInt32 aDataLen);

		/**
		  * Add the data flavor, indicating that this transferable 
		  * can receive this type of flavor
		  *
		  * @param  aDataFlavor a new data flavor to handle
		  */
		void AddDataFlavor([MarshalAs(UnmanagedType.LPStr)] String aDataFlavor);

		/**
		  * Removes the data flavor matching the given one (string compare) and the data
		  * that goes along with it.
		  *
		  * @param  aDataFlavor a data flavor to remove
		  */
		void RemoveDataFlavor([MarshalAs(UnmanagedType.LPStr)] String aDataFlavor);

		nsIFormatConverter Converter { get; set; }
	}
}
