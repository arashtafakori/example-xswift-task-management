using AcceptanceTest;
using System;

namespace TaskFeature
{
    public class TaskFixture : ServiceContext, IDisposable
    {
        public TaskFixture()
        {
        }

        void IDisposable.Dispose()
        {
            ResetDbContext();
            Dispose();
        }
    }
}