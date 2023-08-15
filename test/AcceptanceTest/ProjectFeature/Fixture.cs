using System;

namespace AcceptanceTest.TaskModule
{
    public class Fixture : ServiceContext, IDisposable
    {
        public Fixture()
        {
        }

        void IDisposable.Dispose()
        {
            ResetDbContext();
            Dispose();
        }
    }
}