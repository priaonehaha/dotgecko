using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomDocumentFragment : DomNode
	{
		private DomDocumentFragment(nsIDOMDocumentFragment domDocumentFragment)
			: base(domDocumentFragment)
		{
			Debug.Assert(domDocumentFragment != null);
			m_DomDocumentFragment = domDocumentFragment;
		}

		internal static DomDocumentFragment Create(nsIDOMDocumentFragment domDocumentFragment)
		{
			return domDocumentFragment != null ? new DomDocumentFragment(domDocumentFragment) : null;
		}

		private readonly nsIDOMDocumentFragment m_DomDocumentFragment;
	}
}
