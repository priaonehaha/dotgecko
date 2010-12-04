using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIDOMDOMExceptionConstants
	{
		public const UInt16 INDEX_SIZE_ERR = 1;
		public const UInt16 DOMSTRING_SIZE_ERR = 2;
		public const UInt16 HIERARCHY_REQUEST_ERR = 3;
		public const UInt16 WRONG_DOCUMENT_ERR = 4;
		public const UInt16 INVALID_CHARACTER_ERR = 5;
		public const UInt16 NO_DATA_ALLOWED_ERR = 6;
		public const UInt16 NO_MODIFICATION_ALLOWED_ERR = 7;
		public const UInt16 NOT_FOUND_ERR = 8;
		public const UInt16 NOT_SUPPORTED_ERR = 9;
		public const UInt16 INUSE_ATTRIBUTE_ERR = 10;
		// Introduced in DOM Level 2:
		public const UInt16 INVALID_STATE_ERR = 11;
		// Introduced in DOM Level 2:
		public const UInt16 SYNTAX_ERR = 12;
		// Introduced in DOM Level 2:
		public const UInt16 INVALID_MODIFICATION_ERR = 13;
		// Introduced in DOM Level 2:
		public const UInt16 NAMESPACE_ERR = 14;
		// Introduced in DOM Level 2:
		public const UInt16 INVALID_ACCESS_ERR = 15;
		// Introduced in DOM Level 3:
		public const UInt16 VALIDATION_ERR = 16;
		// Introduced in DOM Level 3:
		public const UInt16 TYPE_MISMATCH_ERR = 17;
	}

	/**
	 * In general, DOM methods return specific error values in ordinary
	 * processing situations, such as out-of-bound errors.
	 * However, DOM operations can raise exceptions in "exceptional"
	 * circumstances, i.e., when an operation is impossible to perform
	 * (either for logical reasons, because data is lost, or because the
	 * implementation has become unstable)
	 *
	 * For more information on this interface please see
	 * http://www.w3.org/TR/DOM-Level-3-Core/
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("a6cf910a-15b3-11d2-932e-00805f8add32"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMDOMException //: nsISupports
	{
		UInt32 Code { get; }
	}
}
