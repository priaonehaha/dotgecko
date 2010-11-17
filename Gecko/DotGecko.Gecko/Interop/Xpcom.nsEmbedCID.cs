using System;

namespace DotGecko.Gecko.Interop
{
	internal static partial class Xpcom
	{
		/**
		 * @file
		 * @brief List of, and documentation for, frozen Gecko embedding contracts.
		 */

		/**
		 * Web Browser ContractID
		 *   Creating an instance of this ContractID (via createInstanceByContractID)
		 *   is the basic way to instantiate a Gecko browser.
		 *
		 * This contract implements the following interfaces:
		 * nsIWebBrowser
		 * nsIWebBrowserSetup
		 * nsIInterfaceRequestor
		 *
		 * @note This contract does not guarantee implementation of any other
		 * interfaces and does not guarantee ability to get any particular
		 * interfaces via the nsIInterfaceRequestor implementation.
		 */
		internal const String NS_WEBBROWSER_CONTRACTID = @"@mozilla.org/embedding/browser/nsWebBrowser;1";

		/**
		 * Prompt Service ContractID
		 *   The prompt service (which can be gotten by calling getServiceByContractID
		 *   on this ContractID) is the way to pose various prompts, alerts,
		 *   and confirmation dialogs to the user.
		 * 
		 * This contract implements the following interfaces:
		 * nsIPromptService
		 * nsIPromptService2 (optional)
		 *
		 * Embedders may override this ContractID with their own implementation if they
		 * want more control over the way prompts, alerts, and confirmation dialogs are
		 * presented to the user.
		 */
		internal const String NS_PROMPTSERVICE_CONTRACTID = @"@mozilla.org/embedcomp/prompt-service;1";

		/**
		 * Non Blocking Alert Service ContractID
		 *   This service is for posing non blocking alerts to the user.
		 *
		 * This contract implements the following interfaces:
		 * nsINonBlockingAlertService
		 *
		 * Embedders may override this ContractID with their own implementation.
		 */
		internal const String NS_NONBLOCKINGALERTSERVICE_CONTRACTID = @"@mozilla.org/embedcomp/nbalert-service;1";

		/**
		 * This contract ID should be implemented by password managers to be able to
		 * override the standard implementation of nsIAuthPrompt2. It will be used as
		 * a service.
		 *
		 * This contract implements the following interfaces:
		 * nsIPromptFactory
		 */
		internal const String NS_PWMGR_AUTHPROMPTFACTORY = @"@mozilla.org/passwordmanager/authpromptfactory;1";
	}
}
