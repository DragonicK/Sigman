using Sigman.Core.Network;
using Sigman.Core.Cryptography.Aes;
using Sigman.Client.Client;
using Sigman.Client.Communication;

namespace Sigman.Client.Network.Packet {
    public class SpAuthenticationResult : IRecvPacket {
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
                var result = (AuthenticationResult)msg.ReadInt32();

                if (result == AuthenticationResult.Sucess) {
                    Global.Forms.Login.CanCloseForm();
                }
            }
        }
    }
}