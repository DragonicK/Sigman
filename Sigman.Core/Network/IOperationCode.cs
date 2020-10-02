using System;

namespace Sigman.Core.Network {
    public interface IOperationCode { 
        bool HasHeader(int header);
        Type GetRecvPacket(int header);
    }
}