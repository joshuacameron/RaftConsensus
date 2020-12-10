namespace RaftConsensus.Common.Messages.Identification
{
    public interface IMessageTypeIdentification : IIdentification
    {
        bool Equals(IMessageTypeIdentification obj);
    }
}
