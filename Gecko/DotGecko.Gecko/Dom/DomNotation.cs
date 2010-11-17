using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomNotation : DomNode
	{
		private DomNotation(nsIDOMNotation domNotation)
			: base(domNotation)
		{
			Debug.Assert(domNotation != null);
			m_DomNotation = domNotation;
		}

		internal static DomNotation Create(nsIDOMNotation domNotation)
		{
			return domNotation != null ? new DomNotation(domNotation) : null;
		}

		public String PublicId { get { return XpcomString.Get(m_DomNotation.GetPublicId); } }

		public String SystemId { get { return XpcomString.Get(m_DomNotation.GetSystemId); } }

		private readonly nsIDOMNotation m_DomNotation;
	}
}
