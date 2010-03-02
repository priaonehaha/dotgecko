using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static partial class Xpcom
	{
		/**
		 * Flags that may be OR'd together to pass to NS_StringContainerInit2:
		 */

		/* Data passed into NS_StringContainerInit2 is not copied; instead, the
		 * string references the passed in data pointer directly.  The caller must
		 * ensure that the data is valid for the lifetime of the string container.
		 * This flag should not be combined with NS_STRING_CONTAINER_INIT_ADOPT. */
		internal const UInt32 NS_STRING_CONTAINER_INIT_DEPEND = (1 << 1);

		/* Data passed into NS_StringContainerInit2 is not copied; instead, the
		 * string takes ownership over the data pointer.  The caller must have
		 * allocated the data array using the XPCOM memory allocator (nsMemory).
		 * This flag should not be combined with NS_STRING_CONTAINER_INIT_DEPEND. */
		internal const UInt32 NS_STRING_CONTAINER_INIT_ADOPT = (1 << 2);

		/* Data passed into NS_StringContainerInit2 is a substring that is not
		 * null-terminated. */
		internal const UInt32 NS_STRING_CONTAINER_INIT_SUBSTRING = (1 << 3);

		/**
		 * NS_StringContainerInit
		 *
		 * @param aContainer    string container reference
		 * @return              NS_OK if string container successfully initialized
		 *
		 * This function may allocate additional memory for aContainer.  When
		 * aContainer is no longer needed, NS_StringContainerFinish should be called.
		 *
		 * @status FROZEN
		 */
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Unicode)]
		internal static extern UInt32 NS_StringContainerInit(nsStringContainer aContainer);

		/**
		 * NS_StringContainerInit2
		 *
		 * @param aContainer    string container reference
		 * @param aData         character buffer (may be null)
		 * @param aDataLength   number of characters stored at aData (may pass
		 *                      PR_UINT32_MAX if aData is null-terminated)
		 * @param aFlags        flags affecting how the string container is
		 *                      initialized.  this parameter is ignored when aData
		 *                      is null.  otherwise, if this parameter is 0, then
		 *                      aData is copied into the string.
		 *
		 * This function resembles NS_StringContainerInit but provides further
		 * options that permit more efficient memory usage.  When aContainer is
		 * no longer needed, NS_StringContainerFinish should be called.
		 *
		 * NOTE: NS_StringContainerInit2(container, nsnull, 0, 0) is equivalent to
		 * NS_StringContainerInit(container).
		 *
		 * @status FROZEN
		 */
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Unicode)]
		internal static extern UInt32 NS_StringContainerInit2(nsStringContainer aContainer, String aData, UInt32 aDataLength, UInt32 aFlags);

		/**
		 * NS_StringContainerFinish
		 *
		 * @param aContainer    string container reference
		 *
		 * This function frees any memory owned by aContainer.
		 *
		 * @status FROZEN
		 */
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Unicode)]
		internal static extern void NS_StringContainerFinish(nsStringContainer aContainer);

		/**
		 * NS_StringGetData
		 *
		 * This function returns a const character pointer to the string's internal
		 * buffer, the length of the string, and a boolean value indicating whether
		 * or not the buffer is null-terminated.
		 *
		 * @param aStr          abstract string reference
		 * @param aData         out param that will hold the address of aStr's
		 *                      internal buffer
		 * @param aTerminated   if non-null, this out param will be set to indicate
		 *                      whether or not aStr's internal buffer is null-
		 *                      terminated
		 * @return              length of aStr's internal buffer
		 *
		 * @status FROZEN
		 */
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Unicode)]
		internal static extern UInt32 NS_StringGetData(nsAString aStr, out IntPtr aData, IntPtr aTerminated);

		/**
		 * NS_StringGetMutableData
		 *
		 * This function provides mutable access to a string's internal buffer.  It
		 * returns a pointer to an array of characters that may be modified.  The
		 * returned pointer remains valid until the string object is passed to some
		 * other string function.
		 *
		 * Optionally, this function may be used to resize the string's internal
		 * buffer.  The aDataLength parameter specifies the requested length of the
		 * string's internal buffer.  By passing some value other than PR_UINT32_MAX,
		 * the caller can request that the buffer be resized to the specified number of
		 * characters before returning.  The caller is not responsible for writing a
		 * null-terminator.
		 *
		 * @param aStr          abstract string reference
		 * @param aDataLength   number of characters to resize the string's internal
		 *                      buffer to or PR_UINT32_MAX if no resizing is needed
		 * @param aData         out param that upon return holds the address of aStr's
		 *                      internal buffer or null if the function failed
		 * @return              number of characters or zero if the function failed
		 *
		 * This function does not necessarily null-terminate aStr after resizing its
		 * internal buffer.  The behavior depends on the implementation of the abstract
		 * string, aStr.  If aStr is a reference to a nsStringContainer, then its data
		 * will be null-terminated by this function.
		 *
		 * @status FROZEN
		 */
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Unicode)]
		internal static extern UInt32 NS_StringGetMutableData(nsAString aStr, UInt32 aDataLength, out IntPtr aData);

		/**
		 * NS_StringCloneData
		 *
		 * This function returns a null-terminated copy of the string's
		 * internal buffer.
		 *
		 * @param aStr          abstract string reference
		 * @return              null-terminated copy of the string's internal buffer
		 *                      (it must be free'd using using nsMemory::Free)
		 *
		 * @status FROZEN
		 */
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Unicode)]
		internal static extern String NS_StringCloneData(nsAString aStr);

		/**
		 * NS_StringSetData
		 *
		 * This function copies aData into aStr.
		 *
		 * @param aStr          abstract string reference
		 * @param aData         character buffer
		 * @param aDataLength   number of characters to copy from source string (pass
		 *                      PR_UINT32_MAX to copy until end of aData, designated by
		 *                      a null character)
		 * @return              NS_OK if function succeeded
		 *
		 * This function does not necessarily null-terminate aStr after copying data
		 * from aData.  The behavior depends on the implementation of the abstract
		 * string, aStr.  If aStr is a reference to a nsStringContainer, then its data
		 * will be null-terminated by this function.
		 *
		 * @status FROZEN
		 */
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Unicode)]
		internal static extern UInt32 NS_StringSetData(nsAString aStr, String aData, UInt32 aDataLength);

		/**
		 * NS_StringSetDataRange
		 *
		 * This function copies aData into a section of aStr.  As a result it can be
		 * used to insert new characters into the string.
		 *
		 * @param aStr          abstract string reference
		 * @param aCutOffset    starting index where the string's existing data
		 *                      is to be overwritten (pass PR_UINT32_MAX to cause
		 *                      aData to be appended to the end of aStr, in which
		 *                      case the value of aCutLength is ignored).
		 * @param aCutLength    number of characters to overwrite starting at
		 *                      aCutOffset (pass PR_UINT32_MAX to overwrite until the
		 *                      end of aStr).
		 * @param aData         character buffer (pass null to cause this function
		 *                      to simply remove the "cut" range)
		 * @param aDataLength   number of characters to copy from source string (pass
		 *                      PR_UINT32_MAX to copy until end of aData, designated by
		 *                      a null character)
		 * @return              NS_OK if function succeeded
		 *
		 * This function does not necessarily null-terminate aStr after copying data
		 * from aData.  The behavior depends on the implementation of the abstract
		 * string, aStr.  If aStr is a reference to a nsStringContainer, then its data
		 * will be null-terminated by this function.
		 *
		 * @status FROZEN
		 */
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Unicode)]
		internal static extern UInt32 NS_StringSetDataRange(nsAString aStr, UInt32 aCutOffset, UInt32 aCutLength, String aData, UInt32 aDataLength);

		/**
		 * NS_StringCopy
		 *
		 * This function makes aDestStr have the same value as aSrcStr.  It is
		 * provided as an optimization.
		 *
		 * @param aDestStr      abstract string reference to be modified
		 * @param aSrcStr       abstract string reference containing source string
		 * @return              NS_OK if function succeeded
		 *
		 * This function does not necessarily null-terminate aDestStr after copying
		 * data from aSrcStr.  The behavior depends on the implementation of the
		 * abstract string, aDestStr.  If aDestStr is a reference to a
		 * nsStringContainer, then its data will be null-terminated by this function.
		 *
		 * @status FROZEN
		 */
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Unicode)]
		internal static extern UInt32 NS_StringCopy(nsAString aDestStr, nsAString aSrcStr);

		/**
		 * NS_StringSetIsVoid
		 *
		 * This function marks a string as being a "void string".  Any data in the
		 * string will be lost.
		 */
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Unicode)]
		internal static extern void NS_StringSetIsVoid(nsAString aStr, Boolean aIsVoid);

		/**
		 * NS_StringGetIsVoid
		 *
		 * This function provides a way to test if a string is a "void string", as
		 * marked by NS_StringSetIsVoid.
		 */
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Unicode)]
		internal static extern Boolean NS_StringGetIsVoid(nsAString aStr);

		/**
		 * Flags that may be OR'd together to pass to NS_StringContainerInit2:
		 */

		/* Data passed into NS_CStringContainerInit2 is not copied; instead, the
		 * string references the passed in data pointer directly.  The caller must
		 * ensure that the data is valid for the lifetime of the string container.
		 * This flag should not be combined with NS_CSTRING_CONTAINER_INIT_ADOPT. */
		internal const UInt32 NS_CSTRING_CONTAINER_INIT_DEPEND = (1 << 1);

		/* Data passed into NS_CStringContainerInit2 is not copied; instead, the
		 * string takes ownership over the data pointer.  The caller must have
		 * allocated the data array using the XPCOM memory allocator (nsMemory).
		 * This flag should not be combined with NS_CSTRING_CONTAINER_INIT_DEPEND. */
		internal const UInt32 NS_CSTRING_CONTAINER_INIT_ADOPT = (1 << 2);

		/* Data passed into NS_CStringContainerInit2 is a substring that is not
		 * null-terminated. */
		internal const UInt32 NS_CSTRING_CONTAINER_INIT_SUBSTRING = (1 << 3);

		/**
		 * NS_CStringContainerInit
		 *
		 * @param aContainer    string container reference
		 * @return              NS_OK if string container successfully initialized
		 *
		 * This function may allocate additional memory for aContainer.  When
		 * aContainer is no longer needed, NS_CStringContainerFinish should be called.
		 *
		 * @status FROZEN
		 */ 
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Ansi)]
		internal static extern UInt32 NS_CStringContainerInit(nsCStringContainer aContainer);

		/**
		 * NS_CStringContainerInit2
		 *
		 * @param aContainer    string container reference
		 * @param aData         character buffer (may be null)
		 * @param aDataLength   number of characters stored at aData (may pass
		 *                      PR_UINT32_MAX if aData is null-terminated)
		 * @param aFlags        flags affecting how the string container is
		 *                      initialized.  this parameter is ignored when aData
		 *                      is null.  otherwise, if this parameter is 0, then
		 *                      aData is copied into the string.
		 *
		 * This function resembles NS_CStringContainerInit but provides further
		 * options that permit more efficient memory usage.  When aContainer is
		 * no longer needed, NS_CStringContainerFinish should be called.
		 *
		 * NOTE: NS_CStringContainerInit2(container, nsnull, 0, 0) is equivalent to
		 * NS_CStringContainerInit(container).
		 *
		 * @status FROZEN
		 */ 
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Ansi)]
		internal static extern UInt32 NS_CStringContainerInit2(nsCStringContainer aContainer, String aData, UInt32 aDataLength, UInt32 aFlags);

		/**
		 * NS_CStringContainerFinish
		 *
		 * @param aContainer    string container reference
		 *
		 * This function frees any memory owned by aContainer.
		 *
		 * @status FROZEN
		 */ 
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Ansi)]
		internal static extern void NS_CStringContainerFinish(nsCStringContainer aContainer);

		/**
		 * NS_CStringGetData
		 *
		 * This function returns a const character pointer to the string's internal
		 * buffer, the length of the string, and a boolean value indicating whether
		 * or not the buffer is null-terminated.
		 *
		 * @param aStr          abstract string reference
		 * @param aData         out param that will hold the address of aStr's
		 *                      internal buffer
		 * @param aTerminated   if non-null, this out param will be set to indicate
		 *                      whether or not aStr's internal buffer is null-
		 *                      terminated
		 * @return              length of aStr's internal buffer
		 *
		 * @status FROZEN
		 */ 
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Ansi)]
		internal static extern UInt32 NS_CStringGetData(nsACString aStr, out IntPtr aData, IntPtr aTerminated);

		/**
		 * NS_CStringGetMutableData
		 *
		 * This function provides mutable access to a string's internal buffer.  It
		 * returns a pointer to an array of characters that may be modified.  The
		 * returned pointer remains valid until the string object is passed to some
		 * other string function.
		 *
		 * Optionally, this function may be used to resize the string's internal
		 * buffer.  The aDataLength parameter specifies the requested length of the
		 * string's internal buffer.  By passing some value other than PR_UINT32_MAX,
		 * the caller can request that the buffer be resized to the specified number of
		 * characters before returning.  The caller is not responsible for writing a
		 * null-terminator.
		 *
		 * @param aStr          abstract string reference
		 * @param aDataLength   number of characters to resize the string's internal
		 *                      buffer to or PR_UINT32_MAX if no resizing is needed
		 * @param aData         out param that upon return holds the address of aStr's
		 *                      internal buffer or null if the function failed
		 * @return              number of characters or zero if the function failed
		 *
		 * This function does not necessarily null-terminate aStr after resizing its
		 * internal buffer.  The behavior depends on the implementation of the abstract
		 * string, aStr.  If aStr is a reference to a nsStringContainer, then its data
		 * will be null-terminated by this function.
		 *
		 * @status FROZEN
		 */ 
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Ansi)]
		internal static extern UInt32 NS_CStringGetMutableData(nsACString aStr, UInt32 aDataLength, out IntPtr aData);

		/**
		 * NS_CStringCloneData
		 *
		 * This function returns a null-terminated copy of the string's
		 * internal buffer.
		 *
		 * @param aStr          abstract string reference
		 * @return              null-terminated copy of the string's internal buffer
		 *                      (it must be free'd using using nsMemory::Free)
		 *
		 * @status FROZEN
		 */ 
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Ansi)]
		internal static extern String NS_CStringCloneData(nsACString aStr);

		/**
		 * NS_CStringSetData
		 *
		 * This function copies aData into aStr.
		 *
		 * @param aStr          abstract string reference
		 * @param aData         character buffer
		 * @param aDataLength   number of characters to copy from source string (pass
		 *                      PR_UINT32_MAX to copy until end of aData, designated by
		 *                      a null character)
		 * @return              NS_OK if function succeeded
		 *
		 * This function does not necessarily null-terminate aStr after copying data
		 * from aData.  The behavior depends on the implementation of the abstract
		 * string, aStr.  If aStr is a reference to a nsStringContainer, then its data
		 * will be null-terminated by this function.
		 *
		 * @status FROZEN
		 */ 
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Ansi)]
		internal static extern UInt32 NS_CStringSetData(nsACString aStr, String aData, UInt32 aDataLength);

		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Ansi)]
		internal static extern UInt32 NS_CStringSetData(nsACString aStr, Byte[] aData, UInt32 aDataLength);

		/**
		 * NS_CStringSetDataRange
		 *
		 * This function copies aData into a section of aStr.  As a result it can be
		 * used to insert new characters into the string.
		 *
		 * @param aStr          abstract string reference
		 * @param aCutOffset    starting index where the string's existing data
		 *                      is to be overwritten (pass PR_UINT32_MAX to cause
		 *                      aData to be appended to the end of aStr, in which
		 *                      case the value of aCutLength is ignored).
		 * @param aCutLength    number of characters to overwrite starting at
		 *                      aCutOffset (pass PR_UINT32_MAX to overwrite until the
		 *                      end of aStr).
		 * @param aData         character buffer (pass null to cause this function
		 *                      to simply remove the "cut" range)
		 * @param aDataLength   number of characters to copy from source string (pass
		 *                      PR_UINT32_MAX to copy until end of aData, designated by
		 *                      a null character)
		 * @return              NS_OK if function succeeded
		 *
		 * This function does not necessarily null-terminate aStr after copying data
		 * from aData.  The behavior depends on the implementation of the abstract
		 * string, aStr.  If aStr is a reference to a nsStringContainer, then its data
		 * will be null-terminated by this function.
		 *
		 * @status FROZEN
		 */ 
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Ansi)]
		internal static extern UInt32 NS_CStringSetDataRange(nsACString aStr, UInt32 aCutOffset, UInt32 aCutLength, String aData, UInt32 aDataLength);

		/**
		 * NS_CStringCopy
		 *
		 * This function makes aDestStr have the same value as aSrcStr.  It is
		 * provided as an optimization.
		 *
		 * @param aDestStr      abstract string reference to be modified
		 * @param aSrcStr       abstract string reference containing source string
		 * @return              NS_OK if function succeeded
		 *
		 * This function does not necessarily null-terminate aDestStr after copying
		 * data from aSrcStr.  The behavior depends on the implementation of the
		 * abstract string, aDestStr.  If aDestStr is a reference to a
		 * nsStringContainer, then its data will be null-terminated by this function.
		 *
		 * @status FROZEN
		 */ 
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Ansi)]
		internal static extern UInt32 NS_CStringCopy(nsACString aDestStr, nsACString aSrcStr);

		/**
		 * NS_CStringSetIsVoid
		 *
		 * This function marks a string as being a "void string".  Any data in the
		 * string will be lost.
		 */ 
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Ansi)]
		internal static extern void NS_CStringSetIsVoid(nsACString aStr, Boolean aIsVoid);

		/**
		 * NS_CStringGetIsVoid
		 *
		 * This function provides a way to test if a string is a "void string", as
		 * marked by NS_CStringSetIsVoid.
		 */ 
		[DllImport(xpcom, ExactSpelling = true, CharSet = CharSet.Ansi)]
		internal static extern Boolean NS_CStringGetIsVoid(nsACString aStr);

		/**
		 * Encodings that can be used with the following conversion routines.
		 */
		internal enum nsCStringEncoding : uint
		{
			/* Conversion between ASCII and UTF-16 assumes that all bytes in the source
			 * string are 7-bit ASCII and can be inflated to UTF-16 by inserting null
			 * bytes.  Reverse conversion is done by truncating every other byte.  The
			 * conversion may result in loss and/or corruption of information if the
			 * strings do not strictly contain ASCII data. */
			NS_CSTRING_ENCODING_ASCII = 0,

			/* Conversion between UTF-8 and UTF-16 is non-lossy. */
			NS_CSTRING_ENCODING_UTF8 = 1,

			/* Conversion from UTF-16 to the native filesystem charset may result in a
			 * loss of information.  No attempt is made to protect against data loss in
			 * this case.  The native filesystem charset applies to strings passed to
			 * the "Native" method variants on nsIFile and nsILocalFile. */
			NS_CSTRING_ENCODING_NATIVE_FILESYSTEM = 2
		}

		/**
		 * NS_CStringToUTF16
		 *
		 * This function converts the characters in a nsACString to an array of UTF-16
		 * characters, in the platform endianness.  The result is stored in a nsAString
		 * object.
		 *
		 * @param aSource       abstract string reference containing source string
		 * @param aSrcEncoding  character encoding of the source string
		 * @param aDest         abstract string reference to hold the result
		 *
		 * @status FROZEN
		 */ 
		[DllImport(xpcom, ExactSpelling = true)]
		internal static extern UInt32 NS_CStringToUTF16(nsACString aSource, nsCStringEncoding aSrcEncoding, nsAString aDest);

		/**
		 * NS_UTF16ToCString
		 *
		 * This function converts the UTF-16 characters in a nsAString to a single-byte
		 * encoding.  The result is stored in a nsACString object.  In some cases this
		 * conversion may be lossy.  In such cases, the conversion may succeed with a
		 * return code indicating loss of information.  The exact behavior is not
		 * specified at this time.
		 *
		 * @param aSource       abstract string reference containing source string
		 * @param aDestEncoding character encoding of the resulting string
		 * @param aDest         abstract string reference to hold the result
		 *
		 * @status FROZEN
		 */ 
		[DllImport(xpcom, ExactSpelling = true)]
		internal static extern UInt32 NS_UTF16ToCString(nsAString aSource, nsCStringEncoding aDestEncoding, nsACString aDest);
	}
}
