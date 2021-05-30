using RaftConsensus.Messages.Identification;
using RaftConsensus.Messages.Identification.Interfaces;

namespace RaftConsensus.MessageBroker.Interfaces
{
    public interface IRaftMessageBroker
    {
        void AddPeerQueues(IPeerIdentification peer, IRaftMessageQueues queues);
    }
}
