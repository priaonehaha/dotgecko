using System;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public enum DomNodeType : ushort
	{
		Element = nsIDOMNodeConstants.ELEMENT_NODE,
		Attribute = nsIDOMNodeConstants.ATTRIBUTE_NODE,
		Text = nsIDOMNodeConstants.TEXT_NODE,
		CDataSection = nsIDOMNodeConstants.CDATA_SECTION_NODE,
		EntityReference = nsIDOMNodeConstants.ENTITY_REFERENCE_NODE,
		Entity = nsIDOMNodeConstants.ENTITY_NODE,
		ProcessingInstruction = nsIDOMNodeConstants.PROCESSING_INSTRUCTION_NODE,
		Comment = nsIDOMNodeConstants.COMMENT_NODE,
		Document = nsIDOMNodeConstants.DOCUMENT_NODE,
		DocumentType = nsIDOMNodeConstants.DOCUMENT_TYPE_NODE,
		DocumentFragment = nsIDOMNodeConstants.DOCUMENT_FRAGMENT_NODE,
		Notation = nsIDOMNodeConstants.NOTATION_NODE
	}

	public class DomNode
	{
		internal DomNode(nsIDOMNode domNode)
		{
			m_DomNode = domNode;
		}

		internal static DomNode Create(nsIDOMNode domNode)
		{
			if (domNode == null)
			{
				return null;
			}

			switch (domNode.NodeType)
			{
				case nsIDOMNodeConstants.ELEMENT_NODE:
					return DomElement.Create((nsIDOMElement)domNode);

				case nsIDOMNodeConstants.ATTRIBUTE_NODE:
					return DomAttribute.Create((nsIDOMAttr)domNode);

				case nsIDOMNodeConstants.TEXT_NODE:
					return DomText.Create((nsIDOMText)domNode);

				case nsIDOMNodeConstants.CDATA_SECTION_NODE:
					return DomCDataSection.Create((nsIDOMCDATASection)domNode);

				case nsIDOMNodeConstants.ENTITY_REFERENCE_NODE:
					return DomEntityReference.Create((nsIDOMEntityReference)domNode);

				case nsIDOMNodeConstants.ENTITY_NODE:
					return DomEntity.Create((nsIDOMEntity)domNode);

				case nsIDOMNodeConstants.PROCESSING_INSTRUCTION_NODE:
					return DomProcessingInstruction.Create((nsIDOMProcessingInstruction)domNode);

				case nsIDOMNodeConstants.COMMENT_NODE:
					return DomComment.Create((nsIDOMComment)domNode);

				case nsIDOMNodeConstants.DOCUMENT_NODE:
					return DomDocument.Create((nsIDOMDocument)domNode);

				case nsIDOMNodeConstants.DOCUMENT_TYPE_NODE:
					return DomDocumentType.Create((nsIDOMDocumentType)domNode);

				case nsIDOMNodeConstants.DOCUMENT_FRAGMENT_NODE:
					return DomDocumentFragment.Create((nsIDOMDocumentFragment)domNode);

				case nsIDOMNodeConstants.NOTATION_NODE:
					return DomNotation.Create((nsIDOMNotation)domNode);
			}

			return new DomNode(domNode);
		}

		public String NodeName
		{
			get { return XpcomStringHelper.Get(m_DomNode.GetNodeName); }
		}

		public String NodeValue
		{
			get { return XpcomStringHelper.Get(m_DomNode.GetNodeValue); }
			set { m_DomNode.SetNodeValue(value); }
		}

		public DomNodeType NodeType
		{
			get { return (DomNodeType)m_DomNode.NodeType; }
		}

		public DomNode ParentNode { get { return DomNode.Create(m_DomNode.ParentNode); } }

		public DomNodeList ChildNodes { get { return DomNodeList.Create(m_DomNode.ChildNodes); } }

		public DomNode FirstChild { get { return DomNode.Create(m_DomNode.FirstChild); } }

		public DomNode LastChild { get { return DomNode.Create(m_DomNode.LastChild); } }

		public DomNode PreviousSibling { get { return DomNode.Create(m_DomNode.PreviousSibling); } }

		public DomNode NextSibling { get { return DomNode.Create(m_DomNode.NextSibling); } }

		public DomNamedNodeMap Attributes { get { return DomNamedNodeMap.Create(m_DomNode.Attributes); } }

		public DomDocument OwnerDocument { get { return DomDocument.Create(m_DomNode.OwnerDocument); } }

		public DomNode InsertBefore(DomNode newChild, DomNode refChild)
		{
			nsIDOMNode domNode = m_DomNode.InsertBefore(newChild.DomObj, refChild.DomObj);
			return DomNode.Create(domNode);
		}

		public DomNode ReplaceChild(DomNode newChild, DomNode oldChild)
		{
			nsIDOMNode domNode = m_DomNode.ReplaceChild(newChild.DomObj, oldChild.DomObj);
			return DomNode.Create(domNode);
		}

		public DomNode RemoveChild(DomNode oldChild)
		{
			nsIDOMNode domNode = m_DomNode.RemoveChild(oldChild.DomObj);
			return DomNode.Create(domNode);
		}

		public DomNode AppendChild(DomNode newChild)
		{
			nsIDOMNode domNode = m_DomNode.AppendChild(newChild.DomObj);
			return DomNode.Create(domNode);
		}

		public Boolean HasChildNodes
		{
			get { return m_DomNode.HasChildNodes(); }
		}

		public DomNode CloneNode(Boolean deep)
		{
			nsIDOMNode domNode = m_DomNode.CloneNode(deep);
			return DomNode.Create(domNode);
		}

		public void Normalize()
		{
			m_DomNode.Normalize();
		}

		public Boolean IsSupported(String feature, String version)
		{
			return m_DomNode.IsSupported(feature, version);
		}

		public String NamespaceURI
		{
			get { return XpcomStringHelper.Get(m_DomNode.GetNamespaceURI); }
		}

		public String Prefix
		{
			get { return XpcomStringHelper.Get(m_DomNode.GetPrefix); }
			set { m_DomNode.SetPrefix(value); }
		}

		public String LocalName
		{
			get { return XpcomStringHelper.Get(m_DomNode.GetLocalName); }
		}

		public Boolean HasAttributes
		{
			get { return m_DomNode.HasAttributes(); }
		}

		internal nsIDOMNode DomObj { get { return m_DomNode; } }

		private readonly nsIDOMNode m_DomNode;
	}
}
