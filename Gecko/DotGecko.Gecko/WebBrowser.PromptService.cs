using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed partial class WebBrowser
	{
		public event EventHandler<AlertEventArgs> Alert
		{
			add { Events.Add(EventKey.Alert, value); }
			remove { Events.Remove(EventKey.Alert, value); }
		}

		public event EventHandler<ConfirmEventArgs> Confirm
		{
			add { Events.Add(EventKey.Confirm, value); }
			remove { Events.Remove(EventKey.Confirm, value); }
		}

		public event EventHandler<PromptEventArgs> Prompt
		{
			add { Events.Add(EventKey.Prompt, value); }
			remove { Events.Remove(EventKey.Prompt, value); }
		}

		public event EventHandler<PromptUsernameAndPasswordEventArgs> PromptUsernameAndPassword
		{
			add { Events.Add(EventKey.PromptUsernameAndPassword, value); }
			remove { Events.Remove(EventKey.PromptUsernameAndPassword, value); }
		}

		public event EventHandler<PromptEventArgs> PromptPassword
		{
			add { Events.Add(EventKey.PromptPassword, value); }
			remove { Events.Remove(EventKey.PromptPassword, value); }
		}

		public event EventHandler<SelectEventArgs> Select
		{
			add { Events.Add(EventKey.Select, value); }
			remove { Events.Remove(EventKey.Select, value); }
		}

		public event EventHandler<PromptAuthEventArgs> PromptAuth
		{
			add { Events.Add(EventKey.PromptAuth, value); }
			remove { Events.Remove(EventKey.PromptAuth, value); }
		}

		public event EventHandler<AsyncPromptAuthEventArgs> AsyncPromptAuth
		{
			add { Events.Add(EventKey.AsyncPromptAuth, value); }
			remove { Events.Remove(EventKey.AsyncPromptAuth, value); }
		}

		[Guid("79E7A6D0-A2FA-4A6E-A486-C5BB5A411DDC")]
		private sealed class PromptService : nsIPromptService, nsIPromptService2
		{
			//TODO: Raise static events if aParent parameter is null.

			void nsIPromptService.Alert(nsIDOMWindow aParent, String aDialogTitle, String aText)
			{
				Boolean checkState = false;
				((nsIPromptService)this).AlertCheck(aParent, aDialogTitle, aText, null, ref checkState);
			}

			void nsIPromptService.AlertCheck(nsIDOMWindow aParent, String aDialogTitle, String aText, String aCheckMsg, ref Boolean aCheckState)
			{
				WebBrowser browser = GetBrowserFromDomWindow(aParent);
				if (browser == null)
				{
					return;
				}

				var e = new AlertEventArgs(aDialogTitle, aText, aCheckMsg, aCheckState);
				browser.Events.Raise(EventKey.Alert, e);
				aCheckState = e.CheckState;
			}

			Boolean nsIPromptService.Confirm(nsIDOMWindow aParent, String aDialogTitle, String aText)
			{
				Boolean checkState = false;
				return ((nsIPromptService)this).ConfirmCheck(aParent, aDialogTitle, aText, null, ref checkState);
			}

			Boolean nsIPromptService.ConfirmCheck(nsIDOMWindow aParent, String aDialogTitle, String aText, String aCheckMsg, ref Boolean aCheckState)
			{
				WebBrowser browser = GetBrowserFromDomWindow(aParent);
				if (browser == null)
				{
					return false;
				}

				var e = new ConfirmEventArgs(aDialogTitle, aText, aCheckMsg, aCheckState);
				browser.Events.Raise(EventKey.Confirm, e);
				aCheckState = e.CheckState;
				return e.ButtonPressed == 0;
			}

			Int32 nsIPromptService.ConfirmEx(nsIDOMWindow aParent, String aDialogTitle, String aText, UInt32 aButtonFlags, String aButton0Title, String aButton1Title, String aButton2Title, String aCheckMsg, ref Boolean aCheckState)
			{
				WebBrowser browser = GetBrowserFromDomWindow(aParent);
				if (browser == null)
				{
					return 0;
				}

				var e = new ConfirmEventArgs(aDialogTitle, aText, aButtonFlags, aButton0Title, aButton1Title, aButton2Title, aCheckMsg, aCheckState);
				browser.Events.Raise(EventKey.Confirm, e);
				aCheckState = e.CheckState;
				return e.ButtonPressed;
			}

			Boolean nsIPromptService.Prompt(nsIDOMWindow aParent, String aDialogTitle, String aText, StringBuilder aValue, String aCheckMsg, ref Boolean aCheckState)
			{
				WebBrowser browser = GetBrowserFromDomWindow(aParent);
				if (browser == null)
				{
					return false;
				}

				var e = new PromptEventArgs(aDialogTitle, aText, aValue.ToString(), aCheckMsg, aCheckState);
				browser.Events.Raise(EventKey.Prompt, e);
				aValue.Clear();
				aValue.Append(e.Value);
				aCheckState = e.CheckState;
				return e.Result;
			}

			Boolean nsIPromptService.PromptUsernameAndPassword(nsIDOMWindow aParent, String aDialogTitle, String aText, StringBuilder aUsername, StringBuilder aPassword, String aCheckMsg, ref Boolean aCheckState)
			{
				WebBrowser browser = GetBrowserFromDomWindow(aParent);
				if (browser == null)
				{
					return false;
				}

				var e = new PromptUsernameAndPasswordEventArgs(aDialogTitle, aText, aUsername.ToString(), aPassword.ToString(), aCheckMsg, aCheckState);
				browser.Events.Raise(EventKey.PromptUsernameAndPassword, e);
				aUsername.Clear();
				aUsername.Append(e.UserName);
				aPassword.Clear();
				aPassword.Append(e.Password);
				aCheckState = e.CheckState;
				return e.Result;
			}

			Boolean nsIPromptService.PromptPassword(nsIDOMWindow aParent, String aDialogTitle, String aText, StringBuilder aPassword, String aCheckMsg, ref Boolean aCheckState)
			{
				WebBrowser browser = GetBrowserFromDomWindow(aParent);
				if (browser == null)
				{
					return false;
				}

				var e = new PromptEventArgs(aDialogTitle, aText, aPassword.ToString(), aCheckMsg, aCheckState);
				browser.Events.Raise(EventKey.PromptPassword, e);
				aPassword.Clear();
				aPassword.Append(e.Value);
				aCheckState = e.CheckState;
				return e.Result;
			}

			Boolean nsIPromptService.Select(nsIDOMWindow aParent, String aDialogTitle, String aText, UInt32 aCount, String[] aSelectList, out Int32 aOutSelection)
			{
				WebBrowser browser = GetBrowserFromDomWindow(aParent);
				if (browser == null)
				{
					aOutSelection = 0;
					return false;
				}

				var e = new SelectEventArgs(aDialogTitle, aText, aSelectList);
				browser.Events.Raise(EventKey.Select, e);
				aOutSelection = e.SelectedIndex;
				return e.Result;
			}

			#region Redirect

			void nsIPromptService2.Alert(nsIDOMWindow aParent, String aDialogTitle, String aText)
			{
				((nsIPromptService)this).Alert(aParent, aDialogTitle, aText);
			}

			void nsIPromptService2.AlertCheck(nsIDOMWindow aParent, String aDialogTitle, String aText, String aCheckMsg, ref Boolean aCheckState)
			{
				((nsIPromptService)this).AlertCheck(aParent, aDialogTitle, aText, aCheckMsg, ref aCheckState);
			}

			Boolean nsIPromptService2.Confirm(nsIDOMWindow aParent, String aDialogTitle, String aText)
			{
				return ((nsIPromptService)this).Confirm(aParent, aDialogTitle, aText);
			}

			Boolean nsIPromptService2.ConfirmCheck(nsIDOMWindow aParent, String aDialogTitle, String aText, String aCheckMsg, ref Boolean aCheckState)
			{
				return ((nsIPromptService)this).ConfirmCheck(aParent, aDialogTitle, aText, aCheckMsg, ref aCheckState);
			}

			Int32 nsIPromptService2.ConfirmEx(nsIDOMWindow aParent, String aDialogTitle, String aText, UInt32 aButtonFlags, String aButton0Title, String aButton1Title, String aButton2Title, String aCheckMsg, ref Boolean aCheckState)
			{
				return ((nsIPromptService)this).ConfirmEx(aParent, aDialogTitle, aText, aButtonFlags, aButton0Title, aButton1Title, aButton2Title, aCheckMsg, ref aCheckState);
			}

			Boolean nsIPromptService2.Prompt(nsIDOMWindow aParent, String aDialogTitle, String aText, StringBuilder aValue, String aCheckMsg, ref Boolean aCheckState)
			{
				return ((nsIPromptService)this).Prompt(aParent, aDialogTitle, aText, aValue, aCheckMsg, ref aCheckState);
			}

			Boolean nsIPromptService2.PromptUsernameAndPassword(nsIDOMWindow aParent, String aDialogTitle, String aText, StringBuilder aUsername, StringBuilder aPassword, String aCheckMsg, ref Boolean aCheckState)
			{
				return ((nsIPromptService)this).PromptUsernameAndPassword(aParent, aDialogTitle, aText, aUsername, aPassword, aCheckMsg, ref aCheckState);
			}

			Boolean nsIPromptService2.PromptPassword(nsIDOMWindow aParent, String aDialogTitle, String aText, StringBuilder aPassword, String aCheckMsg, ref Boolean aCheckState)
			{
				return ((nsIPromptService)this).PromptPassword(aParent, aDialogTitle, aText, aPassword, aCheckMsg, ref aCheckState);
			}

			Boolean nsIPromptService2.Select(nsIDOMWindow aParent, String aDialogTitle, String aText, UInt32 aCount, String[] aSelectList, out Int32 aOutSelection)
			{
				return ((nsIPromptService)this).Select(aParent, aDialogTitle, aText, aCount, aSelectList, out aOutSelection);
			}

			#endregion

			Boolean nsIPromptService2.PromptAuth(nsIDOMWindow aParent, nsIChannel aChannel, UInt32 level, nsIAuthInformation authInfo, String checkboxLabel, ref Boolean checkValue)
			{
				Trace.TraceInformation("nsIPromptService2.PromptAuth");

				WebBrowser browser = GetBrowserFromDomWindow(aParent);
				if (browser == null)
				{
					Trace.TraceWarning("Can't get Browser object from DomWindow");
					return false;
				}

				var e = new PromptAuthEventArgs((AuthLevel)level, authInfo, checkboxLabel, checkValue);
				browser.Events.Raise(EventKey.PromptAuth, e);
				Boolean canProceed = !e.Cancel;

				if (canProceed)
				{
					authInfo.SetUsername(e.Username);
					authInfo.SetPassword(e.Password);
					authInfo.SetDomain(e.Domain);
				}

				checkValue = e.CheckState;
				return canProceed;
			}

			nsICancelable nsIPromptService2.AsyncPromptAuth(nsIDOMWindow aParent, nsIChannel aChannel, nsIAuthPromptCallback aCallback, Object aContext, UInt32 level, nsIAuthInformation authInfo, String checkboxLabel, ref Boolean checkValue)
			{
				Trace.TraceInformation("nsIPromptService2.AsyncPromptAuth");

				WebBrowser browser = GetBrowserFromDomWindow(aParent);
				if (browser == null)
				{
					Trace.TraceWarning("Can't get Browser object from DomWindow");
					aCallback.OnAuthCancelled(aContext, true);
					return null;
				}

				var e = new AsyncPromptAuthEventArgs(aCallback, aContext, (AuthLevel)level, authInfo, checkboxLabel, checkValue);
				browser.Events.Raise(EventKey.AsyncPromptAuth, e);
				checkValue = e.CheckState;
				return e;
			}
		}

		private static Factory<PromptService> ms_PromptServiceFactory;
	}
}
