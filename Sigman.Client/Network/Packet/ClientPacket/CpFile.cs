using Sigman.Core.Network;

namespace Sigman.Client.Network.Packet {
    public class CpFile : SendPacket {
        public CpFile(string fileName, long fileLength, byte[] buffer) {
            msg.Write((int)OperationCode.SendPacket[GetType()]);
            msg.Write(fileName);
            msg.Write(fileLength);
            msg.Write(buffer.Length);
            msg.Write(buffer);
        }
    }
}