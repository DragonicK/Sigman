using Sigman.Core.Cryptography.RSA;

namespace Sigman.Core.Network {
    public abstract class SendPacket {
        protected ByteBuffer msg;
        private bool encrypted;

        public SendPacket() {
            encrypted = false;
            msg = new ByteBuffer();
        }

        protected void Flush() {
            msg.Flush();
        }

        public void Send(Connection connection, bool useCryptography) {
            if (!useCryptography) {
                connection.Send(msg);
            }
          
            if (useCryptography) { 
                if (encrypted) {
                    connection.Send(msg);
                }
                else {
                    msg = Cryptography(connection);

                    if (msg.Length() <= 0) {
                        connection.Disconnect();
                    }
                    else {
                        connection.Send(msg);
                    }
                }
            }  
        }

        private ByteBuffer Cryptography(Connection connection) {
            encrypted = true;

            var buffer = new ByteBuffer();
            var header = msg.ReadInt32();
            var bytes = msg.ReadBytes(msg.Length());
            var keys = connection.RSAKey.GetKey();
            var encryptedBytes = RSACryptography.RSAEncrypt(bytes, keys.GetClientPublicKey(), false);

            if (encryptedBytes != null) {
                buffer = new ByteBuffer(4 + encryptedBytes.Length);
                buffer.Write(header);
                buffer.Write(encryptedBytes);
            }

            return buffer;
        }
    }
}