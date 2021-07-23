using Sigman.Core.IO;
using Sigman.Core.Network;
using Sigman.Core.Cryptography.Aes;
using Sigman.Server.Server;
using Sigman.Server.Communication;

namespace Sigman.Server.Network.Packet {
    public class CpIcon : IRecvPacket {

        private const string IconName = "icon.ico";

        public void Process(byte[] buffer, Connection connection) {
            var key = connection.AesKey.GetClientKey();
            var iv = connection.AesKey.GetClientIv();

            var aes = new AesCryptography() {
                CipherMode = System.Security.Cryptography.CipherMode.CBC,
                KeySize = AesKeySize.KeySize128,
                PaddingMode = System.Security.Cryptography.PaddingMode.PKCS7
            };

            var decrypted = aes.Decrypt(buffer, key, iv, out var sucess);

            if (sucess) {
                var msg = new ByteBuffer(decrypted);
                var fileName = msg.ReadString();
                var fileLength = msg.ReadInt64();
                var length = msg.ReadInt32();
                var bytes = msg.ReadBytes(length);

                FileDownload handler;

                if (Authentication.IconDownloader.ContainsKey(connection.UniqueKey)) {
                    handler = Authentication.IconDownloader[connection.UniqueKey];
                }
                else {
                    handler = new FileDownload();
                    handler.SetFileData(connection.UniqueKey, IconName, fileLength);

                    Authentication.AddIconDownloadHandler(connection.UniqueKey, handler);
                }

                if (!handler.Completed) {
                    if (!handler.IsOpen) {
                        handler.Open();
                    }

                    handler.Save(bytes);
                }
            }
            else {
                connection.Disconnect();
                Global.WriteLog($"Failed to decrypt file packet.", "Black");
            }
        }
    }
}