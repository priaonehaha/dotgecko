using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;
using nsQIResult = System.Object;

namespace DotGecko.Gecko.Interop
{
	public static class nsIPrefBranchConstants
	{
		/**
		 * Values describing the basic preference types.
		 *
		 * @see getPrefType
		 */
		public const Int32 PREF_INVALID = 0;
		public const Int32 PREF_STRING = 32;
		public const Int32 PREF_INT = 64;
		public const Int32 PREF_BOOL = 128;
	}

	/**
	 * The nsIPrefBranch interface is used to manipulate the preferences data. This
	 * object may be obtained from the preferences service (nsIPrefService) and
	 * used to get and set default and/or user preferences across the application.
	 *
	 * This object is created with a "root" value which describes the base point in
	 * the preferences "tree" from which this "branch" stems. Preferences are
	 * accessed off of this root by using just the final portion of the preference.
	 * For example, if this object is created with the root "browser.startup.",
	 * the preferences "browser.startup.page", "browser.startup.homepage",
	 * and "browser.startup.homepage_override" can be accessed by simply passing
	 * "page", "homepage", or "homepage_override" to the various Get/Set methods.
	 *
	 * @see nsIPrefService
	 */
	[ComImport, Guid("56c35506-f14b-11d3-99d3-ddbfac2ccf65"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIPrefBranch //: nsISupports
	{
		/**
		 * Called to get the root on which this branch is based, such as
		 * "browser.startup."
		 */
		String Root { [return: MarshalAs(UnmanagedType.LPStr)] get; }

		/**
		 * Called to determine the type of a specific preference.
		 *
		 * @param aPrefName The preference to get the type of.
		 *
		 * @return long     A value representing the type of the preference. This
		 *                  value will be PREF_STRING, PREF_INT, or PREF_BOOL.
		 */
		Int32 GetPrefType([MarshalAs(UnmanagedType.LPStr)] String aPrefName);

		/**
		 * Called to get the state of an individual boolean preference.
		 *
		 * @param aPrefName The boolean preference to get the state of.
		 *
		 * @return boolean  The value of the requested boolean preference.
		 *
		 * @see setBoolPref
		 */
		Boolean GetBoolPref([MarshalAs(UnmanagedType.LPStr)] String aPrefName);

		/**
		 * Called to set the state of an individual boolean preference.
		 *
		 * @param aPrefName The boolean preference to set the state of.
		 * @param aValue    The boolean value to set the preference to.
		 *
		 * @return NS_OK The value was successfully set.
		 * @return Other The value was not set or is the wrong type.
		 *
		 * @see getBoolPref
		 */
		void SetBoolPref([MarshalAs(UnmanagedType.LPStr)] String aPrefName, Int32 aValue);

		/**
		 * Called to get the state of an individual string preference.
		 *
		 * @param aPrefName The string preference to retrieve.
		 *
		 * @return string   The value of the requested string preference.
		 *
		 * @see setCharPref
		 */
		[return: MarshalAs(UnmanagedType.LPStr)]
		String GetCharPref([MarshalAs(UnmanagedType.LPStr)] String aPrefName);

		/**
		 * Called to set the state of an individual string preference.
		 *
		 * @param aPrefName The string preference to set.
		 * @param aValue    The string value to set the preference to.
		 *
		 * @return NS_OK The value was successfully set.
		 * @return Other The value was not set or is the wrong type.
		 *
		 * @see getCharPref
		 */
		void SetCharPref([MarshalAs(UnmanagedType.LPStr)] String aPrefName, [MarshalAs(UnmanagedType.LPStr)] String aValue);

		/**
		 * Called to get the state of an individual integer preference.
		 *
		 * @param aPrefName The integer preference to get the value of.
		 *
		 * @return long     The value of the requested integer preference.
		 *
		 * @see setIntPref
		 */
		Int32 GetIntPref([MarshalAs(UnmanagedType.LPStr)] String aPrefName);

		/**
		 * Called to set the state of an individual integer preference.
		 *
		 * @param aPrefName The integer preference to set the value of.
		 * @param aValue    The integer value to set the preference to.
		 *
		 * @return NS_OK The value was successfully set.
		 * @return Other The value was not set or is the wrong type.
		 *
		 * @see getIntPref
		 */
		void SetIntPref([MarshalAs(UnmanagedType.LPStr)] String aPrefName, Int32 aValue);

		/**
		 * Called to get the state of an individual complex preference. A complex
		 * preference is a preference which represents an XPCOM object that can not
		 * be easily represented using a standard boolean, integer or string value.
		 *
		 * @param aPrefName The complex preference to get the value of.
		 * @param aType     The XPCOM interface that this complex preference
		 *                  represents. Interfaces currently supported are:
		 *                    - nsILocalFile
		 *                    - nsISupportsString (UniChar)
		 *                    - nsIPrefLocalizedString (Localized UniChar)
		 * @param aValue    The XPCOM object into which to the complex preference 
		 *                  value should be retrieved.
		 *
		 * @return NS_OK The value was successfully retrieved.
		 * @return Other The value does not exist or is the wrong type.
		 *
		 * @see setComplexValue
		 */
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 1)]
		nsQIResult GetComplexValue([MarshalAs(UnmanagedType.LPStr)] String aPrefName, [In] ref Guid aType);

		/**
		 * Called to set the state of an individual complex preference. A complex
		 * preference is a preference which represents an XPCOM object that can not
		 * be easily represented using a standard boolean, integer or string value.
		 *
		 * @param aPrefName The complex preference to set the value of.
		 * @param aType     The XPCOM interface that this complex preference
		 *                  represents. Interfaces currently supported are:
		 *                    - nsILocalFile
		 *                    - nsISupportsString (UniChar)
		 *                    - nsIPrefLocalizedString (Localized UniChar)
		 * @param aValue    The XPCOM object from which to set the complex preference 
		 *                  value.
		 *
		 * @return NS_OK The value was successfully set.
		 * @return Other The value was not set or is the wrong type.
		 *
		 * @see getComplexValue
		 */
		void SetComplexValue([MarshalAs(UnmanagedType.LPStr)] String aPrefName, [In] ref Guid aType, [MarshalAs(UnmanagedType.IUnknown)] nsISupports aValue);

		/**
		 * Called to clear a user set value from a specific preference. This will, in
		 * effect, reset the value to the default value. If no default value exists
		 * the preference will cease to exist.
		 *
		 * @param aPrefName The preference to be cleared.
		 *
		 * @note
		 * This method does nothing if this object is a default branch.
		 *
		 * @return NS_OK The user preference was successfully cleared.
		 * @return Other The preference does not exist or have a user set value.
		 */
		void ClearUserPref([MarshalAs(UnmanagedType.LPStr)] String aPrefName);

		/**
		 * Called to lock a specific preference. Locking a preference will cause the
		 * preference service to always return the default value regardless of
		 * whether there is a user set value or not.
		 *
		 * @param aPrefName The preference to be locked.
		 *
		 * @note
		 * This method can be called on either a default or user branch but, in
		 * effect, always operates on the default branch.
		 *
		 * @return NS_OK The preference was successfully locked.
		 * @return Other The preference does not exist or an error occurred.
		 *
		 * @see unlockPref
		 */
		void LockPref([MarshalAs(UnmanagedType.LPStr)] String aPrefName);

		/**
		 * Called to check if a specific preference has a user value associated to
		 * it.
		 *
		 * @param aPrefName The preference to be tested.
		 *
		 * @note
		 * This method can be called on either a default or user branch but, in
		 * effect, always operates on the user branch.
		 *
		 * @note
		 * If a preference was manually set to a value that equals the default value,
		 * then the preference no longer has a user set value, i.e. it is
		 * considered reset to its default value.
		 * In particular, this method will return false for such a preference and
		 * the preference will not be saved to a file by nsIPrefService.savePrefFile.
		 *
		 * @return boolean  true  The preference has a user set value.
		 *                  false The preference only has a default value.
		 */
		Boolean PrefHasUserValue([MarshalAs(UnmanagedType.LPStr)] String aPrefName);

		/**
		 * Called to check if a specific preference is locked. If a preference is
		 * locked calling its Get method will always return the default value.
		 *
		 * @param aPrefName The preference to be tested.
		 *
		 * @note
		 * This method can be called on either a default or user branch but, in
		 * effect, always operates on the default branch.
		 *
		 * @return boolean  true  The preference is locked.
		 *                  false The preference is not locked.
		 *
		 * @see lockPref
		 * @see unlockPref
		 */
		Boolean PrefIsLocked([MarshalAs(UnmanagedType.LPStr)] String aPrefName);

		/**
		 * Called to unlock a specific preference. Unlocking a previously locked 
		 * preference allows the preference service to once again return the user set
		 * value of the preference.
		 *
		 * @param aPrefName The preference to be unlocked.
		 *
		 * @note
		 * This method can be called on either a default or user branch but, in
		 * effect, always operates on the default branch.
		 *
		 * @return NS_OK The preference was successfully unlocked.
		 * @return Other The preference does not exist or an error occurred.
		 *
		 * @see lockPref
		 */
		void UnlockPref([MarshalAs(UnmanagedType.LPStr)] String aPrefName);

		/**
		 * Called to remove all of the preferences referenced by this branch.
		 *
		 * @param aStartingAt The point on the branch at which to start the deleting
		 *                    preferences. Pass in "" to remove all preferences
		 *                    referenced by this branch.
		 *
		 * @note
		 * This method can be called on either a default or user branch but, in
		 * effect, always operates on both.
		 *
		 * @return NS_OK The preference(s) were successfully removed.
		 * @return Other The preference(s) do not exist or an error occurred.
		 */
		void DeleteBranch([MarshalAs(UnmanagedType.LPStr)] String aStartingAt);

		/**
		 * Returns an array of strings representing the child preferences of the
		 * root of this branch.
		 * 
		 * @param aStartingAt The point on the branch at which to start enumerating
		 *                    the child preferences. Pass in "" to enumerate all
		 *                    preferences referenced by this branch.
		 * @param aCount      Receives the number of elements in the array.
		 * @param aChildArray Receives the array of child preferences.
		 *
		 * @note
		 * This method can be called on either a default or user branch but, in
		 * effect, always operates on both.
		 *
		 * @return NS_OK The preference list was successfully retrieved.
		 * @return Other The preference(s) do not exist or an error occurred.
		 */
		IntPtr GetChildList([MarshalAs(UnmanagedType.LPStr)] String aStartingAt, [Optional] out UInt32 aCount);

		/**
		 * Called to reset all of the preferences referenced by this branch to their
		 * default values.
		 *
		 * @param aStartingAt The point on the branch at which to start the resetting
		 *                    preferences to their default values. Pass in "" to
		 *                    reset all preferences referenced by this branch.
		 *
		 * @note
		 * This method can be called on either a default or user branch but, in
		 * effect, always operates on the user branch.
		 *
		 * @return NS_OK The preference(s) were successfully reset.
		 * @return Other The preference(s) do not exist or an error occurred.
		 */
		void ResetBranch([MarshalAs(UnmanagedType.LPStr)] String aStartingAt);
	}
}
