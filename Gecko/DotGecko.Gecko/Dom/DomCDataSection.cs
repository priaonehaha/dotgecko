using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomCDataSection : DomText
	{
		private DomCDataSection(nsIDOMCDATASection domCDataSection)
			: base(domCDataSection)
		{
			Debug.Assert(domCDataSection != null);
			m_DomCDataSection = domCDataSection;
		}

		internal static DomCDataSection Create(nsIDOMCDATASection domCDataSection)
		{
			return domCDataSection != null ? new DomCDataSection(domCDataSection) : null;
		}

		private readonly nsIDOMCDATASection m_DomCDataSection;
	}
}
