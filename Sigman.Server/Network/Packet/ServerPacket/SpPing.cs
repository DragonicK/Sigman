using Sigman.Core.Network;

namespace Sigman.Server.Network.Packet {
    public class SpPing : SendPacket {
        public SpPing() {
            msg.Write((int)OperationCode.SendPacket[GetType()]);
        }
    }
}