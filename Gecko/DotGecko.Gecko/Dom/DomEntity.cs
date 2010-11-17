using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomEntity : DomNode
	{
		private DomEntity(nsIDOMEntity domEntity)
			: base(domEntity)
		{
			Debug.Assert(domEntity != null);
			m_DomEntity = domEntity;
		}

		internal static DomEntity Create(nsIDOMEntity domEntity)
		{
			return domEntity != null ? new DomEntity(domEntity) : null;
		}

		public String PublicId { get { return XpcomString.Get(m_DomEntity.GetPublicId); } }

		public String SystemId { get { return XpcomString.Get(m_DomEntity.GetSystemId); } }

		public String NotationName { get { return XpcomString.Get(m_DomEntity.GetNotationName); } }

		private readonly nsIDOMEntity m_DomEntity;
	}
}
