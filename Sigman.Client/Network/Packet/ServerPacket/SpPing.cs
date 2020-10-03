using Sigman.Core.Network;

namespace Sigman.Client.Network.Packet {
    public class SpPing : IRecvPacket {
        public void Process(byte[] buffer, Connection connection) {
            var msg = new ByteBuffer(buffer);
            var x = msg.ReadByte();

        }
    }
}