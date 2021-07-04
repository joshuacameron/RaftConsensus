using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RaftConsensus.Consensus.Enums;
using RaftConsensus.Consensus.Interfaces;
using RaftConsensus.Consensus.States.Interfaces;
using RaftConsensus.MessageBroker.Interfaces;
using RaftConsensus.Settings;

namespace RaftConsensus.Consensus.States
{
    public class RaftConsensusContext : IRaftConsensus
    {
        private RaftConsensusStateBase _currentState;
        private readonly ILogger<RaftConsensusContext> _logger;
        private readonly IRaftConsensusStateFactory _raftConsensusStateFactory;

        public RaftConsensusContext(ILogger<RaftConsensusContext> logger, IRaftMessageQueues messageQueues, IRaftConsensusStateFactory raftConsensusStateFactory, RaftConsensusStateSettings settings)
        {
            _logger = logger;
            _raftConsensusStateFactory = raftConsensusStateFactory;
            MessageQueues = messageQueues;
            Settings = settings;

            SetState(RaftConsensusState.Follower);
        }

        public IRaftMessageQueues MessageQueues { get; }

        public RaftConsensusState State { get; private set; }

        public RaftConsensusStateSettings Settings { get; }

        public void SetState(RaftConsensusState state)
        {
            Task.Run(() =>
            {
                _logger.LogDebug(
                    $"Changing from {(_currentState == null ? "Not running" : State)} state to {state} state");

                _logger.LogDebug("Disposing existing state if it exists");
                _currentState?.Dispose();

                _logger.LogDebug($"Creating the next state: {state}");

                State = state;
                _currentState = _raftConsensusStateFactory.CreateState(state, this);

                _logger.LogDebug($"State has been changed to {state}");
            });
        }

        //TODO: Suppress finalize
        public void Dispose()
        {
            _currentState?.Dispose();
        }
    }
}
