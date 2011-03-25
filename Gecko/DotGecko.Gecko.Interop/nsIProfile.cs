using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	public static class nsIProfileConstants
	{
		public const UInt32 SHUTDOWN_PERSIST = 0x00000001;
		public const UInt32 SHUTDOWN_CLEANSE = 0x00000002;
	}

	/**
	 * nsIProfile
	 * 
	 * @version 1.0
	 */
	[ComImport, Guid("02b0625a-e7f3-11d2-9f5a-006008a6efe9"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIProfile //: nsISupports
	{
		Int32 ProfileCount { get; }
		[return: MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)]
		String[] GetProfileList(out UInt32 length);
		Boolean ProfileExists([MarshalAs(UnmanagedType.LPWStr)] String profileName);

		String CurrentProfile { [return: MarshalAs(UnmanagedType.LPWStr)] get; [param: MarshalAs(UnmanagedType.LPWStr)] set; }

		void ShutDownCurrentProfile(UInt32 shutDownType);

		void CreateNewProfile([MarshalAs(UnmanagedType.LPWStr)] String profileName,
							  [MarshalAs(UnmanagedType.LPWStr)] String nativeProfileDir,
							  [MarshalAs(UnmanagedType.LPWStr)] String langcode,
							  Boolean useExistingDir);

		void RenameProfile([MarshalAs(UnmanagedType.LPWStr)] String oldName, [MarshalAs(UnmanagedType.LPWStr)] String newName);
		void DeleteProfile([MarshalAs(UnmanagedType.LPWStr)] String name, Boolean canDeleteFiles);
		void CloneProfile([MarshalAs(UnmanagedType.LPWStr)] String profileName);
	}
}
