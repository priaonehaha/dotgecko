using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	public static class nsIClassInfoConstants
	{
		/**
		 * Bitflags for 'flags' attribute.
		 */
		public const UInt32 SINGLETON = 1 << 0;
		public const UInt32 THREADSAFE = 1 << 1;
		public const UInt32 MAIN_THREAD_ONLY = 1 << 2;
		public const UInt32 DOM_OBJECT = 1 << 3;
		public const UInt32 PLUGIN_OBJECT = 1 << 4;
		public const UInt32 EAGER_CLASSINFO = 1 << 5;
		/**
		 * 'flags' attribute bitflag: whether objects of this type implement
		 * nsIContent.
		 */
		public const UInt32 CONTENT_NODE = 1 << 6;

		// The high order bit is RESERVED for consumers of these flags. 
		// No implementor of this interface should ever return flags 
		// with this bit set.
		public const UInt32 RESERVED = (UInt32)1 << 31;
	}

	/**
	 * Provides information about a specific implementation class
	 * @status FROZEN
	 */
	[ComImport, Guid("986c11d0-f340-11d4-9075-0010a4e73d9a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIClassInfo //: nsISupports
	{
		/**
		 * Get an ordered list of the interface ids that instances of the class 
		 * promise to implement. Note that nsISupports is an implicit member 
		 * of any such list and need not be included. 
		 *
		 * Should set *count = 0 and *array = null and return NS_OK if getting the 
		 * list is not supported.
		 */
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		nsResult GetInterfaces(out UInt32 count, [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 0)] out Guid[] array);

		/**
		 * Get a language mapping specific helper object that may assist in using
		 * objects of this class in a specific lanaguage. For instance, if asked
		 * for the helper for nsIProgrammingLanguage::JAVASCRIPT this might return 
		 * an object that can be QI'd into the nsIXPCScriptable interface to assist 
		 * XPConnect in supplying JavaScript specific behavior to callers of the 
		 * instance object.
		 *
		 * see: nsIProgrammingLanguage.idl
		 *
		 * Should return null if no helper available for given language.
		 */
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports GetHelperForLanguage(UInt32 language);

		/**
		 * A contract ID through which an instance of this class can be created
		 * (or accessed as a service, if |flags & SINGLETON|), or null.
		 */
		String ContractID { [return: MarshalAs(UnmanagedType.LPStr)] get; }

		/**
		 * A human readable string naming the class, or null.
		 */
		String ClassDescription { [return: MarshalAs(UnmanagedType.LPStr)] get; }

		/**
		 * A class ID through which an instance of this class can be created
		 * (or accessed as a service, if |flags & SINGLETON|), or null.
		 */
		Guid ClassID { [return: MarshalAs(UnmanagedType.LPStruct)] get; }

		/**
		 * Return language type from list in nsIProgrammingLanguage
		 */
		UInt32 ImplementationLanguage { get; }

		UInt32 Flags { get; }

		/**
		 * Also a class ID through which an instance of this class can be created
		 * (or accessed as a service, if |flags & SINGLETON|).  If the class does
		 * not have a CID, it should return NS_ERROR_NOT_AVAILABLE.  This attribute
		 * exists so C++ callers can avoid allocating and freeing a CID, as would
		 * happen if they used classID.
		 */
		Guid ClassIDNoAlloc { [return: MarshalAs(UnmanagedType.LPStruct)] get; }
	}
}
