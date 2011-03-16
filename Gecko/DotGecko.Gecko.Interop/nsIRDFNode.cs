using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	// An nsIRDFNode object is an abstract placeholder for a node in the
	// RDF data model. Its concreted implementations (e.g., nsIRDFResource
	// or nsIRDFLiteral) are the actual objects that populate the graph.
	[ComImport, Guid("0F78DA50-8321-11d2-8EAC-00805F29F370"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIRDFNode //: nsISupports
	{
		// Determine if two nodes are identical
		Boolean EqualsNode(nsIRDFNode aNode);
	}
}
