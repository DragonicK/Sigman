using Sigman.Core.Network;

namespace Sigman.Server.Network.Packet {
    public class SpOutputFile : SendPacket {
        public SpOutputFile(string fileName, long fileLength, byte[] buffer) {
            msg.Write((int)OperationCode.SendPacket[GetType()]);
            msg.Write(fileName);
            msg.Write(fileLength);
            msg.Write(buffer.Length);
            msg.Write(buffer);
        }
    }
}