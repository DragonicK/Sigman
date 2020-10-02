using System.Net;
using System.Net.Sockets;
using Sigman.Core.Network;
using Sigman.Server.Server;
using Sigman.Server.Configuration;

namespace Sigman.Server.Network {
    public class TcpServer {
        TcpListener listener;

        public void Start() {
            listener = new TcpListener(IPAddress.Any, ServerConfiguration.Port);
            listener.Start(ServerConfiguration.MaxPendingConnections);
        }

        public void Close() {
            if (listener != null) {
                listener.Stop();
            }
        }

        public void AcceptConnection() {
            if (listener.Pending()) {
                var client = new Connection(0, listener.AcceptTcpClient()) {
                    OperationCode = new OperationCode()
                };

                Authentication.Add(client);
            }
        }
    }
}