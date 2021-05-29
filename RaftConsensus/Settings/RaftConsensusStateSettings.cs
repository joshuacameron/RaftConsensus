namespace RaftConsensus.Settings
{
    public class RaftConsensusStateSettings
    {
        public int LeaderTimeoutMilliseconds { get; set; }
        public int FollowerTimeoutMilliseconds { get; set; }
        public int CandidateTimeoutMilliseconds { get; set; }
    }
}
