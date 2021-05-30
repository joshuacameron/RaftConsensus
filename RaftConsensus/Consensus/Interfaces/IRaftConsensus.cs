using RaftConsensus.Consensus.Enums;
using RaftConsensus.MessageBroker.Interfaces;
using RaftConsensus.Settings;

namespace RaftConsensus.Consensus.Interfaces
{
    public interface IRaftConsensus
    {
        RaftConsensusState State { get; set; }
        IRaftMessageQueues MessageQueues { get; }
        RaftConsensusStateSettings Settings { get; }
    }
}
