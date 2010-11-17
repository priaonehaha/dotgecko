using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser : nsIWebProgressListener, nsIWebProgressListener2
	{
		public event EventHandler<RequestStateChangeEventArgs> RequestStateChange
		{
			add { Events.Add(EventKey.RequestStateChange, value); }
			remove { Events.Remove(EventKey.RequestStateChange, value); }
		}

		public event EventHandler<RequestProgressChangeEventArgs> RequestProgressChange
		{
			add { Events.Add(EventKey.RequestProgressChange, value); }
			remove { Events.Remove(EventKey.RequestProgressChange, value); }
		}

		public event EventHandler<LocationChangeEventArgs> LocationChange
		{
			add { Events.Add(EventKey.LocationChange, value); }
			remove { Events.Remove(EventKey.LocationChange, value); }
		}

		public event EventHandler<RequestStatusChangeEventArgs> RequestStatusChange
		{
			add { Events.Add(EventKey.RequestStatusChange, value); }
			remove { Events.Remove(EventKey.RequestStatusChange, value); }
		}

		public event EventHandler<SecurityChangeEventArgs> SecurityChange
		{
			add { Events.Add(EventKey.SecurityChange, value); }
			remove { Events.Remove(EventKey.SecurityChange, value); }
		}

		public event EventHandler<RefreshAttemptedEventArgs> RefreshAttempted
		{
			add { Events.Add(EventKey.RefreshAttempted, value); }
			remove { Events.Remove(EventKey.RefreshAttempted, value); }
		}

		#region Implementation of nsIWebProgressListener

		void nsIWebProgressListener.OnStateChange(nsIWebProgress aWebProgress, nsIRequest aRequest, UInt32 aStateFlags, UInt32 aStatus)
		{
			Trace.TraceInformation("nsIWebProgressListener.OnStateChange");

			var e = new RequestStateChangeEventArgs((RequestState)aStateFlags);
			Events.Raise(EventKey.RequestStateChange, e);
			if (e.Cancel && (aRequest != null))
			{
				aRequest.Cancel(nsResult.NS_BINDING_ABORTED);
			}
		}

		void nsIWebProgressListener.OnProgressChange(nsIWebProgress aWebProgress, nsIRequest aRequest, Int32 aCurSelfProgress, Int32 aMaxSelfProgress, Int32 aCurTotalProgress, Int32 aMaxTotalProgress)
		{
			Trace.TraceInformation("nsIWebProgressListener.OnProgressChange");

			var e = new RequestProgressChangeEventArgs(aCurSelfProgress, aMaxSelfProgress, aCurTotalProgress, aMaxTotalProgress);
			Events.Raise(EventKey.RequestProgressChange, e);
			if (e.Cancel && (aRequest != null))
			{
				aRequest.Cancel(nsResult.NS_BINDING_ABORTED);
			}
		}

		void nsIWebProgressListener.OnLocationChange(nsIWebProgress aWebProgress, nsIRequest aRequest, nsIURI aLocation)
		{
			Trace.TraceInformation("nsIWebProgressListener.OnLocationChange");

			var e = new LocationChangeEventArgs(aLocation.ToUri());
			Events.Raise(EventKey.LocationChange, e);
			if (e.Cancel && (aRequest != null))
			{
				aRequest.Cancel(nsResult.NS_BINDING_ABORTED);
			}
		}

		void nsIWebProgressListener.OnStatusChange(nsIWebProgress aWebProgress, nsIRequest aRequest, UInt32 aStatus, String aMessage)
		{
			Trace.TraceInformation("nsIWebProgressListener.OnStatusChange");

			var e = new RequestStatusChangeEventArgs(aStatus, aMessage);
			Events.Raise(EventKey.RequestStatusChange, e);
			if (e.Cancel && (aRequest != null))
			{
				aRequest.Cancel(nsResult.NS_BINDING_ABORTED);
			}
		}

		void nsIWebProgressListener.OnSecurityChange(nsIWebProgress aWebProgress, nsIRequest aRequest, UInt32 aState)
		{
			Trace.TraceInformation("nsIWebProgressListener.OnSecurityChange");

			var e = new SecurityChangeEventArgs((SecurityState)aState);
			Events.Raise(EventKey.SecurityChange, e);
			if (e.Cancel && (aRequest != null))
			{
				aRequest.Cancel(nsResult.NS_BINDING_ABORTED);
			}
		}

		#endregion

		#region Implementation of nsIWebProgressListener2

		#region Redirect

		void nsIWebProgressListener2.OnStateChange(nsIWebProgress aWebProgress, nsIRequest aRequest, UInt32 aStateFlags, UInt32 aStatus)
		{
			((nsIWebProgressListener)this).OnStateChange(aWebProgress, aRequest, aStateFlags, aStatus);
		}

		void nsIWebProgressListener2.OnProgressChange(nsIWebProgress aWebProgress, nsIRequest aRequest, Int32 aCurSelfProgress, Int32 aMaxSelfProgress, Int32 aCurTotalProgress, Int32 aMaxTotalProgress)
		{
			((nsIWebProgressListener)this).OnProgressChange(aWebProgress, aRequest, aCurSelfProgress, aMaxSelfProgress, aCurTotalProgress, aMaxTotalProgress);
		}

		void nsIWebProgressListener2.OnLocationChange(nsIWebProgress aWebProgress, nsIRequest aRequest, nsIURI aLocation)
		{
			((nsIWebProgressListener)this).OnLocationChange(aWebProgress, aRequest, aLocation);
		}

		void nsIWebProgressListener2.OnStatusChange(nsIWebProgress aWebProgress, nsIRequest aRequest, UInt32 aStatus, String aMessage)
		{
			((nsIWebProgressListener)this).OnStatusChange(aWebProgress, aRequest, aStatus, aMessage);
		}

		void nsIWebProgressListener2.OnSecurityChange(nsIWebProgress aWebProgress, nsIRequest aRequest, UInt32 aState)
		{
			((nsIWebProgressListener)this).OnSecurityChange(aWebProgress, aRequest, aState);
		}

		#endregion

		void nsIWebProgressListener2.OnProgressChange64(nsIWebProgress aWebProgress, nsIRequest aRequest, Int64 aCurSelfProgress, Int64 aMaxSelfProgress, Int64 aCurTotalProgress, Int64 aMaxTotalProgress)
		{
			Trace.TraceInformation("nsIWebProgressListener2.OnProgressChange64");

			var e = new RequestProgressChangeEventArgs(aCurSelfProgress, aMaxSelfProgress, aCurTotalProgress, aMaxTotalProgress);
			Events.Raise(EventKey.RequestProgressChange, e);
			if (e.Cancel && (aRequest != null))
			{
				aRequest.Cancel(nsResult.NS_BINDING_ABORTED);
			}
		}

		Boolean nsIWebProgressListener2.OnRefreshAttempted(nsIWebProgress aWebProgress, nsIURI aRefreshURI, Int32 aMillis, Boolean aSameURI)
		{
			Trace.TraceInformation("nsIWebProgressListener2.OnRefreshAttempted");

			var e = new RefreshAttemptedEventArgs(aRefreshURI.ToUri(), aMillis, aSameURI);
			Events.Raise(EventKey.RefreshAttempted, e);
			return !e.Cancel;
		}

		#endregion
	}
}
