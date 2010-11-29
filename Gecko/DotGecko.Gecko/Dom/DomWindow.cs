using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public class DomWindow
	{
		internal DomWindow(nsIDOMWindow domWindow)
		{
			Debug.Assert(domWindow != null);
			m_DomWindow = domWindow;
		}

		internal static DomWindow Create(nsIDOMWindow domWindow)
		{
			if (domWindow == null)
			{
				return null;
			}

			if (domWindow is nsIDOMWindow2)
			{
				return DomWindow2.Create((nsIDOMWindow2)domWindow);
			}

			return new DomWindow(domWindow);
		}

		public DomDocument Document
		{
			get
			{
				nsIDOMDocument domDocument = m_DomWindow.Document;
				return DomDocument.Create(domDocument);
			}
		}

		public DomWindow Parent
		{
			get
			{
				nsIDOMWindow domWindow = m_DomWindow.Parent;
				return DomWindow.Create(domWindow);
			}
		}

		public DomWindow Top
		{
			get
			{
				nsIDOMWindow domWindow = m_DomWindow.Top;
				return DomWindow.Create(domWindow);
			}
		}

		public DomBarProp Scrollbars
		{
			get
			{
				nsIDOMBarProp domBarProp = m_DomWindow.Scrollbars;
				return DomBarProp.Create(domBarProp);
			}
		}

		public DomWindowCollection Frames
		{
			get
			{
				nsIDOMWindowCollection domWindowCollection = m_DomWindow.Frames;
				return DomWindowCollection.Create(domWindowCollection);
			}
		}

		public String Name
		{
			get { return XpcomStringHelper.Get(m_DomWindow.GetName); }
			set { m_DomWindow.SetName(value); }
		}

		public Single TextZoom
		{
			get { return m_DomWindow.TextZoom; }
			set { m_DomWindow.TextZoom = value; }
		}

		public Int32 ScrollX { get { return m_DomWindow.ScrollX; } }

		public Int32 ScrollY { get { return m_DomWindow.ScrollY; } }

		public void ScrollTo(Int32 xScroll, Int32 yScroll)
		{
			m_DomWindow.ScrollTo(xScroll, yScroll);
		}

		public void ScrollBy(Int32 xScrollDif, Int32 yScrollDif)
		{
			m_DomWindow.ScrollBy(xScrollDif, yScrollDif);
		}

		public DomSelection GetSelection()
		{
			nsISelection selection = m_DomWindow.GetSelection();
			return DomSelection.Create(selection);
		}

		public void ScrollByLines(Int32 numLines)
		{
			m_DomWindow.ScrollByLines(numLines);
		}

		public void ScrollByPages(Int32 numPages)
		{
			m_DomWindow.ScrollByPages(numPages);
		}

		public void SizeToContent()
		{
			m_DomWindow.SizeToContent();
		}

		internal nsIDOMWindow DomObj { get { return m_DomWindow; } }

		private readonly nsIDOMWindow m_DomWindow;
	}
}
