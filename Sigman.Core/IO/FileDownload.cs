using System;
using System.IO;

namespace Sigman.Core.IO {
    public class FileDownload {
        public Action<string, string> OnDownloadCompleted { get; set; }
        public Action<string, string> OnDownloadFailed { get; set; }
        public Action<int> OnDownloadUpdateProgress { get; set; }

        public string Folder { get; set; }
        public string FileName { get; set; }
        public long FileLength { get; set; }
        public long WritenBytesCount { get; private set; }
        public bool IsOpen { get; private set; }
        public bool Completed { get; private set; }
        public bool HasError { get; private set; }

        private FileStream file;
        private BinaryWriter writer;

        public FileDownload() {
            Folder = string.Empty;
            FileName = string.Empty;
        }

        public void SetFileData(string folder, string fileName, long fileLength) {
            Folder = folder;
            FileName = fileName;
            FileLength = fileLength;

            CheckFolder();
        }

        public void Open() {
            try {
                file = new FileStream($"{Folder}/{FileName}", FileMode.Create, FileAccess.Write);
                writer = new BinaryWriter(file);
                IsOpen = true;
            }
            catch {
                IsOpen = false;
            }
        }

        public void Close() {
            writer?.Dispose();
            file?.Dispose();
        }

        public void Reset() {
            IsOpen = false;
            Completed = false;
            HasError = false;
            FileName = string.Empty;
        }
               
        private void CheckFolder() {
            if (!Directory.Exists($"{Folder}")) {
                Directory.CreateDirectory($"{Folder}");
            }
        }

        public void Save(byte[] buffer) {
            if (!HasError) {
                try {
                    writer.Write(buffer);
                    WritenBytesCount += buffer.Length;

                    OnDownloadUpdateProgress?.Invoke(Convert.ToInt32((WritenBytesCount / (double)FileLength) * 100f));

                    if (WritenBytesCount >= FileLength) {
                        IsOpen = false;
                        Completed = true;
                        Close();

                        OnDownloadCompleted?.Invoke(Folder, FileName);
                    }
                }
                catch {
                    HasError = true;
                    IsOpen = false;
                    OnDownloadFailed?.Invoke(Folder, FileName);
                }
            }
        }
    }
}