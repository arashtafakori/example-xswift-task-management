using System;

namespace WebApiTest
{
    public class TasksFixture : ServiceContext, IDisposable
    {
        public TasksFixture()
        {
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }
    }
}