using RaftConsensus.Messages.Interfaces;
using System;

namespace RaftConsensus.Consensus.Interfaces
{
    public interface IRaftConsensusState : IDisposable
    {
        void ProcessMessage(IRaftMessage raftMessage);
    }
}
