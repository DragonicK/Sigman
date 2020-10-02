using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigman.Server.Logs;
using Sigman.Server.Network;

namespace Sigman.Server.Communication {
    public class Global {
        private static EventHandler<LogEventArgs> logs;

        public static void InitializeLog(EventHandler<LogEventArgs> handler) {
            logs = handler;
        }

        public static void WriteLog(string text, string color) {     
            logs?.Invoke(null, new LogEventArgs(text, color));   
        }
    }
}