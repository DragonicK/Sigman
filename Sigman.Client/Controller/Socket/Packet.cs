using Sigman.Client.Communication;
using Sigman.Client.Network.Packet;

namespace Sigman.Client.Controller.Socket {
    public class Packet : IPacket {
        public void SendLogin(string username, string password) {
            if (username.Length < 1) {
                username = "guest";
            }

            if (password.Length < 1) {
                password = "guest";
            }

            var login = new CpLogin(username, password);
            login.Send(Global.Socket.Connection, true);
        }
    }
}