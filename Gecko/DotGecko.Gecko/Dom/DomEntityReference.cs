using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomEntityReference : DomNode
	{
		private DomEntityReference(nsIDOMEntityReference domEntityReference)
			: base(domEntityReference)
		{
			Debug.Assert(domEntityReference != null);
			m_DomEntityReference = domEntityReference;
		}

		internal static DomEntityReference Create(nsIDOMEntityReference domEntityReference)
		{
			return domEntityReference != null ? new DomEntityReference(domEntityReference) : null;
		}

		private readonly nsIDOMEntityReference m_DomEntityReference;
	}
}
