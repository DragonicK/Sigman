using System.IO;
using Sigman.Core.Network;
using Sigman.Server.Network.Packet;

namespace Sigman.Server.Server {
    public class FileUpload {
        private const int ChunckSize = 1024;
        public string Folder { get; set; }
        public string File { get; set; }
        public long BytesReadCount { get; private set; }
        public bool Started { get; set; }
        public bool IsDone { get; private set; }
        public Connection Connection { get; }
        
        public FileUpload(Connection connection, string folder, string file) {
            Connection = connection;
            Folder = folder;
            File = file;
            Started = true;
        }

        public void Upload() {
            using (var f = new FileStream($"./{Folder}/{File}", FileMode.Open, FileAccess.Read)) {
                using (var r = new BinaryReader(f)) {

                    var length = f.Length;

                    while (Started) {
                        var buffer = r.ReadBytes(ChunckSize);

                        BytesReadCount += buffer.Length;

                        if (CanSendFile(File, length, buffer)) {
                            if (BytesReadCount >= length) {
                                IsDone = true;
                                break;
                            }
                        }
                        else {
                            IsDone = false;
                            break;
                        }
                    }
                }
            }
        }

        private bool CanSendFile(string file, long length, byte[] buffer) {
            if (Connection != null) {
                if (Connection.Connected) {
                    var packet = new SpOutputFile(file, length, buffer);
                    packet.Send(Connection, true);

                    return true;
                }
            }

            return false;
        }
    }
}