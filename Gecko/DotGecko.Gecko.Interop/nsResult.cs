using System;

namespace DotGecko.Gecko.Interop
{
	public static class nsResultExtensions
	{
		/**
		 * @name Standard Module Offset Code. Each Module should identify a unique number
		 *       and then all errors associated with that module become offsets from the
		 *       base associated with that module id. There are 16 bits of code bits for
		 *       each module.
		 */

		public const UInt32 NS_ERROR_MODULE_XPCOM = 1;
		public const UInt32 NS_ERROR_MODULE_BASE = 2;
		public const UInt32 NS_ERROR_MODULE_GFX = 3;
		public const UInt32 NS_ERROR_MODULE_WIDGET = 4;
		public const UInt32 NS_ERROR_MODULE_CALENDAR = 5;
		public const UInt32 NS_ERROR_MODULE_NETWORK = 6;
		public const UInt32 NS_ERROR_MODULE_PLUGINS = 7;
		public const UInt32 NS_ERROR_MODULE_LAYOUT = 8;
		public const UInt32 NS_ERROR_MODULE_HTMLPARSER = 9;
		public const UInt32 NS_ERROR_MODULE_RDF = 10;
		public const UInt32 NS_ERROR_MODULE_UCONV = 11;
		public const UInt32 NS_ERROR_MODULE_REG = 12;
		public const UInt32 NS_ERROR_MODULE_FILES = 13;
		public const UInt32 NS_ERROR_MODULE_DOM = 14;
		public const UInt32 NS_ERROR_MODULE_IMGLIB = 15;
		public const UInt32 NS_ERROR_MODULE_MAILNEWS = 16;
		public const UInt32 NS_ERROR_MODULE_EDITOR = 17;
		public const UInt32 NS_ERROR_MODULE_XPCONNECT = 18;
		public const UInt32 NS_ERROR_MODULE_PROFILE = 19;
		public const UInt32 NS_ERROR_MODULE_LDAP = 20;
		public const UInt32 NS_ERROR_MODULE_SECURITY = 21;
		public const UInt32 NS_ERROR_MODULE_DOM_XPATH = 22;
		public const UInt32 NS_ERROR_MODULE_DOM_RANGE = 23;
		public const UInt32 NS_ERROR_MODULE_URILOADER = 24;
		public const UInt32 NS_ERROR_MODULE_CONTENT = 25;
		public const UInt32 NS_ERROR_MODULE_PYXPCOM = 26;
		public const UInt32 NS_ERROR_MODULE_XSLT = 27;
		public const UInt32 NS_ERROR_MODULE_IPC = 28;
		public const UInt32 NS_ERROR_MODULE_SVG = 29;
		public const UInt32 NS_ERROR_MODULE_STORAGE = 30;
		public const UInt32 NS_ERROR_MODULE_SCHEMA = 31;
		public const UInt32 NS_ERROR_MODULE_DOM_FILE = 32;

		/* NS_ERROR_MODULE_GENERAL should be used by modules that do not
		 * care if return code values overlap. Callers of methods that
		 * return such codes should be aware that they are not
		 * globally unique. Implementors should be careful about blindly
		 * returning codes from other modules that might also use
		 * the generic base.
		 */
		public const UInt32 NS_ERROR_MODULE_GENERAL = 51;

		/**
		 * @name Severity Code.  This flag identifies the level of warning
		 */

		public const UInt32 NS_ERROR_SEVERITY_SUCCESS = 0;
		public const UInt32 NS_ERROR_SEVERITY_ERROR = 1;

		/**
		 * @name Standard Error Handling Macros
		 * @return 0 or 1
		 */

		public static Boolean NS_FAILED(this nsResult nsresult)
		{
			return ((UInt32)nsresult & 0x80000000) != 0;
		}

		public static Boolean NS_SUCCEEDED(this nsResult nsresult)
		{
			return ((UInt32)nsresult & 0x80000000) == 0;
		}

		/**
		 * @name Mozilla Code.  This flag separates consumers of mozilla code
		 *       from the native platform
		 */

		public const UInt32 NS_ERROR_MODULE_BASE_OFFSET = 0x45;

		/**
		 * @name Standard Error Generating Macros
		 */

		public static nsResult NS_ERROR_GENERATE(UInt32 sev, UInt32 module, UInt32 code)
		{
			return (nsResult)((sev << 31) | ((module + NS_ERROR_MODULE_BASE_OFFSET) << 16) | code);
		}

		public static nsResult NS_ERROR_GENERATE_SUCCESS(UInt32 module, UInt32 code)
		{
			return NS_ERROR_GENERATE(NS_ERROR_SEVERITY_SUCCESS, module, code);
		}

		public static nsResult NS_ERROR_GENERATE_FAILURE(UInt32 module, UInt32 code)
		{
			return NS_ERROR_GENERATE(NS_ERROR_SEVERITY_ERROR, module, code);
		}

		/**
		 * @name Standard Macros for retrieving error bits
		 */

		public static UInt32 NS_ERROR_GET_CODE(this nsResult nsresult)
		{
			return (UInt32)nsresult & 0xffff;
		}

		public static UInt32 NS_ERROR_GET_MODULE(this nsResult nsresult)
		{
			return (((UInt32)nsresult >> 16) - NS_ERROR_MODULE_BASE_OFFSET) & 0x1fff;
		}

		public static UInt32 NS_ERROR_GET_SEVERITY(this nsResult nsresult)
		{
			return ((UInt32)nsresult >> 31) & 0x1;
		}
	}

	public enum nsResult : uint
	{
		#region  Standard return values

		/* Standard "it worked" return value */
		NS_OK = 0,

		NS_ERROR_BASE = 0xC1F30000,

		/* Returned when an instance is not initialized */
		NS_ERROR_NOT_INITIALIZED = (NS_ERROR_BASE + 1),

		/* Returned when an instance is already initialized */
		NS_ERROR_ALREADY_INITIALIZED = (NS_ERROR_BASE + 2),

		/* Returned by a not implemented function */
		NS_ERROR_NOT_IMPLEMENTED = 0x80004001,

		/* Returned when a given interface is not supported. */
		NS_NOINTERFACE = 0x80004002,
		NS_ERROR_NO_INTERFACE = NS_NOINTERFACE,

		NS_ERROR_INVALID_POINTER = 0x80004003,
		NS_ERROR_NULL_POINTER = NS_ERROR_INVALID_POINTER,

		/* Returned when a function aborts */
		NS_ERROR_ABORT = 0x80004004,

		/* Returned when a function fails */
		NS_ERROR_FAILURE = 0x80004005,

		/* Returned when an unexpected error occurs */
		NS_ERROR_UNEXPECTED = 0x8000ffff,

		/* Returned when a memory allocation fails */
		NS_ERROR_OUT_OF_MEMORY = 0x8007000e,

		/* Returned when an illegal value is passed */
		NS_ERROR_ILLEGAL_VALUE = 0x80070057,
		NS_ERROR_INVALID_ARG = NS_ERROR_ILLEGAL_VALUE,

		/* Returned when a class doesn't allow aggregation */
		NS_ERROR_NO_AGGREGATION = 0x80040110,

		/* Returned when an operation can't complete due to an unavailable resource */
		NS_ERROR_NOT_AVAILABLE = 0x80040111,

		/* Returned when a class is not registered */
		NS_ERROR_FACTORY_NOT_REGISTERED = 0x80040154,

		/* Returned when a class cannot be registered, but may be tried again later */
		NS_ERROR_FACTORY_REGISTER_AGAIN = 0x80040155,

		/* Returned when a dynamically loaded factory couldn't be found */
		NS_ERROR_FACTORY_NOT_LOADED = 0x800401f8,

		/* Returned when a factory doesn't support signatures */
		NS_ERROR_FACTORY_NO_SIGNATURE_SUPPORT = (NS_ERROR_BASE + 0x101),

		/* Returned when a factory already is registered */
		NS_ERROR_FACTORY_EXISTS = (NS_ERROR_BASE + 0x100),


		/* For COM compatibility reasons, we want to use exact error code numbers
		   for NS_ERROR_PROXY_INVALID_IN_PARAMETER and NS_ERROR_PROXY_INVALID_OUT_PARAMETER.
		   The first matches:

			 #define RPC_E_INVALID_PARAMETER          _HRESULT_TYPEDEF_(0x80010010L)

		   Errors returning this mean that the xpcom proxy code could not create a proxy for
		   one of the in paramaters.

		   Because of this, we are ignoring the convention if using a base and offset for
		   error numbers.

		*/

		/* Returned when a proxy could not be create a proxy for one of the IN parameters
		   This is returned only when the "real" method has NOT been invoked.
		*/

		NS_ERROR_PROXY_INVALID_IN_PARAMETER = 0x80010010,

		/* Returned when a proxy could not be create a proxy for one of the OUT parameters
		   This is returned only when the "real" method has ALREADY been invoked.
		*/

		NS_ERROR_PROXY_INVALID_OUT_PARAMETER = 0x80010011,

		#endregion

		/* I/O Errors */

		/*  Stream closed */
		NS_BASE_STREAM_CLOSED = 0x80470002, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_BASE, 2)
		/*  Error from the operating system */
		NS_BASE_STREAM_OSERROR = 0x80470003, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_BASE, 3)
		/*  Illegal arguments */
		NS_BASE_STREAM_ILLEGAL_ARGS = 0x80470004, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_BASE, 4)
		/*  For unichar streams */
		NS_BASE_STREAM_NO_CONVERTER = 0x80470005, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_BASE, 5)
		/*  For unichar streams */
		NS_BASE_STREAM_BAD_CONVERSION = 0x80470006, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_BASE, 6)

		NS_BASE_STREAM_WOULD_BLOCK = 0x80470007, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_BASE, 7)


		NS_ERROR_FILE_UNRECOGNIZED_PATH = 0x80520001, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 1)
		NS_ERROR_FILE_UNRESOLVABLE_SYMLINK = 0x80520002, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 2)
		NS_ERROR_FILE_EXECUTION_FAILED = 0x80520003, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 3)
		NS_ERROR_FILE_UNKNOWN_TYPE = 0x80520004, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 4)
		NS_ERROR_FILE_DESTINATION_NOT_DIR = 0x80520005, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 5)
		NS_ERROR_FILE_TARGET_DOES_NOT_EXIST = 0x80520006, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 6)
		NS_ERROR_FILE_COPY_OR_MOVE_FAILED = 0x80520007, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 7)
		NS_ERROR_FILE_ALREADY_EXISTS = 0x80520008, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 8)
		NS_ERROR_FILE_INVALID_PATH = 0x80520009, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 9)
		NS_ERROR_FILE_DISK_FULL = 0x8052000A, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 10)
		NS_ERROR_FILE_CORRUPTED = 0x8052000B, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 11)
		NS_ERROR_FILE_NOT_DIRECTORY = 0x8052000C, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 12)
		NS_ERROR_FILE_IS_DIRECTORY = 0x8052000D, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 13)
		NS_ERROR_FILE_IS_LOCKED = 0x8052000E, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 14)
		NS_ERROR_FILE_TOO_BIG = 0x8052000F, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 15)
		NS_ERROR_FILE_NO_DEVICE_SPACE = 0x80520010, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 16)
		NS_ERROR_FILE_NAME_TOO_LONG = 0x80520011, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 17)
		NS_ERROR_FILE_NOT_FOUND = 0x80520012, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 18)
		NS_ERROR_FILE_READ_ONLY = 0x80520013, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 19)
		NS_ERROR_FILE_DIR_NOT_EMPTY = 0x80520014, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 20)
		NS_ERROR_FILE_ACCESS_DENIED = 0x80520015, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_FILES, 21)

		NS_SUCCESS_FILE_DIRECTORY_EMPTY = 0x00520001, //NS_ERROR_GENERATE_SUCCESS(NS_ERROR_MODULE_FILES, 1)

		/* Result codes used by nsIDirectoryServiceProvider2 */

		NS_SUCCESS_AGGREGATE_RESULT = 0x00520002, //NS_ERROR_GENERATE_SUCCESS(NS_ERROR_MODULE_FILES, 2)

		/* Result codes used by nsIVariant */

		NS_ERROR_CANNOT_CONVERT_DATA = 0x80460001, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_XPCOM,  1)
		NS_ERROR_OBJECT_IS_IMMUTABLE = 0x80460002, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_XPCOM,  2)
		NS_ERROR_LOSS_OF_SIGNIFICANT_DATA = 0x80460003, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_XPCOM,  3)

		NS_SUCCESS_LOSS_OF_INSIGNIFICANT_DATA = 0x00460001, //NS_ERROR_GENERATE_SUCCESS(NS_ERROR_MODULE_XPCOM,  1)

		/**
		 * Various operations are not permitted during XPCOM shutdown and will fail
		 * with this exception.
		 */
		NS_ERROR_ILLEGAL_DURING_SHUTDOWN = 0x8046001E, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_XPCOM, 30)

		#region NETWORKING ERROR CODES

		/******************************************************************************
		 * General async request error codes:
		 * 
		 * These error codes are commonly passed through callback methods to indicate
		 * the status of some requested async request.
		 *
		 * For example, see nsIRequestObserver::onStopRequest.
		 */

		/**
		 * The async request completed successfully.
		 */
		NS_BINDING_SUCCEEDED = NS_OK,

		/**
		 * The async request failed for some unknown reason.
		 */
		NS_BINDING_FAILED = 0x804B0001, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 1)

		/**
		 * The async request failed because it was aborted by some user action.
		 */
		NS_BINDING_ABORTED = 0x804B0002, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 2)

		/**
		 * The async request has been "redirected" to a different async request.
		 * (e.g., an HTTP redirect occured).
		 *
		 * This error code is used with load groups to notify the load group observer
		 * when a request in the load group is redirected to another request.
		 */
		NS_BINDING_REDIRECTED = 0x804B0003, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 3)

		/**
		 * The async request has been "retargeted" to a different "handler."
		 *
		 * This error code is used with load groups to notify the load group observer
		 * when a request in the load group is removed from the load group and added
		 * to a different load group.
		 */
		NS_BINDING_RETARGETED = 0x804B0004, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 4)


		/******************************************************************************
		 * Miscellaneous error codes:
		 *
		 * These errors are not typically passed via onStopRequest.
		 */

		/**
		 * The URI is malformed.
		 */
		NS_ERROR_MALFORMED_URI = 0x804B000A, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 10)

		/**
		 * The URI scheme corresponds to an unknown protocol handler.
		 */
		NS_ERROR_UNKNOWN_PROTOCOL = 0x804B0012, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 18)

		/**
		 * Returned from nsIChannel::asyncOpen to indicate that OnDataAvailable will
		 * not be called because there is no content available.
		 *
		 * This is used by helper app style protocols (e.g., mailto).
		 *
		 * XXX perhaps this should be a success code.
		 */
		NS_ERROR_NO_CONTENT = 0x804B0011, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 17)

		/**
		 * The requested action could not be completed while the object is busy.
		 *
		 * Implementations of nsIChannel::asyncOpen will commonly return this error
		 * if the channel has already been opened (and has not yet been closed).
		 */
		NS_ERROR_IN_PROGRESS = 0x804B000F, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 15)

		/**
		 * Returned from nsIChannel::asyncOpen when trying to open the channel again
		 * (reopening is not supported).
		 */
		NS_ERROR_ALREADY_OPENED = 0x804B0049, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 73)

		/**
		 * The content encoding of the source document was incorrect, for example
		 * returning a plain HTML document advertised as Content-Encoding: gzip
		 */
		NS_ERROR_INVALID_CONTENT_ENCODING = 0x804B001B, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 27)

		/******************************************************************************
		 * Connectivity error codes:
		 */

		/**
		 * The connection is already established.
		 * XXX currently unused - consider removing.
		 */
		NS_ERROR_ALREADY_CONNECTED = 0x804B000B, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 11)

		/**
		 * The connection does not exist.
		 * XXX currently unused - consider removing.
		 */
		NS_ERROR_NOT_CONNECTED = 0x804B000C, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 12)

		/**
		 * The connection attempt failed, for example, because no server was listening
		 * at specified host:port.
		 */
		NS_ERROR_CONNECTION_REFUSED = 0x804B000D, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 13)

		/**
		 * The connection attempt to a proxy failed.
		 */
		NS_ERROR_PROXY_CONNECTION_REFUSED = 0x804B0048, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 72)

		/**
		 * The connection was lost due to a timeout error.
		 */
		NS_ERROR_NET_TIMEOUT = 0x804B000E, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 14)

		/**
		 * The requested action could not be completed while the networking library
		 * is in the offline state.
		 */
		NS_ERROR_OFFLINE = 0x804B0010, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 16)

		/**
		 * The requested action was prohibited because it would have caused the
		 * networking library to establish a connection to an unsafe or otherwise
		 * banned port.
		 */
		NS_ERROR_PORT_ACCESS_NOT_ALLOWED = 0x804B0013, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 19)

		/**
		 * The connection was established, but no data was ever received.
		 */
		NS_ERROR_NET_RESET = 0x804B0014, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 20)

		/**
		 * The connection was established, but the data transfer was interrupted.
		 */
		NS_ERROR_NET_INTERRUPT = 0x804B0047, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 71)

		// XXX really need to better rationalize these error codes.  are consumers of
		//     necko really expected to know how to discern the meaning of these??


		/**
		 * This request is not resumable, but it was tried to resume it, or to
		 * request resume-specific data.
		 */
		NS_ERROR_NOT_RESUMABLE = 0x804B0019, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 25)

		/**
		 * It was attempted to resume the request, but the entity has changed in the
		 * meantime.
		 */
		NS_ERROR_ENTITY_CHANGED = 0x804B0020, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 32)

		/**
		 * The request failed as a result of a detected redirection loop.
		 */
		NS_ERROR_REDIRECT_LOOP = 0x804B001F, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 31)

		/**
		 * The request failed because the content type returned by the server was
		 * not a type expected by the channel (for nested channels such as the JAR
		 * channel).
		 */
		NS_ERROR_UNSAFE_CONTENT_TYPE = 0x804B004A, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 74)

		/******************************************************************************
		 * FTP specific error codes:
		 *
		 * XXX document me
		 */

		NS_ERROR_FTP_LOGIN = 0x804B0015, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 21)

		NS_ERROR_FTP_CWD = 0x804B0016, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 22)

		NS_ERROR_FTP_PASV = 0x804B0017, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 23)

		NS_ERROR_FTP_PWD = 0x804B0018, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 24)

		NS_ERROR_FTP_LIST = 0x804B001C, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 28)

		/******************************************************************************
		 * DNS specific error codes:
		 */

		/**
		 * The lookup of a hostname failed.  This generally refers to the hostname
		 * from the URL being loaded.
		 */
		NS_ERROR_UNKNOWN_HOST = 0x804B001E, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 30)

		/**
		 * A low or medium priority DNS lookup failed because the pending
		 * queue was already full. High priorty (the default) always
		 * makes room
		 */
		NS_ERROR_DNS_LOOKUP_QUEUE_FULL = 0x804B0021, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 33)

		/**
		 * The lookup of a proxy hostname failed.
		 *
		 * If a channel is configured to speak to a proxy server, then it will
		 * generate this error if the proxy hostname cannot be resolved.
		 */
		NS_ERROR_UNKNOWN_PROXY_HOST = 0x804B002A, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 42)


		/******************************************************************************
		 * Socket specific error codes:
		 */

		/**
		 * The specified socket type does not exist.
		 */
		NS_ERROR_UNKNOWN_SOCKET_TYPE = 0x804B0033, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 51)

		/**
		 * The specified socket type could not be created.
		 */
		NS_ERROR_SOCKET_CREATE_FAILED = 0x804B0034, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 52)


		/******************************************************************************
		 * Cache specific error codes:
		 *
		 * XXX document me
		 */

		NS_ERROR_CACHE_KEY_NOT_FOUND = 0x804B003D, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 61)

		NS_ERROR_CACHE_DATA_IS_STREAM = 0x804B003E, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 62)

		NS_ERROR_CACHE_DATA_IS_NOT_STREAM = 0x804B003F, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 63)

		NS_ERROR_CACHE_WAIT_FOR_VALIDATION = 0x804B0040, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 64)

		NS_ERROR_CACHE_ENTRY_DOOMED = 0x804B0041, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 65)

		NS_ERROR_CACHE_READ_ACCESS_DENIED = 0x804B0042, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 66)

		NS_ERROR_CACHE_WRITE_ACCESS_DENIED = 0x804B0043, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 67)

		NS_ERROR_CACHE_IN_USE = 0x804B0044, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 68)

		/**
		 * Error passed through onStopRequest if the document could not be fetched
		 * from the cache.
		 */
		NS_ERROR_DOCUMENT_NOT_CACHED = 0x804B0046, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 70)


		/******************************************************************************
		 * Effective TLD Service specific error codes:
		 */

		/**
		 * The requested number of domain levels exceeds those present in the host string.
		 */
		NS_ERROR_INSUFFICIENT_DOMAIN_LEVELS = 0x804B0050, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 80)

		/**
		 * The host string is an IP address.
		 */
		NS_ERROR_HOST_IS_IP_ADDRESS = 0x804B0051, //NS_ERROR_GENERATE_FAILURE(NS_ERROR_MODULE_NETWORK, 81)


		/******************************************************************************
		 * StreamLoader specific result codes:
		 */

		/**
		 * Result code returned by nsIStreamLoaderObserver to indicate that
		 * the observer is taking over responsibility for the data buffer,
		 * and the loader should NOT free it.
		 */
		NS_SUCCESS_ADOPTED_DATA = 0x004B005A, //NS_ERROR_GENERATE_SUCCESS(NS_ERROR_MODULE_NETWORK, 90)

		#endregion
	}
}
