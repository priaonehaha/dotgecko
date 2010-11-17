using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("f36e3ec1-9197-4ad8-8d4c-d3b1927fd6df"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIControllers //: nsISupports
	{
		nsIController GetControllerForCommand([MarshalAs(UnmanagedType.LPStr)] String command);

		void InsertControllerAt(UInt32 index, nsIController controller);
		nsIController RemoveControllerAt(UInt32 index);
		nsIController GetControllerAt(UInt32 index);

		void AppendController(nsIController controller);
		void RemoveController(nsIController controller);

		/*
			Return an ID for this controller which is unique to this
			nsIControllers.
		*/
		UInt32 GetControllerId(nsIController controller);
		/*
			Get the controller specified by the given ID.
		*/
		nsIController GetControllerById(UInt32 controllerID);

		UInt32 GetControllerCount();
	}
}
