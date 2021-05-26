using Sigman.Core.Network;
using Sigman.Core.Cryptography.RSA;
using Sigman.Client.Communication;

namespace Sigman.Client.Network.Packet {
    public class SpAESKey : IRecvPacket {
        public void Process(byte[] buffer, Connection connection) {
            var msg = new ByteBuffer(buffer);
            int length;

            length = msg.ReadInt32();
            var key = msg.ReadBytes(length);            

            length = msg.ReadInt32();
            var iv = msg.ReadBytes(length);

            var rsa = connection.RSAKey;
            var keys = rsa.GetKey();
            var _key = RSACryptography.RSADecrypt(key, keys.GetPrivateKey(), false);
            var _iv = RSACryptography.RSADecrypt(iv, keys.GetPrivateKey(), false);

            connection.AesKey.SetClientKey(_key);
            connection.AesKey.SetClientIv(_iv);

            Global.Socket.HandShake = true;
        }
    }
}