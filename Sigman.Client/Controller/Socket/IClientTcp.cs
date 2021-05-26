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