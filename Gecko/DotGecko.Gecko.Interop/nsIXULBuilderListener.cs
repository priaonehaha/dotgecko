using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	// An nsIXULBuilderListener object is a listener that will be notified
	// when a template builder rebuilds its content.
	[ComImport, Guid("ac46be8f-c863-4c23-84a2-d0fcc8dfa9f4"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXULBuilderListener //: nsISupports
	{
		/**
		 * Called before a template builder rebuilds its content.
		 * @param aBuilder the template builder that rebuilds the content.
		 */
		void WillRebuild(nsIXULTemplateBuilder aBuilder);

		/**
		 * Called after a template builder has rebuilt its content.
		 * @param aBuilder the template builder that has rebuilt the content.
		 */
		void DidRebuild(nsIXULTemplateBuilder aBuilder);
	}
}
