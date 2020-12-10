using RaftConsensus.Consensus.Enums;
using RaftConsensus.Consensus.Interfaces;
using RaftConsensus.Messages.Interfaces;
using System;
using RaftConsensus.Log.Interfaces;

namespace RaftConsensus.Consensus
{
    public class RaftConsensusContext : IRaftConsensus
    {
        private IRaftConsensusState _currentState;
        private readonly IRaftLog _raftLog;

        public RaftConsensusContext(IRaftLog raftLog)
        {
            _raftLog = raftLog;
            SetState(RaftConsensusState.Follower);
        }

        public void ProcessMessage(IRaftMessage raftMessage)
        {
            _currentState.ProcessMessage(raftMessage);
        }

        public void SetState(RaftConsensusState state)
        {
            _currentState?.Dispose();

            _currentState = state switch
            {
                RaftConsensusState.Candidate => new RaftConsensusStateCandidate(this),
                RaftConsensusState.Follower => new RaftConsensusStateFollower(this),
                RaftConsensusState.Leader => new RaftConsensusStateLeader(this),
                _ => throw new NotImplementedException(),
            };
        }

        public IRaftLog GetRaftLog()
        {
            return _raftLog;
        }
    }
}
