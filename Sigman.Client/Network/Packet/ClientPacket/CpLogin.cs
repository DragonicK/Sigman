using Sigman.Core.Network;

namespace Sigman.Client.Network.Packet {
    public class CpLogin : SendPacket {
        public CpLogin(string username, string password) {
            msg.Write((int)OperationCode.SendPacket[GetType()]);
            msg.Write(username);
            msg.Write(password);
        }
    }
}