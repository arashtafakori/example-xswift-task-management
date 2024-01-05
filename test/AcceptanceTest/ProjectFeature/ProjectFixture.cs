using AcceptanceTest;
using System;

namespace AcceptanceTest.ProjectFeature
{
    public class ProjectFixture : ServiceContext, IDisposable
    {
        public ProjectFixture()
        {
        }

        void IDisposable.Dispose()
        {
            EnsureRecreatedDatabase();
            Dispose();
        }
    }
}