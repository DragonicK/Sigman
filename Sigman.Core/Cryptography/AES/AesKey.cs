namespace Sigman.Core.Cryptography.Aes {
    public sealed class AesKey {
        private byte[] key;
        private byte[] iv;

        private byte[] clientKey;
        private byte[] clientIv;

        public bool IsKeyCreated() {
            if (key != null && iv != null) {
                return true;
            }

            return false;
        }

        public byte[] GetKey() {
            return key;
        }

        public byte[] GetIv() {
            return iv;
        }

        public void SetClientKey(byte[] key) {
            clientKey = key;
        }

        public void SetClientIv(byte[] iv) {
            clientIv = iv;
        }

        public byte[] GetClientKey() {
            return clientKey;
        }

        public byte[] GetClientIv() {
            return clientIv;
        }

        public bool CreateKey(string password) {
            if (string.IsNullOrEmpty(password)) {
                return false;
            }

            key = new byte[AesKeyLength.Key];
            iv = new byte[AesKeyLength.Iv];

            var hash = Hash.Compute(password);

            for (var i = 0; i < AesKeyLength.Key; i++) {
                key[i] = hash[i % hash.Length];
            }

            for (var i = (AesKeyLength.Iv - 1); i >= 0; i--) {
                iv[i] = hash[i % hash.Length];
            }

            return true;
        }
    }
}