using System;

namespace Sigman.Client.Controller {
    public class Forms {
        public FormLogin Login { get; set; }
        public FormRegister Register { get; set; }

        public Forms(IClientTcp socket, IPacket packet) {
            Login = new FormLogin(socket, packet);
            Register = new FormRegister(socket, packet);
        }

        public void ShowLogin() {
            Login.Show();
        }

        public void ShowRegister() {
            Register.Show();
        }

        public void Exit() {
            Environment.Exit(0);
        }
    }
}