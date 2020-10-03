using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigman.Client.Controller;
using Sigman.Client.Controller.Socket;
using Sigman.Client.Network;
using Sigman.Client.Network.Packet;
using Sigman.Core.Network;

namespace Sigman.Client.Communication {
    public static class Global {
        public static ClientTcp Socket { get; set; }
        public static Packet Packet { get; set; }
        public static Forms Forms { get; set; }

        public static void Initialize() {
            OperationCode.Initialize();

            Packet = new Packet();

            Socket = new ClientTcp();
            Socket.Start();

            Forms = new Forms(Socket, Packet);
        }
    }
}