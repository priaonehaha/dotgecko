using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/*
	 * nsICategoryManager
	 */
	[ComImport, Guid("3275b2cd-af6d-429a-80d7-f0c5120342ac"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsICategoryManager //: nsISupports
	{
		/**
		 * Get the value for the given category's entry.
		 * @param aCategory The name of the category ("protocol")
		 * @param aEntry The entry you're looking for ("http")
		 * @return The value.
		 */
		[return: MarshalAs(UnmanagedType.LPStr)]
		String GetCategoryEntry([MarshalAs(UnmanagedType.LPStr)] String aCategory, [MarshalAs(UnmanagedType.LPStr)] String aEntry);

		/**
		 * Add an entry to a category.
		 * @param aCategory The name of the category ("protocol")
		 * @param aEntry The entry to be added ("http")
		 * @param aValue The value for the entry ("moz.httprulez.1")
		 * @param aPersist Should this data persist between invocations?
		 * @param aReplace Should we replace an existing entry?
		 * @return Previous entry, if any
		 */
		[return: MarshalAs(UnmanagedType.LPStr)]
		String AddCategoryEntry([MarshalAs(UnmanagedType.LPStr)] String aCategory, [MarshalAs(UnmanagedType.LPStr)] String aEntry,
					[MarshalAs(UnmanagedType.LPStr)] String aValue, Boolean aPersist, Boolean aReplace);

		/**
		 * Delete an entry from the category.
		 * @param aCategory The name of the category ("protocol")
		 * @param aEntry The entry to be added ("http")
		 * @param aPersist Delete persistent data from registry, if present?
		 */
		void DeleteCategoryEntry([MarshalAs(UnmanagedType.LPStr)] String aCategory, [MarshalAs(UnmanagedType.LPStr)] String aEntry, Boolean aPersist);

		/**
		 * Delete a category and all entries.
		 * @param aCategory The category to be deleted.
		 */
		void DeleteCategory([MarshalAs(UnmanagedType.LPStr)] String aCategory);

		/**
		 * Enumerate the entries in a category.
		 * @param aCategory The category to be enumerated.
		 * @return a simple enumerator, each result QIs to
		 *         nsISupportsCString.
		 */
		nsISimpleEnumerator EnumerateCategory([MarshalAs(UnmanagedType.LPStr)] String aCategory);

		/**
		 * Enumerate all existing categories
		 * @param aCategory The category to be enumerated.
		 * @return a simple enumerator, each result QIs to
		 *         nsISupportsCString.
		 */
		nsISimpleEnumerator EnumerateCategories();
	}
}
