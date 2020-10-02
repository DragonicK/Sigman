using Sigman.Core.Network;

namespace Sigman.Server.Network.Packet {
    public class CpRSAKey : IRecvPacket {
        public void Process(byte[] buffer, Connection connection) {
            // Obtem a chave publica do cliente.
            connection.RSAKey.SetClientPublicKey(buffer);

            // Envia a chave publica da conexão para o cliente.
            var key = connection.RSAKey.GetMyPublicKey();
            var msg = new SpRSAKey(key);
            msg.Send(connection);
        }
    }
}