using System.IO;
using System.Security.Cryptography;

namespace Sigman.Core.Cryptography.Aes {
    public sealed class AesCryptography {
        public AesKeySize KeySize { get; set; }
        public CipherMode CipherMode { get; set; }
        public PaddingMode PaddingMode { get; set; }

        private const int BlockSize = 128;

        public byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] key, byte[] iv) {
            byte[] encryptedBytes = null;

            using (var aes = new RijndaelManaged()) {
                using (MemoryStream ms = new MemoryStream()) {
                    aes.KeySize = (int)KeySize;
                    aes.BlockSize = BlockSize;
                    aes.Mode = CipherMode;
                    aes.Padding = PaddingMode;

                    aes.Key = key;
                    aes.IV = iv;

                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write)) {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] key, byte[] iv, out bool success) {
            byte[] encryptedBytes = null;

            try {
                using (var aes = new RijndaelManaged()) {
                    using (MemoryStream ms = new MemoryStream()) {
                        aes.KeySize = (int)KeySize;
                        aes.BlockSize = BlockSize;
                        aes.Mode = CipherMode;
                        aes.Padding = PaddingMode.None;

                        aes.Key = key;
                        aes.IV = iv;

                        using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write)) {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cs.Close();
                        }

                        success = true;
                        encryptedBytes = ms.ToArray();
                    }
                }
            }
            catch {
                success = false;
            }

            if (success) {
                return encryptedBytes;
            }

            // Retorna um número aleatório fora do range para executar a desconexão em caso de falha.
            return new byte[1] { 0 };
        }
    }
}