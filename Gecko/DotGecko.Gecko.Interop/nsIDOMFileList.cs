using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("3bfef9fa-8ad3-4e49-bd62-d6cd75b29298"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMFileList //: nsISupports
	{
		UInt32 Length { get; }
		nsIDOMFile Item(UInt32 index);
	}
}
