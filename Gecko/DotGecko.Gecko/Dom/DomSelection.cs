using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomSelection
	{
		private DomSelection(nsISelection selection)
		{
			Debug.Assert(selection != null);
			m_Selection = selection;
		}

		internal static DomSelection Create(nsISelection selection)
		{
			return selection != null ? new DomSelection(selection) : null;
		}

		public DomNode AnchorNode
		{
			get
			{
				nsIDOMNode domNode = m_Selection.AnchorNode;
				return DomNode.Create(domNode);
			}
		}

		public Int32 AnchorOffset { get { return m_Selection.AnchorOffset; } }

		public DomNode FocusNode
		{
			get
			{
				nsIDOMNode domNode = m_Selection.FocusNode;
				return DomNode.Create(domNode);
			}
		}

		public Int32 FocusOffset { get { return m_Selection.FocusOffset; } }

		public Boolean IsCollapsed { get { return m_Selection.IsCollapsed; } }

		public Int32 RangeCount { get { return m_Selection.RangeCount; } }

		public DomRange GetRangeAt(Int32 index)
		{
			nsIDOMRange domRange = m_Selection.GetRangeAt(index);
			return DomRange.Create(domRange);
		}

		public void Collapse(DomNode parentNode, Int32 offset)
		{
			m_Selection.Collapse(parentNode.DomObj, offset);
		}

		public void Extend(DomNode parentNode, Int32 offset)
		{
			m_Selection.Extend(parentNode.DomObj, offset);
		}

		public void CollapseToStart()
		{
			m_Selection.CollapseToStart();
		}

		public void CollapseToEnd()
		{
			m_Selection.CollapseToEnd();
		}

		public Boolean ContainsNode(DomNode node, Boolean partlyContained)
		{
			return m_Selection.ContainsNode(node.DomObj, partlyContained);
		}

		public void SelectAllChildren(DomNode parentNode)
		{
			m_Selection.SelectAllChildren(parentNode.DomObj);
		}

		public void AddRange(DomRange range)
		{
			m_Selection.AddRange(range.DomObj);
		}

		public void RemoveRange(DomRange range)
		{
			m_Selection.AddRange(range.DomObj);
		}

		public void RemoveAllRanges()
		{
			m_Selection.RemoveAllRanges();
		}

		public void DeleteFromDocument()
		{
			m_Selection.DeleteFromDocument();
		}

		public void SelectionLanguageChange(Boolean langRTL)
		{
			m_Selection.SelectionLanguageChange(langRTL);
		}

		public override String ToString()
		{
			return m_Selection.ToString();
		}

		private readonly nsISelection m_Selection;
	}
}
