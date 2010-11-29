using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;

namespace DotGecko.Gecko.Interop
{
	/**
	 * nsIDownloader
	 *
	 * A downloader is a special implementation of a nsIStreamListener that will
	 * make the contents of the stream available as a file.  This may utilize the
	 * disk cache as an optimization to avoid an extra copy of the data on disk.
	 * The resulting file is valid from the time the downloader completes until
	 * the last reference to the downloader is released.
	 */
	[ComImport, Guid("fafe41a9-a531-4d6d-89bc-588a6522fb4e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDownloader : nsIStreamListener
	{
		#region nsIRequestObserver Members

		new void OnStartRequest(nsIRequest aRequest, [MarshalAs(UnmanagedType.IUnknown)] Object aContext);
		new void OnStopRequest(nsIRequest aRequest, [MarshalAs(UnmanagedType.IUnknown)] Object aContext, UInt32 aStatusCode);

		#endregion

		#region nsIStreamListener Members

		new void OnDataAvailable(nsIRequest aRequest, [MarshalAs(UnmanagedType.IUnknown)] Object aContext, nsIInputStream aInputStream, UInt32 aOffset, UInt32 aCount);

		#endregion

		/**
		 * Initialize this downloader
		 *
		 * @param observer
		 *        the observer to be notified when the download completes.
		 * @param downloadLocation
		 *        the location where the stream contents should be written.
		 *        if null, the downloader will select a location and the
		 *        resulting file will be deleted (or otherwise made invalid)
		 *        when the downloader object is destroyed.  if an explicit
		 *        download location is specified then the resulting file will
		 *        not be deleted, and it will be the callers responsibility
		 *        to keep track of the file, etc.
		 */
		void Init(nsIDownloadObserver observer, nsIFile downloadLocation);
	}

	[ComImport, Guid("44b3153e-a54e-4077-a527-b0325e40924e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDownloadObserver //: nsISupports
	{
		/**
		 * Called to signal a download that has completed.
		 */
		void OnDownloadComplete(nsIDownloader downloader, nsIRequest request,
								[MarshalAs(UnmanagedType.IUnknown)] nsISupports ctxt,
								[MarshalAs(UnmanagedType.U4)] nsResult status,
								nsIFile result);
	}
}
