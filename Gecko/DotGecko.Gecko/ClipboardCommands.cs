using System;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed class ClipboardCommands
	{
		internal ClipboardCommands(nsIClipboardCommands clipboardCommands)
		{
			m_ClipboardCommands = clipboardCommands;
		}

		public Boolean CanCutSelection
		{
			get { return m_ClipboardCommands.CanCutSelection(); }
		}

		public Boolean CanCopySelection
		{
			get { return m_ClipboardCommands.CanCopySelection(); }
		}

		public Boolean CanCopyLinkLocation
		{
			get { return m_ClipboardCommands.CanCopyLinkLocation(); }
		}

		public Boolean CanCopyImageLocation
		{
			get { return m_ClipboardCommands.CanCopyImageLocation(); }
		}

		public Boolean CanCopyImageContents
		{
			get { return m_ClipboardCommands.CanCopyImageContents(); }
		}

		public Boolean CanPaste
		{
			get { return m_ClipboardCommands.CanPaste(); }
		}

		public void CutSelection()
		{
			m_ClipboardCommands.CutSelection();
		}

		public void CopySelection()
		{
			m_ClipboardCommands.CopySelection();
		}

		public void CopyLinkLocation()
		{
			m_ClipboardCommands.CopyLinkLocation();
		}

		public void CopyImageLocation()
		{
			m_ClipboardCommands.CopyImageLocation();
		}

		public void CopyImageContents()
		{
			m_ClipboardCommands.CopyImageContents();
		}

		public void Paste()
		{
			m_ClipboardCommands.Paste();
		}

		public void SelectAll()
		{
			m_ClipboardCommands.SelectAll();
		}

		public void SelectNone()
		{
			m_ClipboardCommands.SelectNone();
		}

		private readonly nsIClipboardCommands m_ClipboardCommands;
	}
}
