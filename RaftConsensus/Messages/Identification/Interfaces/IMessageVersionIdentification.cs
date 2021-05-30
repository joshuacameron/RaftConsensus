using System;

namespace RaftConsensus.Messages.Identification.Interfaces
{
    public interface IMessageVersionIdentification : IIdentification, IComparable
    {
        bool Equals(IMessageVersionIdentification obj);
        int CompareTo(IMessageVersionIdentification obj);
    }
}
