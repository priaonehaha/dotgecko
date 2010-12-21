using System;
using System.Runtime.InteropServices;
using System.Text;
using nsISupports = System.Object;
using nsQIResult = System.Object;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("283EE646-1AEF-11D4-98B3-00C04fA0CE9A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIPropertyElement //: nsISupports
	{
		void GetKey([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void SetKey([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String value);
		void GetValue([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		void SetValue([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
	}

	[ComImport, Guid("1A180F60-93B2-11d2-9B8B-00805F8A16D9"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIPersistentProperties : nsIProperties
	{
		#region nsIProperties Members

		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		new nsResult Get([MarshalAs(UnmanagedType.LPStr)] String prop, [In] ref Guid iid, [MarshalAs(UnmanagedType.Interface, IidParameterIndex = 1)] out nsQIResult result);
		new void Set([MarshalAs(UnmanagedType.LPStr)] String prop, [MarshalAs(UnmanagedType.IUnknown)] nsISupports value);
		new Boolean Has([MarshalAs(UnmanagedType.LPStr)] String prop);
		new void Undefine([MarshalAs(UnmanagedType.LPStr)] String prop);
		[return: MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 0)]
		new String[] GetKeys(out UInt32 count);

		#endregion

		/**
		 * load a set of name/value pairs from the input stream
		 * names and values should be in UTF8
		 */
		void Load(nsIInputStream input);

		/**
		 * output the values to the stream - results will be in UTF8
		 */
		void Save(nsIOutputStream output, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String header);

		/**
		 * call subclass() to make future calls to load() set the properties
		 * in this "superclass" instead
		 */
		void Subclass(nsIPersistentProperties superclass);

		/**
		 * get an enumeration of nsIPropertyElement objects,
		 * which are read-only (i.e. setting properties on the element will
		 * not make changes back into the source nsIPersistentProperties
		 */
		nsISimpleEnumerator Enumerate();

		/**
		 * shortcut to nsIProperty's get() which retrieves a string value
		 * directly (and thus faster)
		 */
		void GetStringProperty(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String key,
			[In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		/**
		 * shortcut to nsIProperty's set() which sets a string value
		 * directly (and thus faster). If the given property already exists,
		 * then the old value will be returned
		 */
		void SetStringProperty(
		  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String key,
		  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value,
		  [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
	}
}
