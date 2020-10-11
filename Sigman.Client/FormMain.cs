using System;
using System.Diagnostics;
using System.Windows.Forms;
using Sigman.Client.Controller;

namespace Sigman.Client {
    public partial class FormMain : Form {
        private IClientTcp Socket { get; set; }
        private IPacket Packet { get; set; }

        private bool Started { get; set; }

        private string FileName { get; set; } = string.Empty;
        private string InputFile { get; set; } = string.Empty;
        private string OutputFolder { get; set; } = string.Empty;

        public FormMain(IClientTcp socket, IPacket packet) {
            InitializeComponent();
            Socket = socket;
            Packet = packet;
        }

        private void ButtonFile_Click(object sender, EventArgs e) {
            var dialog = new OpenFileDialog() {
                Multiselect = false,
                CheckFileExists = true,
                Filter = "All Files|*.*",
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
            TextOutputFolder.Enabled = false;
            ButtonFolder.Enabled = false;
        }

        private void ButtonCancel_Click(object sender, EventArgs e) {
            Started = false;

            TextInputFile.Enabled = true;
            ButtonFile.Enabled = true;
            TextOutputFolder.Enabled = true;
            ButtonFolder.Enabled = true;

            ButtonStart.Enabled = true;
            ButtonCancel.Enabled = false;
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

        }
    }
}