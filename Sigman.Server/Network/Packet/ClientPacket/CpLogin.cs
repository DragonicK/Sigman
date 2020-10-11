using Sigman.Core.Network;
using Sigman.Core.Cryptography.RSA;
using Sigman.Server.Server;

namespace Sigman.Server.Network.Packet {
    public class CpLogin : IRecvPacket {
        public void Process(byte[] buffer, Connection connection) {
            var keys = connection.RSAKey.GetKey();
            var bytes = RSACryptography.RSADecrypt(buffer, keys.GetPrivateKey(), false);

            if (bytes != null) {
                var msg = new ByteBuffer(bytes);
                var username = msg.ReadString();
                var password = msg.ReadString();

                var result = Authentication.Authenticate(username, password);

                var packet = new SpAuthenticationResult(result);
                packet.Send(connection, true);
            }
            else {
                connection.Disconnect();
            }
        }
    }
}