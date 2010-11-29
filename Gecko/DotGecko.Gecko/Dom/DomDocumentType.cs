using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomDocumentType : DomNode
	{
		private DomDocumentType(nsIDOMDocumentType domDocumentType)
			: base(domDocumentType)
		{
			Debug.Assert(domDocumentType != null);
			m_DomDocumentType = domDocumentType;
		}

		internal static DomDocumentType Create(nsIDOMDocumentType domDocumentType)
		{
			return domDocumentType != null ? new DomDocumentType(domDocumentType) : null;
		}

		public String Name { get { return XpcomStringHelper.Get(m_DomDocumentType.GetName); } }

		public DomNamedNodeMap Entities { get { return DomNamedNodeMap.Create(m_DomDocumentType.Entities); } }

		public DomNamedNodeMap Notations { get { return DomNamedNodeMap.Create(m_DomDocumentType.Notations); } }

		public String PublicId { get { return XpcomStringHelper.Get(m_DomDocumentType.GetPublicId); } }

		public String SystemId { get { return XpcomStringHelper.Get(m_DomDocumentType.GetSystemId); } }

		public String InternalSubset { get { return XpcomStringHelper.Get(m_DomDocumentType.GetInternalSubset); } }

		new internal nsIDOMDocumentType DomObj { get { return m_DomDocumentType; } }

		private readonly nsIDOMDocumentType m_DomDocumentType;
	}
}
