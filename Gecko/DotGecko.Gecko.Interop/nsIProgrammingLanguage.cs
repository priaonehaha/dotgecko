using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIProgrammingLanguageConstants
	{
		/**
		 * Identifiers for programming languages.
		 */
		public const UInt32 UNKNOWN = 0;
		public const UInt32 CPLUSPLUS = 1;
		public const UInt32 JAVASCRIPT = 2;
		public const UInt32 PYTHON = 3;
		public const UInt32 PERL = 4;
		public const UInt32 JAVA = 5;
		public const UInt32 ZX81_BASIC = 6;  // it could happen :)
		public const UInt32 JAVASCRIPT2 = 7;
		public const UInt32 RUBY = 8;
		public const UInt32 PHP = 9;
		public const UInt32 TCL = 10;
		// This list can grow indefinitely. Just don't ever change an existing item.
		public const UInt32 MAX = 10; // keep this as the largest index.
	}

	/**
	 * Enumeration of Programming Languages
	 * @status FROZEN
	 */
	[ComImport, Guid("ea604e90-40ba-11d5-90bb-0010a4e73d9a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIProgrammingLanguage //: nsISupports
	{
	}
}
