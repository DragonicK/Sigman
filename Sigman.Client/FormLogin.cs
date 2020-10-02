using Sigman.Client.Communication;
using Sigman.Client.Network;
using System;
using System.Windows.Forms;
using System.Threading;
using Sigman.Client.Client;
using System.Drawing;
using Sigman.Client.Network.Packet;
using System.Collections.Generic;

namespace Sigman.Client {
    public partial class FormLogin : Form {
        Thread t;
        bool ClientRunning = true;

        CpPing ping;
        int pingTick;
        const int PingTime = 3000;

        public FormLogin() {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, EventArgs e) {
            LabelStatus.Text = "Status: Waiting for connection handshake ...";

            var login = new Login();
            login.ProcessConnection();
        }

        private void ButtonQuit_Click(object sender, EventArgs e) {
            ClientRunning = false;
        }

        #region Client

        public void InitializeClient() {
            ClientRunning = true;
            OperationCode.Initialize();

            ping = new CpPing();

            t = new Thread(ClientLoop);
            t.Start();
        }

        private void ClientLoop() {
            pingTick = Environment.TickCount;

            while (ClientRunning) {
                if (Global.Connection != null) {
                    Global.Connection.ReceiveData();

                    // Send ping to check if connection is alive.
                    if (Environment.TickCount >= pingTick + PingTime) {
                        ping.Send(Global.Connection);
                        pingTick = Environment.TickCount;
                    }
                }

                Thread.Sleep(1);
            }

            if (Global.Connection != null) {
                Global.Connection.Disconnect();
            }

            Application.Exit();
        }

        #endregion

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            ClientRunning = false;
            e.Cancel = false;
        }

        private void FormLogin_Load(object sender, EventArgs e) {
            TimerHandShake.Start();
        }

        private void TimerHandShake_Tick(object sender, EventArgs e) {
            if (Global.Connection != null) {
                if (Global.Connection.Connected) {
                    if (Global.HandShakeOk) {
                        LabelStatus.Text = "Status: Connected ...";
                        LabelStatus.ForeColor = Color.ForestGreen;
                    }
                }
                else {
                    LabelStatus.Text = "Status: Disconnected ...";
                    LabelStatus.ForeColor = Color.ForestGreen;
                }
            }
        }
    }
}