using System;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public enum FilePickerMode : short
	{
		Open = nsIFilePickerConstants.modeOpen,
		Save = nsIFilePickerConstants.modeSave,
		GetFolder = nsIFilePickerConstants.modeGetFolder,
		OpenMultiple = nsIFilePickerConstants.modeOpenMultiple
	}

	[Flags]
	public enum FilePickerFilters
	{
		All = nsIFilePickerConstants.filterAll,
		Html = nsIFilePickerConstants.filterHTML,
		Text = nsIFilePickerConstants.filterText,
		Images = nsIFilePickerConstants.filterImages,
		Xml= nsIFilePickerConstants.filterXML,
		Xul = nsIFilePickerConstants.filterXUL,
		Apps = nsIFilePickerConstants.filterApps,
		AllowURLs = nsIFilePickerConstants.filterAllowURLs
	}

	public enum FilePickerResult : short
	{
		Ok = nsIFilePickerConstants.returnOK,
		Cancel = nsIFilePickerConstants.returnCancel,
		Replace = nsIFilePickerConstants.returnReplace
	}

	public sealed class FilePickerEventArgs : EventArgs
	{
		internal FilePickerEventArgs(String title, FilePickerMode mode)
		{
			m_Title = title;
			m_Mode = mode;
			Result = FilePickerResult.Cancel;
		}

		public String Title
		{
			get { return m_Title; }
		}

		public FilePickerMode Mode
		{
			get { return m_Mode; }
		}

		public FilePickerFilters Filters
		{
			get { return m_Filters; }
		}

		public String Filter
		{
			get { return m_Filter; }
		}

		public String DefaultName { get; internal set; }

		public String DefaultExtension { get; internal set; }

		public Int32 FilterIndex { get; internal set; }

		public String InitialDirectory { get; internal set; }

		public String File { get; set; }

		public String[] Files { get; set; }

		public FilePickerResult Result { get; set; }

		internal void AppendFilters(FilePickerFilters filters)
		{
			m_Filters |= filters;
		}

		internal void AppendFilter(String title, String filter)
		{
			if (!String.IsNullOrEmpty(m_Filter))
			{
				m_Filter += "|";
			}
			String newFilter = title + "|" + filter;
			m_Filter += newFilter;
		}

		private readonly String m_Title;
		private readonly FilePickerMode m_Mode;
		private FilePickerFilters m_Filters;
		private String m_Filter;
	}
}
