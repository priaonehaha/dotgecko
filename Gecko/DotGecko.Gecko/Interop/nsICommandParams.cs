using System;
using System.Runtime.InteropServices;
using System.Text;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	internal static class nsICommandParamsConstants
	{
		/*
		 * List of primitive types for parameter values.
		 */
		internal const Int16 eNoType = 0;      /* Only used for sanity checking */
		internal const Int16 eBooleanType = 1;
		internal const Int16 eLongType = 2;
		internal const Int16 eDoubleType = 3;
		internal const Int16 eWStringType = 4;
		internal const Int16 eISupportsType = 5;
		internal const Int16 eStringType = 6;
	}

	/*
	 * nsICommandParams is used to pass parameters to commands executed
	 * via nsICommandManager, and to get command state.
	 *
	 */
	[ComImport, Guid("83f892cf-7ed3-490e-967a-62640f3158e1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsICommandParams //: nsISupports
	{
		/*
		 * getValueType
		 * 
		 * Get the type of a specified parameter
		 */
		Int16 GetValueType([MarshalAs(UnmanagedType.LPStr)] String name);

		/*
		 * get_Value
		 * 
		 * Get the value of a specified parameter. Will return
		 * an error if the parameter does not exist, or if the value
		 * is of the wrong type (no coercion is performed for you).
		 * 
		 * nsISupports values can contain any XPCOM interface,
		 * as documented for the command. It is permissible
		 * for it to contain nsICommandParams, but not *this*
		 * one (i.e. self-containing is not allowed).
		 */
		Boolean GetBooleanValue([MarshalAs(UnmanagedType.LPStr)] String name);
		Int32 GetLongValue([MarshalAs(UnmanagedType.LPStr)] String name);
		Double GetDoubleValue([MarshalAs(UnmanagedType.LPStr)] String name);
		void GetStringValue([MarshalAs(UnmanagedType.LPStr)] String name, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		[return: MarshalAs(UnmanagedType.LPStr)]
		String GetCStringValue([MarshalAs(UnmanagedType.LPStr)] String name);
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports GetISupportsValue([MarshalAs(UnmanagedType.LPStr)] String name);

		/*
		 * set_Value
		 * 
		 * Set the value of a specified parameter (thus creating
		 * an entry for it).
		 * 
		 * nsISupports values can contain any XPCOM interface,
		 * as documented for the command. It is permissible
		 * for it to contain nsICommandParams, but not *this*
		 * one (i.e. self-containing is not allowed).
		 */
		void SetBooleanValue([MarshalAs(UnmanagedType.LPStr)] String name, Boolean value);
		void SetLongValue([MarshalAs(UnmanagedType.LPStr)] String name, Int32 value);
		void SetDoubleValue([MarshalAs(UnmanagedType.LPStr)] String name, Double value);
		void SetStringValue([MarshalAs(UnmanagedType.LPStr)] String name, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
		void SetCStringValue([MarshalAs(UnmanagedType.LPStr)] String name, [MarshalAs(UnmanagedType.LPStr)] String value);
		void SetISupportsValue([MarshalAs(UnmanagedType.LPStr)] String name, [MarshalAs(UnmanagedType.IUnknown)] nsISupports value);

		/*
		 * removeValue
		 * 
		 * Remove the specified parameter from the list.
		 */
		void RemoveValue([MarshalAs(UnmanagedType.LPStr)] String name);

		/*
		 * Enumeration methods
		 * 
		 * Use these to enumerate over the contents of a parameter
		 * list. For each name that getNext() returns, use 
		 * getValueType() and then getMumbleValue to get its
		 * value.
		 */
		Boolean HasMoreElements();

		void First();

		/**
		 * GetNext()
		 * 
		 * @return string pointer that will be allocated and is up 
		 *         to the caller to free
		 */
		[return: MarshalAs(UnmanagedType.LPStr)]
		String GetNext();
	}
}
