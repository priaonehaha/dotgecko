using System;

namespace DotGecko.Gecko.Interop
{
	internal static partial class Xpcom
	{
		/******************************************************************************
		 * netwerk/base/ classes
		 */

		// service implementing nsIIOService and nsIIOService2.
		internal const String NS_IOSERVICE_CLASSNAME = @"nsIOService";
		internal const String NS_IOSERVICE_CONTRACTID = @"@mozilla.org/network/io-service;1";
		/* 9ac9e770-18bc-11d3-9337-00104ba0fd40 */
		internal static readonly Guid NS_IOSERVICE_CID = new Guid(0x9ac9e770, 0x18bc, 0x11d3, 0x93, 0x37, 0x00, 0x10, 0x4b, 0xa0, 0xfd, 0x40);

		// service implementing nsINetUtil
		internal const String NS_NETUTIL_CONTRACTID = @"@mozilla.org/network/util;1";

		// service implementing nsIProtocolProxyService and nsPIProtocolProxyService.
		internal const String NS_PROTOCOLPROXYSERVICE_CLASSNAME = @"nsProtocolProxyService";
		internal const String NS_PROTOCOLPROXYSERVICE_CONTRACTID = @"@mozilla.org/network/protocol-proxy-service;1";
		/* E9B301C0-E0E4-11d3-A1A8-0050041CAF44 */
		internal static readonly Guid NS_PROTOCOLPROXYSERVICE_CID = new Guid(0xe9b301c0, 0xe0e4, 0x11d3, 0xa1, 0xa8, 0x0, 0x50, 0x4, 0x1c, 0xaf, 0x44);

		// service implementing nsIProxyAutoConfig.
		internal const String NS_PROXYAUTOCONFIG_CLASSNAME = @"nsProxyAutoConfig";
		internal const String NS_PROXYAUTOCONFIG_CONTRACTID = @"@mozilla.org/network/proxy-auto-config;1";
		/* 63ac8c66-1dd2-11b2-b070-84d00d3eaece */
		internal static readonly Guid NS_PROXYAUTOCONFIG_CID = new Guid(0x63ac8c66, 0x1dd2, 0x11b2, 0xb0, 0x70, 0x84, 0xd0, 0x0d, 0x3e, 0xae, 0xce);

		// component implementing nsILoadGroup.
		internal const String NS_LOADGROUP_CLASSNAME = @"nsLoadGroup";
		internal const String NS_LOADGROUP_CONTRACTID = @"@mozilla.org/network/load-group;1";
		/* e1c61582-2a84-11d3-8cce-0060b0fc14a3 */
		internal static readonly Guid NS_LOADGROUP_CID = new Guid(0xe1c61582, 0x2a84, 0x11d3, 0x8c, 0xce, 0x00, 0x60, 0xb0, 0xfc, 0x14, 0xa3);

		// component implementing nsIURI, nsISerializable, and nsIClassInfo.
		internal const String NS_SIMPLEURI_CLASSNAME = @"nsSimpleURI";
		internal const String NS_SIMPLEURI_CONTRACTID = @"@mozilla.org/network/simple-uri;1";
		/* e0da1d70-2f7b-11d3-8cd0-0060b0fc14a3 */
		internal static readonly Guid NS_SIMPLEURI_CID = new Guid(0xe0da1d70, 0x2f7b, 0x11d3, 0x8c, 0xd0, 0x00, 0x60, 0xb0, 0xfc, 0x14, 0xa3);

		// component inheriting from the simple URI component and also
		// implementing nsINestedURI.
		/* 56388dad-287b-4240-a785-85c394012503 */
		internal static readonly Guid NS_SIMPLENESTEDURI_CID = new Guid(0x56388dad, 0x287b, 0x4240, 0xa7, 0x85, 0x85, 0xc3, 0x94, 0x01, 0x25, 0x03);

		// component inheriting from the nested simple URI component and also
		// carrying along its base URI
		/* 2f277c00-0eaf-4ddb-b936-41326ba48aae */
		internal static readonly Guid NS_NESTEDABOUTURI_CID = new Guid(0x2f277c00, 0x0eaf, 0x4ddb, 0xb9, 0x36, 0x41, 0x32, 0x6b, 0xa4, 0x8a, 0xae);

		// component implementing nsIStandardURL, nsIURI, nsIURL, nsISerializable,
		// and nsIClassInfo.
		internal const String NS_STANDARDURL_CLASSNAME = @"nsStandardURL";
		internal const String NS_STANDARDURL_CONTRACTID = @"@mozilla.org/network/standard-url;1";
		/* de9472d0-8034-11d3-9399-00104ba0fd40 */
		internal static readonly Guid NS_STANDARDURL_CID = new Guid(0xde9472d0, 0x8034, 0x11d3, 0x93, 0x99, 0x00, 0x10, 0x4b, 0xa0, 0xfd, 0x40);

		// service implementing nsIURLParser that assumes the URL will NOT contain an
		// authority section.
		internal const String NS_NOAUTHURLPARSER_CLASSNAME = @"nsNoAuthURLParser";
		internal const String NS_NOAUTHURLPARSER_CONTRACTID = @"@mozilla.org/network/url-parser;1?auth=no";
		/* 78804a84-8173-42b6-bb94-789f0816a810 */
		internal static readonly Guid NS_NOAUTHURLPARSER_CID = new Guid(0x78804a84, 0x8173, 0x42b6, 0xbb, 0x94, 0x78, 0x9f, 0x08, 0x16, 0xa8, 0x10);

		// service implementing nsIURLParser that assumes the URL will contain an
		// authority section.
		internal const String NS_AUTHURLPARSER_CLASSNAME = @"nsAuthURLParser";
		internal const String NS_AUTHURLPARSER_CONTRACTID = @"@mozilla.org/network/url-parser;1?auth=yes";
		/* 275d800e-3f60-4896-adb7-d7f390ce0e42 */
		internal static readonly Guid NS_AUTHURLPARSER_CID = new Guid(0x275d800e, 0x3f60, 0x4896, 0xad, 0xb7, 0xd7, 0xf3, 0x90, 0xce, 0x0e, 0x42);

		// service implementing nsIURLParser that does not make any assumptions about
		// whether or not the URL contains an authority section.
		internal const String NS_STDURLPARSER_CLASSNAME = @"nsStdURLParser";
		internal const String NS_STDURLPARSER_CONTRACTID = @"@mozilla.org/network/url-parser;1?auth=maybe";
		/* ff41913b-546a-4bff-9201-dc9b2c032eba */
		internal static readonly Guid NS_STDURLPARSER_CID = new Guid(0xff41913b, 0x546a, 0x4bff, 0x92, 0x01, 0xdc, 0x9b, 0x2c, 0x03, 0x2e, 0xba);

		// component implementing nsIRequestObserverProxy.
		internal const String NS_REQUESTOBSERVERPROXY_CLASSNAME = @"nsRequestObserverProxy";
		internal const String NS_REQUESTOBSERVERPROXY_CONTRACTID = @"@mozilla.org/network/request-observer-proxy;1";
		/* 51fa28c7-74c0-4b85-9c46-d03faa7b696b */
		internal static readonly Guid NS_REQUESTOBSERVERPROXY_CID = new Guid(0x51fa28c7, 0x74c0, 0x4b85, 0x9c, 0x46, 0xd0, 0x3f, 0xaa, 0x7b, 0x69, 0x6b);

		// component implementing nsISimpleStreamListener.
		internal const String NS_SIMPLESTREAMLISTENER_CLASSNAME = @"nsSimpleStreamListener";
		internal const String NS_SIMPLESTREAMLISTENER_CONTRACTID = @"@mozilla.org/network/simple-stream-listener;1";
		/* fb8cbf4e-4701-4ba1-b1d6-5388e041fb67 */
		internal static readonly Guid NS_SIMPLESTREAMLISTENER_CID = new Guid(0xfb8cbf4e, 0x4701, 0x4ba1, 0xb1, 0xd6, 0x53, 0x88, 0xe0, 0x41, 0xfb, 0x67);

		// component implementing nsIStreamListenerTee.
		internal const String NS_STREAMLISTENERTEE_CLASSNAME = @"nsStreamListenerTee";
		internal const String NS_STREAMLISTENERTEE_CONTRACTID = @"@mozilla.org/network/stream-listener-tee;1";
		/* 831f8f13-7aa8-485f-b02e-77c881cc5773 */
		internal static readonly Guid NS_STREAMLISTENERTEE_CID = new Guid(0x831f8f13, 0x7aa8, 0x485f, 0xb0, 0x2e, 0x77, 0xc8, 0x81, 0xcc, 0x57, 0x73);

		// component implementing nsIAsyncStreamCopier.
		internal const String NS_ASYNCSTREAMCOPIER_CLASSNAME = @"nsAsyncStreamCopier";
		internal const String NS_ASYNCSTREAMCOPIER_CONTRACTID = @"@mozilla.org/network/async-stream-copier;1";
		/* e746a8b1-c97a-4fc5-baa4-66607521bd08 */
		internal static readonly Guid NS_ASYNCSTREAMCOPIER_CID = new Guid(0xe746a8b1, 0xc97a, 0x4fc5, 0xba, 0xa4, 0x66, 0x60, 0x75, 0x21, 0xbd, 0x08);

		// component implementing nsIInputStreamPump.
		internal const String NS_INPUTSTREAMPUMP_CLASSNAME = @"nsInputStreamPump";
		internal const String NS_INPUTSTREAMPUMP_CONTRACTID = @"@mozilla.org/network/input-stream-pump;1";
		/* ccd0e960-7947-4635-b70e-4c661b63d675 */
		internal static readonly Guid NS_INPUTSTREAMPUMP_CID = new Guid(0xccd0e960, 0x7947, 0x4635, 0xb7, 0x0e, 0x4c, 0x66, 0x1b, 0x63, 0xd6, 0x75);

		// component implementing nsIInputStreamChannel.
		internal const String NS_INPUTSTREAMCHANNEL_CLASSNAME = @"nsInputStreamChannel";
		internal const String NS_INPUTSTREAMCHANNEL_CONTRACTID = @"@mozilla.org/network/input-stream-channel;1";
		/* 6ddb050c-0d04-11d4-986e-00c04fa0cf4a */
		internal static readonly Guid NS_INPUTSTREAMCHANNEL_CID = new Guid(0x6ddb050c, 0x0d04, 0x11d4, 0x98, 0x6e, 0x00, 0xc0, 0x4f, 0xa0, 0xcf, 0x4a);

		// component implementing nsIStreamLoader.
		internal const String NS_STREAMLOADER_CLASSNAME = @"nsStreamLoader";
		internal const String NS_STREAMLOADER_CONTRACTID = @"@mozilla.org/network/stream-loader;1";
		/* 5BA6D920-D4E9-11d3-A1A5-0050041CAF44 */
		internal static readonly Guid NS_STREAMLOADER_CID = new Guid(0x5ba6d920, 0xd4e9, 0x11d3, 0xa1, 0xa5, 0x0, 0x50, 0x4, 0x1c, 0xaf, 0x44);

		// component implementing nsIUnicharStreamLoader.
		internal const String NS_UNICHARSTREAMLOADER_CLASSNAME = @"nsUnicharStreamLoader";
		internal const String NS_UNICHARSTREAMLOADER_CONTRACTID = @"@mozilla.org/network/unichar-stream-loader;1";
		/* 9445791f-fa4c-4669-b174-df5032bb67b3 */
		internal static readonly Guid NS_UNICHARSTREAMLOADER_CID = new Guid(0x9445791f, 0xfa4c, 0x4669, 0xb1, 0x74, 0xdf, 0x50, 0x32, 0xbb, 0x67, 0xb3);

		// component implementing nsIDownloader.
		internal const String NS_DOWNLOADER_CLASSNAME = @"nsDownloader";
		internal const String NS_DOWNLOADER_CONTRACTID = @"@mozilla.org/network/downloader;1";
		/* 510a86bb-6019-4ed1-bb4f-965cffd23ece */
		internal static readonly Guid NS_DOWNLOADER_CID = new Guid(0x510a86bb, 0x6019, 0x4ed1, 0xbb, 0x4f, 0x96, 0x5c, 0xff, 0xd2, 0x3e, 0xce);

		// component implementing nsISyncStreamListener.
		internal const String NS_SYNCSTREAMLISTENER_CLASSNAME = @"nsSyncStreamListener";
		internal const String NS_SYNCSTREAMLISTENER_CONTRACTID = @"@mozilla.org/network/sync-stream-listener;1";
		/* 439400d3-6f23-43db-8b06-8aafe1869bd8 */
		internal static readonly Guid NS_SYNCSTREAMLISTENER_CID = new Guid(0x439400d3, 0x6f23, 0x43db, 0x8b, 0x06, 0x8a, 0xaf, 0xe1, 0x86, 0x9b, 0xd8);

		// component implementing nsIURIChecker.
		internal const String NS_URICHECKER_CLASSNAME = @"nsURIChecker";
		internal const String NS_URICHECKER_CONTRACT_ID = @"@mozilla.org/network/urichecker;1";
		/* cf3a0e06-1dd1-11b2-a904-ac1d6da77a02 */
		internal static readonly Guid NS_URICHECKER_CID = new Guid(0xcf3a0e06, 0x1dd1, 0x11b2, 0xa9, 0x04, 0xac, 0x1d, 0x6d, 0xa7, 0x7a, 0x02);

		// component implementing nsIIncrementalDownload.
		internal const String NS_INCREMENTALDOWNLOAD_CONTRACTID = @"@mozilla.org/network/incremental-download;1";

		// component implementing nsISystemProxySettings.
		internal const String NS_SYSTEMPROXYSETTINGS_CONTRACTID = @"@mozilla.org/system-proxy-settings;1";

		// service implementing nsIStreamTransportService
		internal const String NS_STREAMTRANSPORTSERVICE_CLASSNAME = @"nsStreamTransportService";
		internal const String NS_STREAMTRANSPORTSERVICE_CONTRACTID = @"@mozilla.org/network/stream-transport-service;1";
		/* 0885d4f8-f7b8-4cda-902e-94ba38bc256e */
		internal static readonly Guid NS_STREAMTRANSPORTSERVICE_CID = new Guid(0x0885d4f8, 0xf7b8, 0x4cda, 0x90, 0x2e, 0x94, 0xba, 0x38, 0xbc, 0x25, 0x6e);

		// service implementing nsISocketTransportService
		internal const String NS_SOCKETTRANSPORTSERVICE_CLASSNAME = @"nsSocketTransportService";
		internal const String NS_SOCKETTRANSPORTSERVICE_CONTRACTID = @"@mozilla.org/network/socket-transport-service;1";
		/* c07e81e0-ef12-11d2-92b6-00105a1b0d64 */
		internal static readonly Guid NS_SOCKETTRANSPORTSERVICE_CID = new Guid(0xc07e81e0, 0xef12, 0x11d2, 0x92, 0xb6, 0x00, 0x10, 0x5a, 0x1b, 0x0d, 0x64);

		// component implementing nsIServerSocket
		internal const String NS_SERVERSOCKET_CLASSNAME = @"nsServerSocket";
		internal const String NS_SERVERSOCKET_CONTRACTID = @"@mozilla.org/network/server-socket;1";
		/* 2ec62893-3b35-48fa-ab1d-5e68a9f45f08 */
		internal static readonly Guid NS_SERVERSOCKET_CID = new Guid(0x2ec62893, 0x3b35, 0x48fa, 0xab, 0x1d, 0x5e, 0x68, 0xa9, 0xf4, 0x5f, 0x08);

		internal const String NS_LOCALFILEINPUTSTREAM_CLASSNAME = @"nsFileInputStream";
		internal const String NS_LOCALFILEINPUTSTREAM_CONTRACTID = @"@mozilla.org/network/file-input-stream;1";
		/* be9a53ae-c7e9-11d3-8cda-0060b0fc14a3 */
		internal static readonly Guid NS_LOCALFILEINPUTSTREAM_CID = new Guid(0xbe9a53ae, 0xc7e9, 0x11d3, 0x8c, 0xda, 0x00, 0x60, 0xb0, 0xfc, 0x14, 0xa3);

		internal const String NS_LOCALFILEOUTPUTSTREAM_CLASSNAME = @"nsFileOutputStream";
		internal const String NS_LOCALFILEOUTPUTSTREAM_CONTRACTID = @"@mozilla.org/network/file-output-stream;1";
		/* c272fee0-c7e9-11d3-8cda-0060b0fc14a3 */
		internal static readonly Guid NS_LOCALFILEOUTPUTSTREAM_CID = new Guid(0xc272fee0, 0xc7e9, 0x11d3, 0x8c, 0xda, 0x00, 0x60, 0xb0, 0xfc, 0x14, 0xa3);

		internal const String NS_BUFFEREDINPUTSTREAM_CLASSNAME = @"nsBufferedInputStream";
		internal const String NS_BUFFEREDINPUTSTREAM_CONTRACTID = @"@mozilla.org/network/buffered-input-stream;1";
		/* 9226888e-da08-11d3-8cda-0060b0fc14a3 */
		internal static readonly Guid NS_BUFFEREDINPUTSTREAM_CID = new Guid(0x9226888e, 0xda08, 0x11d3, 0x8c, 0xda, 0x00, 0x60, 0xb0, 0xfc, 0x14, 0xa3);

		internal const String NS_BUFFEREDOUTPUTSTREAM_CLASSNAME = @"nsBufferedOutputStream";
		internal const String NS_BUFFEREDOUTPUTSTREAM_CONTRACTID = @"@mozilla.org/network/buffered-output-stream;1";
		/* 9868b4ce-da08-11d3-8cda-0060b0fc14a3 */
		internal static readonly Guid NS_BUFFEREDOUTPUTSTREAM_CID = new Guid(0x9868b4ce, 0xda08, 0x11d3, 0x8c, 0xda, 0x00, 0x60, 0xb0, 0xfc, 0x14, 0xa3);

		// component implementing nsISafeOutputStream
		internal const String NS_SAFELOCALFILEOUTPUTSTREAM_CLASSNAME = @"nsSafeFileOutputStream";
		internal const String NS_SAFELOCALFILEOUTPUTSTREAM_CONTRACTID = @"@mozilla.org/network/safe-file-output-stream;1";
		/* a181af0d-68b8-4308-94db-d4f859058215 */
		internal static readonly Guid NS_SAFELOCALFILEOUTPUTSTREAM_CID = new Guid(0xa181af0d, 0x68b8, 0x4308, 0x94, 0xdb, 0xd4, 0xf8, 0x59, 0x05, 0x82, 0x15);

		// component implementing nsIPrivateBrowsingService
		internal const String NS_PRIVATE_BROWSING_SERVICE_CONTRACTID = @"@mozilla.org/privatebrowsing-wrapper;1";
		/* c31f4883-839b-45f6-82ad-a6a9bc5ad599 */
		internal static readonly Guid NS_PRIVATE_BROWSING_SERVICE_CID = new Guid(0xc31f4883, 0x839b, 0x45f6, 0x82, 0xad, 0xa6, 0xa9, 0xbc, 0x5a, 0xd5, 0x99);

		// component implementing nsIPrompt
		//
		// NOTE: this implementation does not have any way to correctly parent itself,
		//       it is almost always wrong to get a prompt via this interface.
		//       use nsIWindowWatcher instead whenever possible.
		//
		internal const String NS_DEFAULTPROMPT_CONTRACTID = @"@mozilla.org/network/default-prompt;1";

		// component implementing nsIAuthPrompt
		//
		// NOTE: this implementation does not have any way to correctly parent itself,
		//       it is almost always wrong to get an auth prompt via this interface.
		//       use nsIWindowWatcher instead whenever possible.
		//
		internal const String NS_DEFAULTAUTHPROMPT_CONTRACTID = @"@mozilla.org/network/default-auth-prompt;1";

		/******************************************************************************
		 * netwerk/cache/ classes
		 */

		// service implementing nsICacheService.
		internal const String NS_CACHESERVICE_CLASSNAME = @"nsCacheService";
		internal const String NS_CACHESERVICE_CONTRACTID = @"@mozilla.org/network/cache-service;1";
		/* 6c84aec9-29a5-4264-8fbc-bee8f922ea67 */
		internal static readonly Guid NS_CACHESERVICE_CID = new Guid(0x6c84aec9, 0x29a5, 0x4264, 0x8f, 0xbc, 0xbe, 0xe8, 0xf9, 0x22, 0xea, 0x67);

		// service implementing nsIApplicationCacheService.
		internal const String NS_APPLICATIONCACHESERVICE_CLASSNAME = @"nsApplicationCacheService";
		internal const String NS_APPLICATIONCACHESERVICE_CONTRACTID = @"@mozilla.org/network/application-cache-service;1";
		/* 02bf7a2a-39d8-4a23-a50c-2cbb085ab7a5 */
		internal static readonly Guid NS_APPLICATIONCACHESERVICE_CID = new Guid(0x02bf7a2a, 0x39d8, 0x4a23, 0xa5, 0x0c, 0x2c, 0xbb, 0x08, 0x5a, 0xb7, 0xa5);

		internal const String NS_APPLICATIONCACHENAMESPACE_CLASSNAME = @"nsApplicationCacheNamespace";
		internal const String NS_APPLICATIONCACHENAMESPACE_CONTRACTID = @"@mozilla.org/network/application-cache-namespace;1";
		/* b00ed78a-04e2-4f74-8e1c-d1af79dfd12f */
		internal static readonly Guid NS_APPLICATIONCACHENAMESPACE_CID = new Guid(0xb00ed78a, 0x04e2, 0x4f74, 0x8e, 0x1c, 0xd1, 0xaf, 0x79, 0xdf, 0xd1, 0x2f);

		/******************************************************************************
		 * netwerk/protocol/http/ classes
		 */

		/* 4f47e42e-4d23-4dd3-bfda-eb29255e9ea3 */
		internal static readonly Guid NS_HTTPPROTOCOLHANDLER_CID = new Guid(0x4f47e42e, 0x4d23, 0x4dd3, 0xbf, 0xda, 0xeb, 0x29, 0x25, 0x5e, 0x9e, 0xa3);

		/* dccbe7e4-7750-466b-a557-5ea36c8ff24e */
		internal static readonly Guid NS_HTTPSPROTOCOLHANDLER_CID = new Guid(0xdccbe7e4, 0x7750, 0x466b, 0xa5, 0x57, 0x5e, 0xa3, 0x6c, 0x8f, 0xf2, 0x4e);

		/* fca3766a-434a-4ae7-83cf-0909e18a093a */
		internal static readonly Guid NS_HTTPBASICAUTH_CID = new Guid(0xfca3766a, 0x434a, 0x4ae7, 0x83, 0xcf, 0x09, 0x09, 0xe1, 0x8a, 0x09, 0x3a);

		/* 17491ba4-1dd2-11b2-aae3-de6b92dab620 */
		internal static readonly Guid NS_HTTPDIGESTAUTH_CID = new Guid(0x17491ba4, 0x1dd2, 0x11b2, 0xaa, 0xe3, 0xde, 0x6b, 0x92, 0xda, 0xb6, 0x20);

		/* bbef8185-c628-4cc1-b53e-e61e74c2451a */
		internal static readonly Guid NS_HTTPNTLMAUTH_CID = new Guid(0xbbef8185, 0xc628, 0x4cc1, 0xb5, 0x3e, 0xe6, 0x1e, 0x74, 0xc2, 0x45, 0x1a);

		internal const String NS_HTTPAUTHMANAGER_CLASSNAME = @"nsHttpAuthManager";
		internal const String NS_HTTPAUTHMANAGER_CONTRACTID = @"@mozilla.org/network/http-auth-manager;1";
		/* 36b63ef3-e0fa-4c49-9fd4-e065e85568f4 */
		internal static readonly Guid NS_HTTPAUTHMANAGER_CID = new Guid(0x36b63ef3, 0xe0fa, 0x4c49, 0x9f, 0xd4, 0xe0, 0x65, 0xe8, 0x55, 0x68, 0xf4);

		internal const String NS_HTTPACTIVITYDISTRIBUTOR_CLASSNAME = @"nsHttpActivityDistributor";
		internal const String NS_HTTPACTIVITYDISTRIBUTOR_CONTRACTID = @"@mozilla.org/network/http-activity-distributor;1";
		/* 15629ada-a41c-4a09-961f-6553cd60b1a2 */
		internal static readonly Guid NS_HTTPACTIVITYDISTRIBUTOR_CID = new Guid(0x15629ada, 0xa41c, 0x4a09, 0x96, 0x1f, 0x65, 0x53, 0xcd, 0x60, 0xb1, 0xa2);

		/******************************************************************************
		 * netwerk/protocol/ftp/ classes
		 */

		internal const String NS_FTPPROTOCOLHANDLER_CLASSNAME = @"nsFtpProtocolHandler";
		/* 25029490-F132-11d2-9588-00805F369F95 */
		internal static readonly Guid NS_FTPPROTOCOLHANDLER_CID = new Guid(0x25029490, 0xf132, 0x11d2, 0x95, 0x88, 0x0, 0x80, 0x5f, 0x36, 0x9f, 0x95);

		/******************************************************************************
		 * netwerk/protocol/res/ classes
		 */

		internal const String NS_RESPROTOCOLHANDLER_CLASSNAME = @"nsResProtocolHandler";
		/* e64f152a-9f07-11d3-8cda-0060b0fc14a3 */
		internal static readonly Guid NS_RESPROTOCOLHANDLER_CID = new Guid(0xe64f152a, 0x9f07, 0x11d3, 0x8c, 0xda, 0x00, 0x60, 0xb0, 0xfc, 0x14, 0xa3);

		internal const String NS_RESURL_CLASSNAME = @"nsResURL";
		/* ff8fe7ec-2f74-4408-b742-6b7a546029a8 */
		internal static readonly Guid NS_RESURL_CID = new Guid(0xff8fe7ec, 0x2f74, 0x4408, 0xb7, 0x42, 0x6b, 0x7a, 0x54, 0x60, 0x29, 0xa8);


		/******************************************************************************
		 * netwerk/protocol/file/ classes
		 */

		internal const String NS_FILEPROTOCOLHANDLER_CLASSNAME = @"nsFileProtocolHandler";
		/* fbc81170-1f69-11d3-9344-00104ba0fd40 */
		internal static readonly Guid NS_FILEPROTOCOLHANDLER_CID = new Guid(0xfbc81170, 0x1f69, 0x11d3, 0x93, 0x44, 0x00, 0x10, 0x4b, 0xa0, 0xfd, 0x40);

		/******************************************************************************
		 * netwerk/protocol/data/ classes
		 */

		internal const String NS_DATAPROTOCOLHANDLER_CLASSNAME = @"nsDataProtocolHandler";
		/* {B6ED3030-6183-11d3-A178-0050041CAF44} */
		internal static readonly Guid NS_DATAPROTOCOLHANDLER_CID = new Guid(0xb6ed3030, 0x6183, 0x11d3, 0xa1, 0x78, 0x00, 0x50, 0x04, 0x1c, 0xaf, 0x44);

		/******************************************************************************
		 * netwerk/protocol/viewsource/ classes
		 */

		// service implementing nsIProtocolHandler
		/* {0x9c7ec5d1-23f9-11d5-aea8-8fcc0793e97f} */
		internal static readonly Guid NS_VIEWSOURCEHANDLER_CID = new Guid(0x9c7ec5d1, 0x23f9, 0x11d5, 0xae, 0xa8, 0x8f, 0xcc, 0x07, 0x93, 0xe9, 0x7f);

		/******************************************************************************
		 * netwerk/protocol/about/ classes
		 */

		internal const String NS_ABOUTPROTOCOLHANDLER_CLASSNAME = @"About Protocol Handler";
		/* 9e3b6c90-2f75-11d3-8cd0-0060b0fc14a3 */
		internal static readonly Guid NS_ABOUTPROTOCOLHANDLER_CID = new Guid(0x9e3b6c90, 0x2f75, 0x11d3, 0x8c, 0xd0, 0x00, 0x60, 0xb0, 0xfc, 0x14, 0xa3);

		internal const String NS_SAFEABOUTPROTOCOLHANDLER_CLASSNAME = @"Safe About Protocol Handler";
		/* 1423e739-782c-4081-b5d8-fe6fba68c0ef */
		internal static readonly Guid NS_SAFEABOUTPROTOCOLHANDLER_CID = new Guid(0x1423e739, 0x782c, 0x4081, 0xb5, 0xd8, 0xfe, 0x6f, 0xba, 0x68, 0xc0, 0xef);

		/******************************************************************************
		 * netwerk/dns/ classes
		 */

		internal const String NS_DNSSERVICE_CLASSNAME = @"nsDNSService";
		internal const String NS_DNSSERVICE_CONTRACTID = @"@mozilla.org/network/dns-service;1";
		/* b0ff4572-dae4-4bef-a092-83c1b88f6be9 */
		internal static readonly Guid NS_DNSSERVICE_CID = new Guid(0xb0ff4572, 0xdae4, 0x4bef, 0xa0, 0x92, 0x83, 0xc1, 0xb8, 0x8f, 0x6b, 0xe9);

		internal const String NS_IDNSERVICE_CLASSNAME = @"nsIDNService";
		/* ContractID of the XPCOM package that implements nsIIDNService */
		internal const String NS_IDNSERVICE_CONTRACTID = @"@mozilla.org/network/idn-service;1";
		/* 62b778a6-bce3-456b-8c31-2865fbb68c91 */
		internal static readonly Guid NS_IDNSERVICE_CID = new Guid(0x62b778a6, 0xbce3, 0x456b, 0x8c, 0x31, 0x28, 0x65, 0xfb, 0xb6, 0x8c, 0x91);

		internal const String NS_EFFECTIVETLDSERVICE_CLASSNAME = @"nsEffectiveTLDService";
		internal const String NS_EFFECTIVETLDSERVICE_CONTRACTID = @"@mozilla.org/network/effective-tld-service;1";
		/* cb9abbae-66b6-4609-8594-5c4ff300888e */
		internal static readonly Guid NS_EFFECTIVETLDSERVICE_CID = new Guid(0xcb9abbae, 0x66b6, 0x4609, 0x85, 0x94, 0x5c, 0x4f, 0xf3, 0x00, 0x88, 0x8e);


		/******************************************************************************
		 * netwerk/mime classes
		 */

		internal const String NS_MIMEHEADERPARAM_CLASSNAME = @"nsMIMEHeaderParamImpl";
		internal const String NS_MIMEHEADERPARAM_CONTRACTID = @"@mozilla.org/network/mime-hdrparam;1";
		// {1F4DBCF7-245C-4c8c-943D-8A1DA0495E8A} 
		internal static readonly Guid NS_MIMEHEADERPARAM_CID = new Guid(0x1f4dbcf7, 0x245c, 0x4c8c, 0x94, 0x3d, 0x8a, 0x1d, 0xa0, 0x49, 0x5e, 0x8a);


		/******************************************************************************
		 * netwerk/socket classes
		 */

		internal const String NS_SOCKETPROVIDERSERVICE_CLASSNAME = @"nsSocketProviderService";
		internal const String NS_SOCKETPROVIDERSERVICE_CONTRACTID = @"@mozilla.org/network/socket-provider-service;1";
		/* ed394ba0-5472-11d3-bbc8-0000861d1237 */
		internal static readonly Guid NS_SOCKETPROVIDERSERVICE_CID = new Guid(0xed394ba0, 0x5472, 0x11d3, 0xbb, 0xc8, 0x00, 0x00, 0x86, 0x1d, 0x12, 0x37);

		/* 8dbe7246-1dd2-11b2-9b8f-b9a849e4403a */
		internal static readonly Guid NS_SOCKSSOCKETPROVIDER_CID = new Guid(0x8dbe7246, 0x1dd2, 0x11b2, 0x9b, 0x8f, 0xb9, 0xa8, 0x49, 0xe4, 0x40, 0x3a);

		/* F7C9F5F4-4451-41c3-A28A-5BA2447FBACE */
		internal static readonly Guid NS_SOCKS4SOCKETPROVIDER_CID = new Guid(0xf7c9f5f4, 0x4451, 0x41c3, 0xa2, 0x8a, 0x5b, 0xa2, 0x44, 0x7f, 0xba, 0xce);

		/* 320706D2-2E81-42c6-89C3-8D83B17D3FB4 */
		internal static readonly Guid NS_UDPSOCKETPROVIDER_CID = new Guid(0x320706d2, 0x2e81, 0x42c6, 0x89, 0xc3, 0x8d, 0x83, 0xb1, 0x7d, 0x3f, 0xb4);

		internal const String NS_SSLSOCKETPROVIDER_CONTRACTID = NS_NETWORK_SOCKET_CONTRACTID_PREFIX + @"ssl";

		/* This code produces a normal socket which can be used to initiate the
		 * STARTTLS protocol by calling its nsISSLSocketControl->StartTLS()
		 */
		internal const String NS_STARTTLSSOCKETPROVIDER_CONTRACTID = NS_NETWORK_SOCKET_CONTRACTID_PREFIX + @"starttls";


		/******************************************************************************
		 * netwerk/cookie classes
		 */

		// service implementing nsICookieManager and nsICookieManager2.
		internal const String NS_COOKIEMANAGER_CLASSNAME = @"CookieManager";
		internal const String NS_COOKIEMANAGER_CONTRACTID = @"@mozilla.org/cookiemanager;1";
		/* aaab6710-0f2c-11d5-a53b-0010a401eb10 */
		internal static readonly Guid NS_COOKIEMANAGER_CID = new Guid(0xaaab6710, 0x0f2c, 0x11d5, 0xa5, 0x3b, 0x00, 0x10, 0xa4, 0x01, 0xeb, 0x10);

		// service implementing nsICookieService.
		internal const String NS_COOKIESERVICE_CLASSNAME = @"CookieService";
		internal const String NS_COOKIESERVICE_CONTRACTID = @"@mozilla.org/cookieService;1";
		/* c375fa80-150f-11d6-a618-0010a401eb10 */
		internal static readonly Guid NS_COOKIESERVICE_CID = new Guid(0xc375fa80, 0x150f, 0x11d6, 0xa6, 0x18, 0x00, 0x10, 0xa4, 0x01, 0xeb, 0x10);

		/******************************************************************************
		 * netwerk/wifi classes
		 */
		internal const String NS_WIFI_MONITOR_CLASSNAME = @"WIFI_MONITOR";
		internal const String NS_WIFI_MONITOR_CONTRACTID = @"@mozilla.org/wifi/monitor;1";

		internal static readonly Guid NS_WIFI_MONITOR_COMPONENT_CID = new Guid(0x3FF8FB9F, 0xEE63, 0x48DF, 0x89, 0xF0, 0xDA, 0xCE, 0x02, 0x42, 0xFD, 0x82);

		/******************************************************************************
		 * netwerk/streamconv classes
		 */

		// service implementing nsIStreamConverterService
		internal const String NS_STREAMCONVERTERSERVICE_CONTRACTID = @"@mozilla.org/streamConverters;1";
		/* 892FFEB0-3F80-11d3-A16C-0050041CAF44 */
		internal static readonly Guid NS_STREAMCONVERTERSERVICE_CID = new Guid(0x892ffeb0, 0x3f80, 0x11d3, 0xa1, 0x6c, 0x00, 0x50, 0x04, 0x1c, 0xaf, 0x44);

		/**
		 * General-purpose content sniffer component. Use with CreateInstance.
		 *
		 * Implements nsIContentSniffer
		 */
		internal const String NS_GENERIC_CONTENT_SNIFFER = @"@mozilla.org/network/content-sniffer;1";

		/**
		 * Detector that can act as either an nsIStreamConverter or an
		 * nsIContentSniffer to decide whether text/plain data is "really" text/plain
		 * or APPLICATION_GUESS_FROM_EXT.  Use with CreateInstance.
		 */
		internal const String NS_BINARYDETECTOR_CONTRACTID = @"@mozilla.org/network/binary-detector;1";

		/******************************************************************************
		 * netwerk/system classes
		 */

		// service implementing nsINetworkLinkService
		internal const String NS_NETWORK_LINK_SERVICE_CLASSNAME = @"Network Link Status";
		internal static readonly Guid NS_NETWORK_LINK_SERVICE_CID = new Guid(0x75a500a2, 0x0030, 0x40f7, 0x86, 0xf8, 0x63, 0xf2, 0x25, 0xb9, 0x40, 0xae);

		/******************************************************************************
		 * Contracts that can be implemented by necko users.
		 */

		/**
		 * This contract ID will be gotten as a service and gets the opportunity to look
		 * at and veto all redirects that are processed by necko.
		 *
		 * Must implement nsIChannelEventSink
		 */
		internal const String NS_GLOBAL_CHANNELEVENTSINK_CONTRACTID = @"@mozilla.org/netwerk/global-channel-event-sink;1";

		/**
		 * This contract ID will be gotten as a service implementing nsINetworkLinkService
		 * and monitored by IOService for automatic online/offline management.
		 *
		 * Must implement nsINetworkLinkService
		 */
		internal const String NS_NETWORK_LINK_SERVICE_CONTRACTID = @"@mozilla.org/network/network-link-service;1";

		/**
		 * This contract ID is used when Necko needs to wrap an nsIAuthPrompt as
		 * nsIAuthPrompt2. Implementing it is required for backwards compatibility
		 * with Versions before 1.9.
		 *
		 * Must implement nsIAuthPromptAdapterFactory
		 */
		internal const String NS_AUTHPROMPT_ADAPTER_FACTORY_CONTRACTID = @"@mozilla.org/network/authprompt-adapter-factory;1";

		/**
		 * Must implement nsICryptoHash.
		 */
		internal const String NS_CRYPTO_HASH_CONTRACTID = @"@mozilla.org/security/hash;1";

		/**
		 * Must implement nsICryptoHMAC.
		 */
		internal const String NS_CRYPTO_HMAC_CONTRACTID = @"@mozilla.org/security/hmac;1";

		/******************************************************************************
		 * Categories
		 */
		/**
		 * Services registered in this category will get notified via
		 * nsIChannelEventSink about all redirects that happen and have the opportunity
		 * to veto them. The value of the category entries is interpreted as the
		 * contract ID of the service.
		 */
		internal const String NS_CHANNEL_EVENT_SINK_CATEGORY = @"net-channel-event-sinks";

		/**
		 * Services in this category will get told about each load that happens and get
		 * the opportunity to override the detected MIME type via nsIContentSniffer.
		 * Services should not set the MIME type on the channel directly, but return the
		 * new type. If getMIMETypeFromContent throws an exception, the type will remain
		 * unchanged.
		 *
		 * Note that only channels with the LOAD_CALL_CONTENT_SNIFFERS flag will call
		 * content sniffers. Also note that there can be security implications about
		 * changing the MIME type -- proxies filtering responses based on their MIME
		 * type might consider certain types to be safe, which these sniffers can
		 * override.
		 *
		 * Not all channels may implement content sniffing. See also
		 * nsIChannel::LOAD_CALL_CONTENT_SNIFFERS.
		 */
		internal const String NS_CONTENT_SNIFFER_CATEGORY = @"net-content-sniffers";

		/**
		 * Must implement nsINSSErrorsService.
		 */
		internal const String NS_NSS_ERRORS_SERVICE_CONTRACTID = @"@mozilla.org/nss_errors_service;1";
	}
}
