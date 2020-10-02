namespace Sigman.Core.Network {
    public interface IRecvPacket {
        void Process(byte[] buffer, Connection connection);
    }
}