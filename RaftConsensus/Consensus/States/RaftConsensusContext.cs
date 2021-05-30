using Microsoft.Extensions.Logging;
using RaftConsensus.Consensus.Enums;
using RaftConsensus.Consensus.Interfaces;
using RaftConsensus.MessageBroker.Interfaces;
using RaftConsensus.PeerManagement.Interfaces;
using RaftConsensus.Settings;
using System;

namespace RaftConsensus.Consensus.States
{
    public class RaftConsensusContext : IRaftConsensus
    {
        private RaftConsensusStateBase _currentState;
        private RaftConsensusState _currentStateEnum;
        private readonly ILogger<RaftConsensusContext> _logger;
        public IRaftMessageBroker MessageBroker { get; }

        public RaftConsensusContext(ILogger<RaftConsensusContext> logger, IRaftPeerManagement peerManagement, IRaftMessageBroker messageBroker, RaftConsensusStateSettings settings)
        {
            _logger = logger;
            PeerManagement = peerManagement;
            MessageBroker = messageBroker;
            Settings = settings;

            SetState(RaftConsensusState.Follower);
        }

        public RaftConsensusState State
        {
            get => _currentStateEnum;
            set => SetState(value);
        }

        public IRaftPeerManagement PeerManagement { get; }
        public RaftConsensusStateSettings Settings { get; }

        private void SetState(RaftConsensusState state)
        {
            _logger.LogDebug($"Changing from {(_currentState == null ? "Not running" : _currentStateEnum)} state to {state} state");

            _logger.LogDebug("Disposing existing state if it exists");
            _currentState?.Dispose();

            _logger.LogDebug("Changing the current state enum");
            _currentStateEnum = state;

            _logger.LogDebug($"Creating the next state: {state}");

            _currentState = state switch
            {
                RaftConsensusState.Candidate => new RaftConsensusStateCandidate(this),
                RaftConsensusState.Follower => new RaftConsensusStateFollower(this),
                RaftConsensusState.Leader => new RaftConsensusStateLeader(this),
                _ => throw new NotImplementedException(),
            };

            _logger.LogDebug($"State has been changed to {state}");
        }
    }
}
