using Sigman.Core.Cryptography.RSA;

namespace Sigman.Core.Network {
    public sealed class ConnectionKey {
        readonly RSAKey rsaKey;

        public ConnectionKey() {
            rsaKey = new RSAKey();
        }

        public RSAKey GetKey() {
            return rsaKey;
        }

        public byte[] GetMyPublicKey() {
            var buffer = new ByteBuffer();
            var key = rsaKey.GetPublicKey();

            buffer.Write(key.Exponent.Length);
            buffer.Write(key.Exponent);

            buffer.Write(key.Modulus.Length);
            buffer.Write(key.Modulus);

            return buffer.ToArray();
        }

        public void SetClientPublicKey(byte[] buffer) {
            var msg = new ByteBuffer(buffer);
            var key = rsaKey.GetClientPublicKey();

            var length = msg.ReadInt32();
            key.Exponent = msg.ReadBytes(length);

            length = msg.ReadInt32();
            key.Modulus = msg.ReadBytes(length);

            rsaKey.SetClientPublicKey(key);
        }
    }
}