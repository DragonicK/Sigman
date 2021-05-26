using Sigman.Core.Network;
using Sigman.Core.Cryptography.Aes;
using Sigman.Server.Server;
using Sigman.Server.Communication;

namespace Sigman.Server.Network.Packet {
    public class CpLogin : IRecvPacket {
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
                var username = msg.ReadString();
                var password = msg.ReadString();

                var result = Authentication.Authenticate(username, password);

                var packet = new SpAuthenticationResult(result);
                packet.Send(connection, true);

                Global.WriteLog($"User: {username} trying to login.", "Green");
                Global.WriteLog($"Result: {result}", "Black");
            }
            else {
                connection.Disconnect();
                Global.WriteLog($"Failed to decrypt login packet.", "Black");
            }
        }
    }
}