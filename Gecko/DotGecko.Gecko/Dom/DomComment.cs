using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomComment : DomCharacterData
	{
		private DomComment(nsIDOMComment domComment)
			: base(domComment)
		{
			Debug.Assert(domComment != null);
			m_DomComment = domComment;
		}

		internal static DomComment Create(nsIDOMComment domComment)
		{
			return domComment != null ? new DomComment(domComment) : null;
		}

		private readonly nsIDOMComment m_DomComment;
	}
}
