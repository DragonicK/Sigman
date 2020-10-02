using Sigman.Core.Network;

namespace Sigman.Server.Network.Packet {
    public class SpRSAKey : SendPacket {
        public SpRSAKey(byte[] key) {
            msg.Write((int)OperationCode.SendPacket[GetType()]);
            msg.Write(key);
        }
    }
}