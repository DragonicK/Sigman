namespace Sigman.Core.Common {
    public static class Constants {
        public const string ConfigurationFile = "Configuration.txt";

        /// <summary>
        /// Tempo limite em segundos de uma conexão no sistema.
        /// </summary>
        public const int ConnectionTimeOut = 3;

        /// <summary>
        /// Tempo limite de leitura em microsegundos.
        /// </summary>
        public const int ReceiveTimeOut = 15 * 1000 * 1000;

        /// <summary>
        /// Tempo limite de envio em microsegundos.
        /// </summary>
        public const int SendTimeOut = 15 * 1000 * 1000;

        /// <summary>
        /// Tempo entre cada envio do pacote de ping (milesimos de segundos).
        /// </summary>
        public const int PingTime = 10000;
    }
}