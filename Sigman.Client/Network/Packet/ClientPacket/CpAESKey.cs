using Sigman.Core.Network;

namespace Sigman.Client.Network.Packet {
    public class CpAESKey : SendPacket {
        public CpAESKey(byte[] key, byte[] iv) {
            msg.Write((int)OperationCode.SendPacket[GetType()]);
            msg.Write(key.Length);
            msg.Write(key);
            msg.Write(iv.Length);
            msg.Write(iv);
        }
    }
}