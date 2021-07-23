using Sigman.Core.Network;
using Sigman.Core.Cryptography.Aes;
using Sigman.Client.Communication;

namespace Sigman.Client.Network.Packet {
    public class SpOutputFile : IRecvPacket {
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

                var handler = Global.Downloader;

                if (handler.FileName == string.Empty) {
                    handler.SetFileData(Global.OutputFolder, fileName, fileLength);
                }

                if (!handler.Completed) {
                    if (!handler.IsOpen) {
                        handler.Open();
                    }

                    handler.Save(bytes);
                }
            }
            else {
                Global.Forms.ShowFailedMessage("Failed to download compiled file!");
            }
        }
    }
}