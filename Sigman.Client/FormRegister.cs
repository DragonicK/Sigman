using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sigman.Client.Controller;

namespace Sigman.Client {
    public partial class FormRegister : Form {
        private IClientTcp Socket { get; set; }
        private IPacket Packet { get; set; }

        public FormRegister(IClientTcp socket, IPacket packet) {
            InitializeComponent();
            Socket = socket;
            Packet = packet;
        }
    }
}