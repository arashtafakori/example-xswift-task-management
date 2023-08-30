using AcceptanceTest;
using System;

namespace ProjectFeature
{
    public class ProjectFixture : ServiceContext, IDisposable
    {
        public ProjectFixture()
        {
        }

        void IDisposable.Dispose()
        {
            ResetDbContext();
            Dispose();
        }
    }
}