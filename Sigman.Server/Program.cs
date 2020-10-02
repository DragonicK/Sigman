using System;
using System.Windows.Forms;

using Sigman.Core.Common;
using Sigman.Core.Configuration;
using Sigman.Server.Configuration;

namespace Sigman.Server {
    static class Program {
        static FormMain frmMain;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!ReadConfiguration()) {
                return;
            }

            frmMain = new FormMain();
            frmMain.Show();
            frmMain.InitializeServer();

            Application.Run(frmMain);
        }

        static bool ReadConfiguration() {
            var settings = new Settings();
            settings.ParseConfigFile(Constants.ConfigurationFile);

            ServerConfiguration.Port = settings.GetInt32("Port");
            ServerConfiguration.MaxPendingConnections = settings.GetInt32("MaxPendingConnections");
            ServerConfiguration.EnabledLogin = settings.GetBoolean("EnabledLogin");
            ServerConfiguration.Sleep = settings.GetInt32("Sleep");

            if (ServerConfiguration.Port <= 0) {
                MessageBox.Show("Verifique as configurações de porta.");
                return false;
            }

            if (ServerConfiguration.MaxPendingConnections < 0) {
                ServerConfiguration.MaxPendingConnections = 0;
            }

            return true;
        }
    }
}