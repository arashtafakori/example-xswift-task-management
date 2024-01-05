using System;

namespace WebApiTest
{
    public class ProjectsFixture : ServiceContext, IDisposable
    {
        public ProjectsFixture()
        {
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }
    }
}