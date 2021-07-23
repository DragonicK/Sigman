using System;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using Sigman.Client.Controller;

namespace Sigman.Client {
    public partial class FormMain : Form {
        private const int ChunckSize = 1024;
        public bool Started { get; set; }

        private delegate void ProgressChanged(int progress); 
        private IClientTcp Socket { get; set; }
        private IPacket Packet { get; set; }
        private long BytesReadCount { get; set; }
        private long FileLength { get; set; }
        private string FileName { get; set; } = string.Empty;
        private string InputFile { get; set; } = string.Empty;
        private string IconName { get; set; } = string.Empty;
        private string IconFile { get; set; } = string.Empty;
        public string OutputFolder { get; set; } = string.Empty;

        private delegate void ShowFailed(string message);

        public FormMain(IClientTcp socket, IPacket packet) {
            InitializeComponent();
            Socket = socket;
            Packet = packet;
        }

        public void ShowFailedMessage(string message) {
            if (InvokeRequired) {
                var d = new ShowFailed(ShowFailedMessage);
                Invoke(d, message);
            }
            else {
                MessageBox.Show(message, "Error");
            }
        }

        private void ButtonFile_Click(object sender, EventArgs e) {
            var dialog = new OpenFileDialog() {
                Multiselect = false,
                CheckFileExists = true,
                Filter = "Executable Files | *.exe",
                InitialDirectory = Environment.CurrentDirectory
            };

            var result = dialog.ShowDialog();

            if (result == DialogResult.OK) {
                FileName = dialog.SafeFileName;
                InputFile = dialog.FileName;

                TextInputFile.Text = InputFile;
                
                GroupOutput.Enabled = true;
            }
        }

        private void ButtonFolder_Click(object sender, EventArgs e) {
            var dialog = new FolderBrowserDialog() {
                RootFolder = Environment.SpecialFolder.MyComputer,
                ShowNewFolderButton = true,
            };

            var result = dialog.ShowDialog();
            
            if (result == DialogResult.OK) {
                OutputFolder = dialog.SelectedPath;
                TextOutputFolder.Text = OutputFolder;

                ButtonStart.Enabled = true;
            }
        }

        private void ButtonStart_Click(object sender, EventArgs e) {
            Started = true;

            ButtonCancel.Enabled = true;
            ButtonStart.Enabled = false;

            TextInputFile.Enabled = false;
            ButtonFile.Enabled = false;

            TextInputIcon.Enabled = false;
            ButtonIcon.Enabled = false;

            TextOutputFolder.Enabled = false;
            ButtonFolder.Enabled = false;

            var t = Task.Run(() => {
                StartProcess();
            });
        }

        private void ButtonCancel_Click(object sender, EventArgs e) {
            StopProcess();
        }

        private void CheckForValidNames() {
            if (!Started) {
                if (OutputFolder.Length > 0 && InputFile.Length > 0) {
                    ButtonStart.Enabled = true;
                    ButtonCancel.Enabled = false;
                }
                else {
                    ButtonStart.Enabled = false;
                    ButtonCancel.Enabled = false;
                }
            }
        }

        private void TextInputFile_TextChanged(object sender, EventArgs e) {
            InputFile = TextInputFile.Text.Trim();

            CheckForValidNames();
        }

        private void TextOutputFolder_TextChanged(object sender, EventArgs e) {
            OutputFolder = TextOutputFolder.Text.Trim();

            CheckForValidNames();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;

            Socket.Stop();
 
            e.Cancel = false;
        }

        private void StartProcess() {
            BytesReadCount = 0;
            ProgressSend.Value = 0;
            ProgressReceive.Value = 0;

            // if we selected some icon.
            if (IconName.Length > 0 && File.Exists(IconFile)) {
                using (var f = new FileStream(IconFile, FileMode.Open, FileAccess.Read)) {
                    using (var r = new BinaryReader(f)) {

                        var bytesCount = 0;
                        var fLength = f.Length;

                        while (Started) {
                            var buffer = r.ReadBytes(ChunckSize);

                            bytesCount += buffer.Length;

                            Packet.SendIcon(IconName, fLength, buffer);

                            if (bytesCount >= fLength) {
                                break;
                            }
                        }
                    }
                }
            }

            using (var f = new FileStream(InputFile, FileMode.Open, FileAccess.Read)) {
                using (var r = new BinaryReader(f)) {

                    FileLength = f.Length;

                    while (Started) {
                        var buffer = r.ReadBytes(ChunckSize);

                        BytesReadCount += buffer.Length;

                        Packet.SendFile(FileName, FileLength, buffer);

                        if (BytesReadCount >= FileLength) {
                            Started = false;
                        }
              
                        UpdateSendProgressBar(Convert.ToInt32((BytesReadCount / (double)FileLength) * 100f));
                    }
                }
            }
        }

        private void UpdateSendProgressBar(int progress) {
            if (progress > 100) {
                progress = 100;
            }

            if (ProgressSend.InvokeRequired) {
                var d = new ProgressChanged(UpdateSendProgressBar);
                ProgressSend.Invoke(d, new object[] { progress });
            }
            else {
                ProgressSend.Value = progress;
            }
        }

        private void ButtonIcon_Click(object sender, EventArgs e) {
            var dialog = new OpenFileDialog() {
                Multiselect = false,
                CheckFileExists = true,
                Filter = "Icon Files | *.ico",
                InitialDirectory = Environment.CurrentDirectory
            };

            var result = dialog.ShowDialog();

            if (result == DialogResult.OK) {
                IconName = dialog.SafeFileName;
                IconFile = dialog.FileName;

                TextInputIcon.Text = IconFile;
            }
        }

        public void StopProcess() {
            Started = false;

            ProgressSend.Value = 0;
            ProgressReceive.Value = 0;

            TextInputFile.Enabled = true;
            ButtonFile.Enabled = true;
            TextOutputFolder.Enabled = true;
            ButtonFolder.Enabled = true;

            ButtonStart.Enabled = true;
            ButtonCancel.Enabled = false;
        }

        public void UpdateReceivedProgress(int progress) {
            if (progress > 100) {
                progress = 100;
            }

            if (ProgressReceive.InvokeRequired) {
                var d = new ProgressChanged(UpdateReceivedProgress);
                ProgressReceive.Invoke(d, new object[] { progress });
            }
            else {
                ProgressReceive.Value = progress;
            }
        }
    }
}