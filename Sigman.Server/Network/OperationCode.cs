﻿using System;
using System.Collections.Generic;
using Sigman.Core.Network;
using Sigman.Core.Network.Packet;
using Sigman.Server.Network.Packet;

namespace Sigman.Server.Network {
    public class OperationCode : IOperationCode {
        public static Dictionary<ClientPacket, Type> RecvPacket { get; private set; }
        public static Dictionary<Type, ServerPacket> SendPacket { get; private set; }

        public static void Initialize() {
            RecvPacket = new Dictionary<ClientPacket, Type> {
                 { ClientPacket.Ping, typeof(CpPing) },
                 { ClientPacket.RSAKey, typeof(CpRSAKey) },
                 { ClientPacket.AESKey, typeof(CpAESKey) } ,
                 { ClientPacket.AuthLogin, typeof(CpLogin) },
                 { ClientPacket.File, typeof(CpFile) },
                 { ClientPacket.Icon, typeof(CpIcon) }
            };

            SendPacket = new Dictionary<Type, ServerPacket>() {
                { typeof(SpPing), ServerPacket.Ping },
                { typeof(SpRSAKey), ServerPacket.RSAKey },
                { typeof(SpAESKey), ServerPacket.AESKey },
                { typeof(SpAuthenticationResult), ServerPacket.AuthResult },
                { typeof(SpCompilationResult), ServerPacket.CompilationResult },
                { typeof(SpOutputFile), ServerPacket.OutputFile }
            };
        }

        public Type GetRecvPacket(int header) {
            if (Enum.IsDefined(typeof(ClientPacket), header)) {
                var _header = (ClientPacket)header;

                if (RecvPacket.ContainsKey(_header)) {
                    return RecvPacket[_header];
                }          
            }

            return null;
        }

        public bool HasHeader(int header) {
            if (Enum.IsDefined(typeof(ClientPacket), header)) {
                var _header = (ClientPacket)header;

                if (RecvPacket.ContainsKey(_header)) {
                    return true;
                }
            }

            return false;
        }
    }
}