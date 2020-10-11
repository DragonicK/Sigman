using Sigman.Core.Network;
using Sigman.Server.Server;

namespace Sigman.Server.Network.Packet {
    public class SpAuthenticationResult : SendPacket {
        public SpAuthenticationResult(AuthenticationResult result) {
            msg.Write((int)OperationCode.SendPacket[GetType()]);
            msg.Write((int)result);
        }
    }
}