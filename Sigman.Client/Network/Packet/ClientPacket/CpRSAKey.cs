using Sigman.Core.Network;

namespace Sigman.Client.Network.Packet {
    public class CpRSAKey : SendPacket {
        public CpRSAKey(byte[] rsa) {
            msg.Write((int)OperationCode.SendPacket[GetType()]);
            msg.Write(rsa);
        }
    }
}