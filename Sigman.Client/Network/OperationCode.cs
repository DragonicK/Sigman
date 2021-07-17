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
                 { ServerPacket.RSAKey, typeof(SpRSAKey) },
                 { ServerPacket.AESKey, typeof(SpAESKey) },
                 { ServerPacket.AuthResult, typeof(SpAuthenticationResult) },
                 { ServerPacket.CompilationResult, typeof(SpCompilationResult) }
            };

            SendPacket = new Dictionary<Type, ClientPacket>() {
                { typeof(CpPing), ClientPacket.Ping },
                { typeof(CpRSAKey), ClientPacket.RSAKey },
                { typeof(CpAESKey), ClientPacket.AESKey },
                { typeof(CpLogin), ClientPacket.AuthLogin },
                { typeof(CpFile), ClientPacket.File },
                { typeof(CpIcon), ClientPacket.Icon }
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