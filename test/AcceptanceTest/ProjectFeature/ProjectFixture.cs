using System;

namespace AcceptanceTest.TaskModule
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