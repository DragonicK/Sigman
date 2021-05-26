using System.Text;
using System.Security.Cryptography;

namespace Sigman.Core.Cryptography {
    public class KeyGenerator {

        private const string Words = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        /// <summary>
        /// Quantidade de caracteres da chave.
        /// </summary>
        private readonly int Size = 15;

        public string GetUniqueKey() {
            char[] chars = Words.ToCharArray();
            byte[] data = new byte[Size];

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider()) {
                crypto.GetBytes(data);
            }

            StringBuilder result = new StringBuilder(Size);

            foreach (byte b in data) {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }
    }
}