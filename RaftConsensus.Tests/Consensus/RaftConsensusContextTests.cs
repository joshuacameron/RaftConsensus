using Microsoft.Extensions.Logging;
using Moq;
using RaftConsensus.Consensus.Enums;
using RaftConsensus.Consensus.Interfaces;
using RaftConsensus.Consensus.States;
using RaftConsensus.Consensus.States.Interfaces;
using RaftConsensus.MessageBroker;
using RaftConsensus.Messages.Interfaces;
using RaftConsensus.Settings;
using RaftConsensus.Tests.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RaftConsensus.Tests.Consensus
{
    public class RaftConsensusContextTests : TestBase, IDisposable
    {
        private IRaftConsensus _node;

        private Mock<ILogger<RaftConsensusContext>> _mockLogger;
        private RaftMessageQueues _messageQueues;
        private WaiterManualMock _mockWaiter;
        private IRaftConsensusStateFactory _mockRaftConsensusStateFactory;
        private RaftConsensusStateSettings _settings;

        private const int TestTimeoutCheckMs = 1;
        private const int TestTimeoutFailMs = 5000;
        
        public RaftConsensusContextTests()
        {

        }

        private IRaftConsensus CreateSut()
        {
            _mockLogger = new Mock<ILogger<RaftConsensusContext>>();
            _messageQueues = new RaftMessageQueues();
            _mockWaiter = new WaiterManualMock();

            _mockRaftConsensusStateFactory = new RaftConsensusStateFactory(Container, _mockWaiter);

            _settings = new RaftConsensusStateSettings
            {
                CandidateTimeoutMilliseconds = 300,
                FollowerTimeoutMilliseconds = 300,
                LeaderTimeoutMilliseconds = 100
            };

            _node = new RaftConsensusContext(
                _mockLogger.Object,
                _messageQueues,
                _mockRaftConsensusStateFactory,
                _settings
            );

            return _node;
        }

        [Fact]
        public async Task Raft_When_FollowerTimeouts_Then_ChangesToCandidate()
        {
            var sut = CreateSut();

            _mockWaiter.TriggerTimeout();

            await WaitUntil(() => _node.State == RaftConsensusState.Candidate);
        }

        public void TestRaftConsensusInstantiates()
        {
            
            //push in two messages
                //watch the state change
            //trigger timeout
                //watch sending two messages
            //push in commit responses
                //wait for send out happy everyone
            //push in replies


            //Create Follower

            //Trigger timeout, become candidate

            Thread.Sleep(1000);

            //Send in two messages saying agree
            //Now leader
            //Timeout, send heartbeat
            //Reply to heartbeat
            //Send message out to commit
            //Get responses
            //Send out when happy everyone
            //Everyone reply
        }

        private IRaftMessage CreateAppendEntryReply()
        {
            throw new NotImplementedException();
        }

        private IRaftMessage CreateRequestVoteReply()
        {
            throw new NotImplementedException();
        }

        //TODO: Turn into an assert
        private static async Task WaitUntil(Func<bool> condition, int frequency = TestTimeoutCheckMs, int timeout = TestTimeoutFailMs)
        {
            var waitTask = Task.Run(async () =>
            {
                while (!condition())
                {
                    await Task.Delay(frequency);
                }
            });

            if (waitTask != await Task.WhenAny(waitTask, Task.Delay(timeout)))
            {
                XUnitExtensions.Fail("Timeout occurred when waiting for condition");
            }
        }

        public void Dispose()
        {
            _node.Dispose();
            ((IDisposable)_mockWaiter).Dispose();
        }
    }
}