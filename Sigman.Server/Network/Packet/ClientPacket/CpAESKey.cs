using Sigman.Core.Network;
using Sigman.Core.Cryptography.RSA;

namespace Sigman.Server.Network.Packet {
    public class CpAESKey : IRecvPacket {
        public void Process(byte[] buffer, Connection connection) {
            var msg = new ByteBuffer(buffer);
            int length;

            length = msg.ReadInt32();
            var key = msg.ReadBytes(length);

            length = msg.ReadInt32();
            var iv = msg.ReadBytes(length);

            var rsa = connection.RSAKey.GetKey();
            var _key = RSACryptography.RSADecrypt(key, rsa.GetPrivateKey(), false);
            var _iv = RSACryptography.RSADecrypt(iv, rsa.GetPrivateKey(), false);

            connection.AesKey.SetClientKey(_key);
            connection.AesKey.SetClientIv(_iv);

            // Encria e envia de volta para o cliente.
            _key = connection.AesKey.GetKey();
            _iv = connection.AesKey.GetIv();

            key = RSACryptography.RSAEncrypt(_key, rsa.GetClientPublicKey(), false);
            iv = RSACryptography.RSAEncrypt(_iv, rsa.GetClientPublicKey(), false);

            var aes = new SpAESKey(key, iv);
            aes.Send(connection, false);
        }
    }
}