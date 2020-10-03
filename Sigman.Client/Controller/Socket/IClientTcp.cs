using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigman.Client.Controller {
    public interface IClientTcp {
        bool IsHandShakeOk();
        ConnectionResult Connect();
        bool IsConnected();
        void SendRSAKey();
        void Start();
        void Stop();
        void Disconnect();
    }
}