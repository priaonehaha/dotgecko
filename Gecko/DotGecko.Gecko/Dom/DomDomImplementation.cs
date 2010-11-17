using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomDomImplementation
	{
		private DomDomImplementation(nsIDOMDOMImplementation domDomImplementation)
		{
			Debug.Assert(domDomImplementation != null);
			m_DomDomImplementation = domDomImplementation;
		}

		internal static DomDomImplementation Create(nsIDOMDOMImplementation domDomImplementation)
		{
			return domDomImplementation != null ? new DomDomImplementation(domDomImplementation) : null;
		}

		public Boolean HasFeature(String feature, String version)
		{
			return m_DomDomImplementation.HasFeature(feature, version);
		}

		public DomDocumentType CreateDocumentType(String qualifiedName, String publicId, String systemId)
		{
			nsIDOMDocumentType domDocumentType = m_DomDomImplementation.CreateDocumentType(qualifiedName, publicId, systemId);
			return DomDocumentType.Create(domDocumentType);
		}

		public DomDocument CreateDocument(String namespaceURI, String qualifiedName, DomDocumentType doctype)
		{
			nsIDOMDocument domDocument = m_DomDomImplementation.CreateDocument(namespaceURI, qualifiedName, doctype.DomObj);
			return DomDocument.Create(domDocument);
		}

		private readonly nsIDOMDOMImplementation m_DomDomImplementation;
	}
}
