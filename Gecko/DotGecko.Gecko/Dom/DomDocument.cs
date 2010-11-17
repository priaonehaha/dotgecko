using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomDocument : DomNode
	{
		private DomDocument(nsIDOMDocument domDocument)
			: base(domDocument)
		{
			Debug.Assert(domDocument != null);
			m_DomDocument = domDocument;
		}

		internal static DomDocument Create(nsIDOMDocument domDocument)
		{
			return domDocument != null ? new DomDocument(domDocument) : null;
		}

		public DomDocumentType Doctype
		{
			get
			{
				nsIDOMDocumentType domDocumentType = m_DomDocument.GetDoctype();
				return DomDocumentType.Create(domDocumentType);
			}
		}

		public DomDomImplementation Implementation
		{
			get
			{
				nsIDOMDOMImplementation domDomImplementation = m_DomDocument.GetImplementation();
				return DomDomImplementation.Create(domDomImplementation);
			}
		}

		public DomElement DocumentElement
		{
			get
			{
				nsIDOMElement domElement = m_DomDocument.GetDocumentElement();
				return DomElement.Create(domElement);
			}
		}

		public DomElement CreateElement(String tagName)
		{
			nsIDOMElement domElement = m_DomDocument.CreateElement(tagName);
			return DomElement.Create(domElement);
		}

		public DomDocumentFragment CreateDocumentFragment()
		{
			nsIDOMDocumentFragment domDocumentFragment = m_DomDocument.CreateDocumentFragment();
			return DomDocumentFragment.Create(domDocumentFragment);
		}

		public DomText CreateTextNode(String data)
		{
			nsIDOMText domText = m_DomDocument.CreateTextNode(data);
			return DomText.Create(domText);
		}

		public DomComment CreateComment(String data)
		{
			nsIDOMComment domComment = m_DomDocument.CreateComment(data);
			return DomComment.Create(domComment);
		}

		public DomCDataSection CreateCDATASection(String data)
		{
			nsIDOMCDATASection domCDataSection = m_DomDocument.CreateCDATASection(data);
			return DomCDataSection.Create(domCDataSection);
		}

		public DomProcessingInstruction CreateProcessingInstruction(String target, String data)
		{
			nsIDOMProcessingInstruction domProcessingInstruction = m_DomDocument.CreateProcessingInstruction(target, data);
			return DomProcessingInstruction.Create(domProcessingInstruction);
		}

		public DomAttribute CreateAttribute(String name)
		{
			nsIDOMAttr domAttribute = m_DomDocument.CreateAttribute(name);
			return DomAttribute.Create(domAttribute);
		}

		public DomEntityReference CreateEntityReference(String name)
		{
			nsIDOMEntityReference domEntityReference = m_DomDocument.CreateEntityReference(name);
			return DomEntityReference.Create(domEntityReference);
		}

		public DomNodeList GetElementsByTagName(String tagname)
		{
			nsIDOMNodeList domNodeList = m_DomDocument.GetElementsByTagName(tagname);
			return DomNodeList.Create(domNodeList);
		}

		public DomNode ImportNode(DomNode importedNode, Boolean deep)
		{
			nsIDOMNode domNode = m_DomDocument.ImportNode(importedNode.DomObj, deep);
			return DomNode.Create(domNode);
		}

		public DomElement CreateElementNS(String namespaceURI, String qualifiedName)
		{
			nsIDOMElement domElement = m_DomDocument.CreateElementNS(namespaceURI, qualifiedName);
			return DomElement.Create(domElement);
		}

		public DomAttribute CreateAttributeNS(String namespaceURI, String qualifiedName)
		{
			nsIDOMAttr domAttribute = m_DomDocument.CreateAttributeNS(namespaceURI, qualifiedName);
			return DomAttribute.Create(domAttribute);
		}

		public DomNodeList GetElementsByTagNameNS(String namespaceURI, String localName)
		{
			nsIDOMNodeList domNodeList = m_DomDocument.GetElementsByTagNameNS(namespaceURI, localName);
			return DomNodeList.Create(domNodeList);
		}

		public DomElement GetElementById(String elementId)
		{
			nsIDOMElement domElement = m_DomDocument.GetElementById(elementId);
			return DomElement.Create(domElement);
		}

		private readonly nsIDOMDocument m_DomDocument;
	}
}
