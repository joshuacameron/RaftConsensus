namespace RaftConsensus.Messages.Identification.Interfaces
{
    public interface IPeerIdentification : IIdentification
    {
        bool Equals(IPeerIdentification obj);
    }
}
