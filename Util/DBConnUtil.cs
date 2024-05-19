using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Data.SqlClient;

namespace CarConnect.Util
{
    internal static class DBConnUtil
    {
        private static IConfiguration _iconfiguration;

        static DBConnUtil()
        {
            GetAppSettingsFile();
        }

        private static void GetAppSettingsFile()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                _iconfiguration = builder.Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading appsettings.json: " + ex.Message);
                throw;
            }
        }

        public static SqlConnection GetSqlConnection()
        {
            string connectionString = _iconfiguration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is not defined.");
            }
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
