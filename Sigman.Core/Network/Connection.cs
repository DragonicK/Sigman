using System;
using System.Net.Sockets;
using Sigman.Core.Common;

namespace Sigman.Core.Network {
    public sealed class Connection {
        public Action<int> OnDisconnect { get; set; }
        public bool Authenticated { get; set; }
        public bool Connected { get; set; }
        public int ConnectedTime { get; private set; }
        public IpAddress IpAddress { get; set; }
        public int Index { get; set; }
        public ConnectionKey RSAKey { get; set; }
        public IOperationCode OperationCode { get; set; }

        private readonly ByteBuffer msg;
        private readonly TcpClient client;
        private int packetSize;
        private byte[] buffer;

        public Connection(int index, TcpClient client) {
            Index = index;
            this.client = client;
            buffer = new byte[ushort.MaxValue + 1];

            var address = client.Client.RemoteEndPoint.ToString();
            IpAddress = new IpAddress(address, 0);

            RSAKey = new ConnectionKey();
            msg = new ByteBuffer();

            Connected = client.Connected;
        }

        public void Send(ByteBuffer msg) {
            if (client.Client == null) {
                return;
            }

            var buffer = new byte[msg.Length() + 4];
            var values = BitConverter.GetBytes(msg.Length());

            Array.Copy(values, 0, buffer, 0, 4);
            Array.Copy(msg.ToArray(), 0, buffer, 4, msg.Length());

            if (client.Client.Poll(Constants.SendTimeOut, SelectMode.SelectWrite)) {
                try {
                    client.Client.Send(buffer, SocketFlags.None);
                }
                catch {
                    Disconnect();
                }
            }
            else {
                Disconnect();
            }
        }

        public void ReceiveData() {
            if (client.Client == null) {
                return;
            }

            if (client.Available > 0) {
                packetSize = client.Available;

                if (packetSize > buffer.Length) {
                    buffer = new byte[packetSize];
                }

                if (client.Client.Poll(Constants.ReceiveTimeOut, SelectMode.SelectRead)) {
                    try {
                        client.Client.Receive(buffer, packetSize, SocketFlags.None);
                    }
                    catch {
                        Disconnect();
                        return;
                    }
                }
                else {
                    Disconnect();
                    return;
                }

                msg.WriteReceivedBytes(buffer, packetSize);
                Array.Clear(buffer, 0, packetSize);

                int pLength = 0;

                if (msg.Length() >= 4) {
                    pLength = msg.ReadInt32(false);

                    if (pLength < 0) {
                        return;
                    }
                }

                while (pLength > 0 && pLength <= msg.Length() - 4) {
                    if (pLength <= msg.Length() - 4) {
                        // Remove the first packet (Size of Packet).
                        msg.ReadInt32();

                        // Remove the header.
                        var header = msg.ReadInt32();
                        // Decrease 4 bytes of header.
                        pLength -= 4;

                        if (OperationCode.HasHeader(header)) {
                            ((IRecvPacket)Activator.CreateInstance(OperationCode.GetRecvPacket(header))).Process(msg.ReadBytes(pLength), this);
                        }
                        else {
                            Disconnect();
                        }
                    }

                    pLength = 0;

                    if (msg.Length() >= 4) {
                        pLength = msg.ReadInt32(false);

                        if (pLength < 0) {
                            return;
                        }
                    }
                }

                msg.Trim();
            }
        }

        public void Disconnect() {
            Connected = false;
            client.Close();
            OnDisconnect?.Invoke(Index);
        }
    }
}