using System.Management.Automation;
using System.Threading;
using Oracle.ManagedDataAccess.Client;

namespace Evolve.IntegrationTest.Oracle
{
    public static class TestUtil
    {
        public static void RunContainer()
        {
#if DEBUG
            using (var ps = PowerShell.Create())
            {
                ps.Commands.AddScript($"docker run --name {TestContext.ContainerName} --publish={TestContext.ContainerPort}:{TestContext.ContainerPort} -d {TestContext.ImageName}");
                ps.Invoke();
            }

            Thread.Sleep(30000);
#endif
        }

        public static void RemoveContainer()
        {
#if DEBUG
            using (var ps = PowerShell.Create())
            {
                ps.Commands.AddScript($"docker stop '{TestContext.ContainerName}'");
                ps.Commands.AddScript($"docker rm '{TestContext.ContainerName}'");
                ps.Invoke();
            }
#endif
        }

        public static void CreateTestDatabase(string name)
        {
            string cnnString = $"Data Source=(DESCRIPTION="
                             + $"(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT={TestContext.ContainerPort})))"
                             + $"(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={TestContext.DbSid})));"
                             + $"User Id={TestContext.DbUser};Password={TestContext.DbPwd};";

            var cnn = new OracleConnection(cnnString);
            cnn.Open();

            using (var cmd = cnn.CreateCommand())
            {
                cmd.CommandText = $"CREATE USER {name} IDENTIFIED BY {name}";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"GRANT CREATE SESSION TO {name}";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"GRANT CREATE TABLE TO {name}";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"GRANT CREATE VIEW TO {name}";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"GRANT CREATE ANY TRIGGER TO {name}";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"GRANT CREATE ANY PROCEDURE TO {name}";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"GRANT CREATE SEQUENCE TO {name}";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"GRANT CREATE SYNONYM TO {name}";
                cmd.ExecuteNonQuery();
            }

            cnn.Close();
        }
    }
}
