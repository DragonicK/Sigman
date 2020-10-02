using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigman.Core.Network;

namespace Sigman.Server.Network.Packet {
    public class CpLogin : IRecvPacket {
        public void Process(byte[] buffer, Connection connection) {
            var msg = new ByteBuffer(buffer);
            var username = msg.ReadString();
            var password = msg.ReadString();

            System.Windows.Forms.MessageBox.Show(username + " " + password);
        }
    }
}