using Sigman.Core.Cryptography.Aes;

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
                    msg = Encript(connection);

                    if (msg.Length() <= 0) {
                        connection.Disconnect();
                    }
                    else {
                        connection.Send(msg);
                    }
                }
            }  
        }

        private ByteBuffer Encript(Connection connection) {
            encrypted = true;

            var buffer = new ByteBuffer();
            var header = msg.ReadInt32();
            var bytes = msg.ReadBytes(msg.Length());
            var key = connection.AesKey.GetKey();
            var iv = connection.AesKey.GetIv();

            var aes = new AesCryptography() {
                CipherMode = System.Security.Cryptography.CipherMode.CBC,
                KeySize = AesKeySize.KeySize128,
                PaddingMode = System.Security.Cryptography.PaddingMode.PKCS7
            };

            var encripted = aes.Encrypt(bytes, key, iv);

            if (encripted != null) {
                buffer = new ByteBuffer(4 + encripted.Length);
                buffer.Write(header);
                buffer.Write(encripted);
            }

            return buffer;
        }
    }
}