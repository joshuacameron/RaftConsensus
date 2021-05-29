namespace RaftConsensus.Messages.Identification
{
    public interface IMessageTypeIdentification : IIdentification
    {
        bool Equals(IMessageTypeIdentification obj);
    }
}
