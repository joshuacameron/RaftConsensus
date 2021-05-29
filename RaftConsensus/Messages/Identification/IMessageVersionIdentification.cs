using System;

namespace RaftConsensus.Messages.Identification
{
    public interface IMessageVersionIdentification : IIdentification, IComparable
    {
        bool Equals(IMessageVersionIdentification obj);
        int CompareTo(IMessageVersionIdentification obj);
    }
}
