namespace Sigman.Client.Controller {
    public interface IPacket {
        void SendLogin(string username, string password);
        void SendFile(string fileName, long fileLength, byte[] buffer);
    }
}