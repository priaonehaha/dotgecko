using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("91cca981-c26d-44a8-bebe-d9ed4891503a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsISerializable //: nsISupports
	{
		/**
		 * Initialize the object implementing nsISerializable, which must have
		 * been freshly constructed via CreateInstance.  All data members that
		 * can't be set to default values must have been serialized by write,
		 * and should be read from aInputStream in the same order by this method.
		 */
		void Read(nsIObjectInputStream aInputStream);

		/**
		 * Serialize the object implementing nsISerializable to aOutputStream, by
		 * writing each data member that must be recovered later to reconstitute
		 * a working replica of this object, in a canonical member and byte order,
		 * to aOutputStream.
		 *
		 * NB: a class that implements nsISerializable *must* also implement
		 * nsIClassInfo, in particular nsIClassInfo::GetClassID.
		 */
		void Write(nsIObjectOutputStream aOutputStream);
	}
}
