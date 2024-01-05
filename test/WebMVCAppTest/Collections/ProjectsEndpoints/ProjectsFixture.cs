using System;

namespace WebMVCAppTest
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