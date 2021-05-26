using Sigman.Core.Cryptography.RSA;
using Sigman.Core.Network;

namespace Sigman.Client.Network.Packet {
    public class SpRSAKey : IRecvPacket {
        public void Process(byte[] buffer, Connection connection) {
            connection.RSAKey.SetClientPublicKey(buffer);
            // Quando receber a chave. Encrypta e envia.

            var _key = connection.AesKey.GetKey();
            var _iv = connection.AesKey.GetIv();

            var rsa = connection.RSAKey.GetKey();
            var key = RSACryptography.RSAEncrypt(_key, rsa.GetClientPublicKey(), false);
            var iv = RSACryptography.RSAEncrypt(_iv, rsa.GetClientPublicKey(), false);

            var aes = new CpAESKey(key, iv);
            aes.Send(connection, false);
        }
    }
}