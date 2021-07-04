using Xunit.Sdk;

namespace RaftConsensus.Tests.Extensions
{
    public static class XUnitExtensions
    {
        public static void Fail(string message)
            => throw new XunitException(message);
    }
}
