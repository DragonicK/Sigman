using System.Collections.Generic;
using Sigman.Core.Network;
using Sigman.Server.Communication;
using Sigman.Server.Configuration;

namespace Sigman.Server.Server {
    public static class Authentication {
        public static List<Connection> Connections { get; set; } = new List<Connection>();
        public static Dictionary<string, FileHandler> FileHandler { get; set; } = new Dictionary<string, FileHandler>();

        public static void Add(Connection connection) {
            connection.OnDisconnect += OnDisconnect;

            Connections.Add(connection);

            Global.WriteLog($"Connected: {connection.IpAddress.Ip}", "Green");
        }

        public static void Remove(Connection connection) {
            Connections.Remove(connection);
        }

        public static void OnDisconnect(int index, string ipAddress) {
            Global.WriteLog($"Disconnected {ipAddress}", "Green");
        }

        public static void Clear() {
            for (var i = 0; i < Connections.Count; i++) {
                Connections[i].Disconnect();      
            }

            Connections.Clear();
        }

        public static AuthenticationResult Authenticate(string username, string password) {
            if (!ServerConfiguration.EnabledLogin) {
                return AuthenticationResult.Sucess;
            }

            return AuthenticationResult.Sucess;
        }
    }
}