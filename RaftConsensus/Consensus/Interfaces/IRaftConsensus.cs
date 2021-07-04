using System;
using RaftConsensus.Consensus.Enums;
using RaftConsensus.MessageBroker.Interfaces;
using RaftConsensus.Settings;

namespace RaftConsensus.Consensus.Interfaces
{
    public interface IRaftConsensus : IDisposable
    {
        RaftConsensusState State { get; }
        IRaftMessageQueues MessageQueues { get; }
        RaftConsensusStateSettings Settings { get; }
        void SetState(RaftConsensusState state);
    }
}
