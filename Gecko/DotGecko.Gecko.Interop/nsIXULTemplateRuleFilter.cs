using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * A rule filter may be used to add additional filtering of results to a rule.
	 * The filter is used to further reject results from matching the template's
	 * rules, beyond what the template syntax can do itself, thus allowing for
	 * more complex result filtering. The rule filter is applied after the rule
	 * syntax within the template.
	 *
	 * Only one filter may apply to each rule within the template and may be
	 * assigned using the template builder's addRuleFilter method.
	 */
	[ComImport, Guid("819cd1ed-8010-42e1-a8b9-778b726a1ff3"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXULTemplateRuleFilter //: nsISupports
	{
		/**
		 * Evaluate a result and return true if the result is accepted by this
		 * filter, or false if it is rejected. Accepted results will have output
		 * generated for them for the rule. Rejected results will not, but they
		 * may still match another rule.
		 *
		 * @param aRef the result to examine
		 * @param aRule the rule node
		 *
		 * @return true if the rule matches
		 */
		Boolean Match(nsIXULTemplateResult aRef, nsIDOMNode aRule);
	}
}
