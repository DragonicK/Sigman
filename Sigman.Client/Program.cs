using System;
using System.Windows.Forms;
using Sigman.Core.Common;
using Sigman.Core.Network;
using Sigman.Core.Configuration;
using Sigman.Client.Configuration;
using Sigman.Client.Communication;

namespace Sigman.Client {
    static class Program {
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

            Global.Initialize();  
            Application.Run(Global.Forms.Login);
        }

        static bool ReadConfiguration() {
            var settings = new Settings();
            settings.ParseConfigFile(Constants.ConfigurationFile);

            var port = settings.GetInt32("Port");
            var ipAddress = settings.GetString("IpAddress");

            if (port < 1) {
                MessageBox.Show("Verifique as configurações de porta.");
                return false;
            }

            ClientConfiguration.IpAddress = new IpAddress(ipAddress, port);

            return true;
        }
    }
}