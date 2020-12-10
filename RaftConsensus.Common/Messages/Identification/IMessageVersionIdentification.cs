using System;

namespace RaftConsensus.Common.Messages.Identification
{
    public interface IMessageVersionIdentification : IIdentification, IComparable
    {
        bool Equals(IMessageVersionIdentification obj);
        int CompareTo(IMessageVersionIdentification obj);
    }
}
