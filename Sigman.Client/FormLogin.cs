using System;
using System.Windows.Forms;
using System.Drawing;
using Sigman.Client.Controller;

namespace Sigman.Client {
    public partial class FormLogin : Form {

        private const int ConnectionTimeOut = 8;
        private IClientTcp Socket { get; set; }
        private IPacket Packet { get; set; }

        private int SecondCount = 0;
        private bool SendingLogin = false;

        public FormLogin(IClientTcp socket, IPacket packet) {
            InitializeComponent();
            Socket = socket;
            Packet = packet;
        }

        private void ButtonLogin_Click(object sender, EventArgs e) {
            if (CheckConnectionResult(Socket.Connect())) {
                LabelStatus.Text = "Status: Connecting ...";
                LabelStatus.ForeColor = Color.ForestGreen;

                SecondCount = 0;

              //  ChangeControlActivity(false);

                Socket.SendRSAKey();

                TimerHandShake.Start();
            }
        }

        private void ButtonQuit_Click(object sender, EventArgs e) {
            Socket.Stop();
            Environment.Exit(0);
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;

            Socket.Stop();

            e.Cancel = false;
        }

        private void FormLogin_Load(object sender, EventArgs e) {
            ChangeControlActivity(true);
            TimerHandShake.Start();
        }

        private void TimerHandShake_Tick(object sender, EventArgs e) {
            if (Socket.IsConnected() && Socket.IsHandShakeOk()) {
                Packet.SendLogin(TextLogin.Text.Trim(), TextPassword.Text.Trim());

                TimerHandShake.Stop();
            }
        }

        private bool CheckConnectionResult(ConnectionResult result) {
            if (result == ConnectionResult.InvalidIpAddress) {
                MessageBox.Show("Endereço ip inválido.", "Aviso");
                return false;
            }
            else if (result == ConnectionResult.InvalidPort) {
                MessageBox.Show("Número de porta inválido.", "Aviso");
                return false;
            }
            else if (result == ConnectionResult.Disconnected) {
                MessageBox.Show("Não foi possível a conexão.", "Aviso");
                return false;
            }

            return true;
        }

        private void ChangeControlActivity(bool enabled) {
            TextLogin.Enabled = enabled;
            TextPassword.Enabled = enabled;
            ButtonLogin.Enabled = enabled;
            ButtonRegister.Enabled = enabled;
        }
    }
}