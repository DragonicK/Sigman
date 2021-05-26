using Sigman.Core.Network;

namespace Sigman.Server.Network.Packet {
    public class SpAESKey : SendPacket {
        public SpAESKey(byte[] key, byte[] iv) {
            msg.Write((int)OperationCode.SendPacket[GetType()]);
            msg.Write(key.Length);
            msg.Write(key);
            msg.Write(iv.Length);
            msg.Write(iv);
        }
    }
}