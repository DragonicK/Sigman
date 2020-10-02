using System.Security.Cryptography;

namespace Sigman.Core.Cryptography.RSA {
    public sealed class RSAKey {
        RSAParameters publicKey;
        RSAParameters privateKey;
        RSAParameters clientKey;

        public RSAKey() {
            using (var csp = new RSACryptoServiceProvider(1024)) {
                publicKey = csp.ExportParameters(false);
                privateKey = csp.ExportParameters(true);
            }
        }

        public RSAParameters GetPublicKey() {
            return publicKey;
        }

        public RSAParameters GetPrivateKey() {
            return privateKey;
        }

        public void SetClientPublicKey(RSAParameters key) {
            clientKey = key;
        }

        public RSAParameters GetClientPublicKey() {
            return clientKey;
        }
    }
}