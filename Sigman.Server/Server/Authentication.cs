using System.Collections.Generic;
using Sigman.Core.Network;
using Sigman.Server.Communication;

namespace Sigman.Server.Server {
    public static class Authentication {
        public static List<Connection> Connections { get; set; } = new List<Connection>();

        public static void Add(Connection connection) {
            connection.OnDisconnect += OnDisconnect;

            Connections.Add(connection);

            Global.WriteLog($"Connected: {connection.IpAddress.Ip}", "Green");
        }

        public static void Remove(Connection connection) {
            Connections.Remove(connection);
        }

        public static void OnDisconnect(int index) {
            Global.WriteLog($"Disconnected", "Green");
        }

        public static void Clear() {
            for (var i = 0; i < Connections.Count; i++) {
                Connections[i].Disconnect();      
            }

            Connections.Clear();
        }
    }
}