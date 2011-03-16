using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("0F78DA58-8321-11d2-8EAC-00805F29F370"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIRDFDataSource //: nsISupports
	{
		/** The "URI" of the data source. This used by the RDF service's
		 * |GetDataSource()| method to cache datasources.
		 */
		String URI { [return: MarshalAs(UnmanagedType.LPStr)] get; }

		/** Find an RDF resource that points to a given node over the
		 * specified arc & truth value
		 *
		 * @return NS_RDF_NO_VALUE if there is no source that leads
		 * to the target with the specified property.
		 */
		nsIRDFResource GetSource(nsIRDFResource aProperty,
								 nsIRDFNode aTarget,
								 Boolean aTruthValue);

		/**
		 * Find all RDF resources that point to a given node over the
		 * specified arc & truth value
		 *
		 * @return NS_OK unless a catastrophic error occurs. If the
		 * method returns NS_OK, you may assume that nsISimpleEnumerator points
		 * to a valid (but possibly empty) cursor.
		 */
		nsISimpleEnumerator GetSources(nsIRDFResource aProperty,
									   nsIRDFNode aTarget,
									   Boolean aTruthValue);

		/**
		 * Find a child of that is related to the source by the given arc
		 * arc and truth value
		 *
		 * @return NS_RDF_NO_VALUE if there is no target accessible from the
		 * source via the specified property.
		 */
		nsIRDFNode GetTarget(nsIRDFResource aSource,
							 nsIRDFResource aProperty,
							 Boolean aTruthValue);

		/**
		 * Find all children of that are related to the source by the given arc
		 * arc and truth value.
		 *
		 * @return NS_OK unless a catastrophic error occurs. If the
		 * method returns NS_OK, you may assume that nsISimpleEnumerator points
		 * to a valid (but possibly empty) cursor.
		 */
		nsISimpleEnumerator GetTargets(nsIRDFResource aSource,
									   nsIRDFResource aProperty,
									   Boolean aTruthValue);

		/**
		 * Add an assertion to the graph.
		 */
		void Assert(nsIRDFResource aSource,
					nsIRDFResource aProperty,
					nsIRDFNode aTarget,
					Boolean aTruthValue);

		/**
		 * Remove an assertion from the graph.
		 */
		void Unassert(nsIRDFResource aSource,
					  nsIRDFResource aProperty,
					  nsIRDFNode aTarget);

		/**
		 * Change an assertion from
		 *
		 *   [aSource]--[aProperty]-->[aOldTarget]
		 *
		 * to
		 * 
		 *   [aSource]--[aProperty]-->[aNewTarget]
		 */
		void Change(nsIRDFResource aSource,
					nsIRDFResource aProperty,
					nsIRDFNode aOldTarget,
					nsIRDFNode aNewTarget);

		/**
		 * 'Move' an assertion from
		 *
		 *   [aOldSource]--[aProperty]-->[aTarget]
		 *
		 * to
		 * 
		 *   [aNewSource]--[aProperty]-->[aTarget]
		 */
		void Move(nsIRDFResource aOldSource,
				  nsIRDFResource aNewSource,
				  nsIRDFResource aProperty,
				  nsIRDFNode aTarget);

		/**
		 * Query whether an assertion exists in this graph.
		 */
		Boolean HasAssertion(nsIRDFResource aSource,
							 nsIRDFResource aProperty,
							 nsIRDFNode aTarget,
							 Boolean aTruthValue);

		/**
		 * Add an observer to this data source. If the datasource
		 * supports observers, the datasource source should hold a strong
		 * reference to the observer.
		 */
		void AddObserver(nsIRDFObserver aObserver);

		/**
		 * Remove an observer from this data source.
		 */
		void RemoveObserver(nsIRDFObserver aObserver);

		/**
		 * Get a cursor to iterate over all the arcs that point into a node.
		 *
		 * @return NS_OK unless a catastrophic error occurs. If the method
		 * returns NS_OK, you may assume that labels points to a valid (but
		 * possible empty) nsISimpleEnumerator object.
		 */
		nsISimpleEnumerator ArcLabelsIn(nsIRDFNode aNode);

		/**
		 * Get a cursor to iterate over all the arcs that originate in
		 * a resource.
		 *
		 * @return NS_OK unless a catastrophic error occurs. If the method
		 * returns NS_OK, you may assume that labels points to a valid (but
		 * possible empty) nsISimpleEnumerator object.
		 */
		nsISimpleEnumerator ArcLabelsOut(nsIRDFResource aSource);

		/**
		 * Retrieve all of the resources that the data source currently
		 * refers to.
		 */
		nsISimpleEnumerator GetAllResources();

		/**
		 * Returns whether a given command is enabled for a set of sources. 
		 */
		Boolean IsCommandEnabled(nsISupportsArray aSources,
								 nsIRDFResource aCommand,
								 nsISupportsArray aArguments);

		/**
		 * Perform the specified command on set of sources.
		 */
		void DoCommand(nsISupportsArray aSources,
					   nsIRDFResource aCommand,
					   nsISupportsArray aArguments);

		/**
		 * Returns the set of all commands defined for a given source.
		 */
		nsISimpleEnumerator GetAllCmds(nsIRDFResource aSource);

		/**
		 * Returns true if the specified node is pointed to by the specified arc.
		 * Equivalent to enumerating ArcLabelsIn and comparing for the specified arc.
		 */
		Boolean HasArcIn(nsIRDFNode aNode, nsIRDFResource aArc);

		/**
		 * Returns true if the specified node has the specified outward arc.
		 * Equivalent to enumerating ArcLabelsOut and comparing for the specified arc.
		 */
		Boolean HasArcOut(nsIRDFResource aSource, nsIRDFResource aArc);

		/**
		 * Notify observers that the datasource is about to send several
		 * notifications at once.
		 * This must be followed by calling endUpdateBatch(), otherwise
		 * viewers will get out of sync.
		 */
		void BeginUpdateBatch();

		/**
		 * Notify observers that the datasource has completed issuing
		 * a notification group.
		 */
		void EndUpdateBatch();
	}
}
