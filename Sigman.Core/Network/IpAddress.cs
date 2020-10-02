namespace Sigman.Core.Network {
    public sealed class IpAddress {
        public string Ip { get; set; }
        public int AttemptCount { get; set; }
        public int AccessCount { get; set; }
        public bool BannedPermanent { get; set; }
        public int Time { get; set; }
        public int Port { get; set; }

        public IpAddress(string ipAddress, int port) {
            Ip = ipAddress;
            Port = port;
        }
    }
}