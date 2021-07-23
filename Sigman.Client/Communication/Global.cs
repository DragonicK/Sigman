using Sigman.Client.Controller;
using Sigman.Client.Network;
using Sigman.Core.IO;

namespace Sigman.Client.Communication {
    public static class Global {
        public static FileDownload Downloader { get; set; }
        public static ClientTcp Socket { get; set; }
        public static Packet Packet { get; set; }
        public static Forms Forms { get; set; }

        public static bool Running = false;

        public static bool Authenticated { get; set; }

        public static string OutputFolder {
            get => Forms.Main.OutputFolder;
        }

        public static void Initialize() {
            OperationCode.Initialize();

            Packet = new Packet();

            Socket = new ClientTcp();
            Socket.Start();

            Forms = new Forms(Socket, Packet);

            Downloader = new FileDownload();
            Downloader.OnDownloadCompleted += OnDownloadCompleted;
            Downloader.OnDownloadFailed += OnDownloadFailed; 
            Downloader.OnDownloadUpdateProgress += OnDownloadUpdateProgress; 
        }

        public static void StopDownload() {
            if (Downloader != null) {
                Downloader.Close();
                Downloader.SetFileData(string.Empty, string.Empty, 0);
                Downloader.Reset();   
            }
        }

        public static void OnDownloadUpdateProgress(int progress) {
            if (Downloader != null) {
                Forms.Main.UpdateReceivedProgress(progress);
            }
        }

        public static void OnDownloadCompleted(string folder, string file) {
            if (Downloader != null) {
                Downloader.Reset();
                //Forms.Main.StopProcess();
            }
        }

        public static void OnDownloadFailed(string folder, string file) {
            Forms.ShowFailedMessage("Download failed");
        }
    }
}