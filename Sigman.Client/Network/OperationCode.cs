using System;
using System.Collections.Generic;
using Sigman.Core.Network;
using Sigman.Core.Network.Packet;
using Sigman.Client.Network.Packet;

namespace Sigman.Client.Network {
    public class OperationCode : IOperationCode {
        public static Dictionary<ServerPacket, Type> RecvPacket { get; private set; }
        public static Dictionary<Type, ClientPacket> SendPacket { get; private set; }

        public static void Initialize() {
            RecvPacket = new Dictionary<ServerPacket, Type> {
                 { ServerPacket.Ping, typeof(SpPing) },
                 { ServerPacket.RSAKey, typeof(SpRSAKey) }
            };

            SendPacket = new Dictionary<Type, ClientPacket>() {
                { typeof(CpPing), ClientPacket.Ping },
                { typeof(CpRSAKey), ClientPacket.RSAKey },
                { typeof(CpLogin), ClientPacket.AuthLogin }
            };
        }

        public Type GetRecvPacket(int header) {
            if (Enum.IsDefined(typeof(ServerPacket), header)) {
                var _header = (ServerPacket)header;

                if (RecvPacket.ContainsKey(_header)) {
                    return RecvPacket[_header];
                }
            }

            return null;
        }

        public bool HasHeader(int header) {
            if (Enum.IsDefined(typeof(ServerPacket), header)) {
                var _header = (ServerPacket)header;

                if (RecvPacket.ContainsKey(_header)) {
                    return true;
                }
            }

            return false;
        }
    }
}