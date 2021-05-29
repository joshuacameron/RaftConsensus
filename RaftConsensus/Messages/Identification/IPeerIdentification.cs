namespace RaftConsensus.Messages.Identification
{
    public interface IPeerIdentification : IIdentification
    {
        bool Equals(IPeerIdentification obj);
    }
}
