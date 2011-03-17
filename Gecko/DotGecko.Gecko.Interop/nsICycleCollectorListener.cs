using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * Interface to pass to the cycle collector to get information about the CC
	 * graph while it's being built. The order of calls will be call to begin();
	 * then for every node in the graph a call to noteObject() and calls to
	 * noteEdge() for every edge starting at that node; then a call to
	 * beginDescriptions(); then for every black node in the CC graph a call to
	 * either describeRefcountedObject() or to describeGCedObject(); and then a
	 * call to end(). If begin() returns an error none of the other functions will
	 * be called.
	 */
	[ComImport, Guid("194b749a-4ceb-4dd1-928d-d30b5f14c23e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsICycleCollectorListener //: nsISupports
	{
		void Begin();
		void NoteObject(UInt64 aAddress,
						[MarshalAs(UnmanagedType.LPStr)] String aObjectDescription);
		void NoteEdge(UInt64 aFromAddress,
					  UInt64 aToAddress,
					  [MarshalAs(UnmanagedType.LPStr)] String aEdgeName);
		void BeginDescriptions();
		void DescribeRefcountedObject(UInt64 aAddress,
									  UInt32 aKnownEdges,
									  UInt32 aTotalEdges);
		void DescribeGCedObject(UInt64 aAddress, Boolean aMarked);
		void End();
	}
}
