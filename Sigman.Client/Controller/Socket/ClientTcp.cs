using Sigman.Client.Configuration;
using Sigman.Client.Network;
using Sigman.Client.Network.Packet;
using Sigman.Core.Network;
using System;
using System.Threading;
using System.Net.Sockets;

namespace Sigman.Client.Controller {
    public class ClientTcp : IClientTcp {
        public Connection Connection { get; set; }
        public bool HandShake { get; set; }

        private const int PingTime = 3000;
        private readonly Thread thread;
        private readonly CpPing ping;
        private int pingTick;
        private bool running = false;

        public ClientTcp() {
            ping = new CpPing();
            thread = new Thread(Loop);
        }

        public void Start() {
            pingTick = Environment.TickCount;

            running = true;
            thread.Start();
        }

        public void Stop() {
            running = false;
        }

        public void Loop() {
            while (running) {
                if (Connection != null) {
                    Connection.ReceiveData();

                    SendPing();
                }

                Thread.Sleep(1);
            }

            Disconnect();
        }

        public ConnectionResult Connect() {
            if (ClientConfiguration.IpAddress.Port < 1) {
                return ConnectionResult.InvalidPort;
            }

            if (string.IsNullOrEmpty(ClientConfiguration.IpAddress.Ip)) {
                return ConnectionResult.InvalidIpAddress;
            }

            return CreateConnection() ? ConnectionResult.Connected : ConnectionResult.Disconnected;
        }

        public void OnDisconnect(int index, string ipAddress) {
            HandShake = false;
        }

        public void SendRSAKey() {
            if (Connection != null) {
                if (Connection.Connected) { 
                    var key = Connection.RSAKey.GetMyPublicKey();
                    var msg = new CpRSAKey(key);

                    msg.Send(Connection, false);
                }
            }
        }

        public bool IsConnected() {
            if (Connection != null) {
                return Connection.Connected;
            }

            return false;  
        }

        public bool IsHandShakeOk() {
            return HandShake;
        }

        public void Disconnect() {
            Connection?.Disconnect();
        }

        private bool CreateConnection() {
            if (Connection != null) {
                Connection.Disconnect();
                Connection = null;
            }

            var client = new TcpClient(ClientConfiguration.IpAddress.Ip, ClientConfiguration.IpAddress.Port);

            Connection = new Connection(99, client) {
                OperationCode = new OperationCode()
            };

            Connection.OnDisconnect += OnDisconnect;

            return client.Connected;
        }

        /// <summary>
        /// Send ping to check if connection is alive.
        /// </summary>
        private void SendPing() {
            if (Connection.Connected) {
                if (Environment.TickCount >= pingTick + PingTime) {
                    ping.Send(Connection, false);
                    pingTick = Environment.TickCount;
                }
            }
        }
    }
}