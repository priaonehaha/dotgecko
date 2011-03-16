using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	/**
	 * The nsIDOMValidityState interface is the interface to a ValidityState
	 * object which represents the validity states of an element.
	 *
	 * For more information on this interface please see
	 * http://www.whatwg.org/specs/web-apps/current-work/#validitystate
	 */
	[ComImport, Guid("5e62197a-9b74-4812-b5a2-ca102e886f7a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMValidityState //: nsISupports
	{
		Boolean ValueMissing { get; }
		Boolean TypeMismatch { get; }
		Boolean PatternMismatch { get; }
		Boolean TooLong { get; }
		Boolean RangeUnderflow { get; }
		Boolean RangeOverflow { get; }
		Boolean StepMismatch { get; }
		Boolean CustomError { get; }
		Boolean Valid { get; }
	}
}
