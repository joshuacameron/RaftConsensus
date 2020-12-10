using RaftConsensus.Common.Messages.Interfaces;
using System;

namespace RaftConsensus.Common.Consensus.Interfaces
{
    public interface IRaftConsensusState : IDisposable
    {
        void ProcessMessage(IRaftMessage raftMessage);
    }
}
