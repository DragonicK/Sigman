using System;
using System.Collections.Generic;
using Sigman.Core.Network;
using Sigman.Server.Communication;
using Sigman.Server.Network;
using Sigman.Server.Network.Packet;

namespace Sigman.Server.Server {
    public class ProgramServer {
        const int PingTime = 3000;

        readonly TcpServer Server;
        readonly List<Connection> Disconnected;

        SpPing ping;
        int pingTick;

        public ProgramServer() {
            Server = new TcpServer();
            Disconnected = new List<Connection>();
        }

        public void Start() {
            OperationCode.Initialize();
            ping = new SpPing();

            Server.Start();
        }

        public void Stop() {
            Server.Close();
            Authentication.Clear();
        }

        public void Loop() {
            Server.AcceptConnection();

            var list = Authentication.Connections;

            for (var i = 0; i < list.Count; i++) {
                list[i].ReceiveData();

                if (Environment.TickCount >= pingTick + PingTime) {
                    pingTick = Environment.TickCount;

                    ping.Send(list[i], false);
                }

                if (!list[i].Connected) {
                    Disconnected.Add(list[i]);
                }
            }

            // Remove disconnected users.
            if (Disconnected.Count > 0) {
                for (var i = 0; i < Disconnected.Count; i++) {
                    Authentication.Remove(Disconnected[i]);
                    Global.WriteLog($"Connection Removed {Disconnected[i].IpAddress.Ip}", "Black");
                }

                Disconnected.Clear();
            }
        }
    }
}