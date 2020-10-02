using System.Text;
using System.Security.Cryptography;

namespace Sigman.Core.Cryptography {
    public static class Hash {
        public static byte[] Compute(string data) {
            byte[] buffer;

            using (var sha = new SHA256Managed()) {
                buffer = sha.ComputeHash(Encoding.Unicode.GetBytes(data));
            }

            return buffer;
        }
    }
}