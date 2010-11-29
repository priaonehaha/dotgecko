using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMEventListener interface is a callback interface for
	 * listening to events in the Document Object Model.
	 *
	 * For more information on this interface please see 
	 * http://www.w3.org/TR/DOM-Level-2-Events/
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("df31c120-ded6-11d1-bd85-00805f8ae3f4"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMEventListener //: nsISupports
	{
		/**
		 * This method is called whenever an event occurs of the type for which 
		 * the EventListener interface was registered.
		 *
		 * @param   evt The Event contains contextual information about the 
		 *              event. It also contains the stopPropagation and 
		 *              preventDefault methods which are used in determining the 
		 *              event's flow and default action.
		 */
		void HandleEvent(nsIDOMEvent aEvent);
	}
}
