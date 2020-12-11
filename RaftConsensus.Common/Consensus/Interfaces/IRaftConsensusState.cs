using System;
using RaftConsensus.Common.Messages.Interfaces;

namespace RaftConsensus.Common.Consensus.Interfaces
{
    public interface IRaftConsensusState : IDisposable
    {
        void ProcessMessage(IRaftMessage raftMessage);
    }
}
