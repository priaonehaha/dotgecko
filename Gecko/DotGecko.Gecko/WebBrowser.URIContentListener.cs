using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed class StartUriOpenEventArgs : CancelEventArgs
	{
		internal StartUriOpenEventArgs(Uri uri)
		{
			m_Uri = uri;
		}

		public Uri Uri
		{
			get { return m_Uri; }
		}

		private readonly Uri m_Uri;
	}

	public sealed partial class WebBrowser : nsIURIContentListener
	{
		public event EventHandler<StartUriOpenEventArgs> StartUriOpen
		{
			add { Events.Add(EventKey.StartUriOpen, value); }
			remove { Events.Remove(EventKey.StartUriOpen, value); }
		}

		Boolean nsIURIContentListener.OnStartURIOpen(nsIURI aURI)
		{
			String uriSpec = XpcomStringHelper.Get(aURI.GetSpec);

			Trace.TraceInformation("nsIURIContentListener.OnStartURIOpen: \"{0}\"", uriSpec);

			Uri uri;
			if (!Uri.TryCreate(uriSpec, UriKind.Absolute, out uri))
			{
				Trace.TraceWarning("nsIURIContentListener.OnStartURIOpen: Can't create URI");
				return false;
			}
			var e = new StartUriOpenEventArgs(uri);
			Events.Raise(EventKey.StartUriOpen, e);

			Trace.TraceInformation("nsIURIContentListener.OnStartURIOpen: {0}", e.Cancel ? "canceled" : "allowed");
			return e.Cancel;
		}

		Boolean nsIURIContentListener.DoContent(String aContentType, Boolean aIsContentPreferred, nsIRequest aRequest, out nsIStreamListener aContentHandler)
		{
			Trace.TraceInformation("nsIURIContentListener.DoContent: \"{0}\"", aContentType);

			aContentHandler = null;
			return false;
		}

		Boolean nsIURIContentListener.IsPreferred(String aContentType, out String aDesiredContentType)
		{
			Trace.TraceInformation("nsIURIContentListener.IsPreferred: \"{0}\"", aContentType);
			Boolean isPreferred;
			switch (aContentType)
			{
				case MediaTypeNames.Text.Plain:
				case MediaTypeNames.Text.Html:
					isPreferred = true;
					break;
				default:
					isPreferred = false;
					break;
			}
			aDesiredContentType = null;
			return isPreferred;
		}

		Boolean nsIURIContentListener.CanHandleContent(String aContentType, Boolean aIsContentPreferred, out String aDesiredContentType)
		{
			Trace.TraceInformation("nsIURIContentListener.CanHandleContent: \"{0}\"", aContentType);

			aDesiredContentType = null;
			return false;
		}

		Object nsIURIContentListener.GetLoadCookie()
		{
			Trace.TraceInformation("nsIURIContentListener.GetLoadCookie");

			return m_LoadCookie;
		}

		void nsIURIContentListener.SetLoadCookie(Object value)
		{
			Trace.TraceInformation("nsIURIContentListener.SetLoadCookie");

			m_LoadCookie = value;
		}

		nsIURIContentListener nsIURIContentListener.GetParentContentListener()
		{
			Trace.TraceInformation("nsIURIContentListener.GetParentContentListener");

			if (m_ParentContentListener != null)
			{
				return m_ParentContentListener;
			}

			if (m_ParentContentListenerWeak != null)
			{
				Guid nsIURIContentListenerIID = typeof(nsIURIContentListener).GUID;
				IntPtr pUnk = m_ParentContentListenerWeak.QueryReferent(ref nsIURIContentListenerIID);
				return (nsIURIContentListener)Marshal.GetObjectForIUnknown(pUnk);
			}

			return null;
		}

		void nsIURIContentListener.SetParentContentListener(nsIURIContentListener value)
		{
			Trace.TraceInformation("nsIURIContentListener.SetParentContentListener");

			if (value == null)
			{
				m_ParentContentListener = null;
				m_ParentContentListenerWeak = null;
				return;
			}

			nsISupportsWeakReference supportsWeakReference;
			if (XpcomHelper.TryQueryInterface(value, out supportsWeakReference))
			{
				m_ParentContentListener = null;
				m_ParentContentListenerWeak = supportsWeakReference.GetWeakReference();
			}
			else
			{
				m_ParentContentListenerWeak = null;
				m_ParentContentListener = value;
				Marshal.ReleaseComObject(m_ParentContentListener);
			}
		}

		private Object m_LoadCookie;
		private nsIURIContentListener m_ParentContentListener;
		private nsIWeakReference m_ParentContentListenerWeak;
	}
}
