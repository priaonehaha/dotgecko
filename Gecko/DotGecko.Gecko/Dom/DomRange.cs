using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomRange
	{
		public enum CompareHow : ushort
		{
			StartToStart = nsIDOMRangeConstants.START_TO_START,
			StartToEnd = nsIDOMRangeConstants.START_TO_END,
			EndToEnd = nsIDOMRangeConstants.END_TO_END,
			EndToStart = nsIDOMRangeConstants.END_TO_START
		}

		private DomRange(nsIDOMRange domRange)
		{
			Debug.Assert(domRange != null);
			m_DomRange = domRange;
		}

		internal static DomRange Create(nsIDOMRange domRange)
		{
			return domRange != null ? new DomRange(domRange) : null;
		}

		public DomNode StartContainer
		{
			get
			{
				nsIDOMNode domNode = m_DomRange.StartContainer;
				return DomNode.Create(domNode);
			}
		}

		public Int32 StartOffset { get { return m_DomRange.StartOffset; } }

		public DomNode EndContainer
		{
			get
			{
				nsIDOMNode domNode = m_DomRange.EndContainer;
				return DomNode.Create(domNode);
			}
		}

		public Int32 EndOffset { get { return m_DomRange.EndOffset; } }

		public Boolean Collapsed { get { return m_DomRange.Collapsed; } }

		public DomNode CommonAncestorContainer
		{
			get
			{
				nsIDOMNode domNode = m_DomRange.CommonAncestorContainer;
				return DomNode.Create(domNode);
			}
		}

		public void SetStart(DomNode refNode, Int32 offset)
		{
			m_DomRange.SetStart(refNode.DomObj, offset);
		}

		public void SetEnd(DomNode refNode, Int32 offset)
		{
			m_DomRange.SetEnd(refNode.DomObj, offset);
		}

		public void SetStartBefore(DomNode refNode)
		{
			m_DomRange.SetStartBefore(refNode.DomObj);
		}

		public void SetStartAfter(DomNode refNode)
		{
			m_DomRange.SetStartAfter(refNode.DomObj);
		}

		public void SetEndBefore(DomNode refNode)
		{
			m_DomRange.SetEndBefore(refNode.DomObj);
		}

		public void SetEndAfter(DomNode refNode)
		{
			m_DomRange.SetEndAfter(refNode.DomObj);
		}

		public void Collapse(Boolean toStart)
		{
			m_DomRange.Collapse(toStart);
		}

		public void SelectNode(DomNode refNode)
		{
			m_DomRange.SelectNode(refNode.DomObj);
		}

		public void SelectNodeContents(DomNode refNode)
		{
			m_DomRange.SelectNodeContents(refNode.DomObj);
		}

		public Int16 CompareBoundaryPoints(CompareHow how, DomRange sourceRange)
		{
			return m_DomRange.CompareBoundaryPoints((UInt16)how, sourceRange.DomObj);
		}

		public void DeleteContents()
		{
			m_DomRange.DeleteContents();
		}

		public DomDocumentFragment ExtractContents()
		{
			nsIDOMDocumentFragment domDocumentFragment = m_DomRange.ExtractContents();
			return DomDocumentFragment.Create(domDocumentFragment);
		}

		public DomDocumentFragment CloneContents()
		{
			nsIDOMDocumentFragment domDocumentFragment = m_DomRange.CloneContents();
			return DomDocumentFragment.Create(domDocumentFragment);
		}

		public void InsertNode(DomNode newNode)
		{
			m_DomRange.InsertNode(newNode.DomObj);
		}

		public void SurroundContents(DomNode newParent)
		{
			m_DomRange.SurroundContents(newParent.DomObj);
		}

		public DomRange CloneRange()
		{
			nsIDOMRange domRange = m_DomRange.CloneRange();
			return DomRange.Create(domRange);
		}

		public override String ToString()
		{
			return XpcomString.Get(m_DomRange.ToString);
		}

		public void Detach()
		{
			m_DomRange.Detach();
		}

		internal nsIDOMRange DomObj { get { return m_DomRange; } }

		private readonly nsIDOMRange m_DomRange;
	}
}
