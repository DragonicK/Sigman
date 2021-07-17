namespace Sigman.Core.Network.Packet {
    public enum ServerPacket {
        RSAKey,
        AESKey,
        Ping,
        AuthResult,
        CompilationResult,
        OutputFile
    }
}