using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigman.Client.Network.Packet;
using Sigman.Core.Network;

namespace Sigman.Client.Communication {
    public class Global {
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static Connection Connection { get; set; }
        public static bool HandShakeOk { get; set; }
        public static void OnDisconnect(int index) {
            HandShakeOk = false;
        }
    }
}