using RaftConsensus.Consensus.Enums;
using RaftConsensus.Consensus.States.Interfaces;
using System;
using Autofac;
using Microsoft.Extensions.Logging;

namespace RaftConsensus.Consensus.States
{
    public class RaftConsensusStateFactory : IRaftConsensusStateFactory
    {
        private readonly IComponentContext _container;

        public RaftConsensusStateFactory(IComponentContext container)
        {
            _container = container;
        }

        public RaftConsensusStateBase CreateState(RaftConsensusState state, RaftConsensusContext context)
        {
            return state switch
            {
                RaftConsensusState.Follower => new RaftConsensusStateFollower(_container.Resolve<ILogger<RaftConsensusStateFollower>>(), context),
                RaftConsensusState.Candidate => new RaftConsensusStateCandidate(_container.Resolve<ILogger<RaftConsensusStateCandidate>>(), context),
                RaftConsensusState.Leader => new RaftConsensusStateLeader(_container.Resolve<ILogger<RaftConsensusStateLeader>>(), context),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }
    }
}
