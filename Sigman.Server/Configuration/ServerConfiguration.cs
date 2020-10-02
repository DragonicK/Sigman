namespace Sigman.Server.Configuration {
    public static class ServerConfiguration {
        public static int Port { get; set; }
        public static int MaxPendingConnections { get; set; }
        public static int Sleep { get; set; }
        public static bool EnabledLogin { get; set; }     
    }
}