using AcceptanceTest;
using System;

namespace SprintFeature
{
    public class SprintFixture : ServiceContext, IDisposable
    {
        public SprintFixture()
        {
        }

        void IDisposable.Dispose()
        {
            ResetDbContext();
            Dispose();
        }
    }
}