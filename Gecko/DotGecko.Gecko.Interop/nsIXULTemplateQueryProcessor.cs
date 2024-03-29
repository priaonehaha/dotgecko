using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * A query processor takes a template query and generates results for it given
	 * a datasource and a reference point. There is a one-to-one relationship
	 * between a template builder and a query processor. The template builder
	 * creates the query processor, and there is no other means to retrieve it.
	 *
	 * A template query is the contents inside a <query> element within the
	 * template. The actual syntax is opaque to the template builder and defined
	 * by a query processor. The query is expected to consist of either text or
	 * DOM nodes that, when executed by a call to the generateResults method, will
	 * allow the generation of a list of results.
	 *
	 * The template builder will supply two variables, the reference variable and
	 * the member variable to further indicate what part of the datasource is to
	 * be examined in addition to the query itself. The reference is always
	 * a placeholder for the starting point and the member is always a placeholder
	 * for the end points (the results).
	 *
	 * The reference point is important when generating output recursively, as
	 * the query will be the same for each iteration, however, the reference point
	 * will differ.
	 *
	 * For instance, when examining an XML source, an XML query processor might
	 * begin at the node referred by the reference variable and end at a list of
	 * that node's children.
	 *
	 * Some queries may not need the reference variable if the syntax or the form
	 * of the data implies the value. For instance, a datasource that holds a
	 * table that can only produce one set of results.
	 *
	 * The reference variable may be specified in a template by setting the
	 * "container" attribute on the <template> element to the variable to use. The
	 * member variable may be specified in a similar way using the "member"
	 * attribute, or it may be specified in the first <action> body in the
	 * template as the value of a uri attribute on an element. A breadth-first
	 * search of the first action is performed to find this element.
	 *
	 * If unspecified, the default value of the reference variable is ?uri.
	 *
	 * For example, a query might have the following syntax:
	 *
	 * (?id, ?name, ?url) from Bookmarks where parentfolder = ?start
	 *
	 * This query might generate a result for each bookmark within a given folder.
	 * The variable ?start would be the reference variable, while the variable ?id
	 * would be the member variable, since it is the unique value that identifies
	 * a result. Each result will have the four variables referred to defined for
	 * it and the values may be retrieved using the result's getBindingFor and
	 * getBindingObjectFor methods.
	 *
	 * The template builder must call initializeForBuilding before the other
	 * methods, except for translateRef. The builder will then call compileQuery
	 * for each query in the template to compile the queries. When results need
	 * to be generated, the builder will call generateResults. The
	 * initializeForBuilding, compileQuery and addBinding methods may not be
	 * called after generateResults has been called until the builder indicates
	 * that the generated output is being removed by calling the done method.
	 *
	 * Currently, the datasource supplied to the methods will always be an
	 * nsIRDFDataSource or a DOM node, and will always be the same one in between
	 * calls to initializeForBuilding and done.
	 */
	[ComImport, Guid("C257573F-444F-468A-BA27-DE979DC55FE4"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXULTemplateQueryProcessor //: nsISupports
	{
		/**
		 * Retrieve the datasource to use for the query processor. The list of
		 * datasources in a template is specified using the datasources attribute as
		 * a space separated list of URIs. This list is processed by the builder and
		 * supplied to the query processor in the aDataSources array as a list of
		 * nsIURI objects or nsIDOMNode objects. This method may return an object
		 * corresponding to these URIs and the builder will supply this object to
		 * other query processor methods. For example, for an XML source, the
		 * datasource might be an nsIDOMNode.
		 *
		 * All of these URIs are checked by the builder so it is safe to use them,
		 * however note that a URI that redirects may still needs to be checked to
		 * ensure that the document containing aRootNode may access it. This is the
		 * responsibility of the query processor if it needs to load the content of
		 * the URI.
		 *
		 * If the query processor needs to load the datasource asynchronously, it
		 * may set the aShouldDelayBuilding returned parameter to true to delay
		 * building the template content, and call the builder's Rebuild method when
		 * the data is available.
		 *
		 * @param aDataSources  the list of nsIURI objects and/or nsIDOMNode objects
		 * @param aRootNode     the root node the builder is attached to
		 * @param aIsTrusted    true if the template is in a trusted document
		 * @param aBuilder      the template builder
		 * @param aShouldDelayBuilding [out] whether the builder should wait to
		 *                                   build the content or not
		 * @returns a datasource object
		 */
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports GetDatasource(nsIArray aDataSources,
								  nsIDOMNode aRootNode,
								  Boolean aIsTrusted,
								  nsIXULTemplateBuilder aBuilder,
								  out Boolean aShouldDelayBuilding);

		/**
		 * Initialize for query generation. This will be called before the rules are
		 * processed and whenever the template is rebuilt. This method must be
		 * called once before any of the other query processor methods except for
		 * translateRef.
		 *
		 * @param aDatasource datasource for the data
		 * @param aBuilder the template builder
		 * @param aRootNode the root node the builder is attached to
		 *
		 * @throws NS_ERROR_INVALID_ARG if the datasource is not supported or
		 *         NS_ERROR_UNEXPECTED if generateResults has already been called.
		 */
		void InitializeForBuilding([MarshalAs(UnmanagedType.IUnknown)] nsISupports aDatasource,
								   nsIXULTemplateBuilder aBuilder,
								   nsIDOMNode aRootNode);

		/**
		 * Called when the template builder is being destroyed so that the query
		 * processor can clean up any state. The query processor should remove as
		 * much state as possible, such as results or references to the builder.
		 * This method will also be called when the template is going to be rebuilt.
		 */
		void Done();

		/**
		 * Compile a query from a node. The result of this function will later be
		 * passed to generateResults for result generation. If null is returned,
		 * the query will be ignored.
		 *
		 * The template builder will call this method once for each query within
		 * the template, before any results can be generated using generateResults,
		 * but after initializeForBuilding has been called. This method should not
		 * be called again for the same query unless the template is rebuilt.
		 *
		 * The reference variable may be used by the query processor as a
		 * placeholder for the reference point, or starting point in the query.
		 *
		 * The member variable is determined from the member attribute on the
		 * template, or from the uri in the first action's rule if that attribute is
		 * not present. A rule processor may use the member variable as a hint to
		 * indicate what variable is expected to contain the results.
		 *
		 * @param aBuilder the template builder
		 * @param aQuery <query> node to compile
		 * @param aRefVariable the reference variable
		 * @param aMemberVariable the member variable
		 *
		 * @returns a compiled query object
		 */
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports CompileQuery(nsIXULTemplateBuilder aBuilder,
								 nsIDOMNode aQuery,
								 nsIAtom aRefVariable,
								 nsIAtom aMemberVariable);

		/**
		 * Generate the results of a query and return them in an enumerator. The
		 * enumerator must contain nsIXULTemplateResult objects. If there are no
		 * results, an empty enumerator must be returned.
		 *
		 * The datasource will be the same as the one passed to the earlier
		 * initializeForBuilding method. The context reference (aRef) is a reference
		 * point used when calculating results.
		 *
		 * The value of aQuery must be the result of a previous call to compileQuery
		 * from this query processor. This method may be called multiple times,
		 * typically with different values for aRef.
		 *
		 * @param aDatasource datasource for the data
		 * @param aRef context reference value used as a starting point
		 * @param aQuery the compiled query returned from query compilation
		 *
		 * @returns an enumerator of nsIXULTemplateResult objects as the results
		 *
		 * @throws NS_ERROR_INVALID_ARG if aQuery is invalid
		 */
		nsISimpleEnumerator GenerateResults([MarshalAs(UnmanagedType.IUnknown)] nsISupports aDatasource,
											nsIXULTemplateResult aRef,
											[MarshalAs(UnmanagedType.IUnknown)] nsISupports aQuery);

		/**
		 * Add a variable binding for a particular rule. A binding allows an
		 * additional variable to be set for a result, outside of those defined
		 * within the query. These bindings are always optional, in that they will
		 * never affect the results generated.
		 *
		 * This function will never be called after generateResults. Any bindings
		 * that were added should be applied to each result when the result's
		 * ruleMatched method is called, since the bindings are different for each
		 * rule.
		 *
		 * The reference aRef may be used to determine the reference when
		 * calculating the value for the binding, for example when a value should
		 * depend on the value of another variable.
		 *
		 * The syntax of the expression aExpr is defined by the query processor. If
		 * the syntax is invalid, the binding should be ignored. Only fatal errors
		 * should be thrown, or NS_ERROR_UNEXPECTED if generateResults has already
		 * been called.
		 *
		 * As an example, if the reference aRef is the variable '?count' which
		 * holds the value 5, and the expression aExpr is the string '+2', the value
		 * of the variable aVar would be 7, assuming the query processor considers
		 * the syntax '+2' to mean add two to the reference.
		 *
		 * @param aRuleNode rule to add the binding to
		 * @param aVar variable that will be bound
		 * @param aRef variable that holds reference value
		 * @param aExpr expression used to compute the value to assign
		 */
		void AddBinding(nsIDOMNode aRuleNode,
						nsIAtom aVar,
						nsIAtom aRef,
						[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aExpr);

		/**
		 * Translate a ref attribute string into a result. This is used as the
		 * reference point by the template builder when generating the first level
		 * of content. For recursive generation, the result from the parent
		 * generation phase will be used directly as the reference so a translation
		 * is not needed. This allows all levels to be generated using objects that
		 * all implement the nsIXULTemplateResult interface.
		 *
		 * This method may be called before initializeForBuilding, so the
		 * implementation may use the supplied datasource if it is needed to
		 * translate the reference.
		 *
		 * @param aDatasource datasource for the data
		 * @param aRefString the ref attribute string
		 *
		 * @return the translated ref
		 */
		nsIXULTemplateResult TranslateRef([MarshalAs(UnmanagedType.IUnknown)] nsISupports aDatasource,
										  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aRefString);

		/**
		 * Compare two results to determine their order, used when sorting results.
		 * This method should return -1 when the left result is less than the right,
		 * 0 if both are equivalent, and 1 if the left is greater than the right.
		 * The comparison should only consider the values for the specified
		 * variable.
		 *
		 * If the comparison variable is null, the results may be
		 * sorted in a natural order, for instance, based on the order the data in
		 * stored in the datasource.
		 *
		 * The sort hints are the flags in nsIXULSortService.
		 *
		 * This method must only be called with results that were created by this
		 * query processor.
		 *
		 * @param aLeft the left result to compare
		 * @param aRight the right result to compare
		 * @param aVar variable to compare
		 *
		 * @param returns -1 if less, 0 if equal, or 1 if greater
		 */
		Int32 CompareResults(nsIXULTemplateResult aLeft,
							 nsIXULTemplateResult aRight,
							 nsIAtom aVar,
							 UInt32 aSortHints);
	}
}
