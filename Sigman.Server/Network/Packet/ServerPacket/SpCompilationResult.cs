using Sigman.Core.Network;
using Sigman.Core.Common;

namespace Sigman.Server.Network.Packet {
    public class SpCompilationResult : SendPacket {
        public SpCompilationResult(CompilationResult result) {
            msg.Write((int)OperationCode.SendPacket[GetType()]);
            msg.Write((int)result);
        }
    }
}