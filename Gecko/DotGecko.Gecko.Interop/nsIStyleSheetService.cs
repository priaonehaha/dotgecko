using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIStyleSheetServiceConstants
	{
		public const UInt32 AGENT_SHEET = 0;
		public const UInt32 USER_SHEET = 1;
	}

	/* interface for managing user and user-agent style sheets */

	/*
	 * nsIStyleSheetService allows extensions or embeddors to add to the
	 * built-in list of user or agent style sheets.
	 */
	[ComImport, Guid("1f42a6a2-ab0a-45d4-8a96-396f58ea6c6d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIStyleSheetService //: nsISupports
	{
		/**
		 * Synchronously loads a style sheet from |sheetURI| and adds it to the list
		 * of user or agent style sheets.
		 *
		 * A user sheet loaded via this API will come before userContent.css and
		 * userChrome.css in the cascade (so the rules in it will have lower
		 * precedence than rules in those sheets).
		 *
		 * An agent sheet loaded via this API will come after ua.css in the cascade
		 * (so the rules in it will have higher precedence than rules in ua.css).
		 *
		 * The relative ordering of two user or two agent sheets loaded via
		 * this API is undefined.
		 *
		 * Sheets added via this API take effect on all documents, including
		 * already-loaded ones, immediately.
		 */
		void LoadAndRegisterSheet(nsIURI sheetURI, UInt32 type);

		/**
		 * Returns true if a style sheet at |sheetURI| has previously been
		 * added to the list of style sheets specified by |type|.
		 */
		Boolean SheetRegistered(nsIURI sheetURI, UInt32 type);

		/**
		 * Remove the style sheet at |sheetURI| from the list of style sheets
		 * specified by |type|.  The removal takes effect immediately, even for
		 * already-loaded documents.
		 */
		void UnregisterSheet(nsIURI sheetURI, UInt32 type);
	}
}
