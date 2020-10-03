using Sigman.Core.Network;

namespace Sigman.Client.Network.Packet {
    public class CpPing : SendPacket {
        public CpPing() {
            msg.Write((int)OperationCode.SendPacket[GetType()]);
            msg.Write((byte)1);
        }
    }
}