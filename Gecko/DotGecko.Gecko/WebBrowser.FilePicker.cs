using System;
using System.Runtime.InteropServices;
using DotGecko.Gecko.Interop;
using AString = System.IntPtr;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser
	{
		public event EventHandler<FilePickerEventArgs> ShowFilePicker
		{
			add { Events.Add(EventKey.ShowFilePicker, value); }
			remove { Events.Remove(EventKey.ShowFilePicker, value); }
		}

		[Guid("C475A5DB-277E-42BB-8566-AD59F85361F1")]
		private sealed class FilePicker : nsIFilePicker
		{
			void nsIFilePicker.Init(nsIDOMWindow parent, AString title, Int16 mode)
			{
				m_Parent = GetBrowserFromDomWindow(parent);
				m_FilePickerEventArgs = new FilePickerEventArgs(title.GetString(), (FilePickerMode)mode);
			}

			void nsIFilePicker.AppendFilters(Int32 filterMask)
			{
				if (m_FilePickerEventArgs != null)
				{
					m_FilePickerEventArgs.AppendFilters((FilePickerFilters)filterMask);
				}
			}

			void nsIFilePicker.AppendFilter(AString title, AString filter)
			{
				if (m_FilePickerEventArgs != null)
				{
					m_FilePickerEventArgs.AppendFilter(title.GetString(), filter.GetString());
				}
			}

			void nsIFilePicker.GetDefaultString(AString retval)
			{
				if (m_FilePickerEventArgs != null)
				{
					retval.SetString(m_FilePickerEventArgs.DefaultName);
				}
			}

			void nsIFilePicker.SetDefaultString(AString value)
			{
				if (m_FilePickerEventArgs != null)
				{
					m_FilePickerEventArgs.DefaultName = value.GetString();
				}
			}

			void nsIFilePicker.GetDefaultExtension(AString retval)
			{
				if (m_FilePickerEventArgs != null)
				{
					retval.SetString(m_FilePickerEventArgs.DefaultExtension);
				}
			}

			void nsIFilePicker.SetDefaultExtension(AString value)
			{
				if (m_FilePickerEventArgs != null)
				{
					m_FilePickerEventArgs.DefaultExtension = value.GetString();
				}
			}

			Int32 nsIFilePicker.FilterIndex
			{
				get { return this.m_FilePickerEventArgs != null ? this.m_FilePickerEventArgs.FilterIndex : -1; }
				set
				{
					if (m_FilePickerEventArgs != null)
					{
						m_FilePickerEventArgs.FilterIndex = value;
					}
				}
			}

			nsILocalFile nsIFilePicker.DisplayDirectory
			{
				get
				{
					return this.m_FilePickerEventArgs != null ? Xpcom.NewNativeLocalFile(this.m_FilePickerEventArgs.InitialDirectory) : null;
				}
				set
				{
					if (m_FilePickerEventArgs != null)
					{
						this.m_FilePickerEventArgs.InitialDirectory = value != null ? XpcomString.Get(value.GetPath) : null;
					}
				}
			}

			nsILocalFile nsIFilePicker.File
			{
				get
				{
					return this.m_FilePickerEventArgs != null ? Xpcom.NewNativeLocalFile(this.m_FilePickerEventArgs.File) : null;
				}
			}

			nsIURI nsIFilePicker.FileURL
			{
				get
				{
					if (m_FilePickerEventArgs != null)
					{
						//TODO: Implement me!
					}
					return null;
				}
			}

			nsISimpleEnumerator nsIFilePicker.Files
			{
				get
				{
					if (m_FilePickerEventArgs != null)
					{
						return new SimpleEnumerator<String, nsILocalFile>(m_FilePickerEventArgs.Files, input => Xpcom.NewNativeLocalFile(input));
					}
					return null;
				}
			}

			Int16 nsIFilePicker.Show()
			{
				if ((m_Parent != null) && (m_FilePickerEventArgs != null))
				{
					m_Parent.Events.Raise(EventKey.ShowFilePicker, m_FilePickerEventArgs);
					return (Int16)m_FilePickerEventArgs.Result;
				}

				return nsIFilePickerConstants.returnCancel;
			}

			private WebBrowser m_Parent;
			private FilePickerEventArgs m_FilePickerEventArgs;
		}

		private static Factory<FilePicker> ms_FilePickerFactory;
	}
}
