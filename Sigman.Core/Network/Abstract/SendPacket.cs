namespace Sigman.Core.Network {
    public abstract class SendPacket {
        protected ByteBuffer msg;

        public SendPacket() {
            msg = new ByteBuffer();
        }

        protected void Flush() {
            msg.Flush();
        }

        public void Send(Connection connection) {
            connection.Send(msg);
        }
    }
}