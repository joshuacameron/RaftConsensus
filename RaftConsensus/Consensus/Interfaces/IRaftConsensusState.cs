using System;
using RaftConsensus.Consensus.Enums;
using RaftConsensus.Messages.Interfaces;

namespace RaftConsensus.Consensus.Interfaces
{
    public interface IRaftConsensusState : IDisposable
    {
        void ProcessMessage(IRaftMessage raftMessage);
    }
}
