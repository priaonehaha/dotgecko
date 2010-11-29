using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("D5B61B82-1DA4-11d3-BF87-00105A1B0627"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIController //: nsISupports
	{
		Boolean IsCommandEnabled([MarshalAs(UnmanagedType.LPStr)] String command);
		Boolean SupportsCommand([MarshalAs(UnmanagedType.LPStr)] String command);

		void DoCommand([MarshalAs(UnmanagedType.LPStr)] String command);

		void OnEvent([MarshalAs(UnmanagedType.LPStr)] String eventName);
	}

	/*

	  Enhanced controller interface that allows for passing of parameters
	  to commands.
  
	*/
	[ComImport, Guid("EBE55080-C8A9-11D5-A73C-DD620D6E04BC"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsICommandController //: nsISupports
	{
		void GetCommandStateWithParams([MarshalAs(UnmanagedType.LPStr)] String command, nsICommandParams aCommandParams);

		void DoCommandWithParams([MarshalAs(UnmanagedType.LPStr)] String command, nsICommandParams aCommandParams);
	}


	/*
	  An API for registering commands in groups, to allow for 
	  updating via nsIDOMWindowInternal::UpdateCommands.
	*/
	[ComImport, Guid("9F82C404-1C7B-11D5-A73C-ECA43CA836FC"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIControllerCommandGroup //: nsISupports
	{
		void AddCommandToGroup([MarshalAs(UnmanagedType.LPStr)] String aCommand, [MarshalAs(UnmanagedType.LPStr)] String aGroup);
		void RemoveCommandFromGroup([MarshalAs(UnmanagedType.LPStr)] String aCommand, [MarshalAs(UnmanagedType.LPStr)] String aGroup);

		Boolean IsCommandInGroup([MarshalAs(UnmanagedType.LPStr)] String aCommand, [MarshalAs(UnmanagedType.LPStr)] String aGroup);

		/*
		  We should expose some methods that allow for enumeration.
		*/
		nsISimpleEnumerator GetGroupsEnumerator();

		nsISimpleEnumerator GetEnumeratorForGroup([MarshalAs(UnmanagedType.LPStr)] String aGroup);
	}
}
