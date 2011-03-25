using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/*
	 * Interfaces for representing cross-language exceptions and stack traces.
	 */

	// XXX - most "string"s in this file should probably move to Unicode
	//       so may as well use AStrings...

	[ComImport, Guid("91d82105-7c62-4f8b-9779-154277c0ee90"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIStackFrame //: nsISupports
	{
		// see nsIProgrammingLanguage for list of language consts
		UInt32 Language { get; }
		String LanguageName { [return: MarshalAs(UnmanagedType.LPStr)] get; }
		String Filename { [return: MarshalAs(UnmanagedType.LPStr)] get; }
		String Name { [return: MarshalAs(UnmanagedType.LPStr)] get; }
		// Valid line numbers begin at '1'. '0' indicates unknown.
		Int32 LineNumber { get; }
		String SourceLine { [return: MarshalAs(UnmanagedType.LPStr)] get; }
		nsIStackFrame Caller { get; }

		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	[ComImport, Guid("F3A8D3B4-C424-4edc-8BF6-8974C983BA78"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIException //: nsISupports
	{
		// A custom message set by the thrower.
		String Message { [return: MarshalAs(UnmanagedType.LPStr)] get; }
		// The nsresult associated with this exception.
		nsResult Result { [return: MarshalAs(UnmanagedType.U4)] get; }
		// The name of the error code (ie, a string repr of |result|)
		String Name { [return: MarshalAs(UnmanagedType.LPStr)] get; }

		// Filename location.  This is the location that caused the
		// error, which may or may not be a source file location.
		// For example, standard language errors would generally have
		// the same location as their top stack entry.  File
		// parsers may put the location of the file they were parsing,
		// etc.

		// null indicates "no data"
		String Filename { [return: MarshalAs(UnmanagedType.LPStr)] get; }
		// Valid line numbers begin at '1'. '0' indicates unknown.
		UInt32 LineNumber { get; }
		// Valid column numbers begin at 0. 
		// We don't have an unambiguous indicator for unknown.
		UInt32 ColumnNumber { get; }

		// A stack trace, if available.
		nsIStackFrame Location { get; }
		// An inner exception that triggered this, if available.
		nsIException Inner { get; }

		// Arbitary data for the implementation.
		nsISupports Data { [return: MarshalAs(UnmanagedType.IUnknown)] get; }

		// A generic formatter - make it suitable to print, etc.
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}
}
