using Sigman.Server.Server;
using System;
using System.Threading;
using System.Windows.Forms;
using Sigman.Server.Logs;
using Sigman.Server.Communication;
using Sigman.Server.Configuration;

namespace Sigman.Server {
    public partial class FormMain : Form {
        Thread t;
        ProgramServer Server;
        bool ServerRunning;

        private const int MaxLogsLines = 250;
        private const int PreserveLogsLines = 25;

        private delegate void DelegateWriteLog(object sender, LogEventArgs e);

        public FormMain() {
            InitializeComponent();
        }

        #region Server

        public void InitializeServer() {
            ServerRunning = true;
            Global.InitializeLog(WriteLog);

            Server = new ProgramServer();
            Server.Start();

            t = new Thread(ServerLoop);
            t.Start();
        }

        private void ServerLoop() {
            while (ServerRunning) {

                Server.Loop();

                if (ServerConfiguration.Sleep > 0) {
                    Thread.Sleep(ServerConfiguration.Sleep);
                }
            }

            Server.Stop();
        }

        #endregion

        #region Logs

        private void ClearText(ref RichTextBox text) {
            if (text.Lines.Length >= MaxLogsLines) {
                var currentLines = text.Lines;
                var newLines = new string[PreserveLogsLines];

                Array.Copy(currentLines, currentLines.Length - PreserveLogsLines, newLines, 0, PreserveLogsLines);

                text.Lines = newLines;
            }
        }

        public void WriteLog(object sender, LogEventArgs e) {
            if (!ServerRunning) {
                return;
            }

            if (TextLog.InvokeRequired) {
                var d = new DelegateWriteLog(WriteLog);
                TextLog.Invoke(d, sender, e);
            }
            else {
                TextLog.SelectionStart = TextLog.TextLength;
                TextLog.SelectionLength = 0;
                TextLog.SelectionColor = e.Color;
                TextLog.AppendText($"{DateTime.Now}: {e.Text}{Environment.NewLine}");
                TextLog.ScrollToCaret();
            }
        }

        private void TimerClearText_Tick(object sender, EventArgs e) {
            ClearText(ref TextLog);
        }

        #endregion

        #region Form Events
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            ServerRunning = false;
            e.Cancel = false;
        }

        #endregion

        #region Menu

        private void MenuExit_Click(object sender, EventArgs e) {
            ServerRunning = false;
        }

        private void MenuClear_Click(object sender, EventArgs e) {
            TextLog.Text = string.Empty;
        }

        #endregion
    }
}