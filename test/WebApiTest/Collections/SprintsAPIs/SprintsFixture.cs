using System;

namespace WebApiTest
{
    public class SprintsFixture : ServiceContext, IDisposable
    {
        public SprintsFixture()
        {
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }
    }
}