using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * An nsIRDFCompositeDataSource composes individual data sources, providing
	 * the illusion of a single, coherent RDF graph.
	 */
	[ComImport, Guid("96343820-307C-11D2-BC15-00805F912FE7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIRDFCompositeDataSource : nsIRDFDataSource
	{
		#region nsIRDFDataSource Members

		new String URI { [return: MarshalAs(UnmanagedType.LPStr)] get; }
		new nsIRDFResource GetSource(nsIRDFResource aProperty, nsIRDFNode aTarget, Boolean aTruthValue);
		new nsISimpleEnumerator GetSources(nsIRDFResource aProperty, nsIRDFNode aTarget, Boolean aTruthValue);
		new nsIRDFNode GetTarget(nsIRDFResource aSource, nsIRDFResource aProperty, Boolean aTruthValue);
		new nsISimpleEnumerator GetTargets(nsIRDFResource aSource, nsIRDFResource aProperty, Boolean aTruthValue);
		new void Assert(nsIRDFResource aSource, nsIRDFResource aProperty, nsIRDFNode aTarget, Boolean aTruthValue);
		new void Unassert(nsIRDFResource aSource, nsIRDFResource aProperty, nsIRDFNode aTarget);
		new void Change(nsIRDFResource aSource, nsIRDFResource aProperty, nsIRDFNode aOldTarget, nsIRDFNode aNewTarget);
		new void Move(nsIRDFResource aOldSource, nsIRDFResource aNewSource, nsIRDFResource aProperty, nsIRDFNode aTarget);
		new Boolean HasAssertion(nsIRDFResource aSource, nsIRDFResource aProperty, nsIRDFNode aTarget, Boolean aTruthValue);
		new void AddObserver(nsIRDFObserver aObserver);
		new void RemoveObserver(nsIRDFObserver aObserver);
		new nsISimpleEnumerator ArcLabelsIn(nsIRDFNode aNode);
		new nsISimpleEnumerator ArcLabelsOut(nsIRDFResource aSource);
		new nsISimpleEnumerator GetAllResources();
		new Boolean IsCommandEnabled(nsISupportsArray aSources, nsIRDFResource aCommand, nsISupportsArray aArguments);
		new void DoCommand(nsISupportsArray aSources, nsIRDFResource aCommand, nsISupportsArray aArguments);
		new nsISimpleEnumerator GetAllCmds(nsIRDFResource aSource);
		new Boolean HasArcIn(nsIRDFNode aNode, nsIRDFResource aArc);
		new Boolean HasArcOut(nsIRDFResource aSource, nsIRDFResource aArc);
		new void BeginUpdateBatch();
		new void EndUpdateBatch();

		#endregion

		/**
		 *
		 * Set this value to <code>true</code> if the composite datasource
		 * may contains at least one datasource that has <em>negative</em>
		 * assertions. (This is the default.)
		 *
		 * Set this value to <code>false</code> if none of the datasources
		 * being composed contains a negative assertion. This allows the
		 * composite datasource to perform some query optimizations.
		 *
		 * By default, this value is <code>true</true>.
		 */
		Boolean AllowNegativeAssertions { get; set; }

		/**
		 * Set to <code>true</code> if the composite datasource should
		 * take care to coalesce duplicate arcs when returning values from
		 * queries. (This is the default.)
		 *
		 * Set to <code>false</code> if the composite datasource shouldn't
		 * bother to check for duplicates. This allows the composite
		 * datasource to more efficiently answer queries.
		 *
		 * By default, this value is <code>true</code>.
		 */
		Boolean CoalesceDuplicateArcs { get; set; }

		/**
		 * Add a datasource the the composite data source.
		 * @param aDataSource the datasource to add to composite
		 */
		void AddDataSource(nsIRDFDataSource aDataSource);

		/**
		 * Remove a datasource from the composite data source.
		 * @param aDataSource the datasource to remove from the composite
		 */
		void RemoveDataSource(nsIRDFDataSource aDataSource);

		/**
		 * Retrieve the datasources in the composite data source.
		 * @return an nsISimpleEnumerator that will enumerate each
		 * of the datasources in the composite
		 */
		nsISimpleEnumerator GetDataSources();
	}
}
