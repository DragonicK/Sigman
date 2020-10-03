namespace Sigman.Client.Controller {
    public interface IPacket {
        void SendLogin(string username, string password);
    }
}