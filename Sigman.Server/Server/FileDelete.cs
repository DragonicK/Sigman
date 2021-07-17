using System.IO;

namespace Sigman.Server.Server {
    public static class FileDelete {
        public static void Delete(string folder, string file) {
            if (File.Exists($"./{folder}/file")) {
                File.Delete($"./{folder}/file");
            }
        }
    }
}