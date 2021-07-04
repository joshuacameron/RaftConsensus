using RaftConsensus.Consensus.Enums;
using RaftConsensus.Consensus.States.Interfaces;
using System;
using Autofac;
using Microsoft.Extensions.Logging;
using RaftConsensus.Helpers.Interfaces;

namespace RaftConsensus.Consensus.States
{
    public class RaftConsensusStateFactory : IRaftConsensusStateFactory
    {
        private readonly IComponentContext _container;
        private readonly IWaiter _waiter;

        public RaftConsensusStateFactory(IComponentContext container, IWaiter waiter)
        {
            _container = container;
            _waiter = waiter;
        }

        public RaftConsensusStateBase CreateState(RaftConsensusState state, RaftConsensusContext context)
        {
            return state switch
            {
                RaftConsensusState.Follower => new RaftConsensusStateFollower(
                    _container.Resolve<ILogger<RaftConsensusStateFollower>>(), context, _waiter),
                RaftConsensusState.Candidate => new RaftConsensusStateCandidate(
                    _container.Resolve<ILogger<RaftConsensusStateCandidate>>(), context, _waiter),
                RaftConsensusState.Leader => new RaftConsensusStateLeader(
                    _container.Resolve<ILogger<RaftConsensusStateLeader>>(), context, _waiter),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }
    }
}
