namespace Sigman.Core.Cryptography.Aes {
    public sealed class AesKey {
        private readonly byte[] defaultKey;
        private readonly byte[] defaultIv;

        private byte[] key;
        private byte[] iv;

        public AesKey() {
            defaultKey = new byte[AesKeyLength.Key];
            defaultIv = new byte[AesKeyLength.Iv];

            for (var i = 0; i < AesKeyLength.Key; i++) {
                defaultKey[i] = (byte)i;
            }

            for (var i = 0; i < AesKeyLength.Iv; i++) {
                defaultIv[i] = (byte)i;
            }
        }

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

        public byte[] GetDefaultKey() {
            return defaultKey;
        }

        public byte[] GetDefaultIv() {
            return defaultIv;
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