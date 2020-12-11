using RaftConsensus.Common.Consensus.Enums;
using RaftConsensus.Common.Consensus.Interfaces;
using RaftConsensus.Common.Log.Interfaces;
using RaftConsensus.Common.Messages.Interfaces;
using RaftConsensus.Common.PeerManagement.Interfaces;
using System;
using RaftConsensus.Common.Settings;

namespace RaftConsensus.Consensus
{
    public class RaftConsensusContext : IRaftConsensus
    {
        private IRaftConsensusState _currentState;
        private RaftConsensusState _currentStateEnum;

        public RaftConsensusContext(IRaftLog raftLog, IPeerManagement peerManagement, RaftConsensusStateSettings settings)
        {
            RaftLog = raftLog;
            PeerManagement = peerManagement;
            Settings = settings;

            SetState(RaftConsensusState.Follower);
        }

        public void ProcessMessage(IRaftMessage raftMessage)
        {
            _currentState.ProcessMessage(raftMessage);
        }

        public RaftConsensusState State
        {
            get => _currentStateEnum;
            set => SetState(value);
        }

        public IRaftLog RaftLog { get; }
        public IPeerManagement PeerManagement { get; }
        public RaftConsensusStateSettings Settings { get; }

        private void SetState(RaftConsensusState state)
        {
            _currentState?.Dispose();

            _currentStateEnum = state;

            _currentState = state switch
            {
                RaftConsensusState.Candidate => new RaftConsensusStateCandidate(this),
                RaftConsensusState.Follower => new RaftConsensusStateFollower(this),
                RaftConsensusState.Leader => new RaftConsensusStateLeader(this),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
