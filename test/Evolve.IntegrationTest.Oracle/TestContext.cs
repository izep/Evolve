using System;
using System.IO;
using System.Reflection;

namespace Evolve.IntegrationTest.Oracle
{
    public static class TestContext
    {
        public const string ImageName = "alexeiled/docker-oracle-xe-11g";
        public const string ContainerName = "oracle-xe-11g-evolve";
        public const string ContainerPort = "1521";
        public const string DbName = "my_database";
        public const string DbPwd = "oracle";
        public const string DbUser = "system";
        public const string DbSid = "xe";

        static TestContext()
        {
            ResourcesFolder = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath), "Resources");
            SqlScriptsFolder = Path.Combine(ResourcesFolder, "Sql_Scripts");
            MigrationFolder = Path.Combine(SqlScriptsFolder, "Migration");
            ChecksumMismatchFolder = Path.Combine(SqlScriptsFolder, "Checksum_mismatch");
            EmptyMigrationScriptPath = Path.Combine(ResourcesFolder, "V1_3_2__Migration_description.sql");
        }

        public static string ResourcesFolder { get; }
        public static string SqlScriptsFolder { get; }
        public static string MigrationFolder { get; }
        public static string ChecksumMismatchFolder { get; }
        public static string EmptyMigrationScriptPath { get; }
    }
}
