using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/*
	 * nsICommandManager is an interface used to executing user-level commands,
	 * and getting the state of available commands.
	 *
	 * Commands are identified by strings, which are documented elsewhere.
	 * In addition, the list of required and optional parameters for
	 * each command, that are passed in via the nsICommandParams, are
	 * also documented elsewhere. (Where? Need a good location for this).
	 */
	[ComImport, Guid("080D2001-F91E-11D4-A73C-F9242928207C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsICommandManager //: nsISupports
	{
		/*
		 * Register an observer on the specified command. The observer's Observe
		 * method will get called when the state (enabled/disbaled, or toggled etc)
		 * of the command changes.
		 *
		 * You can register the same observer on multiple commmands by calling this
		 * multiple times.
		 */
		void AddCommandObserver(nsIObserver aCommandObserver, [MarshalAs(UnmanagedType.LPStr)] String aCommandToObserve);

		/*
		 * Stop an observer from observering the specified command. If the observer
		 * was also registered on ther commands, they will continue to be observed.
		 *
		 * Passing an empty string in 'aCommandObserved' will remove the observer
		 * from all commands.
		 */
		void RemoveCommandObserver(nsIObserver aCommandObserver, [MarshalAs(UnmanagedType.LPStr)] String aCommandObserved);

		/*
		 * Ask the command manager if the specified command is supported.
		 * If aTargetWindow is null, the focused window is used.
		 *
		 */
		Boolean IsCommandSupported([MarshalAs(UnmanagedType.LPStr)] String aCommandName, nsIDOMWindow aTargetWindow);

		/*
		 * Ask the command manager if the specified command is currently.
		 * enabled.
		 * If aTargetWindow is null, the focused window is used.
		 */
		Boolean IsCommandEnabled([MarshalAs(UnmanagedType.LPStr)] String aCommandName, nsIDOMWindow aTargetWindow);

		/*
		 * Get the state of the specified commands.
		 *
		 * On input: aCommandParams filled in with values that the caller cares
		 * about, most of which are command-specific (see the command documentation
		 * for details). One boolean value, "enabled", applies to all commands,
		 * and, in return will be set to indicate whether the command is enabled
		 * (equivalent to calling isCommandEnabled).
		 *
		 * aCommandName is the name of the command that needs the state
		 * aTargetWindow is the source of command controller 
		 *      (null means use focus controller)
		 * On output: aCommandParams: values set by the caller filled in with
		 * state from the command.
		 */
		void GetCommandState([MarshalAs(UnmanagedType.LPStr)] String aCommandName, nsIDOMWindow aTargetWindow, /* inout */ nsICommandParams aCommandParams);

		/*
		 * Execute the specified command.
		 * The command will be executed in aTargetWindow if it is specified.
		 * If aTargetWindow is null, it will go to the focused window.
		 *
		 * param: aCommandParams, a list of name-value pairs of command parameters,
		 * may be null for parameter-less commands.
		 *
		 */
		void DoCommand([MarshalAs(UnmanagedType.LPStr)] String aCommandName, nsICommandParams aCommandParams, nsIDOMWindow aTargetWindow);
	}

	/*
	Arguments to observers "Observe" method are as follows:

	  void Observe(   in nsISupports aSubject,          // The nsICommandManager calling this Observer
					  in string      aTopic,            // Name of the command
					  in wstring     aDummy );          // unused
	*/
}
