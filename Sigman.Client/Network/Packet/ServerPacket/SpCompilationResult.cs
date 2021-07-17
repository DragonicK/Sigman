using Sigman.Core.Common;
using Sigman.Core.Network;
using Sigman.Core.Cryptography.Aes;
using Sigman.Client.Communication;

namespace Sigman.Client.Network.Packet {
    public class SpCompilationResult : IRecvPacket {

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
                var result = (CompilationResult)msg.ReadInt32();

                if (result == CompilationResult.Failed) {
                    Global.Forms.ShowFailedMessage("Stub compilation failed");
                }
                else if (result == CompilationResult.DownloadFailed) {
                    Global.Forms.ShowFailedMessage("File upload failed");
                }
            }
        }
    }
}