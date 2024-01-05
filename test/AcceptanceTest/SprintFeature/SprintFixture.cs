using AcceptanceTest;
using System;

namespace AcceptanceTest.SprintFeature
{
    public class SprintFixture : ServiceContext, IDisposable
    {
        public SprintFixture()
        {
        }

        void IDisposable.Dispose()
        {
            EnsureRecreatedDatabase();
            Dispose();
        }
    }
}