namespace RaftConsensus.Common.Settings
{
    public class RaftConsensusStateSettings
    {
        public int LeaderTimeoutSeconds { get; set; }
        public int FollowerTimeoutSeconds { get; set; }
        public int CandidateTimeoutSeconds { get; set; }
    }
}
