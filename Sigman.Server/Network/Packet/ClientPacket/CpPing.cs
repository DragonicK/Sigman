using Sigman.Core.Network;

namespace Sigman.Server.Network.Packet {
    public class CpPing : IRecvPacket {
        public void Process(byte[] buffer, Connection connection) {
            var ping = new SpPing();
            ping.Send(connection);
        }
    }
}