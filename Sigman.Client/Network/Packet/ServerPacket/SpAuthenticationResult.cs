using Sigman.Core.Network;
using Sigman.Core.Cryptography.RSA;
using Sigman.Client.Client;
using Sigman.Client.Controller;
using Sigman.Client.Communication;

namespace Sigman.Client.Network.Packet {
    public class SpAuthenticationResult : IRecvPacket {
        public void Process(byte[] buffer, Connection connection) {
            var keys = connection.RSAKey.GetKey();
            var bytes = RSACryptography.RSADecrypt(buffer, keys.GetPrivateKey(), false); 

            if (bytes != null) {
                var msg = new ByteBuffer(bytes);
                var result = (AuthenticationResult)msg.ReadInt32();

                if (result == AuthenticationResult.Sucess) {
                    Global.Forms.Login.CanCloseForm();
                }
            }
        }
    }
}