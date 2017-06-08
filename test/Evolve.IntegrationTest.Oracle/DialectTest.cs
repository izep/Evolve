using System;
using Xunit;

namespace Evolve.IntegrationTest.Oracle
{
    public class DialectTest : IDisposable
    {
        [Fact(DisplayName = "Run_all_Oracle_integration_tests_work")]
        public void Run_all_Oracle_integration_tests_work()
        {

        }

        /// <summary>
        ///     Start SQLServer server.
        /// </summary>
        public DialectTest()
        {
            TestUtil.RunContainer();
            TestUtil.CreateTestDatabase(TestContext.DbName);
        }

        /// <summary>
        ///     Stop SQLServer server and remove container.
        /// </summary>
        public void Dispose()
        {
            TestUtil.RemoveContainer();
        }
    }
}
