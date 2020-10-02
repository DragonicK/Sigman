using System.Net.Sockets;
using Sigman.Core.Network;
using Sigman.Client.Configuration;
using Sigman.Client.Communication;
using Sigman.Client.Network;
using Sigman.Client.Network.Packet;

namespace Sigman.Client.Client {
    public class Login {
        public void ProcessConnection() {
            if (Global.Connection == null) {
                CreateConnection();
            }
            else {
                Global.Connection.Disconnect();
                CreateConnection();
            }

            var key = Global.Connection.RSAKey.GetMyPublicKey();
            var msg = new CpRSAKey(key);

            msg.Send(Global.Connection);
        }

        public bool CanSendLogin(string username, string password) {
            if (string.IsNullOrEmpty(username)) {
                return false;
            }

            if (string.IsNullOrEmpty(password)) {
                return false;
            }

            var login = new CpLogin(username, password);
            login.Send(Global.Connection);

            return true;
        }

        private void CreateConnection() {
            var tcp = new TcpClient();
            tcp.Connect(ClientConfiguration.IpAddress.Ip, ClientConfiguration.IpAddress.Port);

            Global.Connection = new Connection(0, tcp) {
                OperationCode = new OperationCode()
            };

            Global.Connection.OnDisconnect += Global.OnDisconnect;
        }
    }
}