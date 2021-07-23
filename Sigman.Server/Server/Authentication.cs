using Sigman.Core.IO;
using Sigman.Core.Common;
using Sigman.Core.Network;
using Sigman.Server.Communication;
using Sigman.Server.Configuration;
using Sigman.Server.Network.Packet;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Sigman.Server.Server {
    public static class Authentication {
        public static List<Connection> Connections { get; set; } = new List<Connection>();
        public static Dictionary<string, FileDownload> FileDownloader { get; set; } = new Dictionary<string, FileDownload>();
        public static Dictionary<string, FileDownload> IconDownloader { get; set; } = new Dictionary<string, FileDownload>();

        public static void Add(Connection connection) {
            connection.OnDisconnect += OnDisconnect;

            Connections.Add(connection);

            Global.WriteLog($"Connected: {connection.IpAddress.Ip}", "Green");
        }

        public static void Remove(Connection connection) {
            Connections.Remove(connection);
        }

        public static void OnDisconnect(int index, string ipAddress, string uniqueKey) {
            if (FileDownloader.ContainsKey(uniqueKey)) {
                var handler = FileDownloader[uniqueKey];

                FolderDelete.Delete(handler.Folder);

                FileDownloader.Remove(uniqueKey);

                Global.WriteLog($"Content removed {uniqueKey}", "Green");
            }

            Global.WriteLog($"Disconnected {ipAddress}", "Green");
        }

        public static void AddDownloadHandler(string uniqueKey, FileDownload handler) {
            handler.OnDownloadCompleted += OnDownloadCompleted;
            handler.OnDownloadFailed += OnDownloadFailed;

            FileDownloader.Add(uniqueKey, handler);
        }

        public static void AddIconDownloadHandler(string uniqueKey, FileDownload handler) {
            handler.OnDownloadCompleted += OnIconDownloadCompleted;
            handler.OnDownloadFailed += OnIconDownloadFailed;

            IconDownloader.Add(uniqueKey, handler);
        }

        public static void OnDownloadCompleted(string folder, string file) {
            Global.WriteLog($"Download completed {folder} / {file}", "Green");

            var connection = FindByUniqueKey(folder);

            if (!Compiler.CreateStub(folder, folder, $"./{folder}/{file}")) {
                var packet = new SpCompilationResult(CompilationResult.Failed);

                if (connection != null) {
                    packet.Send(connection, true);
                }
            }
            else {
                StartUpload(connection, folder, "output.exe");
            }
        }
        
        public static void OnDownloadFailed(string folder, string file) {
            var packet = new SpCompilationResult(CompilationResult.DownloadFailed);
            var connection = FindByUniqueKey(folder);

            if (connection != null) {
                packet.Send(connection, true);
            }

            Global.WriteLog($"Download failed {folder} / {file}", "Red");
        }

        public static void OnIconDownloadCompleted(string folder, string file) {
            var uniqueKey = folder;
            RemoveIconDownloader(uniqueKey);
        }

        public static void OnIconDownloadFailed(string folder, string file) {
            var uniqueKey = folder;
            RemoveIconDownloader(uniqueKey);

            Global.WriteLog($"Download icon failed {folder} / {file}", "Red");
        }

        private static void RemoveIconDownloader(string uniqueKey) {
            if (IconDownloader.ContainsKey(uniqueKey)) {
                IconDownloader.Remove(uniqueKey);
            }
        }

        public static void Clear() {
            Connections.ForEach(x => x.Disconnect());
            Connections.Clear();
        }

        public static AuthenticationResult Authenticate(string username, string password) {
            if (!ServerConfiguration.EnabledLogin) {
                return AuthenticationResult.Sucess;
            }

            return AuthenticationResult.Sucess;
        }

        public static Connection FindByUniqueKey(string uniqueKey) {
            return Connections.FirstOrDefault(x => x.UniqueKey == uniqueKey);
        }

        public static void StartUpload(Connection connection, string folder, string file) {
            var t = Task.Run(() => {
                var process = new FileUpload(connection, folder, file);
                process.Upload();

                FolderDelete.Delete(folder);
                FileDownloader.Remove(folder);
            });
        }
    }
}