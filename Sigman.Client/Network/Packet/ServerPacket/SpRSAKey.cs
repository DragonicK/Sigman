using Sigman.Client.Communication;
using Sigman.Core.Network;

namespace Sigman.Client.Network.Packet {
    public class SpRSAKey : IRecvPacket {
        public void Process(byte[] buffer, Connection connection) {
            connection.RSAKey.SetClientPublicKey(buffer);
            Global.HandShakeOk = true;
        }
    }
}