using System.IO;

namespace Sigman.Server.Server {
    public static class FolderDelete {
        public static void Delete(string folder) {
            if (Directory.Exists($"./{folder}")) {
                var files = Directory.GetFiles($"./{folder}");

                foreach (var file in files) {
                    File.Delete(file);
                }

                Directory.Delete($"./{folder}");
            }
        }
    }
}