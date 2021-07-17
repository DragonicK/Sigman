using System;
using System.IO;
using System.Windows.Forms;

using Sigman.Core.Common;
using Sigman.Core.Configuration;
using Sigman.Server.Configuration;

namespace Sigman.Server {
    static class Program {
        const string ErrorMessage = "Failed to load configuration.\nThe program will be closed.";
        const string PortMessage = "Check port's configuration.";

        static FormMain frmMain;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            RemoveUnusedFolder();

            if (!ReadConfiguration()) {
                MessageBox.Show(ErrorMessage);
                    
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
                MessageBox.Show(PortMessage);
                return false;
            }

            if (ServerConfiguration.MaxPendingConnections < 0) {
                ServerConfiguration.MaxPendingConnections = 0;
            }

            return true;
        }

        static void RemoveUnusedFolder() {
            var dir = Directory.GetDirectories(Environment.CurrentDirectory);

            foreach (var folder in dir) {
                var files = Directory.GetFiles(folder);

                foreach (var file in files) {
                    File.Delete(file);
                }

                Directory.Delete(folder);
            }      
        }
    }
}